using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class QueenAnt : ModNPC
    {

        private Player player;
        
        
        
        private Vector2 moveTo;
        private const int electroBallDmage = 18;
        private const int fireBallDamage = 25;
        private const int antDamage = 30;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Ant");
            Main.npcFrameCount[npc.type] = 13;
        }
        public override void SetDefaults()
        {
            
            npc.height = 150;
            npc.width = 88;
            npc.aiStyle = -1;
            npc.lifeMax = DRGNModWorld.MentalMode ? 23616 : Main.expertMode ? 11520 : 6400;
            npc.damage = DRGNModWorld.MentalMode ? 40 : Main.expertMode ? 30 : 23;
            npc.defense = DRGNModWorld.MentalMode ? 24 : Main.expertMode ? 13 : 11;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;

            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0; // part of phase 
            npc.ai[2] = 1;
            bossBag = mod.ItemType("AntsBossBag");

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            return;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            
            
            
            writer.Write((int)moveTo.X);
            writer.Write((int)moveTo.Y);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            
            
            
            moveTo.X = reader.ReadInt32();
            moveTo.Y = reader.ReadInt32();

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
                if (npc.localAI[1] == 0)
                {
                    moveTo = player.Center + new Vector2(0, -300);
                    npc.localAI[1] = 1;
                    npc.spriteDirection = npc.direction;
                }
                if (npc.localAI[1] < 2)
                {
                    FloatTo();
                }
                else
                {
                    npc.localAI[1] += 1;

                    npc.spriteDirection = npc.direction;
                }
                if (npc.localAI[1] >= 20)
                {
                    npc.ai[0] = 1;
                    npc.localAI[1] = 0;
                }
            }
            else if (npc.ai[0] == 1)
            {
                if (npc.localAI[1] == 0)
                {
                    moveTo = player.Center + new Vector2((Main.rand.NextBool() ? -1 : 1) * 600, 0);
                    npc.localAI[1] = 1;
                    npc.spriteDirection = npc.direction;
                }
                if (npc.localAI[1] < 2)
                {
                    FloatTo();
                }
                else
                {
                    npc.localAI[1] += 1;


                    npc.spriteDirection = npc.direction;
                }
                if (npc.localAI[1] >= 40)
                {
                    npc.ai[0] = 2;
                    npc.localAI[1] = 0;
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.localAI[1] == 0)
                {
                    moveTo = player.Center + new Vector2(((player.Center.X > npc.Center.X) ? 1 : -1) * 600, 0);
                    npc.localAI[1] = 1;
                    npc.spriteDirection = npc.direction;
                    int numAnts = (DRGNModWorld.MentalMode ? 7 : Main.expertMode ? 5 : 3);
                    for (int i = 0; i < numAnts; i++)
                    { if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center + new Vector2(-npc.direction * 600, (i * (1500 / numAnts)) - 750), new Vector2(npc.direction * numAnts * 3f, 0), mod.ProjectileType("AntJaws"), antDamage, 0f);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }
                }
                if (npc.localAI[1] < 2)
                {
                    DashTo();
                }
                else
                {
                    npc.localAI[1] += 1;

                    npc.spriteDirection = npc.direction;
                }
                if (npc.localAI[1] >= 20)
                {
                    npc.ai[0] = 3;
                    npc.localAI[1] = 0;
                }

            }
            else if (npc.ai[0] == 3)
            {


                if (npc.ai[1] >= (DRGNModWorld.MentalMode ? 7 : (Main.expertMode ? 5 : 3)))
                {
                    npc.ai[0] = 4;
                    npc.ai[1] = 0;
                }
                else
                {
                    npc.ai[0] = 2;
                    npc.ai[1] += 1;
                }

            }
            else if (npc.ai[0] == 4)
            {
                TeleportNearPlayer(player);

                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projid = Projectile.NewProjectile(npc.Center, ShootAtPlayer(player), mod.ProjectileType("MegaElectroBall"),electroBallDmage, 0f);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
                npc.spriteDirection = npc.direction;
                npc.localAI[0] = (DRGNModWorld.MentalMode ? 40 : (Main.expertMode ? 70 : 90));
                npc.ai[0] = 5;
            }
            else if (npc.ai[0] == 5)
            {
                if (npc.ai[1] >= (DRGNModWorld.MentalMode ? 7 : (Main.expertMode ? 5 : 3)))
                {
                    npc.ai[0] = 6;
                    npc.ai[1] = 0;
                }
                else if (npc.localAI[0] > 0)
                {
                    npc.localAI[0] -= 1;
                }
                else
                {
                    npc.ai[0] = 4;
                    npc.ai[1] += 1;
                }
            }
            else if (npc.ai[0] == 6)
            {
                if (npc.ai[1] >= (DRGNModWorld.MentalMode ? 500 : (Main.expertMode ? 350 : 200)))
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.rotation = 0f;
                    npc.localAI[1] = 0;
                }
                else
                {
                    SpinTowardsPlayer(player);
                    if (npc.localAI[1] % 20 == 0)
                    {
                        
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projid = Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), mod.ProjectileType("FireBallBouncy"), fireBallDamage, 0f, Main.myPlayer);
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                    }
                    npc.localAI[1] += 1;
                    npc.ai[1] += 1;
                }
            }

            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
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
            npc.rotation = 0f;
            float Rotation = Main.rand.NextFloat(0f, 6.28f);
            npc.Center = new Vector2(player.Center.X + (float)(Math.Cos(Rotation) * 600f), player.Center.Y + (float)(Math.Sin(Rotation) * 600f));
            npc.ai[2] = 3;
            npc.velocity = Vector2.Zero;
            for (int i = 0; i < 25; i++)
            {

                int DustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, 226, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;

            }


        }
        private void FloatTo()
        {
            npc.rotation = 0f;
            float speed = Main.expertMode ? 15f : 10f;
            if (DRGNModWorld.MentalMode)
            { speed = 20f; }
            Vector2 moveTo2 = moveTo - npc.Center;

            float magnitude = Magnitude(moveTo2);
            npc.ai[2] = 1;
            if (magnitude > speed * 3)
            {
                moveTo2 *= speed / magnitude;

            }
            else
            {

                npc.localAI[1] = 2;
            }
            npc.velocity.X = (npc.velocity.X * 100f + moveTo2.X) / 101f;
            npc.velocity.Y = (npc.velocity.Y * 100f + moveTo2.Y) / 101f;



        }
        private void SpinTowardsPlayer(Player player)
        {
            float speed = Main.expertMode ? 4.5f : 2.5f;
            if (DRGNModWorld.MentalMode)
            { speed = 6.5f; }
            Vector2 moveTo = player.Center - npc.Center;
            float magnitude = Magnitude(moveTo);
            npc.ai[2] = 4;
            moveTo *= speed / magnitude;
            npc.rotation += 0.2f;
            npc.velocity = moveTo;


        }

        private void DashTo()
        {
            npc.rotation = 0f;
            float speed = Main.expertMode ? 16f : 14f;
            if (DRGNModWorld.MentalMode)
            { speed = 22f; }
            Vector2 moveTo2 = moveTo - npc.Center;
            float magnitude = Magnitude(moveTo2);
            npc.ai[2] = 2;
            if (magnitude > speed)
            {
                moveTo2 *= speed / magnitude;

            }
            else
            {
                npc.localAI[1] = 2;
                npc.ai[2] = 1;
            }


            npc.velocity = moveTo2;

        }



        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[2] == 1)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0) + 4;  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else if (npc.ai[2] == 2)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 150;
            }
            else if (npc.ai[2] == 3)
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
                int rand = Main.rand.Next(1, 5);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<AntBiter>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<AntJaws>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<AntStaff>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ElementalAntWhip>()); }
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