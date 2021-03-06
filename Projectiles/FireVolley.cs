﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FireVolley : ModProjectile
    {
        private int bounces;
        
        private int VolleyCD;

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 3;
            projectile.ai[0] = 0;
            bounces = 0;
            projectile.tileCollide = true;
            VolleyCD = 0;
            
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; // projectile sprite faces up
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
            if (projectile.ai[0] > 0 && VolleyCD > 5)
            { Projectile.NewProjectile(Main.player[projectile.owner].Center, projectile.velocity, mod.ProjectileType("FireVolley"), projectile.damage, projectile.knockBack, projectile.owner,projectile.ai[0] -1); projectile.ai[0] = 0;VolleyCD = 0; }
            VolleyCD += 1;

        }
        public override bool OnTileCollide(Vector2 Oldvel)
        {
            if (bounces < 1)
            {
                if (projectile.velocity.Y != Oldvel.Y)
                {
                    projectile.velocity.Y = 0f - Oldvel.Y;
                }
                if (projectile.velocity.X != Oldvel.X)
                {
                    projectile.velocity.X = 0f - Oldvel.X;
                }
                    bounces += 1; return false; }

            else
            {
                return true;
            }
        }
      public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }





    }
}


