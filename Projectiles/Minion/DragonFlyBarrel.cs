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
    public class DragonFlyBarrel : ModProjectile
    {






        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;

        }

        public override void SetDefaults()
        {

            projectile.friendly = true;


            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.height = 40;
            projectile.width = 40;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ai[1] = 1;
            projectile.minion = true;
            Main.projFrames[projectile.type] = 5;
        }


        public override void AI()
        {



            Player player = Main.player[projectile.owner];

            projectile.minionSlots = player.maxMinions;
            projectile.ai[1] = projectile.minionSlots;
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("DragonFlyMinion"));
            }
            if (player.HasBuff(mod.BuffType("DragonFlyMinion")))
            {
                projectile.timeLeft = 2;
            }

            if (Vector2.Distance(projectile.Center, player.Center) > 1600f)
            { projectile.Center = Main.player[Main.myPlayer].Center + new Vector2(Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1), Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1)); }
            MoveTo(Target(), player);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            if ((Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("DragonFlyMinion")] + Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("DragonFlyMinionFlying")]) < projectile.ai[1])
            {
                if (projectile.frame == 4)
                { projectile.frame = 1; }
                if (++projectile.frameCounter % 10 == 0)
                {
                    projectile.frameCounter = 0;
                    projectile.frame += 1;
                }

                if (projectile.frame == 3 && projectile.frameCounter == 0)
                {
                    if (Main.npc[Target()].noTileCollide || (Main.npc[Target()].Center.Y - projectile.Center.Y) < 80)
                    { Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("DragonFlyMinionFlying"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI); }
                    else { Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("DragonFlyMinion"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI); }



                    //Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("AntMinionFlying"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);

                }

            }
            else { projectile.frame = 1; }


            return false;
        }
        private int Target()
        {
            int targetMag = 1000;
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
        private void MoveTo(int target, Player player)
        {
            if (target != -1)
            {

                projectile.tileCollide = true;
                projectile.velocity.Y += 0.2f;

            }
            else
            {
                projectile.frame = 0;
                projectile.tileCollide = false;
                Vector2 ThisCenter = new Vector2(projectile.Center.X, projectile.Center.Y);
                float Xdiff = player.Center.X - ThisCenter.X + Main.rand.Next(-100, 100);
                float YDiff = player.Center.Y - ThisCenter.Y - Main.rand.Next(-100, 100);
                float Magnitude = (float)Math.Sqrt((double)(Xdiff * Xdiff + YDiff * YDiff));
                float Speed = 14f;
                Magnitude = Speed / Magnitude;
                Xdiff *= Magnitude;
                YDiff *= Magnitude;
                projectile.velocity.X = (projectile.velocity.X * 30f + Xdiff) / 31f;
                projectile.velocity.Y = (projectile.velocity.Y * 30f + YDiff) / 31f;
            }
        }

    }
}