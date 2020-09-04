using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace DRGN.Projectiles.Minion
{
    public class FireBlastMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionShot[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.extraUpdates = 2;
            projectile.hide = true;
        }
        public override void AI()
        {           
            MoveTo();
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            Main.dust[dustid].noGravity = true;
        }
        public void MoveTo()
        {            
            float speed = 8f;
            Vector2 MoveTo = Main.npc[(int)projectile.ai[0]].Center - projectile.Center;
            float magnitude = MoveTo.Length();
            if (magnitude > speed)
            {
                MoveTo *= speed / magnitude;
                projectile.velocity = MoveTo;
                projectile.timeLeft = 2;
            }

        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));
                Main.dust[dustid].noGravity = true;
            }
        }
    }
}