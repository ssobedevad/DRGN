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
using DRGN.Items.Banners;

namespace DRGN.NPCs
{
    public class GlacialAnt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 500;
            npc.height = 34;
            npc.width = 66;
            npc.aiStyle = -1;
            npc.damage = 45;
            npc.defense = 12;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 5000;
            npc.knockBackResist = 0.6f;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;
            Player player = Main.player[npc.target];
            npc.ai[0] = GetAiType(player);
            float maxSpeed = 4.5f;
            float acceleration = 0.13f;
            if (npc.ai[0] < 2)//Long Range Stuff
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(1, 30))
                {
                    Vector2 direction = (player.Center - npc.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                    int projectile = Projectile.NewProjectile(npc.Center, direction * 1, mod.ProjectileType("IceShard"), npc.damage / 4, 0, Main.myPlayer);
                    Main.projectile[projectile].timeLeft = 300;
                }
                if (npc.collideX && npc.velocity.Y == 0) { npc.velocity.Y -= maxSpeed + npc.ai[1]; npc.ai[1] += 1; }
                else if (!npc.collideX) { npc.ai[1] = 0; }
                int mult = (npc.ai[0] == 0 ? 1 : -1);
                if (npc.velocity.X * mult < 0) { mult *= 2; }
                npc.velocity.X += mult * acceleration;
                if (npc.velocity.X * mult > maxSpeed) { npc.velocity.X = maxSpeed * mult; }
            }
            else 
            { 
            if (npc.ai[0] == 2 && npc.velocity.Y == 0) { npc.velocity.Y += maxSpeed; }//Above Stuff
            else if (npc.ai[0] == 3 && npc.velocity.Y == 0) { npc.velocity.Y -= maxSpeed + npc.ai[1]; npc.ai[1] += 1; } //Below Stuff
            
               
                //Close Range Stuff
            }
        }

        private int GetAiType(Player player)
        {
            if (player.Center.X > npc.Center.X + 80) { return 0; }
            else if (player.Center.X < npc.Center.X - 80) { return 1; }
            else if (player.Center.Y > npc.Center.Y - 32) { return 2; }
            else if (player.Center.Y < npc.Center.Y + 32) { return 3; }
            return 4;
        }

        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;
            npc.ai[2] += npc.velocity.X > 0 ? npc.velocity.X : npc.velocity.X * -1;
            if (npc.ai[2] > 20) { frame += 1; npc.ai[2] = 0; }
            if (frame > 3 || npc.velocity.Y < 0) { frame = 0; }

            npc.frame.Y = frame * frameHeight;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("GlacialAntJaw")); }
        }


    }
}