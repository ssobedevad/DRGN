using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;
using System.IO;

namespace DRGN.Projectiles.Reaper
{
    public abstract class ReaperScytheThrown : ModProjectile
    {
        public float RetractSpeed;        
        public float OutTime;     
        public virtual void SSD()
        { }
        public override void SetDefaults()
        {
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;           
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;            
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            SSD();
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);

        }             
        public override void AI()
        {                               
            projectile.rotation += 0.3f;
            if (projectile.ai[1] > OutTime * 0.65f && projectile.ai[1] < OutTime)
            { projectile.velocity *= 0.95f; }
            if (projectile.ai[1] < OutTime)
            {
                projectile.ai[1] += 1;
            }
            else
            {               
                Move();
            }
        }        
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
          
            if (Main.rand.Next(1, 100) < projectile.ai[0])
            { crit = true; }
             target.AddBuff(mod.BuffType("MarkedForDeath"), 45);
            if (crit) { target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target, 1, Main.player[projectile.owner]); }
            projectile.ai[1] = OutTime;
            projectile.damage = (int)(projectile.damage * 0.9f);
        }
        private void Move()
        {
            Vector2 moveTo = Main.player[projectile.owner].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (Vector2.Distance(projectile.Center, moveTo) > RetractSpeed * 2.5f)
            {
                moveVel *= RetractSpeed * 2.5f / magnitude;

                projectile.velocity = moveVel;
            }
            else { projectile.Kill(); }

        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D text = ModContent.GetTexture(Texture);
            spriteBatch.Draw(text, projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, text.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            return false;
        }

    }
}
