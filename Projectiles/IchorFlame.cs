using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class IchorFlame : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ai[0] = 0;
            projectile.light = 2f;



        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            int Dustid = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, DustID.GoldFlame, default(Color), 2f);
            Main.dust[Dustid].noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 120);
        }


    }
}