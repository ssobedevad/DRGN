using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class VoidBladeProj : ModProjectile
    {

        private Player player;



        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 2f;
            drawOriginOffsetY = 26;
            drawOffsetX = 50;

        }
        public override void AI()
        {
            player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.rotation += 0.3f;
            
            if (!player.channel) { projectile.active = false; }
        }

        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("VoidHook"), 1, 0, player.whoAmI, target.whoAmI - 1, 1);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("VoidHook"), 1, 0, player.whoAmI, target.whoAmI - 1, -1);


            target.AddBuff(mod.BuffType("VoidBuff"), 600);
            base.OnHitNPC( target, damage, knockBack, crit);
        }
    }
}