using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public class ReaperKnifeThrown : ModProjectile
    {

        private float RetractSpeed;
        public Texture2D projectileTexture;
        public ModItem ownerItem;
        public int critChance;


        public override void SetDefaults()
        {

            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            RetractSpeed = projectile.velocity.Length();                      
        }

        public override void AI()
        {
            if (projectile.localAI[1] == 0)
            { Init(); projectile.localAI[1] = 1; }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.ai[0] += 1;
            if(projectile.ai[0] > RetractSpeed * 2)
            { projectile.velocity.Y += 0.1f; projectile.velocity.X *= 0.98f; }
            projectile.velocity.Y += 0.01f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {            
            ownerItem.OnHitNPC(Main.player[projectile.owner], target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(1, 100) < critChance)
            { crit = true; }
            target.AddBuff(mod.BuffType("MarkedForDeath"), 45);
            if (crit) { target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target, 1, Main.player[projectile.owner]); }
        }
        
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {



            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, projectileTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);

        }

        


    }
}
