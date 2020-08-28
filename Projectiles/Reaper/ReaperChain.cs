using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;
using DRGN.Items.Weapons.ReaperWeapons;

namespace DRGN.Projectiles.Reaper
{
    public abstract class ReaperChain : ModProjectile
    {

        public float RetractSpeed;
        public int OutTime;
        public float range;        
        private Vector2 npcOffset;
        public int baseDamage;
        public string ChainTexturePath;
        public virtual void SSD()
        { }
        public override void SetDefaults()
        {
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ai[1] = 0;
            projectile.localAI[0] = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;           
            SSD();            
        }       
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead)
            { projectile.timeLeft = 2; }                    
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);            
            if (projectile.ai[1] <= OutTime)
            {
                projectile.ai[1] += 1;
            }
            else if (projectile.localAI[0] <= -1)
            {
                move();
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
            }
            if(projectile.localAI[0] > -1)
            { 
                if (Main.npc[(int)projectile.localAI[0]].active && Vector2.Distance(projectile.Center, player.Center) < range) 
                { 
                    projectile.Center = Main.npc[(int)projectile.localAI[0]].Center - npcOffset;
                    projectile.rotation = Vector2.Normalize(player.Center - projectile.Center).ToRotation() + MathHelper.ToRadians(270f); 
                } 
                else
                {                   
                    projectile.localAI[0] = -1;
                } 
            }
        
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {          
            if (Main.rand.Next(1, 100) < projectile.ai[0])
            { crit = true; }
            Player player = Main.player[projectile.owner];
                
            if (target.HasBuff(mod.BuffType("MarkedForDeath")))
            {                
                int projid = Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("DeathMark"), 0, 0, projectile.owner);
                Main.projectile[projid].Center = target.Center;
                target.DelBuff(target.FindBuffIndex(mod.BuffType("MarkedForDeath"))); CombatText.NewText(target.getRect(), Color.Purple, damage, true); 
                if (target.CanBeChasedBy(this)) 
                { 
                    target.StrikeNPCNoInteraction(damage, 0, 0, true, true);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, target.whoAmI, damage, 0, player.direction, crit ? 1 : 0);
                    }
                }
                int healing = (int)(RetractSpeed * (DRGNModWorld.MentalMode ? 1f : Main.expertMode ? 0.75f : 0.5f)) + (int)(damage * (DRGNModWorld.MentalMode ? 0.05f : Main.expertMode ? 0.0375f : 0.025f));
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
            }
            else
            {
                target.AddBuff(mod.BuffType("MarkedForDeath"), 45);
            }
            if (projectile.localAI[0] == -1)
            {                
                baseDamage = projectile.damage;
                projectile.ai[1] = OutTime;
                projectile.damage = 0;
                projectile.knockBack = 0;
                projectile.localAI[0] = target.whoAmI;
                npcOffset = target.Center - projectile.Center;
            }
        }
        private void move()
        {
            float Speed = RetractSpeed;
            if(projectile.localAI[0] == -2) { Speed *= 2; }
            Vector2 moveTo = Main.player[projectile.owner].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (Vector2.Distance(projectile.Center, moveTo) > Speed)
            {
                moveVel *= Speed / magnitude;

                projectile.velocity = moveVel;
            }
            else {  projectile.Kill(); }

        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            DrawChain(ModContent.GetTexture(ChainTexturePath) , spriteBatch);
            Texture2D text = ModContent.GetTexture(Texture);
            spriteBatch.Draw(text, projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, text.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            return false;
        }
        private void DrawChain(Texture2D chainTexture , SpriteBatch sb)
        {
            Player player = Main.player[projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            var drawPosition = projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;
            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;
            while (true)
            {
                float length = remainingVectorToPlayer.Length();
                if (length < 25f || float.IsNaN(length))
                    break;
                drawPosition += remainingVectorToPlayer * 12 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                sb.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
            }
        }

    }
}
