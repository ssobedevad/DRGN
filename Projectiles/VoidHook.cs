using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class VoidHook : ModProjectile
    {
        private Vector2 target = new Vector2(0, 0);
        private bool targetHooked = false;
        public int whichNpc =0;
      
        private int targetMag = 100;
        private float speed;
       
       


        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.scale = 2f;
            whichNpc = -1;
            targetHooked = false;
            target = new Vector2(0, 0);
            projectile.alpha = 255;
           
        }
        public override void AI()
        {




            

            if (whichNpc == -1)
            {
                Target();
            }
            
            if (whichNpc == -1) { return; }
            
            if (targetHooked == false) { target = Main.npc[whichNpc].Center; }
            if (Main.npc[whichNpc].active == false) { projectile.active = false; }
            if (targetHooked == true && (Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - Main.player[projectile.owner].Center.X) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - Main.player[projectile.owner].Center.Y)>70)) { Main.npc[whichNpc].Center = projectile.Center; }
            if (targetHooked == true && projectile.Center  == target) { projectile.active = false; }




                move();
            



            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
        }
        private void move()
        {

            speed = 15f;
            Vector2 moveTo = target;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                
                
            }
            projectile.velocity = moveVel;

        }

        public override void OnHitNPC(NPC hit, int damage, float knockback, bool crit)
        {

            if (hit.whoAmI != (int)projectile.ai[0])
            {
                targetHooked = true;
                target = Main.player[projectile.owner].Center;
                
            }
        }






        private void Target()
        {
            whichNpc = -1;
            targetMag = 900;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[i].Center.X;
                    float whichNpcYpos = Main.npc[i].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (projectile.ai[1] == 1 && whichNpcXpos > projectile.position.X)
                    {
                        if ((DistanceProjtoNpc < targetMag) && (i != (int)projectile.ai[0]))
                        {
                            targetMag = (int)DistanceProjtoNpc;
                            target = Main.npc[i].Center;
                            whichNpc = i;

                        }
                    }
                    if (projectile.ai[1] == -1 &&  whichNpcXpos < projectile.position.X)
                    {
                        if ((DistanceProjtoNpc < targetMag) && (i != (int)projectile.ai[0]))
                        {
                            targetMag = (int)DistanceProjtoNpc;
                            target = Main.npc[i].Center;
                            whichNpc = i;

                        }
                    }
                }

            }
            
            if (whichNpc == -1)
            { projectile.active = false; } 
            }
        


        
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}