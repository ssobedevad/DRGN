using Terraria;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public abstract class RiochetProjectile : ModProjectile
    {

        
        
        
        
        public float RiochetSpeed = 16;                   
        public float Gravity = 0;
        public float TerminalVelocity = 16;
        public override void SetDefaults()
        {

            
            projectile.aiStyle = -1;
            projectile.friendly = true;
            
            projectile.penetrate = -1;
            
            SafeSetDefaults();
            
        }
        public virtual void SafeSetDefaults()
        { }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                if (Gravity > 0)
                { projectile.velocity.Y += Gravity; }
                if (projectile.velocity.Y > TerminalVelocity) { projectile.velocity.Y = TerminalVelocity; }
            }
            if (projectile.ai[0] == 1)
            {
                int target = Target();
                
                if (target == -1)
                { projectile.ai[0] = 0; }
                else { Move(target); }


            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {

            projectile.ai[0] = 1;
            projectile.localAI[0] = target.whoAmI;
            projectile.tileCollide = false;
            projectile.ai[1] = 1;
            NPCHitEffects(target, damage, knockBack, crit);
        }
        public virtual void NPCHitEffects(NPC target, int damage, float knockBack, bool crit)
        { }

        private void Move(int target)
        {

           
            Vector2 moveTo = Main.npc[target].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > RiochetSpeed)
            {
                moveVel *= RiochetSpeed / magnitude;


            }
            else
            {
                projectile.ai[1] = 1;
                projectile.localAI[0] = target;

            }
            projectile.velocity = moveVel;

        }








        private int Target()
        {
            int target = -1;
            int targetMag = 400;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    
                    float DistanceProjtoNpc = Vector2.Distance(projectile.Center, Main.npc[whichNpc].Center);
                    if (DistanceProjtoNpc < targetMag && whichNpc != (int)projectile.localAI[0])
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }
            projectile.ai[1] = 0;
            return target;


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }






    }
}
