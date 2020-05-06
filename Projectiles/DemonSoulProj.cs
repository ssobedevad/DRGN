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
        private NPC target;
        private Player player;
        public int whichNpc;
        private bool targetFound;
        private int targetMag;
        private float speed;
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.tileCollide = false;
           
            player = Main.player[projectile.owner];
        }
        public override void AI()
        {
            Target();
            if (targetFound == false) { projectile.active = false; return; }
            else
            {
                move();
            }
        }
        private void move()
        {

            speed = 15f;
            Vector2 moveTo = target.Center;
            Vector2 moveVel = moveTo - projectile.Center;
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
            }

            projectile.velocity = moveVel;
        }


        private void Target()
        {
            targetMag = 1000;
            targetFound = false;
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
                        target = Main.npc[whichNpc];
                        targetFound = true;

                    }
                }
            }


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