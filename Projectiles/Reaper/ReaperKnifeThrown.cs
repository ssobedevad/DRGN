using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public abstract class ReaperKnifeThrown : ModProjectile
    {
        private float RetractSpeed;
        public virtual void SSD()
        { }
        public override void SetDefaults()
        {
            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            SSD();
        }        
        public override void AI()
        {           
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.ai[1] += 1;
            if(projectile.ai[1] > 30)
            { projectile.velocity.Y += 0.1f; projectile.velocity.X *= 0.99f; }
            projectile.velocity.Y += 0.01f;
        }
        
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(1, 100) < projectile.ai[0])
            { crit = true; }
            target.AddBuff(mod.BuffType("MarkedForDeath"), 45);
            if (crit) { target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target, 1, Main.player[projectile.owner]); }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D text = ModContent.GetTexture(Texture);
            spriteBatch.Draw(text, projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, text.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            return false;
        }

    }
}
