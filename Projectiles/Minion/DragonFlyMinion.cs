using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public class DragonFlyMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        { 
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.height = 16;
            projectile.width = 32;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.minion = true;
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
        private void HandleBuffs(Player player)
        {
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("DragonFlyMinion"));
            }
            if (player.HasBuff(mod.BuffType("DragonFlyMinion")))
            {
                projectile.timeLeft = 2;
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Animate();
            HandleBuffs(player);
            int target = DavesUtils.FindNearestTargettableNPC(projectile, 1200, true);
            projectile.ai[0] = DetectPhase(target);
            if (projectile.ai[0] == 0) { MoveTo(player.Center); }
            else if (projectile.ai[0] == 1) { MoveTo(Main.npc[target].Center); }
            else
            {
                if (projectile.ai[0] == 4 && projectile.velocity.Y == 0)
                {
                    projectile.velocity.Y -= 16;
                }
                else
                {
                    projectile.velocity.Y += 0.5f;
                    bool left = projectile.ai[0] == 2;
                    float maxSpeed = 16f;
                    projectile.velocity.X = projectile.velocity.X + (left ? 0.5f * (projectile.velocity.Y < 0 ? 2 : 1) : -0.5f * (projectile.velocity.Y > 0 ? 2 : 1));
                    if (projectile.velocity.X > maxSpeed) { projectile.velocity.X = maxSpeed; }
                    if (projectile.velocity.Y > maxSpeed) { projectile.velocity.Y = maxSpeed; }
                }

            }
        }
        private int DetectPhase(int target)
        {
            projectile.tileCollide = true;
            if (target == -1) { projectile.tileCollide = false; return 0; }
            NPC tNPC = Main.npc[target];
            if (tNPC.noTileCollide || tNPC.noGravity || tNPC.Center.Y < projectile.Center.Y - 100 || tNPC.Center.Y > projectile.Center.Y + 10 || !Collision.CanHit(projectile.position, projectile.width, projectile.height, tNPC.position, tNPC.height, tNPC.width)) { projectile.tileCollide = false; return 1; }
            if (Math.Abs(tNPC.Center.X - projectile.Center.X) < 30) { return 4; }
            return tNPC.Center.X > projectile.Center.X ? 2 : 3;
        }
        private void MoveTo(Vector2 targetPos)
        {
            Vector2 Diff = targetPos - projectile.Center;
            float Magnitude = (float)Math.Sqrt((double)(Diff.X * Diff.X + Diff.Y * Diff.Y));
            float Speed = 18f + projectile.minionPos * 2;
            Magnitude = Speed / Magnitude;
            Diff *= Magnitude;
            projectile.velocity = (projectile.velocity * (20f + projectile.minionPos * 2) + Diff) / (21f + projectile.minionPos * 2);
        }

    }
}