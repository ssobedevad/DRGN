using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;
using System.IO;
using Terraria.ModLoader.IO;

namespace DRGN.Projectiles.Reaper
{
    public abstract class ReaperScythe : ModProjectile
    {

        public float RetractSpeed;
        public float OutTime;
        public virtual void SSD()
        { }
        public override void SetDefaults()
        {
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ai[1] = 0;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            SSD();
        }                
        public override void AI()
        {          
            if(Main.player[projectile.owner].active && !Main.player[projectile.owner].dead)
            { projectile.timeLeft = 2; }           
            if (projectile.velocity.Length() > RetractSpeed * 2f) { projectile.velocity = Vector2.Normalize(projectile.velocity) * RetractSpeed * 2f; }
            projectile.rotation += 0.3f;
            if (projectile.ai[1] > OutTime * 0.65f && projectile.ai[1] < OutTime)
            { projectile.velocity *= 0.95f; }
            if (projectile.ai[1] <= OutTime)
            {
                projectile.ai[1] += 1;             
            }            
            else
            {
                move();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(Main.rand.Next(1,100) < projectile.ai[0])
            { crit = true; }
            if (target.HasBuff(mod.BuffType("MarkedForDeath")))
            {
                Player player = Main.player[projectile.owner];
                int projid = Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("DeathMark"), 0, 0, projectile.owner);
                Main.projectile[projid].Center = target.Center;           
                target.DelBuff(target.FindBuffIndex(mod.BuffType("MarkedForDeath"))); CombatText.NewText(target.getRect(), Color.Purple, damage, true); if (target.CanBeChasedBy(this)) { target.StrikeNPCNoInteraction(damage, 0, 0, true, true);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, target.whoAmI , damage, 0, player.direction, crit ? 1 : 0);
                    }
                }
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
                
                target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target,3,player); 
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
                
                projectile.velocity = projectile.velocity + (moveVel/20f) ;
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
