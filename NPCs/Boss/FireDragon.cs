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
    public class FireDragon : ModNPC
    {
        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Dragon");
            Main.npcFrameCount[npc.type] = 21;
            
        }
        
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 250000;
            npc.damage = 120;
            npc.defense = 110;
            npc.knockBackResist = 0f;
            npc.width = 254;
            npc.height = 34;
            npc.value = 1000000;
            
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.drippingSlime = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - set charge R to L, 1 - move, 2 - set charge L to R, 3 - move loop, 4 - set move center above player, 5 - move , 6 - drop  fireballs . repeat to half health  
            npc.ai[1] = 0;
            
            music = MusicID.Boss1;

        }
        

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.125f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.3f);
            npc.defense = (int)(npc.defense * 1.8f) + 25;
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedDragon = true;
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DragonScale"), Main.rand.Next(15, 30));
                Item.NewItem(npc.getRect(), mod.ItemType("SolariumOre"), Main.rand.Next(15, 30));
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("SolariumBar"), Main.rand.Next(10, 20)); }
                if (Main.rand.Next(5) == 0) { Item.NewItem(npc.getRect(), mod.ItemType("SunBook")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("DragonBossBag")); }
        }
        private void Target()
        {
            if (Main.player[npc.target].active)
            {
                player = Main.player[npc.target];
            }
            else { npc.target += 1; }
            if (npc.target == 255) { npc.target = 0; }
        }
        public override void AI()
        {

            npc.target = 0;
            Target();
            if (!player.active) { return; }

            Vector2 moveTo = new Vector2(0, 0);
            float moveToX = npc.ai[1];
            float moveToY = npc.ai[2];
            float  moveSpeed = 5f;
          
            if (npc.ai[0] == 0 )
            { // set dash left 
                Main.npc[npc.whoAmI].modNPC.drawOffsetY = 110f;
                npc.width = 254;
                npc.height = 34;
                moveToX = player.Center.X - 1200;
                moveToY = player.Top.Y - 200 ; 
                moveTo = new Vector2(moveToX , moveToY);
                SetDash(moveTo);
                npc.spriteDirection = -1;
            }
            if (npc.ai[0] == 1 || npc.ai[0] == 3) 
            {
               // test where reached limit 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                DespawnHandler();
                moveSpeed = 5;
                if (TestMoveTo(moveTo, moveSpeed)) { npc.ai[0] += 1f; }
            }

            if (npc.ai[0] == 2)
            {    // set dash right - at original player position
                moveToX = player.Center.X + 1200;
                moveToY = npc.ai[2]-200  ;
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = 1;
            }
            if (npc.ai[0] == 4)
            { // slow to middle
                moveToX = player.Center.X ;
                moveToY = player.Top.Y;
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = -1;
            }
            if (npc.ai[0] == 5)
            {
                Main.npc[npc.whoAmI].modNPC.drawOffsetY = 0f;
                npc.width = 35;
                npc.height = 185;
                // test where reached limit 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                moveSpeed = 3;
                DespawnHandler();
                if (TestMoveTo(moveTo, moveSpeed)) { npc.ai[0] += 1f; }
            }

            if (npc.ai[0] == 6)
            {
                // in middle , drop fireballs and exit randomly 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                moveSpeed = 0;
                if (Main.rand.Next((int) (npc.life * 50 / npc.lifeMax )) == 1)
                {
                    DespawnHandler();
                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000, 0, Main.rand.Next(0,20), mod.ProjectileType("DragonFireballProjHostile"), npc.damage /2 , 0);
                }
                if (Main.rand.Next(800) == 1) { npc.ai[0] += 1;
                    ;
                }
                DespawnHandler();
            }

            if (npc.ai[0] > 6)
            {
                npc.ai[0] += 1;
            }
            if (npc.ai[0] > 200)
            {
                npc.ai[0] = 0;
            }

            if (npc.life < npc.lifeMax / 2)
            {  // when dragon gets to half health increase speed 
                moveSpeed *= 2;
                if (Main.expertMode == true ) {
                    npc.damage = 400;
                    npc.defense = 300;
                } else {
                    npc.damage = 200;
                    npc.defense = 200;
                }
            }

            DespawnHandler(); // Handles if the NPC should despawn.
            moveTo = new Vector2(npc.ai[1], npc.ai[2]);
            Move(moveTo, moveSpeed); // Calls the Move Method

            // sprite animation 
            if (npc.frameCounter  == 31 && npc.ai[0] < 5 ) {
                if (npc.spriteDirection == 1)
                {
                    Projectile.NewProjectile(npc.Right.X+12, npc.Bottom.Y + 25, npc.velocity.X*2, 5, mod.ProjectileType("DragonFireballProjHostile"), npc.damage, 0);
                }
                else
                {
                    Projectile.NewProjectile(npc.Left.X-12, npc.Bottom.Y + 25, npc.velocity.X*2, 5, mod.ProjectileType("DragonFireballProjHostile"), npc.damage, 0);
                }

            }
      
        }

        private void SetDash(Vector2 moveTo) {
            npc.ai[0] += 1f;
            npc.ai[1] = moveTo.X;
            npc.ai[2] = moveTo.Y;
            npc.velocity = new Vector2(0, 0);
        }

        private bool TestMoveTo(Vector2 moveTo, float speed)
        {

            return (Math.Abs(npc.Center.X - moveTo.X) < speed  ||  ( npc.velocity.X > 0  && npc.Center.X > moveTo.X) || (npc.velocity.X < 0 && npc.Center.X < moveTo.X));
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] < 5)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 64;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 8.0);  // only change frame every second tick
                npc.frame.Y = frame * 256;
            }
            else {
                npc.frameCounter += 1;
                npc.frameCounter %= 48;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 4.0) + 9 ;  // only change frame every second tick
                npc.frame.Y = frame * 256;
            }

    }
        private void Move(Vector2 moveTo , float moveSpeed)
        {
            speed = moveSpeed; // Sets the max speed of the npc.
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead || !DRGNPlayer.DragonBiome)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !DRGNPlayer.DragonBiome)
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