﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.SummonStaves;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class ToxicFrog : ModNPC
    {
        private Player player;





        private const int tongueDamage = 20;
        private const int spikeDamage = 25;
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Frog");
            Main.npcFrameCount[npc.type] = 2;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = DRGNModWorld.MentalMode ? 5555 : Main.expertMode ? 3800 : 4800;
            npc.damage = DRGNModWorld.MentalMode ? 28 : Main.expertMode ? 21 : 15;
            npc.defense = DRGNModWorld.MentalMode ? 24 : Main.expertMode ? 13 : 10;
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 92;
            npc.value = 50000;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  
            npc.ai[1] = 0;
            bossBag = mod.ItemType("FrogBossBag");
            npc.ai[3] = -1;
            music = MusicID.Boss1;
            

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            return;
        }


        public override void NPCLoot()
        {
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/FrogHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity+new Vector2(Main.rand.Next(-1,1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/FrogLeg"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/FrogBody"), 1f);
            DRGNModWorld.downedToxicFrog = true;
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("ToxicFlesh"), Main.rand.Next(15, 20));
                Item.NewItem(npc.getRect(), mod.ItemType("EarthenOre"), Main.rand.Next(15, 20));
                int rand = Main.rand.Next(1,8);

                
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ThePlague>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ToxicRifle>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ThrowingTongue>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<Lobber>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TongueSword>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TongueWhip>()); }
                else if (rand == 7)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<FrogStaff>()); }

            }
        
            else { npc.DropBossBags(); }
            
        }
        private void Target()
        {
            npc.TargetClosest(false);
            player = Main.player[npc.target];
            
        }
        public override void AI()
        {
            float JumpPower = 8f;
            int MaxDist = 500;
            if (Main.expertMode) { MaxDist = 400; JumpPower = 9.5f; }
            if (DRGNModWorld.MentalMode) { MaxDist = 300; JumpPower = 12.5f; }
            if (npc.collideY) { npc.velocity.X *= 0.9f; }
            Target();
            npc.frame.Y = 96;
           
            if (npc.velocity.Y > 0) { npc.noTileCollide = false; }
            if (npc.ai[3] > -1 && (!Main.projectile[(int)npc.ai[3]].active || Main.projectile[(int)npc.ai[3]].type != mod.ProjectileType("FrogTongueHostile"))) { npc.ai[3] = -1;
                if (Main.netMode != 1)
                {
                    npc.netUpdate = true;
                }
            }
            if (npc.ai[0] ==0 && npc.ai[3] == -1)
            {
                npc.frame.Y = 0;
                npc.ai[2] += 1;
                if(npc.ai[2] == 100) { npc.localAI[1] = player.Center.X; npc.localAI[2] = player.Top.Y - 16; }
                if (npc.ai[2] > 150) 
                
                {
                    npc.Center = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[2] = 0;
                    for (int i = 0; i < 250; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 4f);
                        Main.dust[DustID].noGravity = true;
                    }
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }
                }
                  



                     
                
                if (player.Center.X > npc.Center.X + MaxDist)
                {
                    if (npc.collideX) { npc.velocity.Y = -10; npc.velocity.X = 5; }
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {

                        npc.velocity.X += JumpPower;

                        npc.velocity.Y -= JumpPower;
                        npc.spriteDirection = 1;



                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; }

                }
                else if (player.Center.X < npc.Center.X - MaxDist)
                {
                    if (npc.collideX) { npc.velocity.Y = -10; npc.velocity.X = -5; }
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {

                        npc.velocity.X -= JumpPower;

                        npc.velocity.Y -= JumpPower;
                        npc.spriteDirection = -1;



                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; }

                }
                else if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height)) { npc.ai[0] = 1; npc.ai[1] = 0; npc.ai[2] = 0; npc.velocity = Vector2.Zero;
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }
                }
                
            }
            if (npc.ai[0] == 1 && npc.velocity == Vector2.Zero)
            {
               
                
                
                if (npc.ai[3] == -1 && npc.ai[1] > 10)
                {
                    
                   
                    
                    
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.ai[3] = Projectile.NewProjectile(npc.Center, ShootTo(), mod.ProjectileType("FrogTongueHostile"), tongueDamage, 0f ,Main.myPlayer , npc.whoAmI);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, (int)npc.ai[3]);
                        npc.netUpdate = true;
                    }
                    if (player.Center.X > npc.Center.X ) { npc.spriteDirection = 1; }
                    else { npc.spriteDirection = -1; }
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }
                }
                
                
                npc.ai[1] += 1;
                if (npc.ai[1] >= 50) { npc.ai[0] = 2; 
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }
                }
            }
            if (npc.ai[0] == 2)
            {

                for (int i = 0; i < 50; i++)
                {
                    if (Main.rand.Next(0, 10) == 1)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (DRGNModWorld.MentalMode)
                            {
                                int projid = Projectile.NewProjectile(npc.Center, new Vector2(-10, -10 + Main.rand.Next(-5, 5)), ProjectileID.JungleSpike, spikeDamage, 0);


                                int projid2 = Projectile.NewProjectile(npc.Center, new Vector2(10, -10 + Main.rand.Next(-5, 5)), ProjectileID.JungleSpike, spikeDamage, 0);
                                
                                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                                
                            }
                            if (!Main.expertMode)
                            {
                                if (Main.rand.Next(5) == 1)
                                {
                                    int npcid = NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-40, 40), (int)npc.Center.Y + Main.rand.Next(-40, 40), NPCID.BeeSmall);

                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);

                                }
                            }
                            else if (Main.rand.Next(15) == 1)
                            {
                                int npcid = NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-40, 40), (int)npc.Center.Y + Main.rand.Next(-40, 40), NPCID.BigHornetStingy); if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
                                }
                            }
                        }
                    }
                    int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width + 1, npc.height + 1, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 4f);
                    Main.dust[DustID].noGravity = true;
                }
                npc.ai[0] = 0;
                if (Main.netMode != 1)
                {
                    npc.netUpdate = true;
                }
            }






            
            DespawnHandler();
            
        }

        


        private Vector2 ShootTo()
        {
            float speed = 14f; // Sets the max speed of the npc.
            Vector2 move = player.Center - npc.Top;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            return move;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }



        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead || !Main.dayTime || ! player.ZoneOverworldHeight)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime )
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 2)
                    {
                        npc.timeLeft = 2;
                    }
                    return;
                }
            }
        }
    }
}