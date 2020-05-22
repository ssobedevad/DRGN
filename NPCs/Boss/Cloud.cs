using System;
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
    public class Cloud : ModNPC
    {
        private Player player;
        private float speed;
        private bool needAnimate = false;
        public static bool channel = false;
        private int proj1,proj2,proj3,proj4;

        private Vector2 SpawnPos = new Vector2(0,0);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big angry nimbus");
            Main.npcFrameCount[npc.type] = 37;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 65000;
            npc.damage = 60;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.width = 126;
            npc.height = 91;
            npc.value = 100000;
            npc.npcSlots = 5f;
            npc.boss = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.scale = 3f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - asleep , 1 - happy, 2 sad ,   3 angry. repeat   1,2,3 not 0.
            npc.ai[1] = 0;
            npc.ai[3] = 0;
            speed = 0;
            music = MusicID.Boss1;
            

        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.125f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.2f);
            npc.defense = (int)(npc.defense * 1.4f);
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedSerpent = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudEyeRight"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudEyeLeft"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudMouth"), 1f);
            if (!Main.expertMode)
            { Main.NewText("Only got loot in expert mode mate", 200, 200, 200); }
            else { Item.NewItem(npc.getRect(), mod.ItemType("CloudBossBag")); }
            }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
          
            if (SpawnPos == new Vector2(0, 0)) { SpawnPos = new Vector2(npc.Center.X, npc.Center.Y); }
            
            Target();
           
            if (npc.ai[0] > 0) { SpawnPos = (Vector2)(player.Center) + new Vector2(0, -350); }
            Move();



            if (npc.ai[0] == 0)
            {
                if (npc.frameCounter == 48) { npc.ai[0] = 1; }
                


            }
            if (npc.ai[0] == 1)
            {
                if (channel == false && npc.active == true)
                {
                    channel =  true;
                    speed = 3f;
                   proj1 =  Projectile.NewProjectile(npc.Center, new Vector2( 0, 14) , mod.ProjectileType("SunRayHostile"), npc.damage/2, 0f, 0, (float) npc.whoAmI);
                 proj2 =   Projectile.NewProjectile(npc.Center, new Vector2(14, 0), mod.ProjectileType("SunRayHostile"), npc.damage/2, 0,0, (float)npc.whoAmI);
                  proj3 =  Projectile.NewProjectile(npc.Center, new Vector2(0, -14), mod.ProjectileType("SunRayHostile"), npc.damage/2, 0,0,(float) npc.whoAmI);
                   proj4 = Projectile.NewProjectile(npc.Center, new Vector2(-14,0), mod.ProjectileType("SunRayHostile"), npc.damage/2, 0,0,(float) npc.whoAmI);
                   
                }
        npc.ai[3] += 1;
                if (npc.ai[3] == 150) { npc.ai[0] = 2; npc.ai[3] = 0; needAnimate = true; channel = false;Main.projectile[proj1].ai[0] = -1; Main.projectile[proj2].ai[0] = -1; Main.projectile[proj3].ai[0] = -1; Main.projectile[proj4].ai[0] = -1; }

            }

            if (npc.ai[0] == 2 && needAnimate == false)
            {
                speed = 20f;
                if (Main.rand.Next(0, 6) == 1)
                {
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Bottom.Y, 0, 5, mod.ProjectileType("Rain"), npc.damage, 0);
                }
                npc.ai[3] += 1;
                if (npc.ai[3] == 250) { npc.ai[0] = 3; npc.ai[3] = 0;needAnimate = true; }
            }

            if (npc.ai[0] == 3 && needAnimate == false)
            {
                speed = 10f;
                npc.ai[3] += 1;
                if (npc.ai[3] % 30  == 1) {Projectile.NewProjectile(Main.player[npc.target].Center + new Vector2(0, -1000f), new Vector2((float)Main.rand.Next(-200, 200), 500f), mod.ProjectileType("Lightning"), npc.damage/4 , 1f, 0, (float)npc.whoAmI, 1); }
                if (npc.ai[3] % 10 == 1)
                {
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Bottom.Y, 0, 5, mod.ProjectileType("Rain"), npc.damage, 0);
                }

                if (npc.ai[3] == 250) { npc.ai[0] = 1; npc.ai[3] = 0; }
            }
          

            DespawnHandler(); // Handles if the NPC should despawn.
           
        }

      

        

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 180;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 30.0);  // only change frame every second tick
                npc.frame.Y = frame * 91;
            }
            else if (npc.ai[0] == 1)
            {

                npc.frameCounter += 1;
                npc.frameCounter %= 140;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 20) + 7;  // only change frame every second tick
                npc.frame.Y = frame * 91;
            }
            else if (npc.ai[0] == 2 && needAnimate == true)
            {
                npc.frameCounter += 1;
                
                if (npc.frameCounter > 100) { needAnimate = false; }  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 14;  // only change frame every 10 tick
                npc.frame.Y = frame * 91;
            }
            else if (npc.ai[0] == 2 && needAnimate == false)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;

                // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 24;  // only change frame every 10 tick
                npc.frame.Y = frame * 91;
            }
            else if (npc.ai[0] == 3&& needAnimate == true)
            {
                npc.frameCounter += 1;

                if (npc.frameCounter > 60) { npc.frameCounter = 0; needAnimate = false; }  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 27;  // only change frame every 10 tick
                npc.frame.Y = frame * 91;
            }
            else if (npc.ai[0] == 3&& needAnimate == false)
            {
                npc.frameCounter += 1;

                if (npc.frameCounter > 20) { npc.frameCounter = 0; }  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 33;  // only change frame every 10 tick
                npc.frame.Y = frame * 91;
            }

        }
        private void Move()
        {
             // Sets the max speed of the npc.
            Vector2 moveTo = SpawnPos + new Vector2((float)Math.Sin(npc.ai[1] / 2) * 70, (float)Math.Cos(npc.ai[1]) * 50);
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
            npc.ai[1] += 0.025f;
        }
        

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
           
         if (!player.active || player.dead )
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead )
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 2)
                    {
                       
                        npc.timeLeft = 2;
                    }
                    
                    
                }
            }
        }
    }
}