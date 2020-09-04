using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class AntWarrior : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.height = 16;
            projectile.width = 32;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            Main.projFrames[projectile.type] = 6;
        }

        private void Animate()
        {
            projectile.spriteDirection = -projectile.direction;
            int baseFrame = projectile.ai[0] > 1 ? 0 : 3;
            if (++projectile.frameCounter % 6 == 0)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame >= baseFrame + 3 || projectile.frame < baseFrame)
            { projectile.frame = baseFrame; }
        }      
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Animate();
            int target = DavesUtils.FindNearestTargettableNPC(projectile, 1000, true);
            projectile.ai[0] = DetectPhase(target);
            if (projectile.ai[0] == 0) { MoveTo(projectile.Center + new Vector2(0,100)); }
            else if (projectile.ai[0] == 1) { MoveTo(Main.npc[target].Center); }
            else
            {
                projectile.timeLeft = 160;
                if (projectile.ai[0] == 4 && projectile.velocity.Y == 0)
                {
                    projectile.velocity.Y -= 8;
                }
                else
                {
                    projectile.velocity.Y += 0.5f;
                    bool left = projectile.ai[0] == 2;
                    float maxSpeed = 8f * player.GetModPlayer<DRGNPlayer>().antSpeedMult;
                    float acceleration = 0.2f * player.GetModPlayer<DRGNPlayer>().antSpeedMult;
                    projectile.velocity.X = projectile.velocity.X + (left ? acceleration * (projectile.velocity.Y < 0 ? 2 : 1) : -acceleration * (projectile.velocity.Y > 0 ? 2 : 1));
                    if (projectile.velocity.X > maxSpeed) { projectile.velocity.X = maxSpeed; }
                    if (projectile.velocity.Y > maxSpeed) { projectile.velocity.Y = maxSpeed; }
                }

            }
        }
        private int DetectPhase(int target)
        {

            projectile.tileCollide = true;
            if (target == -1 || !Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().antArmorSet) { projectile.tileCollide = false; return 0; }
            NPC tNPC = Main.npc[target];
            if (tNPC.noTileCollide || tNPC.noGravity || tNPC.Center.Y < projectile.Center.Y - 100 || tNPC.Center.Y > projectile.Center.Y + 10 || !Collision.CanHit(projectile.position, projectile.width, projectile.height, tNPC.position, tNPC.height, tNPC.width)) { projectile.tileCollide = false; return 1; }
            if (Math.Abs(tNPC.Center.X - projectile.Center.X) < 30) { return 4; }
            return tNPC.Center.X > projectile.Center.X ? 2 : 3;
        }
        private void MoveTo(Vector2 targetPos)
        {
            Vector2 Diff = targetPos - projectile.Center;
            float Magnitude = (float)Math.Sqrt((double)(Diff.X * Diff.X + Diff.Y * Diff.Y));
            float Speed = 12f + projectile.minionPos * 2;
            Magnitude = Speed / Magnitude;
            Diff *= Magnitude;
            projectile.velocity = (projectile.velocity * (30f + projectile.minionPos * 2) + Diff) / (31f + projectile.minionPos * 2);
        }
    }
}