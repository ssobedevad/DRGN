using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class AntBiterJaws : ModProjectile
    {

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
            projectile.light = 0.1f;

        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }           
        }
        public override void Kill(int timeLeft)
        {
            if (Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().antArmorSet)
            {
                if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("AntWarrior")] < Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().maxAnts)
                { Projectile.NewProjectile(projectile.position + new Vector2(0, 600), Vector2.Zero, mod.ProjectileType("AntWarrior"), (int)(projectile.damage * Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().antDamageMult), projectile.knockBack, projectile.owner); }
            }
        }
    }

}