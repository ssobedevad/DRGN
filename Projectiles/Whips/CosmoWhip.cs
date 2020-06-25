using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class CosmoWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 20;
            summonTagCrit = 10;
            rangeMult = 0.9f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            Projectile.NewProjectile(target.position.X, target.position.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), projectile.damage, projectile.knockBack, Main.myPlayer);
        }




    }
}
