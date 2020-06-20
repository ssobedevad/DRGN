using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class QueenAnt : ModNPC
    {

        private Player player;
        private int phaseRepeats;
        private int animationPhase;
        private int teleportCD;
        private Vector2 moveTo;
        private int dashPhase;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Ant");
            Main.npcFrameCount[npc.type] = 13;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 6400;
            npc.height = 150;
            npc.width = 88;
            npc.aiStyle = -1;
            npc.damage = 21;
            npc.defense = 8;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;

            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0; // part of phase 
            animationPhase = 1;
            bossBag = mod.ItemType("AntsBossBag");

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
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
            if (npc.ai[0] == 0)
            {
                if (dashPhase == 0)
                {
                    moveTo = player.Center + new Vector2(0, -300);
                    dashPhase = 1;
                    npc.spriteDirection = npc.direction;
                }
                if (dashPhase < 2)
                {
                    FloatTo();
                }
                else
                {
                    dashPhase += 1;

                    npc.spriteDirection = npc.direction;
                }
                if (dashPhase >= 20)
                {
                    npc.ai[0] = 1;
                    dashPhase = 0;
                }
            }
            else if (npc.ai[0] == 1)
            {
                if (dashPhase == 0)
                {
                    moveTo = player.Center + new Vector2((Main.rand.NextBool() ? -1 : 1) * 600, 0);
                    dashPhase = 1;
                    npc.spriteDirection = npc.direction;
                }
                if (dashPhase < 2)
                {
                    FloatTo();
                }
                else
                {
                    dashPhase += 1;


                    npc.spriteDirection = npc.direction;
                }
                if (dashPhase >= 40)
                {
                    npc.ai[0] = 2;
                    dashPhase = 0;
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (dashPhase == 0)
                {
                    moveTo = player.Center + new Vector2(((player.Center.X > npc.Center.X) ? 1 : -1) * 600, 0);
                    dashPhase = 1;
                    npc.spriteDirection = npc.direction;
                    int numAnts = (DRGNModWorld.MentalMode ? 8 : (Main.expertMode ? 5 : 3));
                    for (int i = 0; i < numAnts; i++)
                    {int projid = Projectile.NewProjectile(npc.Center + new Vector2(-npc.direction * 600, (i * (1500 / numAnts)) - 750), new Vector2(npc.direction * numAnts * 3f, 0), mod.ProjectileType("AntJaws"), npc.damage / 5, 0f, Main.myPlayer); if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }
                }
                if (dashPhase < 2)
                {
                    DashTo();
                }
                else
                {
                    dashPhase += 1;

                    npc.spriteDirection = npc.direction;
                }
                if (dashPhase >= 20)
                {
                    npc.ai[0] = 3;
                    dashPhase = 0;
                }

            }
            else if (npc.ai[0] == 3)
            {


                if (phaseRepeats >= (DRGNModWorld.MentalMode ? 7 : (Main.expertMode ? 5 : 3)))
                {
                    npc.ai[0] = 4;
                    phaseRepeats = 0;
                }
                else
                {
                    npc.ai[0] = 2;
                    phaseRepeats += 1;
                }

            }
            else if (npc.ai[0] == 4)
            {
                TeleportNearPlayer(player);

                int projid = Projectile.NewProjectile(npc.Center, ShootAtPlayer(player), mod.ProjectileType("MegaElectroBall"), npc.damage / 5, 0f, Main.myPlayer);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
                npc.spriteDirection = npc.direction;
                teleportCD = (DRGNModWorld.MentalMode ? 40 : (Main.expertMode ? 70 : 90));
                npc.ai[0] = 5;
            }
            else if (npc.ai[0] == 5)
            {
                if (phaseRepeats >= (DRGNModWorld.MentalMode ? 7 : (Main.expertMode ? 5 : 3)))
                {
                    npc.ai[0] = 6;
                    phaseRepeats = 0;
                }
                else if (teleportCD > 0)
                {
                    teleportCD -= 1;
                }
                else
                {
                    npc.ai[0] = 4;
                    phaseRepeats += 1;
                }
            }
            else if (npc.ai[0] == 6)
            {
                if (phaseRepeats >= (DRGNModWorld.MentalMode ? 500 : (Main.expertMode ? 350 : 200)))
                {
                    npc.ai[0] = 0;
                    phaseRepeats = 0;
                    npc.rotation = 0f;
                    dashPhase = 0;
                }
                else
                {
                    SpinTowardsPlayer(player);
                    if (dashPhase % 15 == 0)
                    {
                        int projid = Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), mod.ProjectileType("FireBallBouncy"), npc.damage / 5, 0f, Main.myPlayer);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }
                    dashPhase += 1;
                    phaseRepeats += 1;
                }
            }



            DespawnHandler(); // Handles if the NPC should despawn.

        }
        private Vector2 ShootAtPlayer(Player player)
        {
            float speed = Main.expertMode ? 8f : 7f;
            if (DRGNModWorld.MentalMode)
            { speed = 9f; }
            Vector2 moveTo2 = player.Center - npc.Center;

            float magnitude = Magnitude(moveTo2);


            moveTo2 *= speed / magnitude;



            return moveTo2;
        }
        private void TeleportNearPlayer(Player player)
        {
            npc.Center = new Vector2(player.Center.X + Main.rand.Next(-400, 400), player.Center.Y + Main.rand.Next(-400, 400));
            animationPhase = 3;
            npc.velocity = Vector2.Zero;
            for (int i = 0; i < 25; i++)
            {

                int DustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, 226, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;

            }


        }
        private void FloatTo()
        {
            float speed = Main.expertMode ? 15f : 10f;
            if (DRGNModWorld.MentalMode)
            { speed = 20f; }
            Vector2 moveTo2 = moveTo - npc.Center;

            float magnitude = Magnitude(moveTo2);
            animationPhase = 1;
            if (magnitude > speed * 3)
            {
                moveTo2 *= speed / magnitude;

            }
            else
            {

                dashPhase = 2;
            }
            npc.velocity.X = (npc.velocity.X * 100f + moveTo2.X) / 101f;
            npc.velocity.Y = (npc.velocity.Y * 100f + moveTo2.Y) / 101f;



        }
        private void SpinTowardsPlayer(Player player)
        {
            float speed = Main.expertMode ? 5f : 3f;
            if (DRGNModWorld.MentalMode)
            { speed = 9f; }
            Vector2 moveTo = player.Center - npc.Center;
            float magnitude = Magnitude(moveTo);
            animationPhase = 4;
            moveTo *= speed / magnitude;
            npc.rotation += 0.2f;
            npc.velocity = moveTo;


        }

        private void DashTo()
        {
            float speed = Main.expertMode ? 16f : 14f;
            if (DRGNModWorld.MentalMode)
            { speed = 22f; }
            Vector2 moveTo2 = moveTo - npc.Center;
            float magnitude = Magnitude(moveTo2);
            animationPhase = 2;
            if (magnitude > speed)
            {
                moveTo2 *= speed / magnitude;

            }
            else
            {
                dashPhase = 2;
                animationPhase = 1;
            }


            npc.velocity = moveTo2;

        }



        public override void FindFrame(int frameHeight)
        {
            if (animationPhase == 1)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0) + 4;  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else if (animationPhase == 2)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else if (animationPhase == 3)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0) + 8;  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else { npc.frame.Y = 12 * 150; }
            npc.netUpdate = true;
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
            else { npc.DropBossBags(); }


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