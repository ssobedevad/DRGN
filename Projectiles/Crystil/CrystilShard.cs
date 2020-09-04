using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles.Crystil
{
    public class CrystilShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
            projectile.tileCollide = true;
        }
        public override void AI()
        {
           
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);            
            projectile.ai[0]++;
            if (projectile.ai[0] > 60)
            { projectile.velocity.Y += 0.5f; }
            if (projectile.velocity.Y > 16f) { projectile.velocity.Y = 16f; }
            if (Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().crystilArmorSet)
            {
                Move();
            }
        }
        public override void Kill(int timeLeft)
        {
            if(Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().crystilArmorSet)
            {
                Projectile.NewProjectile(projectile.Center, projectile.velocity, ProjectileID.CrystalShard, (int)(projectile.damage*0.5f), projectile.knockBack, projectile.owner);
            }
        }
        private void Move()
        {
            int target = DavesUtils.FindNearestTargettableNPC(projectile,150);
            if (target > -1)
            {
                projectile.tileCollide = false;
                float speed = 12f;
                Vector2 moveTo = Main.npc[target].Center - projectile.Center;
                float magnitude = moveTo.Length();
                if (magnitude > speed)
                {
                    moveTo *= speed / magnitude;                  
                    projectile.timeLeft = 2;
                }
                projectile.velocity = moveTo;
            }
        }
    }
}
