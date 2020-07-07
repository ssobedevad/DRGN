using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class VoidedOrbSplit : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            
            projectile.penetrate = 1;


        }

        public override void AI()
        {
            int target = Target();
            if(target != -1)
            { move(target); projectile.rotation += 0.2f; }
            projectile.rotation += 0.1f;


        }
        private int Target()
        {
            int target = -1;
            int targetMag = 300;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {

                    float DistanceProjtoNpc = Vector2.Distance(Main.npc[whichNpc].Center, projectile.Center);
                    if (DistanceProjtoNpc < targetMag && whichNpc != projectile.ai[1])
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }

            return target;

        }
        private void move(int target)
        {
            
            if (target != -1)
            {
                float speed = 15f;
                Vector2 moveTo = Main.npc[target].Center;
                Vector2 moveVel = moveTo - projectile.Center;
                float magnitude = Vector2.Distance(moveTo, projectile.Center);
                if (magnitude > speed)
                {
                    moveVel *= speed / magnitude;
                }

                projectile.velocity = (projectile.velocity * 20f + moveVel) / 21f;
            }

        }




    }

}

