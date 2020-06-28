using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class DemonSoulProj : ModProjectile
    {
       
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.tileCollide = false;
           
            
        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            int Dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0, 0, 120, default(Color), 2f);
            Main.dust[Dustid].noGravity = true;
            
            
                move();
            
        }
        private void move()
        {
            int target = Target();
            if (target != -1)
            {
                float speed = 15f;
                Vector2 moveTo = Main.npc[target].Center;
                Vector2 moveVel = moveTo - projectile.Center;
                float magnitude = Magnitude(moveVel);
                if (magnitude > speed)
                {
                    moveVel *= speed / magnitude;
                }

                projectile.velocity = (projectile.velocity * 20f + moveVel) / 21f;
            }

        }


        private int Target()
        {
            int targetMag = 1000;
            int target = -1;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {

                    float DistanceProjtoNpc = Vector2.Distance(Main.npc[whichNpc].Center, projectile.Center);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;


                    }
                }
            }
            return target;


        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
        if (Main.rand.Next(0,3)==1)
            NPC.NewNPC((int)target.position.X, (int)target.position.Y, mod.NPCType("LifestealBolt"));
        }
    }
}