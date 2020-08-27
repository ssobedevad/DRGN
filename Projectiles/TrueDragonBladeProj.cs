using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class TrueDragonBladeProj : ModProjectile
    {
        public override void SetDefaults()
        {
      
            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
           

        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            if (target.active == true)
            {
                target.AddBuff(BuffID.Daybreak, 600); }
            Projectile.NewProjectile(target.Center.X, target.Top.Y - 5, 0, -16, mod.ProjectileType("TrueDragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Center.X, target.Bottom.Y + 5, 0, 16, mod.ProjectileType("TrueDragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Left.X - 10, target.Center.Y, -16, 0, mod.ProjectileType("TrueDragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Right.X + 10, target.Center.Y, 16, 0, mod.ProjectileType("TrueDragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), projectile.damage, 0f, projectile.owner);
            if (target.boss == true )
                Main.player[projectile.owner].AddBuff(mod.BuffType("BossSlayer"), 360);

        }
        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 61, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 3f);
            Main.dust[DustID].noGravity = true;
            int target = Target();


            if (target != -1)
            {
                move(target);
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

        }
        private void move(int Target)
        {

            float speed = 30f;
            Vector2 moveTo = Main.npc[Target].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                projectile.velocity = projectile.velocity + moveVel / 20f;
            }

        }








        private int Target()
        {
            int target = -1;
            int targetMag = 1000;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;


                    }
                }
            }
            return target;


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }

}

