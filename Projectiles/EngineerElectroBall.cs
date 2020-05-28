using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class EngineerElectroBall : ModProjectile
    {

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;

            projectile.tileCollide = true;
            projectile.melee = false;
            projectile.ranged = false;
            projectile.magic = false;
            projectile.thrown = false;
            projectile.minion = false;
            projectile.ai[0] = 1;
            projectile.penetrate = (int)projectile.ai[0];
            projectile.ai[1] = 0;



        }

        public override void AI()
        {
            projectile.penetrate = (int)projectile.ai[0];
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];


            }
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.6f);
            Main.dust[DustID].noGravity = true;

            projectile.rotation += 0.1f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 60);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(0, 100) <= (int)(projectile.ai[1]))
            {
                crit = true;


            }

        }

    }
}

