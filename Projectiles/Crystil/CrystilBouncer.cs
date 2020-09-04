using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Projectiles.Crystil
{
    public class CrystilBouncer : ModProjectile
    {
        private Vector2 target;
        public override void SetDefaults()
        {
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = 3;
            projectile.localAI[0] = -1;
            projectile.tileCollide = false;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
            projectile.extraUpdates = DRGNModWorld.MentalMode ? 2 : 1;
        }
        public override void AI()
        {
            projectile.rotation += 0.15f;
            if (target == Vector2.Zero)
            {
                Target();
            }
            else
            {
                Move(8f);
            }
        }
        private void Move(float moveSpeed)
        {
            if (projectile.ai[0] > -1)
            {
                Vector2 moveTo2 = target - projectile.Center;
                float magnitude = moveTo2.Length();
                if (magnitude > moveSpeed * 2)
                {
                    moveTo2 *= moveSpeed / magnitude;
                }
                else { projectile.ai[0] = -1; }
                projectile.velocity = (projectile.velocity * 20f + moveTo2) / 21f;
            }
        }
        private void Target()
        {           
            int targetMag = 0;
            int Target = -1;
            for (int plrnum = 0; plrnum < 255; plrnum++)
            {
                if (Main.player[plrnum].active && !Main.player[plrnum].dead)
                {
                    float DistanceProjtoPlr = Vector2.Distance(Main.player[plrnum].Center, projectile.Center);
                    if (targetMag <= 0 || DistanceProjtoPlr < targetMag)
                    {
                        targetMag = (int)DistanceProjtoPlr;
                        Target = plrnum;
                        target = Main.player[Target].Center;
                    }
                }
            }
            
        }
    }
}


