using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DRGN.Projectiles.GalacticGuardian
{
    public class MegaGalacticMissile : ModProjectile
    {
        private Vector2 target;
        private bool passedTarget;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.hostile = true;

            projectile.tileCollide = false;
            projectile.penetrate = -1;



        }

        public override void AI()
        {
            if (target == Vector2.Zero)
            {
                int TG = Target();
                if (TG != -1)
                {
                    target = Main.player[Target()].Center;
                }
            }

            projectile.rotation += 0.3f;
            float shootSpeed = DRGNModWorld.MentalMode ? 28f : Main.expertMode ? 26f : 24f;
            if (!passedTarget)
            {
                Move(shootSpeed);
            }

            int Dustid = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 100, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1.5f);
            Main.dust[Dustid].noGravity = true;

        }
        private void Move(float moveSpeed)
        {

            Vector2 moveTo2 = target - projectile.Center;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 4)
            {
                moveTo2 *= moveSpeed / magnitude;
            }
            else { passedTarget = true; }


            projectile.velocity = (projectile.velocity * 11f + moveTo2) / 12f;


        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("GalacticCurse"), 60);
        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private int Target()
        {
            int targetMag = 0;
            int target = -1;
            for (int plrnum = 0; plrnum < 255; plrnum++)
            {
                if (Main.player[plrnum].active && !Main.player[plrnum].dead)
                {

                    float DistanceProjtoPlr = Vector2.Distance(Main.player[plrnum].Center, projectile.Center);
                    if (targetMag <= 0 || DistanceProjtoPlr < targetMag)
                    {
                        targetMag = (int)DistanceProjtoPlr;
                        target = plrnum;


                    }
                }
            }
            return target;


        }

    }
}
