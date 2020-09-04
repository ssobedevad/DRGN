using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 13f;
            range = 695;
            OutTime = 50;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/CrystilChain";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            if (projectile.ai[1] <= OutTime)
            {
                projectile.ai[1] += 1;
            }
            else if (projectile.localAI[0] <= -1)
            {               
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);
            }
            if (projectile.localAI[0] > -1)
            {                
                projectile.rotation = Vector2.Normalize(Main.player[projectile.owner].Center - projectile.Center).ToRotation() + MathHelper.ToRadians(225f);                             
            }
        }
    }
}
