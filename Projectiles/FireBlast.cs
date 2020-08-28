using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class FireBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.extraUpdates = 2;
            projectile.hide = true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0 && projectile.owner == Main.myPlayer)
            {
                int target = DavesUtils.FindNearestTargettableNPC(projectile, 200);
                if (target != -1) { projectile.velocity = Vector2.Normalize(Main.npc[target].Center - projectile.Center) * 10f; }
                Vector2 desiredPos = projectile.Center + DavesUtils.Rotate(Vector2.Normalize(projectile.velocity) * 50f, Main.rand.NextFloat(-0.5f, 0.5f));
                projectile.ai[0] = desiredPos.X;
                projectile.ai[1] = desiredPos.Y;
                projectile.netUpdate = true;
                projectile.localAI[0] = 1;
            }
            MoveTo();
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height,DustID.Fire);
            Main.dust[dustid].noGravity = true;
        }
        public void MoveTo()
        {
            float speed = 6f;
            Vector2 MoveTo = new Vector2(projectile.ai[0], projectile.ai[1]) - projectile.Center;
            float magnitude = MoveTo.Length();
            if (magnitude > speed * 2)
            {
                projectile.timeLeft = 2;
                MoveTo *= speed / magnitude;
                projectile.velocity = MoveTo;
            }


        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        public override void Kill(int timeLeft)
        {
            if (projectile.localAI[1] < 20)
            {
                int proj = Projectile.NewProjectile(projectile.Center, Vector2.Normalize(Main.MouseWorld - Main.player[projectile.owner].Center) * 10, projectile.type, (int)(projectile.damage * 0.8f), projectile.knockBack * 0.8f, projectile.owner);
                Main.projectile[proj].localAI[0] = 0;
                Main.projectile[proj].localAI[1] = projectile.localAI[1] + 1;               
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));
                    Main.dust[dustid].noGravity = true;
                }
            }
            if (projectile.localAI[1] < 2)
            {
                int proj = Projectile.NewProjectile(projectile.Center, Vector2.Normalize(Main.MouseWorld - Main.player[projectile.owner].Center) * 10, projectile.type, (int)(projectile.damage * 0.8f), projectile.knockBack * 0.8f, projectile.owner);
                Main.projectile[proj].localAI[0] = 0;
                Main.projectile[proj].localAI[1] = projectile.localAI[1] + 1;
            }
        }
    }
}