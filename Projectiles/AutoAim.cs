﻿using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class AutoAim : ModProjectile
    {
        private Vector2 target = new Vector2 (0,0);

        public int whichNpc;
        private Vector2 moveVel;
        private int targetMag = 10000;
        private float speed;
        private Vector2 moveTo;
        
        
        
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            
            
            

        }
        public override void AI()
        {



            
                targetMag = 10000;
                Target();
                if (Main.npc[whichNpc].active == false) { projectile.timeLeft = 1; }
                
                
            

            if (Math.Abs(target.X + target.Y)  > 0)
            { 
            move(); 
            }
            
            
            
            if (Main.rand.Next(0, 1) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 20, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
        }
        private void move()
        {

            speed = 15f;
             moveTo = target;
             moveVel = (moveTo - projectile.Center);
             float magnitude = Magnitude(moveVel);
                if (magnitude > speed)
                {
                    moveVel *= speed / magnitude;
                projectile.timeLeft = 2;
                projectile.velocity = moveVel;
            }
                
            }

                
                
                
            
        


        private void Target()
        {
           

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = Main.npc[whichNpc].Center;

                    }
                }
            }


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}