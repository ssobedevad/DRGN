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

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class ToxicFrog : ModNPC
    {
        private Player player;
        private int frame;
        private int proj1;
        private Vector2 tongueVelocity;
        private Vector2 moveTo;
        private int hopTime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Frog");
            Main.npcFrameCount[npc.type] = 10;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 3500;
            npc.damage = 15;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 192;
            npc.height = 192;
            npc.value = 50000;
            
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - float to under npc left , 1 - move, 2, fly up  3 - Drop to surface, 4 - Spit at npc, 5 - Coil, 6 - Dash . repeat   
            npc.ai[1] = 0;
            

            music = MusicID.Boss1;
            //bossBag = mod.ItemType("SerpentBossBag");

        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.4f);
            npc.defense = (int)(npc.defense * 1.3f);
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
                int i = Main.rand.Next(4);

                if (i == 0) { Item.NewItem(npc.getRect(), mod.ItemType("ThePlague")); }
                else if (i == 1) { Item.NewItem(npc.getRect(), mod.ItemType("ToxicRifle")); }
                else if (i == 2) { Item.NewItem(npc.getRect(), mod.ItemType("ThrowingTongue")); }
                else if (i == 3) { Item.NewItem(npc.getRect(), mod.ItemType("Lobber")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("FrogBossBag")); }
            
        }
        private void Target()
        {
             npc.TargetClosest(false);
            player = Main.player[npc.target];
            
        }
        public override void AI()
        {

            npc.target = 0;
            Target();
            if (!player.active) { return; }
            if (npc.velocity.Y > 0) { npc.noTileCollide = false; }
            if (npc.ai[0] ==0)
            {
                hopTime += 1;
                if (hopTime > 150) { if (player.Center.X > npc.Center.X ) { npc.Center = player.Center+ new Vector2( 300,-50); } else { npc.Center = player.Center + new Vector2(-300, -35); } Main.NewText("Stop running!", 0, 255, 100); for (int i = 0; i < 250; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width + 1, npc.height + 1, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 4f);
                        Main.dust[DustID].noGravity = true;
                    } }
                if (player.Center.X > npc.Center.X + 500)
                {
                    if (npc.collideX) { npc.velocity.Y = -10; npc.velocity.X = 5; }
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {

                        npc.velocity.X += 8;
                       
                        npc.velocity.Y -= 8;
                        npc.spriteDirection = 1;
                        frame = 0;
                        npc.frame.Y = frame * 194;
                        
                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; frame = 1; npc.frame.Y = frame * 194; }

                }
                else if (player.Center.X < npc.Center.X - 500)
                {
                    if (npc.collideX) { npc.velocity.Y = -10; npc.velocity.X = -5; }
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {

                        npc.velocity.X -= 8;
                        
                        npc.velocity.Y -= 8;
                        npc.spriteDirection = -1;
                        frame = 0;
                        npc.frame.Y = frame * 194;
                      
                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; frame = 1; npc.frame.Y = frame * 194; }

                }
              
                else { npc.ai[0] = 1; proj1 = -1; npc.frame.Y = 2 * 194; npc.ai[1] = 0; hopTime = 0; }
            }
            if (npc.ai[0] == 1)
            {
                npc.velocity.X = 0;
                if (proj1 != -1)
                {
                    Main.projectile[proj1].ai[0] = -1;
                }
                if (proj1 == -1 && npc.ai[1] > 10)
                {
                    npc.frame.Y = 3 * 194;
                    moveTo = player.Top;
                    ShootTo();
                    
                    proj1 = Projectile.NewProjectile(npc.Center, tongueVelocity, mod.ProjectileType("FrogTongueHostile"), npc.damage/3, 0f, 0, (float)npc.whoAmI);
                    if (player.Center.X > npc.Center.X ) { npc.spriteDirection = 1; }
                    else { npc.spriteDirection = -1; }
                }
                else if ( proj1 > -1 && Main.projectile[proj1].ai[0] == -1) { proj1 = -1; }
                
                npc.ai[1] += 1;
                if (npc.ai[1] >= 50) { npc.ai[0] = 2; npc.frame.Y = 2 * 194; }
            }
            if (npc.ai[0] == 2)
            {

                for (int i = 0; i < 50; i++)
                {
                    if (Main.rand.Next(0, 10) == 1)
                    {
                        if (!Main.expertMode)
                        {

                            NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-40, 40), (int)npc.Center.Y + Main.rand.Next(-40, 40), NPCID.BeeSmall);
                        }
                        else if (Main.rand.Next(0,15) == 1){ NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-40, 40), (int)npc.Center.Y + Main.rand.Next(-40, 40), NPCID.BigHornetStingy); }
                    }
                    int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width + 1, npc.height + 1, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 4f);
                    Main.dust[DustID].noGravity = true;
                }
                npc.ai[0] = 0;
            }







            DespawnHandler();
            
        }

        


        private void ShootTo()
        {
            float speed = 14f; // Sets the max speed of the npc.
            Vector2 move = moveTo - npc.Top;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            tongueVelocity = move + new Vector2 (0, Main.rand.Next (-5,5));
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
                if (!player.active || player.dead || !Main.dayTime || !player.ZoneOverworldHeight)
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