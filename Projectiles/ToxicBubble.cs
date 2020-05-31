using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ToxicBubble : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
        

        }

        public override void AI()
        {
            if (Main.rand.Next(5) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 273, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
            projectile.rotation += 0.1f;
           
        }
        public override void Kill(int timeLeft) {
        int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 273, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 3f);
        Main.dust[DustID].noGravity = true; }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 273, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 3f);
            Main.dust[DustID].noGravity = true;
            target.AddBuff(mod.BuffType("Melting"), 100);
            base.OnHitNPC(target, damage, knockBack, crit);
        }

    }

}

