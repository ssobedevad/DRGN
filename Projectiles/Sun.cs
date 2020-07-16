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
    public class Sun : ModProjectile
    {
        public int whichNpc;
        private int target ;
        private Vector2 targetPos;
        
        private int targetMag ;
       
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
            projectile.damage = 100;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.minionSlots = 0f;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 2;

            projectile.minion = true;
            
            projectile.light = 10f;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            
            Player player = Main.player[projectile.owner];
            
            if (player.dead || !player.active|| !player.GetModPlayer<DRGNPlayer>().cloudArmorSet)
            {
                player.ClearBuff(mod.BuffType("Sun"));
            }
            if (player.HasBuff(mod.BuffType("Sun")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            
            if (target == -1)
            { targetPos = player.Center + new Vector2(0, -20); projectile.frame = 0;projectile.rotation = 0f; }
           else { targetPos = Main.npc[target].Center; projectile.frame = 1;projectile.rotation += 0.6f; }
           
            Move();
        }
        public override void Kill(int timeLeft) { Player player = Main.player[projectile.owner];  }

        private void Target()
        {
            targetMag = 500;
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
            Vector2 moveTo = targetPos;
            Vector2 move = moveTo - projectile.Center;
            float magnitude = Magnitude(move);
            if (magnitude > 10)
            {
                move *= 10 / magnitude;
            }

            projectile.velocity = move;
            
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}