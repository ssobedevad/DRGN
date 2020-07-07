using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class VoidedOrb : ModProjectile
    {
        
        public override void SetDefaults()
        {
            
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 90;
            projectile.penetrate = 1;


        }

        public override void AI()
        {

            projectile.velocity *= 0.95f;
            projectile.rotation += 0.1f;

        }
        public override void Kill(int timeleft)
        { 
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("VoidedExplosion"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Top,  new Vector2(0,-5), mod.ProjectileType("VoidedOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Left, new Vector2(-5, 0), mod.ProjectileType("VoidedOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Right, new Vector2(5, 0), mod.ProjectileType("VoidedOrbSplit"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Top, new Vector2(0, 5), mod.ProjectileType("VoidedOrbSplit"), projectile.damage, 0f, projectile.owner);



        }


    }

}

