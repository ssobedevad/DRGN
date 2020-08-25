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
        private const int tornadoDamage = 50;
        private const int spitDamage = 25;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Serpent");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = DRGNModWorld.MentalMode ? 2793 : Main.expertMode ? 1763 : 2350;
            npc.damage = DRGNModWorld.MentalMode ? 38 : Main.expertMode ? 25 : 20;
            npc.defense = DRGNModWorld.MentalMode ? 13 : Main.expertMode ? 9 : 7;
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 78;
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
            npc.ai[0] = 0;
            npc.ai[1] = 0;
            npc.ai[2] = 0;
            music = MusicID.Boss1;
            bossBag = mod.ItemType("SerpentBossBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            return;      
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
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
        public void SetStats()
        {
            if (npc.ai[0] == 0)
            {
                npc.width = 32;
                npc.height = 78;
                npc.noGravity = true;
                npc.noTileCollide = true;
            }
            if (npc.ai[0] == 2)
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                npc.height = 60;
                npc.width = 60;
            }
        }
        public override void AI()
        {
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 0)
            {
                if (npc.ai[2] == 0)
                {
                    SetStats();
                    npc.ai[2] = 1; npc.localAI[0] = player.Center.X; npc.localAI[1] = player.Center.Y + 400;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;
                    }
                }
                if (Move(new Vector2(npc.localAI[0], npc.localAI[1]), DRGNModWorld.MentalMode ? 12f : Main.expertMode ? 10f : 8f)) 
                { 
                    npc.ai[0] = 1;
                    npc.localAI[0] = player.Center.X;
                    npc.localAI[1] = player.Center.Y - 200; 
                    npc.ai[2] = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;
                    }
                }
            }
            if (npc.ai[0] == 1)
            {
                float moveSpeed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 12f : 8f;
                if (npc.ai[1] <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                        int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                    }
                    npc.ai[1] = 20;
                }
                if (DRGNModWorld.MentalMode)
                {                    
                    if (npc.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                            int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                        }
                        npc.ai[1] = 20;
                    }
                }
                if (npc.ai[1] > 0) { npc.ai[1] -= 1; }
                if (Move(new Vector2(npc.localAI[0], npc.localAI[1]), moveSpeed))
                {
                    npc.ai[0] = 2; SetStats();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (DRGNModWorld.MentalMode)
                {
                    if (npc.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                            int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 5, 1, mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                        }
                        npc.ai[1] = 40;
                    }
                }
                if (npc.ai[1] > 0) { npc.ai[1] -= 1; }
                if (SolidTilesBelowNPC())
                {
                    npc.ai[0] = 3; npc.ai[1] = 0; npc.velocity.X = 0; npc.frameCounter = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[0] == 3)
            {
                if (npc.frameCounter == 38)
                {
                    int projNum = Main.expertMode ? 2 : 1;
                    if (DRGNModWorld.MentalMode) { projNum = 3; }
                    for (int i = 0; i < projNum; i++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center, ProjMove(10, player) + new Vector2(0, projNum == 1 ? 0 : ((projNum - 1 / 2) + i)), mod.ProjectileType("PoisonSpit"), spitDamage, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }
                    npc.ai[0] = 4;
                    npc.frameCounter = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.netUpdate = true;                        
                    }
                }

            }
            else if (npc.ai[0] == 4)
            {
                if (npc.frameCounter == 0)
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
                if (npc.frameCounter == 25)
                {
                    npc.velocity = JumpMove(20f, player);
                    npc.noTileCollide = true;
                    npc.ai[0] = 5;

                    if (player.Center.X < npc.Center.X)
                    {
                        npc.spriteDirection = -1;
                    }
                    else { npc.spriteDirection = 1; }
                    if (DRGNModWorld.MentalMode)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.localAI[0], npc.localAI[1], 0, 0, ProjectileID.SandnadoHostile, tornadoDamage, 0);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                            npc.netUpdate = true;
                        }
                    }

                }
            }
            else if (player.Center.Y < npc.Center.Y - (DRGNModWorld.MentalMode ? 50 : Main.expertMode ? 100 : 150))
            {
                npc.ai[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.netUpdate = true;                   
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {                
                DespawnHandler(player);
            }


        }
        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;
            Player player = Main.player[npc.target];
            if (npc.ai[0] <= 2)
            { frame = 0; }
            else if (npc.ai[0] == 3)
            {
                npc.frameCounter += 1;
                if (npc.frameCounter % 10 == 0)
                {
                    if (frame <= 2)
                    { frame += 1; }
                    else { frame = player.Center.X > npc.Center.X ? 4 : 3; }
                }
            }
            else if (npc.ai[0] == 4)
            {
                npc.frameCounter += 1;
                frame = player.Center.X > npc.Center.X ? 4 : 3;
            }
            else if (npc.ai[0] == 5) { frame = 5; }
            npc.frame.Y = frame * frameHeight;
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

            npc.velocity = (npc.velocity * 10f + move)/11f;
            return false;
        }
        private Vector2 ProjMove(float Speed, Player player)
        {
            Vector2 move = player.Top - npc.Bottom;
            float magnitude = Magnitude(move);
            move *= Speed / magnitude;
            return move;
        }
        private Vector2 JumpMove(float Speed, Player player)
        {
            Vector2 move = player.Top - npc.Bottom;
            float magnitude = Magnitude(move);
            move *= Speed / magnitude;
            move.Y -= magnitude / 220f;
            return move;
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private void DespawnHandler(Player player)
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
        public bool SolidTilesBelowNPC()
        {
            int startX = (int)(npc.BottomLeft.X / 16f) - 3;
            int endX = (int)(npc.BottomRight.X / 16f) + 3;
            int startY = (int)(npc.Bottom.Y / 16f);
            int endY = (int)(npc.Bottom.Y / 16f) + 5;
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