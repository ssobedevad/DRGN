using DRGN.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class ElementalAntWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 7;
            summonTagCrit = 3;
            rangeMult = 0.65f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {
            int[] buffchoice = new int[3] { ModContent.BuffType<Shocked>(), ModContent.BuffType<Burning>(), ModContent.BuffType<Melting>() }; target.AddBuff(Main.rand.Next(buffchoice), 220);
            
             Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), 40, 1f, Main.myPlayer); 
        }




    }
}
