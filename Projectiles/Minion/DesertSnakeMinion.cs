using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public class DesertSnakeMinion : ModProjectile
    {
        public int whichNpc;
        private int target;
        private bool tileCollision;
        private int animationType;
        private int oldAnimationType;
        private int targetMag;


        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[projectile.type] = 15;
        }
        public override void SetDefaults()
        {

            projectile.friendly = true;

            projectile.height = 58;
            projectile.width = 54;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.tileCollide = true;
            projectile.penetrate = -1;

            projectile.minion = true;

        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {


            if (projectile.velocity.X > 1 || projectile.velocity.X < -1) { projectile.velocity.Y = -10f; projectile.velocity.X = oldVelocity.X; }
            if (oldVelocity.Y > 0)
            { projectile.velocity.Y = 0; tileCollision = true; if (animationType == 0) { animationType = 1; } }
            else
            {
                projectile.velocity.Y += 2f;

            }
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("DesertSnakeMinion"));
            }
            if (player.HasBuff(mod.BuffType("DesertSnakeMinion")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            if (player.MinionAttackTargetNPC > 0) { target = player.MinionAttackTargetNPC; }
            if (Vector2.Distance(projectile.Center, player.Center) > 1600f)
            { projectile.Center = player.Center + new Vector2(Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1), Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1)); }

            if (target == -1 )
            { FollowPlayer(player); }
            else
            {

                AttackEnemy(Main.npc[target]);

            }
            Animation();
            oldAnimationType = animationType;
            if(projectile.velocity.Y > 16f){ projectile.velocity.Y = 16f; }


        }
        private void Animation()
        {
            if (animationType != oldAnimationType) { projectile.frameCounter = 0; }
            if (projectile.velocity.X < -0.1) { projectile.direction = 1; } else if (projectile.velocity.X > 0.1) { projectile.direction = -1; }
            if (animationType == -2)
            {
                if (++projectile.frameCounter % 10 == 0)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = (projectile.frame == 13) ? 14 : 13;

                }
                projectile.spriteDirection = projectile.direction;
            }
            if (animationType == -1)
            {
                if (++projectile.frameCounter % 40 == 0)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = (projectile.frame == 11) ? 12 : 11;

                }
                projectile.spriteDirection = projectile.direction;
            }
            else if (animationType == 0)
            {
                if (++projectile.frameCounter % 6 == 0)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = (projectile.frame == 0) ? 1 : 0;
                }
                projectile.spriteDirection = projectile.direction;
            }
            else if (animationType == 1)
            {

                projectile.frameCounter += 1;
                if (projectile.frameCounter % 12 == 0) { projectile.frameCounter = 0; animationType = 2; return; };
                projectile.frame = 2 + (int)projectile.frameCounter / 4;
                projectile.spriteDirection = projectile.direction;



            }
            else if (animationType == 2)
            {

                projectile.frameCounter += 1;
                if (projectile.frameCounter % 30 == 0) { projectile.frameCounter = 0; animationType = 3; return; };
                projectile.frame = 5 + (int)projectile.frameCounter / 6;
                projectile.spriteDirection = projectile.direction;



            }
            else if (animationType == 3)
            { projectile.frameCounter += 1; projectile.frame = 10; projectile.spriteDirection = projectile.direction; if (projectile.frameCounter < 20) { projectile.tileCollide = false; } else { projectile.tileCollide = true; }
                if (projectile.velocity.Y  == 0) { animationType = 1; }
            
            }


        }
        private void AttackEnemy(NPC target)
        {
            projectile.tileCollide = true;
            if (Vector2.Distance(projectile.Center, target.Center) > ((animationType == 3 && projectile.frameCounter > 40) ? 200:400) || !Collision.CanHitLine(projectile.position, projectile.width,projectile.height, target.Center, target.width,target.height))
            {

                Move(target.Center);
                projectile.tileCollide = false;

            }
            
            if (tileCollision && animationType == 1) { projectile.velocity = Vector2.Zero; }
            else if (animationType == 3 && projectile.velocity == Vector2.Zero) { projectile.velocity = JumpAtEnemy(target);tileCollision = false; }
            else if (animationType == 3) { projectile.velocity.Y += 0.1f; tileCollision = false; }
            else { projectile.velocity.Y += 0.2f; }


        }
        private void FollowPlayer(Player player)
        {
            float DesiredX = player.Center.X + (projectile.minionPos * -player.direction * 75 + (-player.direction * 30));

            float XDiff = Math.Abs(DesiredX - projectile.Center.X);
            float YDiff = Math.Abs(player.Center.Y - projectile.Center.Y);
            if (YDiff > 300 || XDiff > 600 || !Collision.CanHitLine(projectile.position, projectile.width, projectile.height, new Vector2(DesiredX, player.Center.Y), projectile.width, projectile.height)) { Move(new Vector2(DesiredX, player.Center.Y - 100)); projectile.tileCollide = false; }
            else
            {
                projectile.tileCollide = true;
                if (projectile.Center.X > DesiredX + 10) { if (tileCollision) { if (projectile.velocity.X > -2) { projectile.velocity.X = -4; } tileCollision = false; animationType = -2; projectile.velocity.X *= 0.96f; } else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.95f; } }
                else if (projectile.Center.X < DesiredX - 10) { if (tileCollision) { if (projectile.velocity.X < 2) { projectile.velocity.X = 4; } tileCollision = false; animationType = -2; projectile.velocity.X *= 0.96f; } else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.95f; } }
                else { projectile.velocity.X *= 0.5f; projectile.velocity.Y += 0.6f; if (projectile.velocity.Y + projectile.velocity.X < 1 && projectile.velocity.Y + projectile.velocity.X > -1 && tileCollision) { projectile.velocity = Vector2.Zero; animationType = -1; } }
            }


        }
        private void Target()
        {
            targetMag = 800;
            target = -1;

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {


                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {


                    float DistanceProjtoNpc = Vector2.Distance(projectile.Center, Main.npc[whichNpc].Center);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }


        }
        private void Move(Vector2 MoveTo)
        {
            // Sets the max speed of the npc.
            animationType = 0;
            Vector2 move = MoveTo - projectile.Center;
            float magnitude = Magnitude(move);

            if (magnitude > 20)
            {
                move *= 20 / magnitude;
            }
            

            projectile.velocity = (projectile.velocity * 30f + move) / 31f;

        }
        private Vector2 JumpAtEnemy(NPC target)
        {
            float speed = 10f; // Sets the max speed of the npc.
            Vector2 move = target.Center - projectile.Bottom;
            float magnitude = Magnitude(move);

            move *= speed / magnitude;


            return move;

        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}