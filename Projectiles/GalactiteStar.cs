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
    public class GalactiteStar : ModProjectile
    {
        public int whichNpc;
        private int target;
        
        private int shootCD;
        private int targetMag;
        public NPC currentTarget;
        
        
        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = false;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {

            projectile.friendly = true;
            projectile.damage = 1000;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.minionSlots = 0f;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 5;
            
            projectile.minion = true;

            projectile.light = 10f;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            if (shootCD > 0) { shootCD -= 1; }
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];


            }
            
            Player player = Main.player[projectile.owner];
            player.GetModPlayer<DRGNPlayer>().starAlive = true;
            if (player.dead || !player.active || !player.GetModPlayer<DRGNPlayer>().galactiteArmorSet)
            {
                player.ClearBuff(mod.BuffType("GalactiteStar"));
            }
            if (player.HasBuff(mod.BuffType("GalactiteStar")))
            {
                projectile.timeLeft = 2;
            }
            Target();

            
             projectile.Center = player.Center + new Vector2(0, -40);
        
            if (target != -1)
            {
                currentTarget = Main.npc[target];
                
                

                if (shootCD == 0) { Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("GalactiteStarLaser"), projectile.damage, 0f, 0, target);shootCD = 20; }
                
            }

                
            }
        public override void Kill(int timeLeft) { Player player = Main.player[projectile.owner]; player.GetModPlayer<DRGNPlayer>().starAlive = false; 
                
                
            } 

        private void Target()
        {
            targetMag = 1300;
            target = -1;
            int closestTargetMag = 1300;
            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {


                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag && DistanceProjtoNpc < closestTargetMag )
                    {
                        closestTargetMag = targetMag;
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }


        }
        
        
        
    }
}