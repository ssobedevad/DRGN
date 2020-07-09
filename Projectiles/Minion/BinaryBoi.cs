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
    public class BinaryBoi : ModProjectile
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

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.minionSlots = 0.5f;

            projectile.penetrate = -1;
            projectile.tileCollide = false;

            projectile.minion = true;
            Main.projFrames[projectile.type] = 2;
        }


        public override void AI()
        {

            
                
                projectile.frame = (int)projectile.ai[1];
                


            
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("BinaryBoi"));
            }
            if (player.HasBuff(mod.BuffType("BinaryBoi")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            if ((Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - Main.player[Main.myPlayer].Center.X) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - Main.player[Main.myPlayer].Center.Y)) > 1600f)
            { projectile.Center = Main.player[Main.myPlayer].Center + new Vector2(Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1), Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1)); }
           

            MoveTo((int)projectile.ai[1] , (int)projectile.ai[0]);
            
        }

        private void Target()
        {
            int targetMag = 1000;
            int target = -1;
            if (Main.player[projectile.owner].MinionAttackTargetNPC > 0)
            { projectile.ai[0] = Main.player[projectile.owner].MinionAttackTargetNPC; return; }
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
            projectile.ai[0] = target;


        }
        private void MoveTo(int style , int target)
        {
            if (style == 0 ||target == -1|| (target != -1 && Vector2.Distance(Main.npc[target].Center, projectile.Center) > 250))
            {
                Vector2 targetPos;
                if (target == -1) { targetPos = Main.player[projectile.owner].Center; }
                else
                {
                    targetPos = Main.npc[target].Center;
                }
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
                projectile.rotation += 0.3f;
            }
            else 
            {
                projectile.rotation = 0f;
                projectile.velocity *= 0.9f;
                projectile.localAI[0] += 1;
                if (projectile.localAI[0] > 30)
                { Projectile.NewProjectile(projectile.Center, shootVel(target), mod.ProjectileType("BinaryShot"), projectile.damage, 0f, projectile.owner); projectile.localAI[0] = 0; }
            
            }
        }
        private Vector2 shootVel(int target)
        {
            Vector2 targetPos = Main.npc[target].Center;
            targetPos = targetPos - projectile.Center;
            targetPos = Vector2.Normalize(targetPos);
            targetPos *= 16f;
            projectile.velocity += -targetPos /2;
            return targetPos;


        }

    }
}