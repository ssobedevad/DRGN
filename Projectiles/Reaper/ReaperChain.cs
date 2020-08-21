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
    public class ReaperChain : ModProjectile
    {

        private float RetractSpeed;
        public Texture2D projectileTexture;
        public Texture2D chainTexture;
        public ModItem ownerItem;
        public float outTime;
        public int stuckToNPC = -1;
        public Vector2 npcOffset;
        public int baseDamage;
        public int critChance;

        
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ai[0] = 0;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            outTime = projectile.ai[0];
            RetractSpeed = projectile.velocity.Length() * 2f;
            projectile.width = projectileTexture.Width;
            projectile.height = projectileTexture.Height;
            baseDamage = projectile.damage;
        }
        
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead)
            { projectile.timeLeft = 2; }
            if (projectile.localAI[0] == 0)
            { Init(); projectile.localAI[0] = 1; }
            

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            
            if (projectile.ai[0] >= 0)
            {
                projectile.ai[0] -= 1;

            }
            else if (stuckToNPC <= -1)
            {
                move();
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);



            }
            if(stuckToNPC > -1)
            { 
                if (Main.npc[stuckToNPC].active && Vector2.Distance(projectile.Center, player.Center) < RetractSpeed * (15 + outTime)) 
                { 
                    projectile.Center = Main.npc[stuckToNPC].Center - npcOffset;
                    projectile.rotation = Vector2.Normalize(player.Center - projectile.Center).ToRotation() + MathHelper.ToRadians(270f); 
                } 
                else
                { 
                    player.GetModPlayer<ReaperPlayer>().hookedTargets[stuckToNPC].RemoveThisProj(projectile.modProjectile as ReaperChain);
                    stuckToNPC = -1;
                } 
            }
        
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ownerItem.OnHitNPC(Main.player[projectile.owner], target, damage, knockback, crit);
            if(stuckToNPC == -1)
            {
                projectile.ai[0] = -1;
                projectile.damage = 0;
                projectile.knockBack = 0;
                stuckToNPC = target.whoAmI;
                npcOffset = target.Center - projectile.Center;


            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(1, 100) < critChance)
            { crit = true; }
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<ReaperPlayer>().hookedTargets.ContainsKey(target.whoAmI)){ player.GetModPlayer<ReaperPlayer>().hookedTargets[target.whoAmI].AddProjAndValues(projectile.modProjectile as ReaperChain, crit); }
            else { player.GetModPlayer<ReaperPlayer>().hookedTargets.Add(target.whoAmI, new HookedData(projectile.modProjectile as ReaperChain, target.whoAmI , crit));
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, target.whoAmI, damage, 0, player.direction, crit ? 1 : 0);
                }
            }
           
            if (target.HasBuff(mod.BuffType("MarkedForDeath")))
            {
                ReaperWeapon Rw = ownerItem as ReaperWeapon;
                int projid = Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("DeathMark"), 0, 0, projectile.owner);
                Main.projectile[projid].Center = target.Center;
                target.DelBuff(target.FindBuffIndex(mod.BuffType("MarkedForDeath"))); CombatText.NewText(target.getRect(), Color.Purple, damage, true); if (target.CanBeChasedBy(this)) { target.StrikeNPCNoInteraction(damage, 0, 0, true, true); }
                int healing = (int)(Rw.DashSpeed * (DRGNModWorld.MentalMode ? 1f : Main.expertMode ? 0.75f : 0.5f)) + (int)(damage * (DRGNModWorld.MentalMode ? 0.05f : Main.expertMode ? 0.0375f : 0.025f));
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
        }
        private void move()
        {

            float Speed = RetractSpeed;
            if(stuckToNPC == -2) { Speed *= 2; }
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
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {



            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);
            Player player = Main.player[projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;


            var drawPosition = projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 12 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
                spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, new Vector2(chainTexture.Width / 2, chainTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            }
        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }


    }
}
