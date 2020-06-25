using DRGN.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{

    public class IceChainWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 12;
            summonTagCrit = 5;
            rangeMult = 0.7f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y - 500, (float)(Main.rand.Next(-100, 100)) / 100f, 5, mod.ProjectileType("Icicle"), 50, 1f, Main.myPlayer);
        }




    }
}
