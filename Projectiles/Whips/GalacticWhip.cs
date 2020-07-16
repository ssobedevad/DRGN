using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class GalacticWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 250;
            summonTagCrit = 65;
            rangeMult = 2f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), damage, knockback, projectile.owner);
        }




    }
}
