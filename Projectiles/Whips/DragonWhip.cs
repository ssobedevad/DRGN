using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class DragonWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 75;
            summonTagCrit = 25;
            rangeMult = 1f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            for (int i = 0; i < 3;i++)
            
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("Spark"), projectile.damage, projectile.knockBack, projectile.owner); }
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);
        }




    }
}
