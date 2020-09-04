using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public abstract class ThrowingFlask : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;            
        }
        public override void AI()
        {
           projectile.ai[0]++;
           projectile.rotation += 0.3f;
           if(projectile.ai[0] > 30) { projectile.velocity.Y += 0.5f; }
           if(projectile.velocity.Y > 16f) { projectile.velocity.Y = 16f; }
        }
        public virtual void DeathEffect()
        { }
        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item107, projectile.position);
            DeathEffect();
        }
    }
}