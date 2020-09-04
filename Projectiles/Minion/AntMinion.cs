using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public class AntMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
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
                player.ClearBuff(mod.BuffType("QueenAntMinion"));
            }
            if (player.HasBuff(mod.BuffType("QueenAntMinion")))
            {
                projectile.timeLeft = 2;
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Animate();
            HandleBuffs(player);
            int target = DavesUtils.FindNearestTargettableNPC(projectile, 1000, true);
            projectile.ai[0] = DetectPhase(target);
            if(projectile.ai[0] == 0) { MoveTo(player.Center); }
            else if (projectile.ai[0] == 1) { MoveTo(Main.npc[target].Center); }
            else
            {
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
            if (target == -1) { projectile.tileCollide = false; return 0; }
            NPC tNPC = Main.npc[target];
            if(tNPC.noTileCollide || tNPC.noGravity || tNPC.Center.Y < projectile.Center.Y - 100 || tNPC.Center.Y > projectile.Center.Y + 10 || !Collision.CanHit(projectile.position,projectile.width,projectile.height,tNPC.position,tNPC.height,tNPC.width)) { projectile.tileCollide = false; return 1;}
            if(Math.Abs(tNPC.Center.X - projectile.Center.X) < 30) { return 4; }
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
           if(Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().antArmorSet)
           {
                if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("AntWarrior")] < Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().maxAnts)
                { Projectile.NewProjectile(projectile.position + new Vector2(0, 600), Vector2.Zero, mod.ProjectileType("AntWarrior"), (int)(projectile.damage * Main.player[projectile.owner].GetModPlayer<DRGNPlayer>().antDamageMult), projectile.knockBack, projectile.owner); }
            }
        }
    }
}