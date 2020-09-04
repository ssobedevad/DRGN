using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilDagger : ReaperKnife
    {
        public override void SSD()
        {
            speed = 11.25f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
    }
    public class CrystilDaggerThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }

    }
}
