using DRGN.Items;
using DRGN.Items.Equipables;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class TheVirus : ModNPC
    {






        private const int BinaryShotDamage = 50;
       
        private Vector2 MoveTo;
        private int[,] shootAngles = new int[8, 2] { { 0, 8 }, { 4, 4 }, { 8, 0 }, { 4, -4 }, { 0, -8 }, { -4, -4 }, { -8, 0 }, { -4, 4 } };
        private Vector2[] oldPos = new Vector2[9] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, };


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Virus");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = DRGNModWorld.MentalMode ? 126332 : Main.expertMode ? 61625 : 72500;
            npc.damage = DRGNModWorld.MentalMode ? 75 : Main.expertMode ? 55 : 46;
            npc.defense = DRGNModWorld.MentalMode ? 67 : Main.expertMode ? 36 : 30;
            npc.height = 200;
            npc.width = 200;
            npc.aiStyle = -1;
            npc.value = 180000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;

            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            
            bossBag = mod.ItemType("TechnoBossBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            return;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {

            writer.Write((int)MoveTo.X);
            writer.Write((int)MoveTo.Y);


        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {



            MoveTo.X = reader.ReadInt32();
            MoveTo.Y = reader.ReadInt32();

        }

        
        
        public override void AI()
        {

            for (int i = 8; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = npc.Center; }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }



                if (oldPos[i] == Vector2.Zero) { oldPos[i] = npc.Center; }

            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 0)
            {
                npc.rotation = 0f;
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2(0, -300); npc.ai[1] = 1; }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 12f : Main.expertMode ? 10f : 8f;
                    if (Move(speed)) { npc.ai[0] = 1; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 1)
            {
                if (npc.ai[1] == 0)
                {
                    npc.ai[2] = Main.rand.NextBool() ? -1 : 1;
                    npc.netUpdate = true;
                    MoveTo = player.Center + new Vector2(500 * npc.ai[2], -150);
                    npc.ai[1] = 1;

                }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 14f : 12f;
                    if (Move(speed)) { npc.ai[0] = 2; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2(500 * -npc.ai[2], 0); npc.ai[1] = 1; }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 18f : Main.expertMode ? 16f : 14f;
                    if (Move(speed)) { npc.ai[0] = 3; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 3)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2(0, -400); npc.ai[1] = 1; }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 8f : 6f;
                    if (Move(speed)) { npc.ai[0] = 4; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 4)
            {
                npc.velocity *= 0.8f;
                float shootDelay = DRGNModWorld.MentalMode ? 15f : Main.expertMode ? 20f : 25f;
                if (npc.ai[1] % shootDelay == 0)
                {

                    if (Main.netMode != 1)
                    {
                        int shootAngle = (int)(npc.ai[1] / shootDelay);
                        if (shootAngle < 8)
                        {
                            int projid = Projectile.NewProjectile(npc.Center, new Vector2(shootAngles[shootAngle, 0], shootAngles[shootAngle, 1]), mod.ProjectileType("BinaryBlast"), BinaryShotDamage, 0f);

                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }




                    }
                }
                if (npc.ai[1] >= shootDelay * 8)
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 5; 
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }

                }
                else
                {
                    npc.ai[1] += 1;
                }

            }
            
            else if (npc.ai[0] == 5)
            {
                if (npc.ai[1] == 0)
                {
                    npc.ai[2] *= -1;
                    npc.netUpdate = true;
                    MoveTo = player.Center + new Vector2(500 * npc.ai[2], -150);
                    npc.ai[1] = 1;
                    npc.ai[2] = 0;

                }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 14f : 12f;
                    if (Move(speed)) { npc.ai[0] = 6; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 6)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2(500 * -npc.ai[2], 0); npc.ai[1] = 1; }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 18f : Main.expertMode ? 16f : 14f;
                    if (Move(speed)) { npc.ai[0] = 7; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 7)
            {
                if (npc.ai[1] == 0)
                { MoveTo = player.Center + new Vector2(0, -400); npc.ai[1] = 1; }
                if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 10f : Main.expertMode ? 8f : 6f;
                    if (Move(speed)) { npc.ai[0] = 8; npc.ai[1] = 0; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 8)
            {
                if (npc.ai[1] < 2)
                {
                    npc.velocity *= 0.7f;
                    npc.rotation += 0.2f;
                    if (npc.rotation > 6f && npc.ai[1] == 0)
                    {
                        npc.ai[1] = 1;
                        if (Main.netMode != 1 && NPC.CountNPCS(mod.NPCType("BinaryServants")) < 3)
                        {
                            int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("BinaryServants"));
                            
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
                        }
                    }
                    if (npc.rotation > 12f && npc.ai[1] == 1)
                    {
                        npc.ai[1] = 2;
                        if (Main.netMode != 1 && NPC.CountNPCS(mod.NPCType("BinaryServants")) < 3)
                        {
                            int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("BinaryServants"), 0, 1);
                            
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
                        }
                    }
                    
                }
                else
                {
                    npc.rotation = 0;
                    float Delay = DRGNModWorld.MentalMode ? 60f : Main.expertMode ? 90f : 120f;

                    npc.ai[1] += 1; 
                    if(npc.ai[1] >= Delay) { npc.ai[1] = 0; npc.ai[0] = 9; 
                        if (Main.netMode != 1)
                        {
                            npc.netUpdate = true;
                        }
                    }
                
                
                
                }
            }
            else if (npc.ai[0] == 9)
            {
                npc.velocity *= 0.8f;
                float shootDelay = DRGNModWorld.MentalMode ? 15f : Main.expertMode ? 20f : 25f;
                if (npc.ai[1] % shootDelay == 0)
                {

                    if(Main.netMode != 1)
                        {
                        int shootAngle = (int)(npc.ai[1] / shootDelay);
                        if (shootAngle < 8)
                        {
                            int projid = Projectile.NewProjectile(npc.Center, new Vector2(shootAngles[shootAngle, 0], shootAngles[shootAngle, 1]), mod.ProjectileType("BinaryBlast"), BinaryShotDamage, 0f);

                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                       



                    }
                }
                if (npc.ai[1] >= shootDelay * 8)
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    if (Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                    }

                }
                else
                {
                    npc.ai[1] += 1;
                }
                
            }

            DespawnHandler();
            


        }



        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;

            npc.frameCounter += 1;
           
                if ((npc.frameCounter %= 10) == 0) { frame = (frame == 0 ? 1 : 0); }

          
            npc.frame.Y = frame * frameHeight;
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedTheVirus = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)), mod.GetGoreSlot("Gores/VirusMouth"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)), mod.GetGoreSlot("Gores/VirusShell"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)), mod.GetGoreSlot("Gores/VirusMiddle"), 1f);
            
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("TechnoOre"), Main.rand.Next(40,60));
                
                int rand = Main.rand.Next(1, 10);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TechnoShuriken>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TechnoSlicer>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<GlitchHunter>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TechnoSpear>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TechnoWhip>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SourceCode>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SourceThrow>()); }
                else if (rand == 7)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<SecurityBreach>()); }
                else if (rand == 8)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<BinaryStaff>()); }
                else if (rand == 9)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TheBug>()); }

            }
            else { npc.DropBossBags(); }


        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }


        private bool Move(float moveSpeed)
        {

            Vector2 moveTo2 = MoveTo - npc.Center;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                moveTo2 *= moveSpeed / magnitude;
            }
            else { return true; }

            npc.velocity = (npc.velocity * 10f + moveTo2) / 11f;

            return false;
        }



        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private Vector2 ShootAtPlayer(float shootSpeed)
        {
            // Sets the max speed of the npc.
            Vector2 moveTo2 = Main.player[npc.target].Center - npc.Center;
            float magnitude = Magnitude(moveTo2);

            moveTo2 *= shootSpeed / magnitude;
            return moveTo2;



        }
        private void DespawnHandler()
        {
            Player player = Main.player[npc.target];
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int i = 8; i >= 0; i -= 2)
            {
                Vector2 oldV = oldPos[i];
                Vector2 vect = new Vector2(oldV.X - Main.screenPosition.X, oldV.Y - Main.screenPosition.Y);
                Rectangle rect = npc.frame;

                Color alpha9 = lightColor;
                alpha9.R = (byte)(alpha9.R * (30 - (3 * i)) / 30);
                alpha9.G = (byte)(alpha9.G * (30 - (3 * i)) / 30);
                alpha9.B = (byte)(alpha9.B * (30 - (3 * i)) / 30);
                alpha9.A = (byte)(alpha9.A * (30 - (3 * i)) / 30);
                spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect, rect, alpha9, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);




            }
            Vector2 vect2 = new Vector2(npc.position.X + npc.width / 2 - Main.screenPosition.X, npc.position.Y + npc.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = npc.frame;
            spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect2, rect2, lightColor, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }

    }
}