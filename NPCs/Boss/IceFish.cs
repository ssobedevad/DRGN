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

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class IceFish : ModNPC
    {
        
        private Player player;
        private float Rage;
        private int Max;
        private int RageCounter;
        private float phaseCounter;
        private float spinCD;
        private int phaseCounter2;
        private int shootCD;
        private Vector2 MoveTo;
        
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Fish");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 18000;
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
            Rage = 0;
            phaseCounter2 = 0;
            phaseCounter = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.125f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.3f);
            npc.defense = (int)(npc.defense * 1.4f);
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
            Max = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 75 : 100;
            if (npc.ai[0] == 0) 
            {
                if (phaseCounter == 0)
                { MoveTo = player.Center + new Vector2((Main.rand.NextBool() ? -1 : 1) * 600, 0); phaseCounter = 1; }
                else if (phaseCounter == 1)
                { Move(DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f); }
                else if (phaseCounter == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 3 : Main.expertMode ? 2: 1);
                }
                else
                { npc.ai[0] = 1; Rage += 5f; phaseCounter = 0; phaseCounter2 = 0; }
                
            }
            if (npc.ai[0] == 1)
            {
                if (phaseCounter == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? -1 : 1) * 600, 0); phaseCounter = 1; }
                else if (phaseCounter == 1)
                { Move(DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f); }
                else if (phaseCounter == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 3 : Main.expertMode ? 2 : 1);
                }
                else if (phaseCounter2 < 4)
                { phaseCounter2 += 1;phaseCounter = 0; }
                else
                { npc.ai[0] = 2; Rage += 5f; phaseCounter = 0;phaseCounter2 = 0; Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000, 0, 5, ModContent.ProjectileType<MassiveIcicle>(), npc.damage/3, 0f, Main.myPlayer); }

            }
            if (npc.ai[0] == 2)
            {
                if (phaseCounter == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? 1 : -1) * 600, -100); phaseCounter = 1; }
                else if (phaseCounter == 1)
                { MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); }
                else if (phaseCounter == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 3 : 2);
                }
                else
                { npc.ai[0] = 3; phaseCounter = 0; phaseCounter2 = 0; Rage += 5f; }

            }
            if (npc.ai[0] == 3)
            {
                if (phaseCounter == 0)
                { MoveTo = player.Center + new Vector2((npc.Center.X > player.Center.X ? -1 : 1) * 600, -100); phaseCounter = 1; }
                else if (phaseCounter == 1)
                { MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); }
                else if (phaseCounter == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 3 : 2);
                }
                else if (phaseCounter2 < 4)
                { phaseCounter2 += 1; phaseCounter = 0; }
                else
                { npc.ai[0] = 4; phaseCounter = 0; phaseCounter2 = 0; Rage += 5f; }

            }
            if(npc.ai[0] == 4)
            {
                if (phaseCounter == 0)
                { 
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 1000, 0, 5, ModContent.ProjectileType<MassiveIcicle>(), npc.damage / 3, 0f, Main.myPlayer);
                    
                    MoveTo = player.Center + new Vector2( 0, -300); 
                    phaseCounter = 1; }
                else if (phaseCounter == 1)
                { 
                    MoveandDropIcicles(DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 9f : 8f); 
                }
                
            
                else if (phaseCounter == 2)
                {
                    SpinAndShoot(DRGNModWorld.MentalMode ? 8 : Main.expertMode ? 4 : 3);
                }
                else
                { npc.ai[0] = 0; phaseCounter = 0; phaseCounter2 = 0;  Rage += 20;  }

            }
            if(RageCounter > 0) { RageCounter -= 1; }
            else { Rage -= 0.5f; }
            if(Rage < 0f) { Rage = 0f; }
            else if ( Rage > Max) { Rage = Max; }


        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if(RageCounter >= 40) { Rage += 1.2f; }
            Rage += 0.3f;
            RageCounter = 60;
        }



        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 49;
            int frame;
            if (Rage >= Max)
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
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("IceSpear")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("FishBossBag")); }
            
            
        }
        
        private void Move(float moveSpeed)
        {
             // Sets the max speed of the npc.
             if(Rage >= Max) { moveSpeed *= 2; }
            Vector2 moveTo2 = MoveTo - npc.Bottom;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                moveTo2 *= moveSpeed / magnitude;
            }
            else { phaseCounter = 2;}

            npc.velocity.X = (npc.velocity.X * 50f + moveTo2.X) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50f + moveTo2.Y) / 51f;
        }
        private void MoveandDropIcicles(float moveSpeed)
        {
            // Sets the max speed of the npc.
            if(shootCD > 0) { shootCD -= 1; if (Rage >= Max) { shootCD -= 2; } }
            Vector2 moveTo2 = MoveTo - npc.Bottom;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                if(shootCD <= 0) { Projectile.NewProjectile(npc.Center, Vector2.Zero, (DRGNModWorld.MentalMode ? ModContent.ProjectileType<IceCluster>() : ModContent.ProjectileType<IceShard>()), npc.damage / 3, 0f, Main.myPlayer);shootCD = (DRGNModWorld.MentalMode ? 20 : Main.expertMode ? 35 : 50); }
                moveTo2 *= moveSpeed / magnitude;
            }
            else { phaseCounter = 2;shootCD = 0; }

            npc.velocity.X = (npc.velocity.X * 50f + moveTo2.X) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50f + moveTo2.Y) / 51f;
        }
        private void SpinAndShoot(int numTurns)
        {
            if (Rage >= Max) { numTurns *= 4; }
            
            
             npc.rotation += 0.1f;
             npc.velocity *= 0.9f;
            npc.position.X += (float) Math.Cos(npc.rotation) * 10f * npc.direction;
            npc.position.Y += (float) Math.Sin(npc.rotation) * 10f * npc.direction;
            spinCD += 0.1f;
            if (Rage >= Max) { npc.rotation += 0.25f; spinCD += 0.25f; }
            if (npc.rotation >= numTurns * 6)
            { phaseCounter = 3;npc.rotation = 0;spinCD = 0; }
            if(spinCD >= 2.5f) { Projectile.NewProjectile(npc.Center, ShootAtPlayer(DRGNModWorld.MentalMode ? 12f : Main.expertMode ? 9f : 6f), (DRGNModWorld.MentalMode ? ModContent.ProjectileType<IceCluster>() : ModContent.ProjectileType<IceShard>()), npc.damage / 3, 0f, Main.myPlayer);spinCD = 0; }
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