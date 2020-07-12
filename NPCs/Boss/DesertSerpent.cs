using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class DesertSerpent : ModNPC
    {
        private Player player;
        private float speed;


        private int shootCD;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Serpent");
            Main.npcFrameCount[npc.type] = 13;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1500;
            npc.damage = 20;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 22;
            npc.height = 124;
            npc.value = 10000;
            npc.netAlways = true;
            npc.netUpdate = true;
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.6f);
            npc.damage = (int)(npc.damage * 1.3f);
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
                int rand = Main.rand.Next(1, 6);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ToxicFang>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SnakeSlayer>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SnakeWhip>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SnakeStaff>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SnakeHeadThrown>()); }

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

            Target();

            float moveSpeed = 5f;



            if (npc.ai[0] == 0)
            { // set bottom  left 
                npc.noGravity = true;
                npc.noTileCollide = true;
                //Main.npc[npc.whoAmI].modNPC.drawOffsetX = 18f;
                npc.width = 22;
                npc.height = 124;
                moveSpeed = 25f;
                Vector2 moveTo = new Vector2(player.Center.X, player.Center.Y + 400);
                if (Move(moveTo, moveSpeed)) { npc.ai[0] = 1; npc.localAI[0] = player.Center.X; npc.localAI[1] = player.Center.Y - 200; }


            }


            if (npc.ai[0] == 1)
            {    // set dash right - at original npc position


                moveSpeed = 15f;
                if (DRGNModWorld.MentalMode) { moveSpeed = 22f; }
                if (shootCD <= 0)
                {
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                        int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                    }
                    shootCD = 20;
                }
                moveSpeed = 15f;
                if (DRGNModWorld.MentalMode)
                {
                    moveSpeed = 10f;

                    if (shootCD <= 0)
                    {
                       
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                            int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                        }
                        shootCD = 20;
                    }

                }
                if (shootCD > 0) { shootCD -= 1; }


                if (Move(new Vector2(npc.localAI[0], npc.localAI[1]), moveSpeed))
                {
                    npc.ai[0] = 2; npc.noTileCollide = false;
                    npc.noGravity = false;

                }
            }




            if (npc.ai[0] == 2)
            { // spit
                npc.height = 60;
                npc.width = 100;
                //npc.collideY || npc.collideX || (npc.velocity.Y == 0 && npc.oldVelocity.Y > 0) ||
                if (SolidTiles((int)(npc.BottomLeft.X / 16f) - 3, (int)(npc.BottomRight.X / 16f) + 3, (int)(npc.Bottom.Y / 16f), (int)(npc.Bottom.Y / 16f) + 5))
                {
                    npc.velocity.X = 0;
                    if (npc.frameCounter == 38)
                    {
                        if (player.Center.X < npc.Center.X)
                        {
                            npc.spriteDirection = -1;

                        }
                        else { npc.spriteDirection = 1; }
                        int projNum = Main.expertMode ? 2 : 1;
                        if (DRGNModWorld.MentalMode) { projNum = 4; }
                        for (int i = 0; i < projNum; i++)
                        {

                            
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int projid = Projectile.NewProjectile(npc.Center, ProjMove() + new Vector2(0, Main.rand.Next(-projNum + 1, projNum - 1)), mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                            }
                        }
                        npc.ai[0] = 3;
                        npc.frameCounter = 0;

                    }
                }
                else
                {
                    if (DRGNModWorld.MentalMode)
                    {


                        if (shootCD <= 0)
                        {
                            
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                                int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), npc.damage / 2, 0);
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                            }
                            shootCD = 40;
                        }

                    }
                    if (shootCD > 0) { shootCD -= 1; }
                }

            }
            if (npc.ai[0] == 3)
            { // dash forwards
                if (npc.frameCounter == 35)
                {
                    if (DRGNModWorld.MentalMode)
                    {
                        

                        
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid;
                            if (player.Center.X < npc.Center.X)
                            {
                                projid = Projectile.NewProjectile(player.Center.X - 150, player.Center.Y - 20, 0, 0, ProjectileID.SandnadoHostileMark, 0, 0);

                            }
                            else
                            {
                                Projectile.NewProjectile(player.Center.X + 150, player.Center.Y - 20, 0, 0, projid = ProjectileID.SandnadoHostileMark, 0, 0);
                            }
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                            npc.localAI[0] = Main.projectile[projid].Center.X;
                            npc.localAI[1] = Main.projectile[projid].Center.Y;
                        }
                        
                    }
                }
                if (player.Center.X < npc.Center.X)
                {
                    npc.spriteDirection = -1;
                }
                else { npc.spriteDirection = 1; }
                if (npc.frameCounter == 88)
                {




                    npc.velocity = JumpMove();



                    npc.noTileCollide = true;
                    npc.ai[0] = 4;


                    if (DRGNModWorld.MentalMode)
                    {
                        

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.localAI[0], npc.localAI[1], 0, 0, ProjectileID.SandnadoHostile, npc.damage / 2, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }






                }
            }

            if (npc.ai[0] >= 4)
            { npc.ai[0] += 1; }


            if (npc.ai[0] > 200)
            {
                npc.ai[0] = 0;
            }



            DespawnHandler(); // Handles if the NPC should despawn.

            // sprite animation 
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }

        }





        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 2 && (SolidTiles((int)(npc.BottomLeft.X / 16f) - 3, (int)(npc.BottomRight.X / 16f) + 3, (int)(npc.Bottom.Y / 16f), (int)(npc.Bottom.Y / 16f) + 5)))
            {

                npc.frameCounter += 1;
                npc.frameCounter %= 40;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 20) + 10;  // only change frame every second tick
                npc.frame.Y = frame * 124;
            }
            else
            if (npc.ai[0] < 3 || npc.ai[0] > 150)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 24;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 8.0);  // only change frame every second tick
                npc.frame.Y = frame * 124;
            }

            else if (npc.ai[0] >= 3)
            {
                npc.frameCounter += 1;

                if (npc.frameCounter > 90) { npc.frameCounter = 90; }  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 10) + 3;  // only change frame every 10 tick
                npc.frame.Y = frame * 124;
            }

        }
        private bool Move(Vector2 moveTo, float moveSpeed)
        {
            float speed = moveSpeed; // Sets the max speed of the npc.
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > speed * 4)
            {
                move *= speed / magnitude;
            }
            else { return true; }

            npc.velocity = move;
            return false;
        }
        private Vector2 ProjMove()
        {
            speed = 10f; // Sets the max speed of the npc.
            Vector2 move = player.Top - npc.Bottom;
            float magnitude = Magnitude(move);

            move *= speed / magnitude;


            return move;

        }
        private Vector2 JumpMove()
        {
            speed = 20f; // Sets the max speed of the npc.
            Vector2 move = player.Top - npc.Bottom;
            float magnitude = Magnitude(move);

            move *= speed / magnitude;
            move.Y -= magnitude / 220f;


            return move;

        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead || !Main.dayTime || !player.ZoneDesert)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !Main.dayTime)
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
        public static bool SolidTiles(int startX, int endX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (endX >= Main.maxTilesX)
            {
                return true;
            }
            if (startY < 0)
            {
                return true;
            }
            if (endY >= Main.maxTilesY)
            {
                return true;
            }
            for (int vector = startX; vector < endX + 1; vector++)
            {
                for (int i = startY; i < endY + 1; i++)
                {

                    if (Main.tile[vector, i].active() && !Main.tile[vector, i].inActive() && Main.tileSolid[Main.tile[vector, i].type])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}