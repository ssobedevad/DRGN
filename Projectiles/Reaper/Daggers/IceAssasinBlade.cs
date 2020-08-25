using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class IceAssasinBlade : ReaperKnife
    {
        public override void SSD()
        {
            speed = 9.25f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {            
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockback, projectile.owner);
            target.AddBuff(BuffID.Frostburn, 60);
        }
    }
    public class IceAssasinBladeThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockback, projectile.owner);
            target.AddBuff(BuffID.Frostburn, 60);
        }

    }
}
