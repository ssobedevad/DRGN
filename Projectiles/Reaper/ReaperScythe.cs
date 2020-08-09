using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public class ReaperScythe : ModProjectile
    {

        private float RetractSpeed;
        public Texture2D projectileTexture;
        public ModItem ownerItem;
        public float outTime;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ai[0] = 0;

            projectile.tileCollide = false;
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            outTime = projectile.ai[0];
            RetractSpeed = projectile.velocity.Length();
            projectile.width = projectileTexture.Width;
            projectile.height = projectileTexture.Height;
        }

        public override void AI()
        {
          
            if(Main.player[projectile.owner].active && !Main.player[projectile.owner].dead)
            { projectile.timeLeft = 2; }
           if(projectile.localAI[0] ==0)
            { Init(); projectile.localAI[0] = 1; }
            if (projectile.velocity.Length() > RetractSpeed * 2f) { projectile.velocity = Vector2.Normalize(projectile.velocity) * RetractSpeed * 2f; }

            projectile.rotation += 0.3f;
            if (projectile.ai[0] > 0 && projectile.ai[0] < outTime * 0.65f)
            { projectile.velocity *= 0.95f; }
            if (projectile.ai[0] >= 0)
            {
                projectile.ai[0] -= 1;
               
            }
            
            else
            {
                move();




            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ownerItem.OnHitNPC(Main.player[projectile.owner], target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(Main.rand.Next(1,100) < projectile.ai[1])
            { crit = true; }
            if (target.HasBuff(mod.BuffType("MarkedForDeath")))
            {
                Player player = Main.player[projectile.owner];
                int projid = Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("DeathMark"), 0, 0, projectile.owner);
                Main.projectile[projid].Center = target.Center;           
                target.DelBuff(target.FindBuffIndex(mod.BuffType("MarkedForDeath"))); CombatText.NewText(target.getRect(), Color.Purple, damage, true); if (target.CanBeChasedBy(this)) { target.StrikeNPCNoInteraction(damage, 0, 0, true, true);  }
                int healing = (int)(RetractSpeed * (DRGNModWorld.MentalMode ? 3f : Main.expertMode ? 2.25f : 1.5f)) + (int)(damage * (DRGNModWorld.MentalMode ? 0.05f : Main.expertMode ? 0.0375f : 0.025f));
                if (player.statLifeMax2 > player.statLife + healing)
                {
                    player.HealEffect(healing);
                    player.statLife += healing;


                }
                else if (player.statLife != player.statLifeMax2)
                {
                    player.HealEffect(player.statLifeMax2 - player.statLife);
                    player.statLife = player.statLifeMax2;
                }
                if (target.boss)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(target.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), ReaperPlayer.getSoulDamage(), 0, projectile.owner);
                    }
                }
                else { target.GetGlobalNPC<ReaperGlobalNPC>().soulReward += 3; }
            }
        }
        private void move()
        {

            
            Vector2 moveTo = Main.player[projectile.owner].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (Vector2.Distance(projectile.Center,moveTo) > RetractSpeed)
            {
                moveVel *= RetractSpeed / magnitude;
                
                projectile.velocity = projectile.velocity + (moveVel/15f) ;
            }
            else { projectile.Kill(); }

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {

           

            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation,  new Vector2(projectile.width/2, projectile.height/2), 1f, SpriteEffects.None, 0f);
           
        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }


    }
}
