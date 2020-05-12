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

namespace DRGN.Projectiles
{
    public class CloudSummon : ModProjectile
    {
        public int whichNpc;
        private int target ;
        private Vector2 targetPos;
        private int randMode;
        private int targetMag ;
        private int proj1;
        private float rotation;
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
            rotation = 0;
            projectile.height = 45;
            projectile.width = 62;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            randMode = -1;
            proj1 = -1;
            projectile.minion = true;
            Main.projFrames[projectile.type] = 4;
        }
        
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("CloudSummon"));
            }
            if (player.HasBuff(mod.BuffType("CloudSummon")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            if (target == -1 || (Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - Main.player[Main.myPlayer].Center.X) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - Main.player[Main.myPlayer].Center.Y)) > 1600f)
            { targetPos = Main.player[Main.myPlayer].Center + new Vector2(0, -100); randMode = -1; projectile.frame = 0; if (proj1 >= 0) { Main.projectile[proj1].ai[0] = -1;proj1 = -1; } }
           else { targetPos = Main.npc[target].Center + new Vector2(0, -100); 
           if (randMode == -1)
           { randMode = Main.rand.Next(1,4); }
                projectile.frame = randMode;
                if (randMode == 1 && proj1 == -1)
                { proj1 = Projectile.NewProjectile(projectile.Center, new Vector2(0, 14), mod.ProjectileType("SunRayMini"), (int)(projectile.damage), 0f, Main.myPlayer, projectile.whoAmI); }
                else if (randMode == 2)
                {
                    if (Main.rand.Next(0, 12) == 1)
                    {
                        Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-20, 20), projectile.Bottom.Y, 0, 5, mod.ProjectileType("RainMini"), (int)(projectile.damage), 0, Main.myPlayer, Main.myPlayer);
                    }
                }
                else if (randMode == 3)
                {
                    if (Main.rand.Next(0, 18) == 1)
                    { int projid = Projectile.NewProjectile(projectile.Center, new Vector2((float)Main.rand.Next(-20, 20), 500f), mod.ProjectileType("SingleLighteningMini"), (int)(projectile.damage), 1f, Main.myPlayer, Main.myPlayer, 1); }
                }
            


           }
           
            Move();
        }

        private void Target()
        {
            targetMag = 1000;
            target = -1;

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
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
        private void Move()
        {   
            // Sets the max speed of the npc.
            Vector2 moveTo = targetPos + new Vector2((float)Math.Sin(rotation / 2) * 50, (float)Math.Cos(rotation) * 50);
            Vector2 move = moveTo - projectile.Center;
            float magnitude = Magnitude(move);
            if (magnitude > 20)
            {
                move *= 20 / magnitude;
            }

            projectile.velocity = move;
            rotation+= 0.045f;
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}