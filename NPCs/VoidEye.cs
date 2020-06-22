﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Terraria.ID;

namespace DRGN.NPCs
{
    public class VoidEye : ModNPC
    {
        private Player player;
       
        private int shootCD;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.netUpdate = true;
            npc.netAlways = true;
            npc.damage = 100;
            npc.height = 20;
            npc.width = 36;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontCountMe = true;
            npc.lifeMax = 20000;
            npc.knockBackResist = 0f;
            npc.active = true;
            shootCD = 200;
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
            
            Target();
            if (shootCD > 0) { shootCD -= 1; }
            npc.ai[2] += 0.005f;
            npc.Center = new Vector2(npc.ai[0] + (float)Math.Cos(npc.ai[2]) * 600, npc.ai[1] + (float)Math.Sin(npc.ai[2]) * 600);

            if (NPC.AnyNPCs(mod.NPCType("VoidSnakeHead")) == false) { npc.active = false; }else { npc.timeLeft = 1800; }
            if (shootCD <= 0)
            {
                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projid =  Projectile.NewProjectile(npc.Center, Shoot(), mod.ProjectileType("VoidLazer"), npc.damage / 2, 0);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
                npc.frame.Y = 20;
                shootCD = 200;
            }
            if (shootCD <= 160) { npc.frame.Y = 0; }

        }
        private Vector2 Shoot()
        {
            float speed = 10f;
            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            
                move *= speed / magnitude;



            return move;

        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
       public override bool CheckActive()
       { return false; }
        public override bool CheckDead()
        { return false; }


    }
}