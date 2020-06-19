using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Steamworks;

namespace DRGN.Projectiles
{
    public class ProbeFriendly : ModProjectile
    {
        public float speed;
        public int shootCD;
        public int target;
        public int targetMag;
        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            
            
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {

            projectile.width = 32;               //this is where you put the npc sprite width.     important
            projectile.height = 32;              //this is where you put the npc sprite height.   important
            projectile.damage = 25;
           
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            
           
           

        }
        public override void AI()
        {
            if (shootCD > 0) { shootCD -= 1; }
            Target();
            float Xdiff = Main.player[projectile.owner].Center.X - projectile.Center.X;
            float YDiff = Main.player[projectile.owner].Center.Y - projectile.Center.Y;
            if (target != -1)
            {
                Xdiff = Main.npc[target].Center.X - projectile.Center.X;
                YDiff = Main.npc[target].Center.Y - projectile.Center.Y;
            }


            Vector2 difference = new Vector2(Xdiff, YDiff);
            float Magnitude = Mag(difference);

            speed = 1f;
            if (Magnitude > 100) { speed = 3f; }
            else if (Magnitude > 220) { speed = 18f; }
            else if (Magnitude > 450) { speed = 30f; }
            else if (Magnitude > 650) { speed = 48f; }
            else if (Magnitude > 950) { speed = 75f; }

            Magnitude = speed / Magnitude;
            Xdiff *= Magnitude;
            YDiff *= Magnitude;

            projectile.velocity.X = (projectile.velocity.X * 100f + Xdiff) / 101f;
            projectile.velocity.Y = (projectile.velocity.Y * 100f + YDiff) / 101f;
            if (shootCD == 0 && target != -1) { int projid = Projectile.NewProjectile(projectile.Center, AimAtNPC(10f), ProjectileID.DeathLaser, 35, 0, projectile.owner); shootCD = 120; Main.projectile[projid].hostile = false; Main.projectile[projid].friendly = true; }

            projectile.rotation = (float)Math.Atan2((double)YDiff, (double)Xdiff) - 1.57f;

            Lighting.AddLight((int)((projectile.Center.X + (float)(projectile.width / 2)) / 16f), (int)((projectile.Center.Y + (float)(projectile.height / 2)) / 16f), 1.5f, 1.5f, 1.5f);
        }
        private void Target()
        {
            target = -1;
            targetMag = 600;
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


        }

        private Vector2 AimAtNPC(float projSpeed)
        {
            NPC npc = Main.npc[target];
            Vector2 playerDiff = npc.Center - projectile.Center;
            float Magnitude = Mag(playerDiff);
            Magnitude = projSpeed / Magnitude;
            return playerDiff *= Magnitude;
        }
        private float Mag(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }



    }
}
