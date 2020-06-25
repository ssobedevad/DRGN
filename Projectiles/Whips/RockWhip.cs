using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class RockWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 15;
            summonTagCrit = 8;
            rangeMult = 0.8f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), projectile.damage, projectile.knockBack);
        }




    }
}
