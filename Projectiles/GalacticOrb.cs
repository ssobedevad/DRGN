using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class GalacticOrb : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 100;
            projectile.penetrate = 2;


        }

        public override void AI()
        {

            projectile.velocity *= 0.98f;
            projectile.rotation += 0.15f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity *= 0.5f;
            splitAction(1);
        }
        public override void Kill(int timeleft)
        {
            splitAction(2);



        }
        private void splitAction(int type)
        {

            
            Projectile.NewProjectile(projectile.Top, new Vector2(0, -6), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Left, new Vector2(-6, 0), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Right, new Vector2(6, 0), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Top, new Vector2(0, 6), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
            if (type == 2)
            {
                Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), projectile.damage, 0f, projectile.owner);
                Projectile.NewProjectile(projectile.TopLeft, new Vector2(-3, -3), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
                Projectile.NewProjectile(projectile.BottomLeft, new Vector2(-3, 3), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
                Projectile.NewProjectile(projectile.TopRight, new Vector2(3, -3), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
                Projectile.NewProjectile(projectile.BottomRight, new Vector2(3, 3), mod.ProjectileType("GalacticOrbSplit"), projectile.damage, 0f, projectile.owner);
            }


        }


    }

}

