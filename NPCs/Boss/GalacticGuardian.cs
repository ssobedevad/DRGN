﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Projectiles;
using System.Runtime.InteropServices;
using System.IO;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.SummonStaves;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class GalacticGuardian: ModNPC
    {

       





        private Vector2 MoveTo;
        private int[,] shootAngles = new int[8, 2] { { 0, 32 }, { 16, 16 }, { 32, 0 }, { 16, -16 }, { 0, -32 }, { -16, -16 }, { -32, 0 }, { -16, 16 } };
        private Vector2[] oldPos = new Vector2[9] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, };


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Guardian");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 10000000;
            npc.damage = 300;
            npc.defense = 150;
            npc.height = 176;
            npc.width = 216;
            npc.aiStyle = -1;
            npc.value = 4000000;

            
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
            npc.localAI[2] = 0;
            npc.ai[2] = 0;
            npc.ai[1] = 0;
            bossBag = mod.ItemType("FishBossBag");
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

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 3f);
            npc.damage = (int)(npc.damage * 1.4f);
            npc.defense = (int)(npc.defense * 2f);
        }
        private void Target()
        {

            npc.TargetClosest(true);
            
            
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
            Target();
           
            
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 0 || npc.ai[0] == 2)
            {
                if (npc.ai[1] == 0)
                {
                    MoveTo = new Vector2(player.Center.X, player.Center.Y);
                    npc.ai[1] = 1;
                }
                else if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f;
                    if (Move(speed)) { npc.ai[1] = 2; }

                }
                else if (npc.ai[1] < (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 60 : 90))
                {
                    npc.ai[1] += 1;
                    npc.velocity *= 0.9f;
                }
                else { npc.ai[0] += 1; npc.ai[1] = 0; npc.velocity = Vector2.Zero; }
            }
            else if (npc.ai[0] == 1 || npc.ai[0] == 3)
            {

                npc.ai[1] += 1;
                int shootDelay = DRGNModWorld.MentalMode ? 15 : Main.expertMode ? 20 : 25;
                if ((npc.ai[1] % shootDelay) == 0)
                {
                    float shootSpeed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f;

                    if (Main.netMode != 1)
                    {
                        int projid = Projectile.NewProjectile(npc.Center + new Vector2(-80, -70), new Vector2(-4, -8), mod.ProjectileType("GalacticMissile"), npc.damage / 5, 0f);
                        int projid2 = Projectile.NewProjectile(npc.Center + new Vector2(80, -70), new Vector2(4, -8), mod.ProjectileType("GalacticMissile"), npc.damage / 5, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                    }



                }
                if (npc.ai[1] >= shootDelay * 8f)
                { npc.ai[0] += 1; npc.ai[1] = 0; npc.velocity = Vector2.Zero; }




            }
            else if (npc.ai[0] == 4)
            {

                if (npc.ai[1] == 0)
                {
                    
                    MoveTo = new Vector2(player.Center.X + (Main.rand.NextBool() ? 600 : -600), player.Center.Y);
                    npc.ai[1] = 1;

                }

                else if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 16f : Main.expertMode ? 13f : 10f;
                    if (Move(speed)) { npc.ai[1] = 2; }
                }
                else if (npc.ai[1] < (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 60 : 90))
                { npc.ai[1] += 1; }
                else { npc.ai[0] += 1; npc.ai[1] = 0;npc.velocity = Vector2.Zero; }


            }
            else if (npc.ai[0] == 5 || npc.ai[0] == 7 || npc.ai[0] == 9 || npc.ai[0] == 11)
            {

               
               float speed = DRGNModWorld.MentalMode ? 32f : Main.expertMode ? 24f : 20f;
                MoveTo = new Vector2(player.Center.X + (npc.Center.X > player.Center.X ? 600 : -600), player.Center.Y);
                Move(speed);
                
                if (npc.ai[1] < (DRGNModWorld.MentalMode ? 60 : Main.expertMode ? 90 : 120))
                { npc.ai[1] += 1; }
                else { npc.ai[0] += 1; npc.ai[1] = 0; }
            }
            else if (npc.ai[0] == 6 || npc.ai[0] == 8 || npc.ai[0] == 10 || npc.ai[0] == 12)
            {

                if (npc.ai[1] == 0)
                {
                    MoveTo = new Vector2(player.Center.X + (npc.Center.X < player.Center.X ? 600 : -600), player.Center.Y);
                    npc.ai[1] = 1;
                    npc.velocity = Vector2.Zero;
                }

                else if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 32f : Main.expertMode ? 24f : 20f;
                    if (Move(speed)) { npc.ai[1] = 2; }
                }
                else if (npc.ai[1] < (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 60 : 90))
                { npc.ai[1] += 1; }
                else { npc.ai[0] += 1; npc.ai[1] = 0;npc.velocity = Vector2.Zero; }
            }
            else if (npc.ai[0] == 13)
            {

                if (npc.ai[1] == 0)
                {
                    MoveTo = player.Center;
                    npc.ai[1] = 1;
                    npc.velocity = Vector2.Zero;
                }

                else if (npc.ai[1] == 1)
                {
                    float speed = DRGNModWorld.MentalMode ? 22f : Main.expertMode ? 16f : 13f;
                    if (Move(speed)) { npc.ai[1] = 2; }
                }
                else if (npc.ai[1] < (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 60 : 90))
                { npc.ai[1] += 1; }
                else { npc.ai[0] = 14; npc.ai[1] = 0; npc.velocity = Vector2.Zero; }

            }
            else if (npc.ai[0] == 14)
            {

                npc.ai[1] += 1;
                int shootDelay = DRGNModWorld.MentalMode ? 35 : Main.expertMode ? 40 : 45;
                
                if ((npc.ai[1] % shootDelay) == 0)
                {
                    

                    if (Main.netMode != 1)
                    {
                        int opposite = (int)npc.ai[2] + 4;
                        opposite %= 8;
                        npc.ai[2] %= 8;
                        int projid = Projectile.NewProjectile(npc.Center, new Vector2(shootAngles[(int)npc.ai[2],0], shootAngles[(int)npc.ai[2], 1]), mod.ProjectileType("GalacticMissile"), npc.damage / 5, 0f);
                        int projid2 = Projectile.NewProjectile(npc.Center, new Vector2(shootAngles[opposite, 0], shootAngles[opposite, 1]), mod.ProjectileType("GalacticMissile"), npc.damage / 5, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
                        npc.ai[2] += 1;
                    }



                }
                if (npc.ai[1] >= shootDelay * 24f)
                { npc.ai[0] = 0; npc.ai[1] = 0;npc.ai[2] = 0; }
            }


            DespawnHandler();
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }

        }



        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;
            
            npc.frameCounter += 1;
            if (npc.ai[0] == 1 || npc.ai[0] == 3 || npc.ai[0] == 14)
            {frame = 2; }
            else if(npc.ai[0] >= 5 && npc.ai[0] < 13)
            { if ((npc.frameCounter %= 5) == 0) { frame = (frame == 0 ? 1 : 0); }
                
            }
            npc.frame.Y = frame * frameHeight;
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedIceFish = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishTail"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishBody"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialShard"), 10);
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialOre"), 20);
                int rand = Main.rand.Next(1, 8);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IceSpear>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ArcticHuntingRifle>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IcicleBlaster>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IcicleSlicer>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<IceChainWhip>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<FishStaff>()); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.IceChains>()); }
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

            npc.velocity = (npc.velocity * 10f  + moveTo2) / 11f;
           
            return false;
        }


        private void RotateAround()
        {

            Vector2 moveTo2 = MoveTo;
            moveTo2 += new Vector2((float)Math.Cos(npc.ai[1]) , (float)Math.Sin(npc.ai[1])) * 450f;
            


            npc.Center = moveTo2;
            
            
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
            for (int i = 8; i >= 0; i-= 2)
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