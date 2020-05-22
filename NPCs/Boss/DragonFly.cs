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
    public class DragonFly : ModNPC
    {
        
        private Player player;
        private float speed;
        private Vector2 target;
        private int dashCount;
        private int shootCD;
       
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fly");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 250000;
            npc.height = 80;
            npc.width = 400;
            npc.aiStyle = -1;
            npc.damage = 75;
            npc.defense = 70;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            drawOffsetY = 90f;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
            shootCD = 100;

            
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
            
        }
        public override void AI()
        {
            if (npc.ai[0] >= 4)
            {
                if (Main.rand.Next(0, 80) == 0)

                { Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.Next(-500, 500), -1000), new Vector2(Main.rand.Next(-5, 5), 5), mod.ProjectileType("BlueFireball"), npc.damage /2, 0f);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("BlueFireMeteor"), npc.damage/2, 0f);
                }
            }
            Target();
            
            if (npc.ai[0] == 0)
            { target = player.Center + new Vector2(1000, -500); move();npc.ai[0] = 1;npc.spriteDirection = npc.direction;dashCount = 0; }
            else if (npc.ai[0] == 1)
            { if (Math.Abs(target.X - npc.Center.X) < 20 ) { npc.ai[0] = 2; }
                if (shootCD > 0) { shootCD -= 1; }
                if (shootCD == 0)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 5), mod.ProjectileType("BlueFireball"), npc.damage/2, 0f);
                    shootCD = 35;


                }
            }
            else if (npc.ai[0] == 2)
            { target = player.Center + new Vector2(-1000, -500); move(); npc.ai[0] = 3; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 3)
            { 
                if (shootCD > 0) { shootCD -= 1; }
                if (shootCD == 0) 
                { 
                Projectile.NewProjectile(npc.Center , new Vector2(0, 5), mod.ProjectileType("BlueFireball"), npc.damage/2, 0f);
                    shootCD = 35;
                       
                    
                } 
                if (Math.Abs(target.X - npc.Center.X) < 20) { npc.ai[0] = 4; }
             }
            else if(npc.ai[0] == 4)
            { target = player.Center + new Vector2(-1000, Main.rand.Next(-5, 5)); move(); npc.ai[0] = 5; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 5)
            { if (Math.Abs(target.X - npc.Center.X) < 20) { npc.ai[0] = 6; } }
            else if(npc.ai[0] == 6)
            { target = player.Center + new Vector2(1000, Main.rand.Next(-5, 5)); DashTo(); npc.ai[0] = 7; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 7)
            {
                
                if (Math.Abs(target.X - npc.Center.X) < 20) { npc.ai[0] = 8; }
            }
            else if(npc.ai[0] == 8)
            { target = player.Center + new Vector2(-1000, Main.rand.Next(-5, 5)); DashTo(); npc.ai[0] = 9; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 9)
            {
                
                if (Math.Abs(target.X - npc.Center.X) < 20 ) { npc.ai[0] = 0 ; }
                
            }



            DespawnHandler(); // Handles if the NPC should despawn.

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("BrokenWings"), 60);

        }
        private void move()
        {

            speed = 8f;
           
            Vector2 moveVel = (target - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
               
              
            }
            npc.velocity = moveVel;

        }
        private void DashTo()
        {
            speed = 35f;
            Vector2 moveVel = (target - npc.Center);
            float magnitude = Magnitude(moveVel);

            moveVel *= speed / magnitude;



            npc.velocity = moveVel;

        }

        public override void FindFrame(int frameHeight)
        {
           
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 272;
            
            
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedDragonFly = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyBody"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyTail"), 1f);
            if (!Main.expertMode)
            {
                
                Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyDust"), 20);
                Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyWing"), 20);
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("GalacticEssence")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyBossBag")); }


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