using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public class AntMinion : ModProjectile
    {



        public bool fallthrough;


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
            projectile.minionSlots = 0f;
            projectile.height = 16;
            projectile.width = 32;
            projectile.penetrate = -1;
            projectile.tileCollide = true;

            projectile.minion = true;
            Main.projFrames[projectile.type] = 6;
        }


        public override void AI()
        {

            projectile.spriteDirection = -projectile.direction;


            if (++projectile.frameCounter % 6 == 0)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame == 6)
            { projectile.frame = 1; }

            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("QueenAntMinion"));
            }
            if (player.HasBuff(mod.BuffType("QueenAntMinion")))
            {
                projectile.timeLeft = 2;
            }

            if (Vector2.Distance(projectile.Center, player.Center) > 1600f)
            { projectile.Center = Main.projectile[(int)projectile.ai[0]].Center; }

            MoveTo(Target());

        }
        private int Target()
        {
            int targetMag = 600;
            int target = -1;
            if (Main.player[projectile.owner].MinionAttackTargetNPC > 0)
            { return Main.player[projectile.owner].MinionAttackTargetNPC; }
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {


                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag && Collision.CanHit(projectile.Center, projectile.width, projectile.height, Main.npc[whichNpc].Center, Main.npc[whichNpc].width, Main.npc[whichNpc].height))
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }
            return target;


        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = fallthrough;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        private void MoveTo(int target)
        {

            if (target == -1 || !Main.projectile[(int)projectile.ai[0]].tileCollide)
            {
                Projectile targetproj = Main.projectile[(int)projectile.ai[0]];
                if (targetproj.Center.X > projectile.Center.X + 20)
                { projectile.velocity.X = 5f; }
                else if (targetproj.Center.X < projectile.Center.X - 20)
                { projectile.velocity.X = -5f; }
                else if (targetproj.Center.Y > projectile.Center.Y && projectile.velocity.Y == 0)
                { projectile.velocity.Y = 3; fallthrough = true; }
                else if (targetproj.Center.Y < projectile.Center.Y && projectile.velocity.Y == 0)
                { projectile.velocity.Y = -10; fallthrough = false; }
                else { projectile.velocity.Y += 0.1f; }
                if (Vector2.Distance(projectile.Center, targetproj.Center) < 50) { projectile.active = false; }


            }
            else
            {
                NPC targetnpc = Main.npc[target];
                if (targetnpc.Center.X > projectile.Center.X + 20 && projectile.velocity.Y == 0)
                { projectile.velocity.X = 5f; }
                else if (targetnpc.Center.X < projectile.Center.X - 20 && projectile.velocity.Y == 0)
                { projectile.velocity.X = -5f; }
                else if (targetnpc.Center.Y > projectile.Center.Y + 20 && projectile.velocity.Y == 0)
                { projectile.velocity.Y = 3; fallthrough = true; }
                else if (targetnpc.Center.Y < projectile.Center.Y - 400 && projectile.velocity.Y == 0)
                { projectile.velocity.Y = -12; fallthrough = false; }
                else if (targetnpc.Center.Y < projectile.Center.Y - 100 && projectile.velocity.Y == 0)
                { projectile.velocity.Y = -8; fallthrough = false; }
                else if (targetnpc.Center.Y < projectile.Center.Y - 20 && projectile.velocity.Y == 0)
                { projectile.velocity.Y = -4; fallthrough = false; }
                else if (targetnpc.Center.Y < projectile.Center.Y + 5)
                { fallthrough = false; }
                else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.95f; }
            }
        }

    }
}