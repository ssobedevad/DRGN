using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class AcceleratingArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 4;
            projectile.width = 4;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hide = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 4;                       

        }
        public override bool? CanHitNPC(NPC target)
        {
            return !target.dontTakeDamageFromHostiles;
        }
        public override void AI()
        {
            if(projectile.ai[0] == 0) { projectile.velocity = Vector2.Normalize(projectile.velocity); projectile.ai[0] = 1; projectile.ai[1] = projectile.damage; if (Main.player[projectile.owner].magicQuiver) { projectile.extraUpdates = 6; } }
            if(projectile.velocity.Length() > 16f) { projectile.velocity = Vector2.Normalize(projectile.velocity) * 16f; }
            int DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height , 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
            Main.dust[DustID].noGravity = true;
            projectile.velocity *= 1.005f;
            projectile.damage = (int)(projectile.ai[1] * (projectile.velocity.Length() / 6f));
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            projectile.ai[1] *=  1.1f;           
        }

    }

}

