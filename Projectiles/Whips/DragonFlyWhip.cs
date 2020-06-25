using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class DragonFlyWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 40;
            summonTagCrit = 15;
            rangeMult = 0.95f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            
             Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("DragonFlyJaws"), projectile.damage, 1f, Main.myPlayer); 
        }




    }
}
