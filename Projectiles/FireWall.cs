using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class FireWall : ModProjectile
    {

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 13;
            projectile.height = 96;
            projectile.width = 96;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 1f;

        }

        public override void AI()
        {
            projectile.Center = Main.player[projectile.owner].Center;
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];


            }
            for (int i = 0; i < Main.projectile.Length;i++)
            { if (Main.projectile[i].hostile && Main.projectile[i].active) 
            { 
            if (Magnitude(Main.projectile[i].Center - projectile.Center) < 80)
            { Main.projectile[i].active = false; if (projectile.timeLeft > 60) { projectile.timeLeft = 60; }
                        for (int j = 0; j < 10; j++)
                        {
                            int DustID = Dust.NewDust(Main.projectile[i].position, Main.projectile[i].width, Main.projectile[i].height, 174, Main.projectile[i].velocity.X * 0.2f, Main.projectile[i].velocity.Y * 0.2f, 120, default(Color), 2f);
                            Main.dust[DustID].noGravity = true;
                        }
                        
                        return;

                    }
            
            
            }
            
            }
            Main.player[projectile.owner].AddBuff(mod.BuffType("FireWall"), 2);


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }

}