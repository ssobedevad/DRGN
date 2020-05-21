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

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class QueenAnt : ModNPC
    {
        private float speed;
        private Player player;
        private Vector2 moveTo;
        private int resetCounter;
        private int dashCD;
        private int shootCD;
        private int attackType;
        private Vector2 projVel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Ant");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 6400;
            npc.height = 150;
            npc.width = 88;
            npc.aiStyle = -1;
            npc.damage = 21;
            npc.defense = 8;
           
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
          
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
            speed = 10f;
            attackType = 1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax  * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.3f);
            npc.defense = (int)(npc.defense * 1.4f);
        }
        private void Target()
        {
            
            npc.TargetClosest(true);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
           
             Target();
             if (dashCD > 0) { dashCD -= 1; }
            if (shootCD > 0) { shootCD -= 1; }



            if (npc.ai[0] == 0)
            { moveTo = player.Center; npc.ai[0] = 1; 
                npc.height = 150;
                npc.width = 88;
            }
            else if (npc.ai[0] == 1)
            { RotateAround(); npc.spriteDirection = npc.direction; if (shootCD == 0) 
            { 
            moveTo = player.Center;
            shootCD = 150;
                    if (attackType == 1)
                    {
                        ProjMove();
                        if (Main.expertMode)
                        { for (int i = 0; i < 4; i++) { Projectile.NewProjectile(npc.Center, projVel + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), mod.ProjectileType("MegaElectroBall"), npc.damage/3, 0f); } }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, projVel, mod.ProjectileType("MegaElectroBall"), npc.damage / 2, 0f);
                        }
                        attackType = 2;
                    }
                    else if (attackType == 2)
                    {

                        for (int i = 0; i < 6; i++)
                        {
                            if (Main.expertMode)
                            {
                                Projectile.NewProjectile(player.Center + new Vector2(1300, Main.rand.Next(-1000, 1000)), new Vector2(-18, 0), mod.ProjectileType("AntJaws"), npc.damage/2, 0f);
                            }
                            else { Projectile.NewProjectile(player.Center + new Vector2(1300, Main.rand.Next(-1000, 1000)), new Vector2(-15, 0), mod.ProjectileType("AntJaws"), npc.damage/2, 0f); }
                        }
                        attackType = 3;


                    }
                    else if (attackType == 3)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (Main.expertMode)
                            {
                                Projectile.NewProjectile(player.Center + new Vector2(Main.rand.Next(-1000, 1000), -1000), Vector2.Zero, mod.ProjectileType("FireBallBouncy"), npc.damage/3, 0f);
                                Projectile.NewProjectile(player.Center + new Vector2(Main.rand.Next(-1000, 1000), -1000), Vector2.Zero, mod.ProjectileType("FireBallBouncy"), npc.damage/3, 0f);
                            }
                            else { Projectile.NewProjectile(player.Center + new Vector2(Main.rand.Next(-1000, 1000), -1000), Vector2.Zero, mod.ProjectileType("FireBallBouncy"), npc.damage/3, 0f); }
                        }
                        attackType = 1;
                    }
                
            } 
            if (Math.Abs(npc.Center.Y - player.Center.Y) <= 20 && dashCD == 0) { npc.ai[0] = 2; } }
            else if (npc.ai[0] == 2)
            { npc.velocity = Vector2.Zero; moveTo = (player.Center - npc.Center); npc.ai[0] = 3; }
            else if (npc.ai[0] == 3)
            { DashTo(); npc.ai[0] = 4; npc.height = 64;
                npc.width = 187;
            }
            else if (npc.ai[0] == 4)
            { if (Math.Abs(npc.Center.X - player.Center.X) <= 20 || Math.Abs(npc.Center.Y - player.Center.Y) >= 100 ) { npc.ai[0] = 5; resetCounter = 0; } }
            else if (npc.ai[0] == 5)
            { resetCounter += 1; if (resetCounter >= 30) { npc.ai[0] = 0; dashCD += 220; } }




                DespawnHandler(); // Handles if the NPC should despawn.
            
        }
        private void RotateAround()
        {
            speed = 10f;
            Vector2 moveTo2 = moveTo + new Vector2((float)(Math.Sin(npc.ai[1]) * 2000), (float)(Math.Cos(npc.ai[1]) * 2000));
            Vector2 move = moveTo2 - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            
            npc.velocity = move;
            npc.ai[1] += 0.025f;
        }
        private void ProjMove()
        {
            speed = 10f; // Sets the max speed of the proj.
            Vector2 move = player.Center - npc.Center;
            float magnitude = Magnitude(move);

            move *= speed / magnitude;


            projVel = move;

        }
        private void DashTo()
        {
            speed = 25f;
            float magnitude = Magnitude(moveTo);
           
                moveTo *= speed / magnitude;
                
            
           
            npc.velocity = moveTo;
          
        }


        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0 || npc.ai[0] == 1)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 40;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0) + 4;  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }

        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedQueenAnt = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/QueenAntHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/QueenAntBody"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/QueenAntTail"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("AntKey"));
                Item.NewItem(npc.getRect(), mod.ItemType("AntEssence"), Main.rand.Next(15, 30));
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("AntBiter")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("AntsBossBag")); }


        }
       
        
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
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