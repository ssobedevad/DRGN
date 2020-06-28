using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ExplodingFrog : ModProjectile
    {


        public int whichNpc;
        private int target;
        private int targetMag;
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exploding Frog");

        }
        public override void SetDefaults()
        {
            
            projectile.height = 38;
            projectile.width = 37;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            target = -1;
            projectile.tileCollide = true;
            
            

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.Y > 0)
            {
                projectile.velocity.Y = -3;
            }
            else { projectile.velocity.X = 0; projectile.velocity.Y = 0; }
            return false;
        }
        public override void AI()
        {
            Target();
            projectile.velocity.Y += 0.3f;
            if (target != -1)
            {
                
                if (Main.npc[target].Center.X > projectile.Center.X + 30)
                {
                    projectile.tileCollide = true;
                    if (projectile.velocity.Y == 0 && projectile.velocity.X == 0)
                    {
                        projectile.velocity.X += 5;
                        projectile.velocity.Y -= 5;
                        projectile.spriteDirection = -1;
                        
                    }
                }

                else if (Main.npc[target].Center.X < projectile.Center.X - 30)
                {
                    projectile.tileCollide = true;
                    if (projectile.velocity.Y == 0 && projectile.velocity.X == 0)
                    {
                        projectile.velocity.X -= 5;
                        projectile.velocity.Y -= 5;
                        projectile.spriteDirection = 1;
                       

                    }
                   
                }
                else if (Main.npc[target].Center.Y < projectile.Center.Y - 30)
                {
                    projectile.velocity.Y = -7;
                    projectile.tileCollide = false;
                }
                else if (Main.npc[target].Center.Y > projectile.Center.Y + 30)
                {
                    projectile.velocity.Y = -7;
                    projectile.tileCollide = false;
                }
                
            }
        }

        private void Target()
        {
            targetMag = 1000; 

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - whichNpcXpos) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }



        }
    }
}