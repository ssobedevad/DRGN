using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class AutoAim : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
        }
        public override void AI()
        {
            int target = DavesUtils.FindNearestTargettableNPC(projectile);
            if(target > -1) { Move(target); projectile.tileCollide = false;}
            if (Main.rand.NextBool())
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 20, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
        }
        private void Move(int target)
        {
            float speed = 15f;
            Vector2 moveTo = Main.npc[target].Center - projectile.Center;            
            float magnitude = moveTo.Length();
            if (magnitude > speed)
            {
                moveTo *= speed / magnitude;                
                projectile.velocity = moveTo;
            }
        }
    }
}