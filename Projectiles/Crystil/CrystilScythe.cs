using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilScythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 14.5f;
            OutTime = 80;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
    }
    public class CrystilScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 12.5f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }

    }
}
