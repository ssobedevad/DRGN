using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class ShadowScream : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.extraUpdates = 16;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0 && projectile.owner == Main.myPlayer)
            {
                int target = DavesUtils.FindNearestTargettableNPC(projectile, 100);
                if(target != -1) { projectile.velocity = Vector2.Normalize(Main.npc[target].Center - projectile.Center) * 10f; }
                Vector2 desiredPos = projectile.Center + DavesUtils.Rotate(Vector2.Normalize(projectile.velocity) * 200f, Main.rand.NextFloat(-3f, 3f));
                projectile.ai[0] = desiredPos.X;
                projectile.ai[1] = desiredPos.Y;
                projectile.netUpdate = true;
                projectile.localAI[0] = 1;
            }
            MoveTo();
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("ShadowDust"));
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
                projectile.velocity = (projectile.velocity * 100f + MoveTo) / 101f;
            }
            
            
        }
        public override void Kill(int timeLeft)
        {
            if(projectile.localAI[1] < 4)
            {
                int proj = Projectile.NewProjectile(projectile.Center, Vector2.Normalize(Main.MouseWorld - projectile.Center) * 10, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                Main.projectile[proj].localAI[0] = 0;
                Main.projectile[proj].localAI[1] = projectile.localAI[1] + 1;
            }
        }
    }
}