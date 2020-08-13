using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public class ReaperKnife : ModProjectile
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
            

            projectile.tileCollide = false;
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            
            RetractSpeed = projectile.velocity.Length();
            projectile.width = projectileTexture.Width;
            projectile.height = projectileTexture.Height;
            projectile.penetrate = (int)(RetractSpeed);
        }

        public override void AI()
        {
            if (projectile.localAI[1] == 0)
            { Init(); projectile.localAI[1] = 1; }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            
            int target = Target((int)projectile.ai[0], RetractSpeed * 40);

            if (target != -1)
            { Move(target); }
            else { projectile.Kill(); }


            
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
            projectile.localAI[0] = target.whoAmI;
            projectile.tileCollide = false;
            projectile.ai[1] = 1;
            ownerItem.OnHitNPC(Main.player[projectile.owner], target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(1, 100) < critChance)
            { crit = true; }
            if (target.HasBuff(mod.BuffType("MarkedForDeath")))
            {
                Player player = Main.player[projectile.owner];
                int projid = Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("DeathMark"), 0, 0, projectile.owner);
                Main.projectile[projid].Center = target.Center;
                target.DelBuff(target.FindBuffIndex(mod.BuffType("MarkedForDeath"))); CombatText.NewText(target.getRect(), Color.Purple, damage, true); if (target.CanBeChasedBy(this)) { target.StrikeNPCNoInteraction(damage, 0, 0, true, true); }
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
                target.GetGlobalNPC<ReaperGlobalNPC>().AddSoulReward(target, 2, player);
            }
        }
        private void Move(int target)
        {


            Vector2 moveTo = Main.npc[target].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > RetractSpeed)
            {
                moveVel *= RetractSpeed / magnitude;


            }
            else
            {
                projectile.ai[1] = 1;
                projectile.localAI[0] = target;

            }
            projectile.velocity = moveVel;

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {



            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);

        }

        private int Target(int favouredTarget = -1 , float targetMag = 400)
        {
            if(favouredTarget != -1)
            {
                if (Main.npc[favouredTarget].CanBeChasedBy(this, false))
                {
                    
                    if (favouredTarget != (int)projectile.localAI[0])
                    {
                        return favouredTarget;

                    }
                }

            }
            int target = -1;
           
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {

                    float DistanceProjtoNpc = Vector2.Distance(projectile.Center, Main.npc[whichNpc].Center);
                    if (DistanceProjtoNpc < targetMag && whichNpc != (int)projectile.localAI[0])
                    {
                        targetMag = DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }
            projectile.ai[1] = 0;
            if(target == -1)
            {
                if (Main.npc[favouredTarget].CanBeChasedBy(this, false))
                {
                   
                   
                        return favouredTarget;

                    
                }
            }
            return target;


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }


    }
}
