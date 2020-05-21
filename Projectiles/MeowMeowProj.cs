using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class MeowMeowProj : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 18;
            projectile.width = 18;
            projectile.aiStyle = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;


        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Daybreak, 600);
            if (target.boss == true)
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("BossSlayer"), 360);
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);
            base.OnHitNPC(target, damage, knockBack, crit);
        }


    }
}
