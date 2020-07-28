using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN
{
    public class FlailsAI : GlobalProjectile
    {
        public static List<int> projectilesToDrawShadowTrails = new List<int>();
        public override bool InstancePerEntity => true;
        public Vector2[] oldPos = new Vector2[9] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, };
        public int combatText = -1;
        public int baseDamage;
        public float baseKnockBack;
        public Texture2D ChainTexture;
        public float charge;
        private float ChargeTime;
        public float ChargeTimeReduction = 0;
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.aiStyle == 15)
            {
                if (!projectilesToDrawShadowTrails.Contains(projectile.type))

                {
                    projectilesToDrawShadowTrails.Add(projectile.type);
                }
                if (projectile.type == ProjectileID.BallOHurt)
                { ChainTexture = Main.chain2Texture; }
                else if (projectile.type == ProjectileID.TheMeatball)
                { ChainTexture = Main.chain13Texture; }
                else if (projectile.type == ProjectileID.Sunfury)
                { ChainTexture = Main.chain6Texture; }
                else if (projectile.type == ProjectileID.TheDaoofPow)
                { ChainTexture = Main.chain7Texture; }
                else if (projectile.type == ProjectileID.FlowerPow)
                { ChainTexture = Main.chain19Texture; }

            }

        }
        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.aiStyle == 15)
            {
                Main.player[projectile.owner].itemAnimation = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == projectile.owner && Main.projectile[i].aiStyle == 15 && i != projectile.whoAmI)
                    {

                        Main.projectile[i].active = false;


                    }
                }
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if (baseDamage == 0)
            {
                baseDamage = projectile.damage;
                baseKnockBack = projectile.knockBack;
            }

            for (int i = 8; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = projectile.Center; }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }



                if (oldPos[i] == Vector2.Zero) { oldPos[i] = projectile.Center; }

            }
            if (projectile.aiStyle == 15)
            {


                Player player = Main.player[projectile.owner];
                player.itemAnimation = 1;
                if (!player.active || player.dead || player.noItems || player.CCed || Vector2.Distance(projectile.Center, player.Center) > 900f)
                {
                    projectile.Kill();
                    return false;
                }
                if (Main.myPlayer == projectile.owner && Main.mapFullscreen)
                {
                    projectile.Kill();
                    return false;
                }
                if (projectile.type == 948 && projectile.wet && !projectile.lavaWet)
                {
                    projectile.type = 947;
                    projectile.netUpdate = true;
                }
                Vector2 mountedCenter = player.MountedCenter;
                bool doFastThrowDust = false;
                bool flag = true;
                bool OwnerHitCheck = false;
                int rangeMult = 10;
                float speed = 24f;
                float AbsoluteMaxRange = 800f;
                float topspeed = 3f;
                float minPlayerDist = 16f;
                float topspeed2 = 8f;
                float minPlayerDist2 = 48f;
                float topspeed3 = 1f;
                float velocityMax = 14f;
                int num10 = 60;
                int num11 = 10;
                int npcImmunity = 20;
                int num13 = 10;
                int num14 = rangeMult + 5;
                Vector2[] rangeMultActual = DRGN.FlailsRangeMult.ToArray();
                Vector2[] TopSpeedActual = DRGN.FlailsTopSpeed.ToArray();
                Vector2[] NPCImmunityActual = DRGN.FlailsNPCImmunity.ToArray();
                Vector3[] MinPlayerDists = DRGN.FlailsMinPlayerDists.ToArray();

                int ThisNum = 1;
                int TotalProjs = 1;

                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (i < rangeMultActual.Length)
                    {
                        if (rangeMultActual[i].X == projectile.type)
                        { rangeMult = (int)rangeMultActual[i].Y; }
                    }
                    if (i < TopSpeedActual.Length)
                    {
                        if (TopSpeedActual[i].X == projectile.type)
                        { speed = TopSpeedActual[i].Y; }
                    }
                    if (i < NPCImmunityActual.Length)
                    {
                        if (NPCImmunityActual[i].X == projectile.type)

                        { npcImmunity = (int)NPCImmunityActual[i].Y; }
                    }
                    if (i < MinPlayerDists.Length)
                    {
                        if (MinPlayerDists[i].X == projectile.type)
                        { minPlayerDist = MinPlayerDists[i].Y; minPlayerDist2 = MinPlayerDists[i].Z; }
                    }

                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 15 && i != projectile.whoAmI)
                    {
                        if (i < projectile.whoAmI)
                        {
                            ThisNum += 1;

                        }
                        TotalProjs += 1;
                       

                    }
                    


                }
                if (ThisNum == 1 && player.GetModPlayer<DRGNPlayer>().maxFlails > TotalProjs) { Projectile.NewProjectile(player.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner); }
                float meleeSpeed = player.meleeSpeed;
                float speedMult = 1f / meleeSpeed;
                speed *= speedMult;
                topspeed3 *= speedMult;
                velocityMax *= speedMult;
                topspeed *= speedMult;
                minPlayerDist *= speedMult;
                topspeed2 *= speedMult;
                minPlayerDist2 *= speedMult;
                float RangeMulti = speed * (float)rangeMult;
                float maxRange = RangeMulti + 160f;
                projectile.localNPCHitCooldown = num11;
                charge = projectile.localAI[1];
                ChargeTime = player.HeldItem.useTime * 3;
                ChargeTime -= ChargeTimeReduction;
                if(ChargeTime < 1) { ChargeTime = 1; }
                if (charge > ChargeTime) { charge = ChargeTime; if (combatText == -1 && ThisNum == 1) { combatText = CombatText.NewText(player.getRect(), Color.White, "Max Charge"); } }
                charge /= ChargeTime;
                switch ((int)projectile.ai[0])
                {
                    case 0:
                        {
                            projectile.tileCollide = false;
                            projectile.damage = baseDamage / 2;
                            projectile.knockBack = baseKnockBack / 2;
                            OwnerHitCheck = true;
                            if (projectile.owner == Main.myPlayer)
                            {
                                Vector2 origin = projectile.Center;
                                Vector2 mouseWorld = Main.MouseWorld;
                                Vector2 DesiredDirection = Vector2.Normalize(mouseWorld - origin).SafeNormalize(Vector2.UnitX * player.direction);
                                player.ChangeDir((DesiredDirection.X > 0f) ? 1 : (-1));
                                if (!player.channel)
                                {
                                    if (charge < 0.5f) { charge = 0.5f; }
                                    if (charge == 1f) { charge = 1.5f; }
                                    projectile.damage = (int)(baseDamage * charge);
                                    projectile.knockBack = baseKnockBack * charge;
                                    projectile.ai[0] = 1f;
                                    projectile.ai[1] = 0f;
                                    projectile.velocity = DesiredDirection * speed * charge + player.velocity;
                                    
                                    projectile.netUpdate = true;
                                    
                                    projectile.localNPCHitCooldown = num13;
                                    break;
                                }
                            }
                           
                            projectile.localAI[1] += 0.38f + (0.62f * charge);
                            Vector2 offset = new Vector2(player.direction).RotatedBy((float)Math.PI*2*ThisNum/TotalProjs+((float)Math.PI * 10f * (projectile.localAI[1] / 60f) * (float)player.direction));
                            offset.Y *= 0.8f;
                            if (offset.Y * player.gravDir > 0f)
                            {
                                offset.Y *= 0.5f;
                            }
                            projectile.Center = mountedCenter + offset  * (20 + 10*player.GetModPlayer<DRGNPlayer>().maxFlails);
                            projectile.velocity = Vector2.Zero;
                            projectile.localNPCHitCooldown = npcImmunity;
                            break;
                        }
                    case 1:
                        {
                            projectile.tileCollide = false;
                            if (charge < 0.5f) { charge = 0.5f; }
                            if (charge == 1f) { charge = 1.5f; }
                            doFastThrowDust = true;
                            bool maxRangeReached = projectile.ai[1]++ >= (float)rangeMult * charge;
                            maxRangeReached |= (projectile.Distance(mountedCenter) >= AbsoluteMaxRange);
                            if (player.controlUseItem)
                            {
                                projectile.ai[0] = 6f;
                                projectile.ai[1] = 0f;
                                projectile.netUpdate = true;
                                projectile.velocity *= 0.2f;
                                if (Main.myPlayer == projectile.owner && projectile.type == 757)
                                {
                                    Projectile.NewProjectile(projectile.Center, projectile.velocity, 928, projectile.damage, projectile.knockBack, Main.myPlayer);
                                }
                                break;
                            }
                            if (maxRangeReached)
                            {
                                projectile.ai[0] = 2f;
                                projectile.ai[1] = 0f;
                                projectile.netUpdate = true;
                                projectile.velocity *= 0.3f;
                                if (Main.myPlayer == projectile.owner && projectile.type == 757)
                                {
                                    Projectile.NewProjectile(projectile.Center, projectile.velocity, 928, projectile.damage, projectile.knockBack, Main.myPlayer);
                                }
                            }
                            player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                            projectile.localNPCHitCooldown = num13;
                            break;
                        }
                    case 2:
                        {
                            projectile.tileCollide = false;
                            Vector2 value2 = projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                            if (projectile.Distance(mountedCenter) <= minPlayerDist)
                            {
                                projectile.Kill();
                                return false;
                            }
                            if (player.controlUseItem)
                            {
                                projectile.ai[0] = 6f;
                                projectile.ai[1] = 0f;
                                projectile.netUpdate = true;
                                projectile.velocity *= 0.2f;
                            }
                            else
                            {
                                projectile.velocity *= 0.98f;
                                projectile.velocity = MoveToWards(projectile.velocity, value2 * minPlayerDist, topspeed);
                                player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                            }
                            break;
                        }
                    case 3:
                        {
                            projectile.tileCollide = false;
                            if (!player.controlUseItem)
                            {
                                projectile.ai[0] = 4f;
                                projectile.ai[1] = 0f;
                                projectile.netUpdate = true;
                                break;
                            }
                            float num18 = projectile.Distance(mountedCenter);
                            
                            if (num18 > (float)num10)
                            {
                                if (num18 >= RangeMulti)
                                {
                                    projectile.velocity *= 0.5f;
                                    projectile.velocity = MoveToWards(projectile.velocity, projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * velocityMax, velocityMax);
                                }
                                projectile.velocity *= 0.98f;
                                projectile.velocity = MoveToWards(projectile.velocity, projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * velocityMax, topspeed3);
                            }
                            else
                            {
                                if (projectile.velocity.Length() < 6f)
                                {
                                    projectile.velocity.X *= 0.96f;
                                    projectile.velocity.Y += 0.2f;
                                }
                                if (player.velocity.X == 0f)
                                {
                                    projectile.velocity.X *= 0.96f;
                                }
                            }
                            player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                            break;
                        }
                    case 4://try return and kill
                        {
                            projectile.tileCollide = false;
                            Vector2 vector = projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                            if (projectile.Distance(mountedCenter) <= minPlayerDist2)
                            {
                                projectile.Kill();
                                return false;
                            }
                            projectile.velocity *= 0.98f;
                            projectile.velocity = MoveToWards(projectile.velocity, vector * minPlayerDist2, topspeed2);
                            Vector2 target = projectile.Center + projectile.velocity;
                            Vector2 value = Vector2.Normalize(mountedCenter - target).SafeNormalize(Vector2.Zero);
                            if (Vector2.Dot(vector, value) < 0f)
                            {
                                projectile.Kill();
                                return false;
                            }
                            player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                            break;
                        }
                    case 5://drop to ground
                        projectile.tileCollide = true;
                        if (projectile.ai[1]++ >= (float)num14)
                        {
                            projectile.ai[0] = 6f;
                            projectile.ai[1] = 0f;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            projectile.localNPCHitCooldown = num13;
                            projectile.velocity.Y += 0.6f;
                            projectile.velocity.X *= 0.95f;
                            player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                        }
                        break;
                    case 6://drop on ground
                        projectile.tileCollide = true;
                        if (!player.controlUseItem || projectile.Distance(mountedCenter) > maxRange)
                        {
                            projectile.ai[0] = 4f;
                            projectile.ai[1] = 0f;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            projectile.velocity.Y += 0.8f;
                            projectile.velocity.X *= 0.95f;
                            player.ChangeDir((player.Center.X < projectile.Center.X) ? 1 : (-1));
                        }
                        break;
                }
                int num19 = projectile.type;
                if (num19 == 247)
                {
                    flag = false;
                    float num20 = (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
                    projectile.rotation += ((projectile.velocity.X > 0f) ? num20 : (0f - num20));
                    if (projectile.ai[0] == 0f)
                    {
                        projectile.rotation += (float)Math.PI * 2f / 15f * (float)player.direction;
                    }
                    float num21 = 600f;
                    NPC nPC = null;
                    if (projectile.owner == Main.myPlayer)
                    {
                        projectile.localAI[0] += 1f;
                        if (projectile.localAI[0] >= 20f)
                        {
                            projectile.localAI[0] = 17f;
                            for (int i = 0; i < 200; i++)
                            {
                                NPC nPC2 = Main.npc[i];
                                if (nPC2.CanBeChasedBy(this))
                                {
                                    float num22 = projectile.Distance(nPC2.Center);
                                    if (!(num22 >= num21) && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                                    {
                                        nPC = nPC2;
                                        num21 = num22;
                                    }
                                }
                            }
                        }
                        if (nPC != null)
                        {
                            projectile.localAI[0] = 0f;
                            float scaleFactor = 14f;
                            Vector2 center = projectile.Center;
                            Vector2 velocity = Vector2.Normalize(nPC.Center - center) * scaleFactor;
                            Projectile.NewProjectile(center, velocity, ProjectileID.FlowerPowPetal, (int)((double)projectile.damage / 1.5), projectile.knockBack / 2f, Main.myPlayer);
                        }
                    }
                }
                projectile.direction = ((projectile.velocity.X > 0f) ? 1 : (-1));
                projectile.spriteDirection = projectile.direction;
                projectile.ownerHitCheck = OwnerHitCheck;
                if (flag)
                {
                    if (projectile.velocity.Length() > 1f)
                    {
                        projectile.rotation = projectile.velocity.ToRotation() + projectile.velocity.X * 0.1f;
                    }
                    else
                    {
                        projectile.rotation += projectile.velocity.X * 0.1f;
                    }
                }
                projectile.timeLeft = 2;
                player.heldProj = projectile.whoAmI;

                player.itemRotation = projectile.DirectionFrom(mountedCenter).ToRotation();
                if (projectile.Center.X < mountedCenter.X)
                {
                    player.itemRotation += (float)Math.PI;
                }
                player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
                FlailDust(doFastThrowDust, projectile);





















            }
            else
            {
                return true;
            }
            return false;
        }
        private void FlailDust(bool doFastThrowDust, Projectile projectile)
        {
            if (projectile.type == 25)
            {
                int maxValue = 15;
                if (doFastThrowDust)
                {
                    maxValue = 1;
                }
                if (Main.rand.Next(maxValue) == 0)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, 0f, 0f, 150, default(Color), 1.3f);
                }
            }
            else if (projectile.type == 757)
            {
                int num = 4;
                if (projectile.velocity.Length() < 8f)
                {
                    num = 10;
                }
                if (doFastThrowDust)
                {
                    num /= 2;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (Main.rand.Next(num) == 0)
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 5, 0f, 0f, 0, default(Color), 0.8f);
                        dust.velocity += projectile.velocity / 4f;
                        dust.fadeIn = 1.3f;
                    }
                }
                num = 40;
                if (doFastThrowDust)
                {
                    num /= 2;
                }
                for (float num2 = 0f; num2 < 1f; num2 += 0.1f)
                {
                    if (Main.rand.Next(num) == 0)
                    {
                        Dust.NewDustDirect(Vector2.Lerp(Main.player[projectile.owner].Center, projectile.Center, Main.rand.NextFloat()) + new Vector2(-8f), 16, 16, 5, 0f, 0f, 0, default(Color), 1.3f).velocity += projectile.velocity / 4f;
                    }
                }
            }
            else if (projectile.type == 26)
            {
                int num3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.5f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].velocity.X /= 2f;
                Main.dust[num3].velocity.Y /= 2f;
            }
            else if (projectile.type == 948 && !projectile.wet)
            {
                int num4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.2f);
                Main.dust[num4].noGravity = true;
                Main.dust[num4].velocity.X *= 4f;
                Main.dust[num4].velocity.Y *= 4f;
                Main.dust[num4].velocity = (Main.dust[num4].velocity + projectile.velocity) / 2f;
            }
            else if (projectile.type == 35)
            {
                int num5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity.X *= 2f;
                Main.dust[num5].velocity.Y *= 2f;
            }
            else if (projectile.type == 154)
            {
                int num6 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 115, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 140, default(Color), 1.5f);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].velocity *= 0.25f;
            }
        }
        private static Vector2 MoveToWards(Vector2 currentPosition, Vector2 targetPosition, float maxAmountAllowedToMove)
        {
            Vector2 v = targetPosition - currentPosition;
            if (v.Length() < maxAmountAllowedToMove)
            {
                return targetPosition;
            }
            return currentPosition + v.SafeNormalize(Vector2.Zero) * maxAmountAllowedToMove;
        }
        public override bool PreDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {

            if (projectilesToDrawShadowTrails.Contains(projectile.type))
            {
                DoDrawShadowTrails(projectile, spriteBatch, lightColor);
                if (ChainTexture != null)
                { DoDrawFlailChains(projectile, spriteBatch, lightColor, ChainTexture); }
                return false;
            }
            return true;
        }
        public void DoDrawShadowTrails(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D text = Main.projectileTexture[projectile.type];
            Rectangle rect = new Rectangle(0, (text.Height / Main.projFrames[projectile.type]) * projectile.frame, text.Width, (text.Height / Main.projFrames[projectile.type]));
            Vector2 RotationCenter = new Vector2(text.Width, (text.Height / Main.projFrames[projectile.type])) / 2;
            for (int i = 4; i >= 0; i--)
            {
                Vector2 oldV = oldPos[i];
                Vector2 vect = oldV - Main.screenPosition;


                Color alpha9 = projectile.GetAlpha(lightColor);
                alpha9.R = (byte)(alpha9.R * (10 - (2 * i)) / 20);
                alpha9.G = (byte)(alpha9.G * (10 - (2 * i)) / 20);
                alpha9.B = (byte)(alpha9.B * (10 - (2 * i)) / 20);
                alpha9.A = (byte)(alpha9.A * (10 - (2 * i)) / 20);
                spriteBatch.Draw(
                   text,
                     vect, rect, alpha9, projectile.rotation, RotationCenter, projectile.scale, SpriteEffects.None, 0f);




            }
            Vector2 vect2 = projectile.Center - Main.screenPosition;

            spriteBatch.Draw(
                   Main.projectileTexture[projectile.type],
                     vect2, rect, lightColor, projectile.rotation, RotationCenter, projectile.scale, SpriteEffects.None, 0f);

        }
        public void DoDrawFlailChains(Projectile projectile, SpriteBatch spriteBatch, Color lightColor, Texture2D chainText)
        {

            var player = Main.player[projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;


            var drawPosition = projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (projectile.alpha == 0)
            {
                int direction = -1;

                if (projectile.Center.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }

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
                spriteBatch.Draw(chainText, drawPosition - Main.screenPosition, null, color, rotation, chainText.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
        }


    }

}