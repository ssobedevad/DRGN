using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class ShadowArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);

        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            if(projectile.ai[0] == 0 && projectile.timeLeft > 35) { projectile.timeLeft = 35; }
            projectile.ai[1]++;
            if (projectile.ai[1] > 20)
            {
                projectile.velocity.Y += 0.15f;
            }
            if(projectile.velocity.Y > 16f) { projectile.velocity.Y = 16f; }
            if (Main.player[projectile.owner].magicQuiver) { projectile.extraUpdates = 2; }
        }

        public override void Kill(int timeleft)
        {
            
            if (projectile.ai[0] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 vel = DavesUtils.Rotate(projectile.velocity * 1.4f, 0.5f * (i == 0 ? 1f : -1f));
                    Projectile.NewProjectile(projectile.Center, vel, projectile.type, (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner, 1); 
                }
                Projectile.NewProjectile(projectile.Center, projectile.velocity * 1.4f, projectile.type, (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner, 1);
            }
        }


    }
}
