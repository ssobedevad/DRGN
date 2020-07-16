using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Projectiles;
using System.Runtime.InteropServices;
using System.IO;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.SummonStaves;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class IceFish : ModNPC
    {
        
        private Player player;

        private const int bigIcicle = 80;
        private const int smallIcicle = 30;
        private const int iceBall = 45;





        private Vector2 MoveTo;
        
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Fish");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 28000;
            npc.height = 100;
            npc.width = 202;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 20;
            
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
            npc.localAI[2] = 0;
            npc.ai[2] = 0;
            npc.ai[1] = 0;
            bossBag = mod.ItemType("FishBossBag");
        }
        public override void SendExtraAI(BinaryWriter writer)
        {



            writer.Write((int)MoveTo.X);
            writer.Write((int)MoveTo.Y);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {



            MoveTo.X = reader.ReadInt32();
            MoveTo.Y = reader.ReadInt32();

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 2f * numPlayers);
            npc.damage = (int)(npc.damage * 1.2f);
            npc.defense = (int)(npc.defense * 1.2f);
        }
        private void Target()
        {

            npc.TargetClosest(true);
            player = Main.player[npc.target];
            if (player.dead) { npc.target = -1; }
        }
        public override void AI()
        {
            
            Target();

            if (npc.target == -1) { if (npc.timeLeft > 10) { npc.timeLeft = 10; } }
            npc.spriteDirection = npc.direction;
            int Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            if (npc.ai[0] == 0) 
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2((Main.rand.NextBool() ? -1 : 1) * 600, 0); npc.ai[1] = 1; }
                else if (npc.ai[1] == 1)
                { Move(DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f); }
                else if (npc.ai[1] == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 3 : Main.expertMode ? 2: 1);
                }
                else
                { npc.ai[0] = 1; npc.localAI[2] += 5f; npc.ai[1] = 0; npc.ai[2] = 0; }
                
            }
            if (npc.ai[0] == 1)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? -1 : 1) * 600, 0); npc.ai[1] = 1; }
                else if (npc.ai[1] == 1)
                { Move(DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f); }
                else if (npc.ai[1] == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 3 : Main.expertMode ? 2 : 1);
                }
                else if (npc.ai[2] < 4)
                { npc.ai[2] += 1;npc.ai[1] = 0; }
                else
                { npc.ai[0] = 2; npc.localAI[2] += 5f; npc.ai[1] = 0;npc.ai[2] = 0; if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000, 0, 5, ModContent.ProjectileType<MassiveIcicle>(), bigIcicle, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                }

            }
            if (npc.ai[0] == 2)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? 1 : -1) * 600, -100); npc.ai[1] = 1; }
                else if (npc.ai[1] == 1)
                { MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); }
                else if (npc.ai[1] == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 3 : 2);
                }
                else
                { npc.ai[0] = 3; npc.ai[1] = 0; npc.ai[2] = 0; npc.localAI[2] += 5f; }

            }
            if (npc.ai[0] == 3)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? -1 : 1) * 600, -100); npc.ai[1] = 1;npc.localAI[0] = 0; }
                else if (npc.ai[1] == 1)
                { MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); }
                else if (npc.ai[1] == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 3 : 2);
                }
                else if (npc.ai[2] < 4)
                { npc.ai[2] += 1; npc.ai[1] = 0; }
                else
                { npc.ai[0] = 4; npc.ai[1] = 0; npc.ai[2] = 0; npc.localAI[2] += 5f; }

            }
            if(npc.ai[0] == 4)
            {
                if (npc.ai[1] == 0)
                {
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000, 0, 5, ModContent.ProjectileType<MassiveIcicle>(), bigIcicle, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }

                    MoveTo = player.Center + new Vector2( 0, -300); 
                    npc.ai[1] = 1; }
                else if (npc.ai[1] == 1)
                { 
                    MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); 
                }
                
            
                else if (npc.ai[1] == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 8 : Main.expertMode ? 4 : 3);
                }
                else
                { npc.ai[0] = 0; npc.ai[1] = 0; npc.ai[2] = 0;  npc.localAI[2] += 20;  }

            }
            if(npc.localAI[1] > 0) { npc.localAI[1] -= 1; }
            else { npc.localAI[2] -= 0.5f; }
            if(npc.localAI[2] < 0f) { npc.localAI[2] = 0f; }
            else if ( npc.localAI[2] > Max) { npc.localAI[2] = Max; }
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }

        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if(npc.localAI[1] >= 40) { npc.localAI[2] += 1.2f; }
            npc.localAI[2] += 0.3f;
            npc.localAI[1] = 60;
        }



        public override void FindFrame(int frameHeight)
        {
            int Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            npc.frameCounter += 1;
            npc.frameCounter %= 49;
            int frame;
            if (npc.localAI[2] >= Max)
            {
                // number of frames * tick count
                frame = (int)(npc.frameCounter / 7.0) + 7;  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                
            }
            else
            {
                // number of frames * tick count
                frame = (int)(npc.frameCounter / 7.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]/2) frame = 0;  // check for final frame
                
            }
            npc.frame.Y = frame * 142;

        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedIceFish = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishTail"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishBody"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialShard"), 10);
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialOre"), 20);
                int rand = Main.rand.Next(1, 8);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IceSpear>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ArcticHuntingRifle>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IcicleBlaster>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IcicleSlicer>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IceChainWhip>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<FishStaff>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.IceChains>()); }
            }
            else { npc.DropBossBags(); }
            
            
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }


        private void Move(float moveSpeed)
        {
            int Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            // Sets the max speed of the npc.
            if (npc.localAI[2] >= Max) { moveSpeed *= 2; }
            Vector2 moveTo2 = MoveTo - npc.Bottom;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                moveTo2 *= moveSpeed / magnitude;
            }
            else { npc.ai[1] = 2; npc.localAI[0] = 0; }

            npc.velocity.X = (npc.velocity.X * 50f + moveTo2.X) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50f + moveTo2.Y) / 51f;
        }
        private void MoveandDropIcicles(float moveSpeed)
        {
            int Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            // Sets the max speed of the npc.
            if (npc.localAI[0] > 0) { npc.localAI[0] -= 1; if (npc.localAI[2] >= Max) { npc.localAI[0] -= 2; } }
            Vector2 moveTo2 = MoveTo - npc.Bottom;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                if(npc.localAI[0] <= 0) {  npc.localAI[0] = (DRGNModWorld.MentalMode ? 20 : Main.expertMode ? 35 : 50); if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center, Vector2.Zero, (DRGNModWorld.MentalMode ? ModContent.ProjectileType<IceCluster>() : ModContent.ProjectileType<IceShard>()), (DRGNModWorld.MentalMode ? iceBall : smallIcicle), 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                }
                moveTo2 *= moveSpeed / magnitude;
            }
            else { npc.ai[1] = 2; npc.localAI[0] = 0; }

            npc.velocity.X = (npc.velocity.X * 50f + moveTo2.X) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50f + moveTo2.Y) / 51f;
        }
        private void SpinAndShoot(int numTurns)
        {
            int Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            if (npc.localAI[2] >= Max) { numTurns *= 4; }
            
            
             npc.rotation += 0.1f;
             npc.velocity *= 0.9f;
            npc.position.X += (float) Math.Cos(npc.rotation) * 10f * npc.direction;
            npc.position.Y += (float) Math.Sin(npc.rotation) * 10f * npc.direction;
            npc.localAI[0] += 0.1f;
            if (npc.localAI[2] >= Max) { npc.rotation += 0.25f; npc.localAI[0] += 0.25f; }
            if (npc.rotation >= numTurns * 6)
            { npc.ai[1] = 3;npc.rotation = 0;npc.localAI[0] = 0; }
            if(npc.localAI[0] >= 2.5f) { if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projid = Projectile.NewProjectile(npc.Center, ShootAtPlayer(DRGNModWorld.MentalMode ? 12f : Main.expertMode ? 9f : 6f), (DRGNModWorld.MentalMode ? ModContent.ProjectileType<IceCluster>() : ModContent.ProjectileType<IceShard>()), (DRGNModWorld.MentalMode ? iceBall : smallIcicle), 0f);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
                npc.localAI[0] = 0;
            }
        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private Vector2 ShootAtPlayer(float moveSpeed)
        {
             // Sets the max speed of the npc.
            Vector2 moveTo2 = player.Top - npc.Bottom;
            float magnitude = Magnitude(moveTo2);
            
            moveTo2 *= moveSpeed / magnitude;
            return moveTo2;
            

            
        }
        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
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