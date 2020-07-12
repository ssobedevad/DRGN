using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles.Minion
{
    public class IceFishMinion : ModProjectile
    {
        public int whichNpc;
        private int target;
        private Vector2 targetPos;
        private int targetMag;


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

            projectile.height = 34;
            projectile.width = 44;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;

            projectile.penetrate = -1;
            projectile.tileCollide = false;

            projectile.minion = true;
            Main.projFrames[projectile.type] = 4;
        }
        

        public override void AI()
        {

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                

            }
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("IceFishMinion"));
            }
            if (player.HasBuff(mod.BuffType("IceFishMinion")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            if (Vector2.Distance(projectile.Center, player.Center) > 1600f)
            { projectile.Center = player.Center + new Vector2(Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1), Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1)); }

            if (target == -1)
            { targetPos = Main.player[Main.myPlayer].Center; }
            else
            {
                targetPos = Main.npc[target].Center;




            }

            MoveTo();
            projectile.spriteDirection = projectile.direction;
        }

        private void Target()
        {
            targetMag = 1000;
            target = -1;
            if (Main.player[projectile.owner].MinionAttackTargetNPC > 0)
            { target = Main.player[projectile.owner].MinionAttackTargetNPC; return; }
            for (whichNpc = 0; whichNpc < 200; whichNpc++)
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


        }
        private void MoveTo()
        {

            Vector2 ThisCenter = new Vector2(projectile.Center.X, projectile.Center.Y);
            float Xdiff = targetPos.X - ThisCenter.X + Main.rand.Next(-100, 100);
            float YDiff = targetPos.Y - ThisCenter.Y - Main.rand.Next(-100, 100);
            float Magnitude = (float)Math.Sqrt((double)(Xdiff * Xdiff + YDiff * YDiff));
            float Speed = 32f;
            Magnitude = Speed / Magnitude;
            Xdiff *= Magnitude;
            YDiff *= Magnitude;
            projectile.velocity.X = (projectile.velocity.X * 40f + Xdiff) / 41f;
            projectile.velocity.Y = (projectile.velocity.Y * 40f + YDiff) / 41f;
        }

    }
}