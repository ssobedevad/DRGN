using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class BlueFireMeteor : ModProjectile
    {
        public int whichPlayer;
        private Vector2 moveVel;
        private Player target;
        private float speed;
        private int homeCD;



        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            

            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.light = 1f;
            homeCD = 0;


        }
        public override void AI()
        {
            if (homeCD > 0)
            {
                homeCD -= 1;
            }
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 2f);
                Main.dust[DustID].noGravity = true;


            }
            
                Target();
                
            if (homeCD == 0)
            {
                move();
                homeCD = 600;
            }
        }
        private void move()
        {

            speed = 8f;
          
            Vector2 moveTo = target.Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;


                projectile.velocity = moveVel;
            }
            else { projectile.active = false; }

        }
        private void Target()
        {


            for (whichPlayer = 0; whichPlayer < 256; whichPlayer++)
            {
                if (Main.player[whichPlayer].active)
                {


                    target = Main.player[whichPlayer];


                }
            }


        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("BrokenWings"), 60);
           
            projectile.active = false;
        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}