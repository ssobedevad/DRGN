using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class VoidedKnives : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 60;
            projectile.penetrate = 1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);

        }
        public override void AI()
        {





            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            




        }


        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("VoidedExplosion"), projectile.damage, 0f, projectile.owner);
           



        }


    }

}

