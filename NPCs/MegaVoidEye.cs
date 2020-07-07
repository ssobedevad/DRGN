using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DRGN.Projectiles;

namespace DRGN.NPCs
{
    public class MegaVoidEye : ModNPC
    {
        private Player player;
        
        private int Proj1 = -1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Void Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.netUpdate = true;
            npc.netAlways = true;
            npc.damage = 120;
            npc.height = 32;
            npc.width = 64;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 250000;
            npc.knockBackResist = 0f;
            npc.dontCountMe = true;
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
            Target();
            Vector2 moveVel = (player.Center - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude >= 1800) { player.AddBuff(mod.BuffType("Webbed"),60); }
            if (NPC.AnyNPCs(mod.NPCType("VoidSnakeHead")) == false) { npc.active = false; } else { npc.timeLeft = 1800; }
            //if (Proj1 != -1 && Main.projectile[Proj1].active == false && laserRespawnCD > 0) { laserRespawnCD -= 1; if (laserRespawnCD <= 0) { Proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -14, mod.ProjectileType("VoidBeamHostile"), npc.damage / 3, 0, 0, (float)npc.whoAmI);  } }
                //if (Main.projectile[Proj1].active == false) { laserRespawnCD = 60; }
            if (Proj1 == -1)
            {
                
                Proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0,-14, mod.ProjectileType("VoidBeamHostile"), npc.damage/3, 0, 0, (float)npc.whoAmI);
            }
            if (Main.rand.Next(0, 500) == 1)
            {
                if (Projectiles.VoidBeamHostile.leftRight == false)
                {
                    Projectiles.VoidBeamHostile.leftRight = true;
                }
                else { Projectiles.VoidBeamHostile.leftRight = false; }
                
                npc.frame.Y = 32;
            }
            if (Main.rand.Next(20) == 1) { npc.frame.Y = 0;  }

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }


    }
}