using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class TrueUnstableMeteor : ModProjectile
    {


        public int num33;

        private float speed;
        private int num31;
        private int num32;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ai[0] = 0;
            projectile.light = 8f;



        }
        public override void AI()
        {
            if (projectile.ai[0] >= 15)
            { projectile.tileCollide = true; }
            projectile.ai[0] += 1;
            Target();
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1.8f);
                Main.dust[DustID].noGravity = true;
            }
        }
        private void Target()
        {
            int num3 = 0;

            for (num33 = 0; num33 < 200; num33 = num3 + 1)
            {
                if (Main.npc[num33].CanBeChasedBy(this, false))
                {
                    float num34 = Main.npc[num33].Center.X;
                    float num35 = Main.npc[num33].Center.Y;
                    float num36 = (this.projectile.position.X + (float)(this.projectile.width / 2) - num34);
                    if (num36 < 0f && num36 > -120f)
                    {
                        projectile.velocity.X += ((0 - num36) / 120);


                    }
                    if (num36 > 0f && num36 < 120f) { projectile.velocity.X -= ((num36) / 120); }
                }
                num3 = num33;

            }
        }
        public override bool OnTileCollide(Vector2 Vc)
        {
            for (int i = 0; i < Main.rand.Next(5, 12); ++i)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), mod.ProjectileType("TrueUnstableMeteorShatter"), projectile.damage, projectile.knockBack, Main.myPlayer);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(5, 12); ++i)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), mod.ProjectileType("TrueUnstableMeteorShatter"), projectile.damage, projectile.knockBack, Main.myPlayer);
            }

        }
    }
}