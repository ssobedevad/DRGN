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
    public class DesertSerpent : ModNPC
    {
        private Player player;
        private float speed;
        private bool falling;
        private Vector2 projVel = new Vector2(0, 0);
        private float heightInc;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Serpent");
            Main.npcFrameCount[npc.type] = 14;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1500;
            npc.damage = 20;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 22;
            npc.height =124;
            npc.value = 10000;
            
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - float to under npc left , 1 - move, 2, fly up  3 - Drop to surface, 4 - Spit at npc, 5 - Coil, 6 - Dash . repeat   
            npc.ai[1] = 0;
            npc.ai[3] = 0;

            music = MusicID.Boss1;
            bossBag = mod.ItemType("SerpentBossBag");

        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.4f);
            npc.defense = (int)(npc.defense * 1.3f);
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedSerpent = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/SnakeHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/SnakeTail"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/SnakeBody"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SnakeScale"), Main.rand.Next(15, 25));
                Item.NewItem(npc.getRect(), ItemID.Cactus, Main.rand.Next(15, 25));
                if (Main.rand.Next(5) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("ToxicFang")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("SerpentBossBag")); }
        }
        private void Target()
        {
            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {

            Target();
            Vector2 moveTo = new Vector2(0, 0);
            float moveToX = npc.ai[1];
            float moveToY = npc.ai[2];
            float moveSpeed = 5f;

            

            if (npc.ai[0] == 0)
            { // set bottom  left 
                npc.noGravity = true;
                npc.noTileCollide = true;
                //Main.npc[npc.whoAmI].modNPC.drawOffsetX = 18f;
                npc.width = 22;
                npc.height = 124;
                moveToX = player.Center.X ;
                moveToY = player.Center.Y + 400;
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);


            }
            if (npc.ai[0] == 1)
            {
                // test where reached limit 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                DespawnHandler();
                moveSpeed = 25f;
                if (TestMoveTo(moveTo, moveSpeed) || (npc.velocity.X == 0 && npc.velocity.Y == 0)) { npc.ai[0] += 1f; }
            }

            if (npc.ai[0] == 2)
            {    // set dash right - at original npc position
                moveToX = player.Center.X;
                moveToY = player.Center.Y - 200;
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
            }
             
            if (npc.ai[0] == 3)
                {
                    // test where reached limit 
                    moveTo = new Vector2(npc.ai[1], npc.ai[2]);

                moveSpeed = 15f;


                    if (TestMoveTo(moveTo, moveSpeed) || (npc.velocity.X == 0 && npc.velocity.Y == 0))
                    {
                        
                            npc.noTileCollide = false;
                            npc.noGravity = false;
                            npc.ai[0] += 1f;
                        

                    }
                }


            
            if (npc.ai[0] == 4)
            { // spit
                npc.height = 60;
                npc.width = 100;
            if (npc.velocity.Y > 0) { falling = true; }
                if (falling == true && npc.velocity.Y == 0)
                {
                    npc.velocity.X = 0;
                    if (npc.frameCounter == 38)
                    {
                        if (player.Center.X < npc.Center.X)
                        {
                            npc.spriteDirection = -1;
                            
                        }
                        else { npc.spriteDirection = 1;  }

                        for (int i = 0; i < 5; i++)
                        {
                            ProjMove();
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, projVel.X, projVel.Y + Main.rand.Next(-2,2), mod.ProjectileType("PoisonSpit"), npc.damage/2, 0);
                        }
                        npc.ai[0] += 1f;
                        npc.frameCounter = 0;

                    }
                }

            }
            if (npc.ai[0] == 5)
            { // dash forwards
                if (player.Center.X < npc.Center.X)
                {
                    npc.spriteDirection = -1;
                }
                else { npc.spriteDirection = 1; }
                if (npc.frameCounter == 88)
                {

                    
                        ProjMove();

                    npc.velocity = projVel ;
                    
                  

                    npc.noTileCollide = true;
                        npc.ai[0] += 1;



                    

                }
            }


            npc.ai[3] = npc.velocity.Y;
            if (npc.ai[0] > 5 || npc.ai[0] == 0 || npc.ai[0] ==2)
            {
                npc.velocity.Y -=  5/(npc.ai[0]);
                npc.ai[0] += 1;
            }
            if (npc.ai[0] > 200)
            {
                npc.ai[0] = 0;
            }



            DespawnHandler(); // Handles if the NPC should despawn.
            if (npc.ai[0] < 4) {
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                Move(moveTo, moveSpeed); // Calls the Move Method
            }
            // sprite animation 


        }

        private void SetDash(Vector2 moveTo)
        {
            
            npc.ai[1] = moveTo.X;
            npc.ai[2] = moveTo.Y;
            npc.velocity = new Vector2(0, 0);
            
        }

        private bool TestMoveTo(Vector2 moveTo, float speed)
        {
               
            return  ((Math.Abs(npc.Center.X - moveTo.X) < speed && (Math.Abs(npc.Center.Y - moveTo.Y) < speed ))
             || ((npc.velocity.X > 0 && npc.Center.X > moveTo.X) && (npc.velocity.Y > 0 && npc.Center.Y > moveTo.Y)) 
             || ((npc.velocity.X < 0 && npc.Center.X < moveTo.X) && (npc.velocity.Y < 0 && npc.Center.Y < moveTo.Y)));
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] < 4 || npc.ai[0] > 150)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 24;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 8.0);  // only change frame every second tick
                npc.frame.Y = frame * 125;
            }
            else if (npc.ai[0] == 4 && npc.velocity.Y == 0 )
            {

                npc.frameCounter += 1;
                npc.frameCounter %= 40;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 20) + 10;  // only change frame every second tick
                npc.frame.Y = frame * 125;
            }
            else if (npc.ai[0] >= 5) 
            {
                npc.frameCounter += 1;
                
                if (npc.frameCounter > 100){ npc.frameCounter = 100; }  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 3;  // only change frame every 10 tick
                npc.frame.Y = frame * 125;
            }

        }
        private void Move(Vector2 moveTo, float moveSpeed)
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
        private void ProjMove()
        {
            speed = 10f; // Sets the max speed of the npc.
             Vector2 move = player.Center - npc.Bottom;
            float magnitude = Magnitude(move);
            
                move *= speed / magnitude;
          

            projVel = move;
            
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead|| !Main.dayTime|| !player.ZoneDesert)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime || !player.ZoneDesert)
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