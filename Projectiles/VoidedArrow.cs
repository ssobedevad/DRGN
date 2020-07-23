using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Projectiles
{
    public class VoidedArrow : RiochetProjectile
    {




        public override void SafeSetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;

            projectile.ranged = true;
            projectile.penetrate = 8;
            Gravity = 0.2f;
            TerminalVelocity = 16f;
            RiochetSpeed = 16f;

        }
        public override void NPCHitEffects(NPC target, int damage, float knockBack, bool crit)
        {
            projectile.hide = true;
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("VoidedExplosion"), projectile.damage, 0f, projectile.owner);
        }
        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (projectile.hide)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 98, 0, 0, 200, default(Color), 1.5f);
                Main.dust[DustID].noGravity = true;
            }
        }




    }
}
