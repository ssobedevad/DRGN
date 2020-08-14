using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public class ReaperScytheThrown : ModProjectile
    {

        private float RetractSpeed;
        public Texture2D projectileTexture;
        public ModItem ownerItem;
        public float outTime;
        public int critChance;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ai[0] = 0;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;            
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            outTime = projectile.ai[0] / 2f;
            projectile.ai[0] = outTime;
            RetractSpeed = projectile.velocity.Length();
            projectile.width = projectileTexture.Width;
            projectile.height = projectileTexture.Height;
        }

        public override void AI()
        {                       
            if (projectile.localAI[0] == 0)
            { Init(); projectile.localAI[0] = 1; }
            projectile.rotation += 0.3f;
            if (projectile.ai[0] > 0 && projectile.ai[0] < outTime * 0.45f)
            { projectile.velocity *= 0.95f; }
            if (projectile.ai[0] >= 0)
            {
                projectile.ai[0] -= 1;
            }
            else
            {               
                Move();
            }
        }        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = 0;
            projectile.damage = (int)(projectile.damage * 0.9f);
            ownerItem.OnHitNPC(Main.player[projectile.owner], target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(1, 100) < critChance)
            { crit = true; }
             target.AddBuff(mod.BuffType("MarkedForDeath"), 45);
            if (crit) { target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target, 1, Main.player[projectile.owner]); }
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
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {



            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, projectileTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f) ;

        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

       
    }
}
