using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeTriplets : GlobalNPC


    {



        public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
                if (npc.aiStyle == 30)
                {
                    if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                    {
                        npc.TargetClosest();
                    }
                    bool dead2 = Main.player[npc.target].dead;
                    float num396 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
                    float num397 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
                    float num398 = (float)Math.Atan2(num397, num396) + 1.57f;
                    if (num398 < 0f)
                    {
                        num398 += 6.283f;
                    }
                    else if ((double)num398 > 6.283)
                    {
                        num398 -= 6.283f;
                    }
                    float num399 = 0.1f;
                    if (npc.rotation < num398)
                    {
                        if ((double)(num398 - npc.rotation) > 3.1415)
                        {
                            npc.rotation -= num399;
                        }
                        else
                        {
                            npc.rotation += num399;
                        }
                    }
                    else if (npc.rotation > num398)
                    {
                        if ((double)(npc.rotation - num398) > 3.1415)
                        {
                            npc.rotation += num399;
                        }
                        else
                        {
                            npc.rotation -= num399;
                        }
                    }
                    if (npc.rotation > num398 - num399 && npc.rotation < num398 + num399)
                    {
                        npc.rotation = num398;
                    }
                    if (npc.rotation < 0f)
                    {
                        npc.rotation += 6.283f;
                    }
                    else if ((double)npc.rotation > 6.283)
                    {
                        npc.rotation -= 6.283f;
                    }
                    if (npc.rotation > num398 - num399 && npc.rotation < num398 + num399)
                    {
                        npc.rotation = num398;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        int num400 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
                        Main.dust[num400].velocity.X *= 0.5f;
                        Main.dust[num400].velocity.Y *= 0.1f;
                    }
                    if (Main.netMode != 1 && !Main.dayTime && !dead2 && npc.timeLeft < 10)
                    {
                        for (int num401 = 0; num401 < 200; num401++)
                        {
                            if (num401 != npc.whoAmI && Main.npc[num401].active && (Main.npc[num401].type == 125 || Main.npc[num401].type == 126))
                            {
                                npc.timeLeft = 6000;
                            }
                        }
                    }
                    if (Main.dayTime || dead2)
                    {
                        npc.velocity.Y -= 0.04f;
                        npc.timeLeft = 10;
                        return false;
                    }
                    if (npc.ai[0] == 0f)
                    {
                        if (npc.ai[1] == 0f)
                        {
                            float num402 = 7f;
                            float num403 = 0.1f;
                            if (Main.expertMode)
                            {
                                num402 = 8.25f;
                                num403 = 0.115f;
                            }
                            if (DRGNModWorld.MentalMode)
                            {
                                num402 *= 1.15f;
                                num403 *= 1.15f;
                            }
                            int num404 = 1;
                            if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                            {
                                num404 = -1;
                            }
                            Vector2 vector37 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num405 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num404 * 300) - vector37.X;
                            float num406 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - vector37.Y;
                            float num407 = (float)Math.Sqrt(num405 * num405 + num406 * num406);
                            float num408 = num407;
                            num407 = num402 / num407;
                            num405 *= num407;
                            num406 *= num407;
                            if (npc.velocity.X < num405)
                            {
                                npc.velocity.X += num403;
                                if (npc.velocity.X < 0f && num405 > 0f)
                                {
                                    npc.velocity.X += num403;
                                }
                            }
                            else if (npc.velocity.X > num405)
                            {
                                npc.velocity.X -= num403;
                                if (npc.velocity.X > 0f && num405 < 0f)
                                {
                                    npc.velocity.X -= num403;
                                }
                            }
                            if (npc.velocity.Y < num406)
                            {
                                npc.velocity.Y += num403;
                                if (npc.velocity.Y < 0f && num406 > 0f)
                                {
                                    npc.velocity.Y += num403;
                                }
                            }
                            else if (npc.velocity.Y > num406)
                            {
                                npc.velocity.Y -= num403;
                                if (npc.velocity.Y > 0f && num406 < 0f)
                                {
                                    npc.velocity.Y -= num403;
                                }
                            }
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 600f)
                            {
                                npc.ai[1] = 1f;
                                npc.ai[2] = 0f;
                                npc.ai[3] = 0f;
                                npc.target = 255;
                                npc.netUpdate = true;
                            }
                            else if (npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && num408 < 400f)
                            {
                                if (!Main.player[npc.target].dead)
                                {
                                    npc.ai[3] += 1f;
                                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.9)
                                    {
                                        npc.ai[3] += 0.3f;
                                    }
                                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.8)
                                    {
                                        npc.ai[3] += 0.3f;
                                    }
                                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.7)
                                    {
                                        npc.ai[3] += 0.3f;
                                    }
                                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.6)
                                    {
                                        npc.ai[3] += 0.3f;
                                    }
                                    if (DRGNModWorld.MentalMode)
                                    {
                                        npc.ai[3] += 0.5f;
                                    }
                                }
                                if (npc.ai[3] >= 60f)
                                {
                                    npc.ai[3] = 0f;
                                    vector37 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                    num405 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector37.X;
                                    num406 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector37.Y;
                                    if (Main.netMode != 1)
                                    {
                                        float num409 = 9f;
                                        int attackDamage_ForProjectiles3 = 25;
                                        int num410 = 83;
                                        if (Main.expertMode)
                                        {
                                            num409 = 10.5f;
                                        }
                                        num407 = (float)Math.Sqrt(num405 * num405 + num406 * num406);
                                        num407 = num409 / num407;
                                        num405 *= num407;
                                        num406 *= num407;
                                        num405 += (float)Main.rand.Next(-40, 41) * 0.08f;
                                        num406 += (float)Main.rand.Next(-40, 41) * 0.08f;
                                        vector37.X += num405 * 15f;
                                        vector37.Y += num406 * 15f;
                                        int num411 = Projectile.NewProjectile(vector37.X, vector37.Y, num405, num406, num410, attackDamage_ForProjectiles3, 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }
                        else if (npc.ai[1] == 1f)
                        {
                            npc.rotation = num398;
                            float num412 = 12f;
                            if (Main.expertMode)
                            {
                                num412 = 15f;
                            }
                            if (DRGNModWorld.MentalMode)
                            {
                                num412 += 2f;
                            }
                            Vector2 vector38 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num413 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector38.X;
                            float num414 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector38.Y;
                            float num415 = (float)Math.Sqrt(num413 * num413 + num414 * num414);
                            num415 = num412 / num415;
                            npc.velocity.X = num413 * num415;
                            npc.velocity.Y = num414 * num415;
                            npc.ai[1] = 2f;
                        }
                        else if (npc.ai[1] == 2f)
                        {
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 25f)
                            {
                                npc.velocity.X *= 0.96f;
                                npc.velocity.Y *= 0.96f;
                                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                                {
                                    npc.velocity.X = 0f;
                                }
                                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                                {
                                    npc.velocity.Y = 0f;
                                }
                            }
                            else
                            {
                                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                            }
                            if (npc.ai[2] >= 70f)
                            {
                                npc.ai[3] += 1f;
                                npc.ai[2] = 0f;
                                npc.target = 255;
                                npc.rotation = num398;
                                if (npc.ai[3] >= 4f)
                                {
                                    npc.ai[1] = 0f;
                                    npc.ai[3] = 0f;
                                }
                                else
                                {
                                    npc.ai[1] = 1f;
                                }
                            }
                        }
                        if ((double)npc.life < (double)npc.lifeMax)
                        {
                            npc.ai[0] = 1f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        return false;
                    }
                    if (npc.ai[0] == 1f || npc.ai[0] == 2f)
                    {
                        if (npc.ai[0] == 1f)
                        {
                            npc.ai[2] += 0.005f;
                            if ((double)npc.ai[2] > 0.5)
                            {
                                npc.ai[2] = 0.5f;
                            }
                        }
                        else
                        {
                            npc.ai[2] -= 0.005f;
                            if (npc.ai[2] < 0f)
                            {
                                npc.ai[2] = 0f;
                            }
                        }
                        npc.rotation += npc.ai[2];
                        npc.ai[1] += 1f;
                        if (npc.ai[1] == 100f)
                        {
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            if (npc.ai[0] == 3f)
                            {
                                npc.ai[2] = 0f;
                            }
                            else
                            {
                                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y);
                                for (int num416 = 0; num416 < 2; num416++)
                                {
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 143);
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
                                }
                                for (int num417 = 0; num417 < 20; num417++)
                                {
                                    Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                                }
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                        }
                        Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                        npc.velocity.X *= 0.98f;
                        npc.velocity.Y *= 0.98f;
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                        return false;
                    }
                    npc.damage = (int)((double)npc.defDamage * 1.5);
                    npc.defense = npc.defDefense + 10;
                    npc.HitSound = SoundID.NPCHit4;
                    if (npc.ai[1] == 0f)
                    {
                        float num418 = 8f;
                        float num419 = 0.15f;
                        if (Main.expertMode)
                        {
                            num418 = 9.5f;
                            num419 = 0.175f;
                        }
                        if (DRGNModWorld.MentalMode)
                        {
                            num418 *= 1.15f;
                            num419 *= 1.15f;
                        }
                        Vector2 vector39 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num420 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector39.X;
                        float num421 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - vector39.Y;
                        float num422 = (float)Math.Sqrt(num420 * num420 + num421 * num421);
                        num422 = num418 / num422;
                        num420 *= num422;
                        num421 *= num422;
                        if (npc.velocity.X < num420)
                        {
                            npc.velocity.X += num419;
                            if (npc.velocity.X < 0f && num420 > 0f)
                            {
                                npc.velocity.X += num419;
                            }
                        }
                        else if (npc.velocity.X > num420)
                        {
                            npc.velocity.X -= num419;
                            if (npc.velocity.X > 0f && num420 < 0f)
                            {
                                npc.velocity.X -= num419;
                            }
                        }
                        if (npc.velocity.Y < num421)
                        {
                            npc.velocity.Y += num419;
                            if (npc.velocity.Y < 0f && num421 > 0f)
                            {
                                npc.velocity.Y += num419;
                            }
                        }
                        else if (npc.velocity.Y > num421)
                        {
                            npc.velocity.Y -= num419;
                            if (npc.velocity.Y > 0f && num421 < 0f)
                            {
                                npc.velocity.Y -= num419;
                            }
                        }
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= 300f)
                        {
                            npc.ai[1] = 1f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.TargetClosest();
                            npc.netUpdate = true;
                        }
                        vector39 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        num420 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector39.X;
                        num421 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector39.Y;
                        npc.rotation = (float)Math.Atan2(num421, num420) - 1.57f;
                        if (Main.netMode == 1)
                        {
                            return false;
                        }
                        npc.localAI[1] += 1f;
                        if ((double)npc.life < (double)npc.lifeMax * 0.75)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.25)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.1)
                        {
                            npc.localAI[1] += 2f;
                        }
                        if (npc.localAI[1] > 180f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            npc.localAI[1] = 0f;
                            float num423 = 8.5f;
                            int attackDamage_ForProjectiles4 = 25;
                            int num424 = 100;
                            if (Main.expertMode)
                            {
                                num423 = 10f;
                            }
                            num422 = (float)Math.Sqrt(num420 * num420 + num421 * num421);
                            num422 = num423 / num422;
                            num420 *= num422;
                            num421 *= num422;
                            vector39.X += num420 * 15f;
                            vector39.Y += num421 * 15f;
                            int num425 = Projectile.NewProjectile(vector39.X, vector39.Y, num420, num421, num424, attackDamage_ForProjectiles4, 0f, Main.myPlayer);
                        }
                        return false;
                    }
                    int num426 = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        num426 = -1;
                    }
                    float num427 = 8f;
                    float num428 = 0.2f;
                    if (Main.expertMode)
                    {
                        num427 = 9.5f;
                        num428 = 0.25f;
                    }
                    if (DRGNModWorld.MentalMode)
                    {
                        num427 *= 1.15f;
                        num428 *= 1.15f;
                    }
                    Vector2 vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num429 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num426 * 340) - vector40.X;
                    float num430 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector40.Y;
                    float num431 = (float)Math.Sqrt(num429 * num429 + num430 * num430);
                    num431 = num427 / num431;
                    num429 *= num431;
                    num430 *= num431;
                    if (npc.velocity.X < num429)
                    {
                        npc.velocity.X += num428;
                        if (npc.velocity.X < 0f && num429 > 0f)
                        {
                            npc.velocity.X += num428;
                        }
                    }
                    else if (npc.velocity.X > num429)
                    {
                        npc.velocity.X -= num428;
                        if (npc.velocity.X > 0f && num429 < 0f)
                        {
                            npc.velocity.X -= num428;
                        }
                    }
                    if (npc.velocity.Y < num430)
                    {
                        npc.velocity.Y += num428;
                        if (npc.velocity.Y < 0f && num430 > 0f)
                        {
                            npc.velocity.Y += num428;
                        }
                    }
                    else if (npc.velocity.Y > num430)
                    {
                        npc.velocity.Y -= num428;
                        if (npc.velocity.Y > 0f && num430 < 0f)
                        {
                            npc.velocity.Y -= num428;
                        }
                    }
                    vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    num429 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector40.X;
                    num430 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector40.Y;
                    npc.rotation = (float)Math.Atan2(num430, num429) - 1.57f;
                    if (Main.netMode != 1)
                    {
                        npc.localAI[1] += 1f;
                        if ((double)npc.life < (double)npc.lifeMax * 0.75)
                        {
                            npc.localAI[1] += 0.5f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            npc.localAI[1] += 0.75f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.25)
                        {
                            npc.localAI[1] += 1f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.1)
                        {
                            npc.localAI[1] += 1.5f;
                        }
                        if (Main.expertMode)
                        {
                            npc.localAI[1] += 1.5f;
                        }
                        if (npc.localAI[1] > 60f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            npc.localAI[1] = 0f;
                            float num432 = 9f;
                            int attackDamage_ForProjectiles5 = 22;
                            int num433 = 100;
                            num431 = (float)Math.Sqrt(num429 * num429 + num430 * num430);
                            num431 = num432 / num431;
                            num429 *= num431;
                            num430 *= num431;
                            vector40.X += num429 * 15f;
                            vector40.Y += num430 * 15f;
                            int num434 = Projectile.NewProjectile(vector40.X, vector40.Y, num429, num430, num433, attackDamage_ForProjectiles5, 0f, Main.myPlayer);
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 180f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.TargetClosest();
                        npc.netUpdate = true;
                    }
                }
                else if (npc.aiStyle == 31)
                {
                    if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                    {
                        npc.TargetClosest();
                    }
                    bool dead3 = Main.player[npc.target].dead;
                    float num435 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
                    float num436 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
                    float num437 = (float)Math.Atan2(num436, num435) + 1.57f;
                    if (num437 < 0f)
                    {
                        num437 += 6.283f;
                    }
                    else if ((double)num437 > 6.283)
                    {
                        num437 -= 6.283f;
                    }
                    float num438 = 0.15f;
                    if (npc.rotation < num437)
                    {
                        if ((double)(num437 - npc.rotation) > 3.1415)
                        {
                            npc.rotation -= num438;
                        }
                        else
                        {
                            npc.rotation += num438;
                        }
                    }
                    else if (npc.rotation > num437)
                    {
                        if ((double)(npc.rotation - num437) > 3.1415)
                        {
                            npc.rotation += num438;
                        }
                        else
                        {
                            npc.rotation -= num438;
                        }
                    }
                    if (npc.rotation > num437 - num438 && npc.rotation < num437 + num438)
                    {
                        npc.rotation = num437;
                    }
                    if (npc.rotation < 0f)
                    {
                        npc.rotation += 6.283f;
                    }
                    else if ((double)npc.rotation > 6.283)
                    {
                        npc.rotation -= 6.283f;
                    }
                    if (npc.rotation > num437 - num438 && npc.rotation < num437 + num438)
                    {
                        npc.rotation = num437;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        int num439 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
                        Main.dust[num439].velocity.X *= 0.5f;
                        Main.dust[num439].velocity.Y *= 0.1f;
                    }
                    if (Main.netMode != 1 && !Main.dayTime && !dead3 && npc.timeLeft < 10)
                    {
                        for (int num440 = 0; num440 < 200; num440++)
                        {
                            if (num440 != npc.whoAmI && Main.npc[num440].active && (Main.npc[num440].type == 125 || Main.npc[num440].type == 126))
                            {
                                Main.npc[num440].timeLeft = 6000;
                            }
                        }
                    }
                    if (Main.dayTime || dead3)
                    {
                        npc.velocity.Y -= 0.04f;
                        npc.timeLeft = (10);
                        return false;
                    }
                    if (npc.ai[0] == 0f)
                    {
                        if (npc.ai[1] == 0f)
                        {
                            npc.TargetClosest();
                            float num441 = 12f;
                            float num442 = 0.4f;
                            if (DRGNModWorld.MentalMode)
                            {
                                num441 *= 1.15f;
                                num442 *= 1.15f;
                            }
                            int num443 = 1;
                            if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                            {
                                num443 = -1;
                            }
                            Vector2 vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num444 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num443 * 400) - vector41.X;
                            float num445 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
                            float num446 = (float)Math.Sqrt(num444 * num444 + num445 * num445);
                            float num447 = num446;
                            num446 = num441 / num446;
                            num444 *= num446;
                            num445 *= num446;
                            if (npc.velocity.X < num444)
                            {
                                npc.velocity.X += num442;
                                if (npc.velocity.X < 0f && num444 > 0f)
                                {
                                    npc.velocity.X += num442;
                                }
                            }
                            else if (npc.velocity.X > num444)
                            {
                                npc.velocity.X -= num442;
                                if (npc.velocity.X > 0f && num444 < 0f)
                                {
                                    npc.velocity.X -= num442;
                                }
                            }
                            if (npc.velocity.Y < num445)
                            {
                                npc.velocity.Y += num442;
                                if (npc.velocity.Y < 0f && num445 > 0f)
                                {
                                    npc.velocity.Y += num442;
                                }
                            }
                            else if (npc.velocity.Y > num445)
                            {
                                npc.velocity.Y -= num442;
                                if (npc.velocity.Y > 0f && num445 < 0f)
                                {
                                    npc.velocity.Y -= num442;
                                }
                            }
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 600f)
                            {
                                npc.ai[1] = 1f;
                                npc.ai[2] = 0f;
                                npc.ai[3] = 0f;
                                npc.target = 255;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                if (!Main.player[npc.target].dead)
                                {
                                    npc.ai[3] += 1f;
                                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.8)
                                    {
                                        npc.ai[3] += 0.6f;
                                    }
                                    if (DRGNModWorld.MentalMode)
                                    {
                                        npc.ai[3] += 0.4f;
                                    }
                                }
                                if (npc.ai[3] >= 60f)
                                {
                                    npc.ai[3] = 0f;
                                    vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                    num444 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector41.X;
                                    num445 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
                                    if (Main.netMode != 1)
                                    {
                                        float num448 = 12f;
                                        int attackDamage_ForProjectiles6 = 25;
                                        int num449 = 96;
                                        if (Main.expertMode)
                                        {
                                            num448 = 14f;
                                        }
                                        num446 = (float)Math.Sqrt(num444 * num444 + num445 * num445);
                                        num446 = num448 / num446;
                                        num444 *= num446;
                                        num445 *= num446;
                                        num444 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                        num445 += (float)Main.rand.Next(-40, 41) * 0.05f;
                                        vector41.X += num444 * 4f;
                                        vector41.Y += num445 * 4f;
                                        int num450 = Projectile.NewProjectile(vector41.X, vector41.Y, num444, num445, num449, attackDamage_ForProjectiles6, 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }
                        else if (npc.ai[1] == 1f)
                        {
                            npc.rotation = num437;
                            float num451 = 13f;
                            if (Main.expertMode)
                            {
                                if ((double)npc.life < (double)npc.lifeMax * 0.9)
                                {
                                    num451 += 0.5f;
                                }
                                if ((double)npc.life < (double)npc.lifeMax * 0.8)
                                {
                                    num451 += 0.5f;
                                }
                                if ((double)npc.life < (double)npc.lifeMax * 0.7)
                                {
                                    num451 += 0.55f;
                                }
                                if ((double)npc.life < (double)npc.lifeMax * 0.6)
                                {
                                    num451 += 0.6f;
                                }
                                if ((double)npc.life < (double)npc.lifeMax * 0.5)
                                {
                                    num451 += 0.65f;
                                }
                            }
                            if (DRGNModWorld.MentalMode)
                            {
                                num451 *= 1.2f;
                            }
                            Vector2 vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num452 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector42.X;
                            float num453 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector42.Y;
                            float num454 = (float)Math.Sqrt(num452 * num452 + num453 * num453);
                            num454 = num451 / num454;
                            npc.velocity.X = num452 * num454;
                            npc.velocity.Y = num453 * num454;
                            npc.ai[1] = 2f;
                        }
                        else if (npc.ai[1] == 2f)
                        {
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 8f)
                            {
                                npc.velocity.X *= 0.9f;
                                npc.velocity.Y *= 0.9f;
                                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                                {
                                    npc.velocity.X = 0f;
                                }
                                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                                {
                                    npc.velocity.Y = 0f;
                                }
                            }
                            else
                            {
                                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                            }
                            if (npc.ai[2] >= 42f)
                            {
                                npc.ai[3] += 1f;
                                npc.ai[2] = 0f;
                                npc.target = 255;
                                npc.rotation = num437;
                                if (npc.ai[3] >= 10f)
                                {
                                    npc.ai[1] = 0f;
                                    npc.ai[3] = 0f;
                                }
                                else
                                {
                                    npc.ai[1] = 1f;
                                }
                            }
                        }
                        if ((double)npc.life < (double)npc.lifeMax)
                        {
                            npc.ai[0] = 1f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        return false;
                    }
                    if (npc.ai[0] == 1f || npc.ai[0] == 2f)
                    {
                        if (npc.ai[0] == 1f)
                        {
                            npc.ai[2] += 0.005f;
                            if ((double)npc.ai[2] > 0.5)
                            {
                                npc.ai[2] = 0.5f;
                            }
                        }
                        else
                        {
                            npc.ai[2] -= 0.005f;
                            if (npc.ai[2] < 0f)
                            {
                                npc.ai[2] = 0f;
                            }
                        }
                        npc.rotation += npc.ai[2];
                        npc.ai[1] += 1f;
                        if (npc.ai[1] == 100f)
                        {
                            npc.ai[0] += 1f;
                            npc.ai[1] = 0f;
                            if (npc.ai[0] == 3f)
                            {
                                npc.ai[2] = 0f;
                            }
                            else
                            {
                                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y);
                                for (int num455 = 0; num455 < 2; num455++)
                                {
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 144);
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
                                    Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
                                }
                                for (int num456 = 0; num456 < 20; num456++)
                                {
                                    Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                                }
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                        }
                        Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                        npc.velocity.X *= 0.98f;
                        npc.velocity.Y *= 0.98f;
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                        return false;
                    }
                    npc.HitSound = SoundID.NPCHit4;
                    npc.damage = (int)((double)npc.defDamage * 1.5);
                    npc.defense = npc.defDefense + 18;
                    if (npc.ai[1] == 0f)
                    {
                        float num457 = 4f;
                        float num458 = 0.1f;
                        int num459 = 1;
                        if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                        {
                            num459 = -1;
                        }
                        Vector2 vector43 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num460 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num459 * 180) - vector43.X;
                        float num461 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector43.Y;
                        float num462 = (float)Math.Sqrt(num460 * num460 + num461 * num461);
                        if (Main.expertMode)
                        {
                            if (num462 > 300f)
                            {
                                num457 += 0.5f;
                            }
                            if (num462 > 400f)
                            {
                                num457 += 0.5f;
                            }
                            if (num462 > 500f)
                            {
                                num457 += 0.55f;
                            }
                            if (num462 > 600f)
                            {
                                num457 += 0.55f;
                            }
                            if (num462 > 700f)
                            {
                                num457 += 0.6f;
                            }
                            if (num462 > 800f)
                            {
                                num457 += 0.6f;
                            }
                        }
                        if (DRGNModWorld.MentalMode)
                        {
                            num457 *= 1.15f;
                            num458 *= 1.15f;
                        }
                        num462 = num457 / num462;
                        num460 *= num462;
                        num461 *= num462;
                        if (npc.velocity.X < num460)
                        {
                            npc.velocity.X += num458;
                            if (npc.velocity.X < 0f && num460 > 0f)
                            {
                                npc.velocity.X += num458;
                            }
                        }
                        else if (npc.velocity.X > num460)
                        {
                            npc.velocity.X -= num458;
                            if (npc.velocity.X > 0f && num460 < 0f)
                            {
                                npc.velocity.X -= num458;
                            }
                        }
                        if (npc.velocity.Y < num461)
                        {
                            npc.velocity.Y += num458;
                            if (npc.velocity.Y < 0f && num461 > 0f)
                            {
                                npc.velocity.Y += num458;
                            }
                        }
                        else if (npc.velocity.Y > num461)
                        {
                            npc.velocity.Y -= num458;
                            if (npc.velocity.Y > 0f && num461 < 0f)
                            {
                                npc.velocity.Y -= num458;
                            }
                        }
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= 400f)
                        {
                            npc.ai[1] = 1f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.target = 255;
                            npc.netUpdate = true;
                        }
                        if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            return false;
                        }
                        npc.localAI[2] += 1f;
                        if (npc.localAI[2] > 22f)
                        {
                            npc.localAI[2] = 0f;
                            Main.PlaySound(SoundID.Item34, npc.position);
                        }
                        if (Main.netMode != 1)
                        {
                            npc.localAI[1] += 1f;
                            if ((double)npc.life < (double)npc.lifeMax * 0.75)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.5)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.25)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.1)
                            {
                                npc.localAI[1] += 2f;
                            }
                            if (npc.localAI[1] > 8f)
                            {
                                npc.localAI[1] = 0f;
                                float num463 = 6f;
                                int attackDamage_ForProjectiles7 = 28;
                                int num464 = 101;
                                vector43 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                num460 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector43.X;
                                num461 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector43.Y;
                                num462 = (float)Math.Sqrt(num460 * num460 + num461 * num461);
                                num462 = num463 / num462;
                                num460 *= num462;
                                num461 *= num462;
                                num461 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num460 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num461 += npc.velocity.Y * 0.5f;
                                num460 += npc.velocity.X * 0.5f;
                                vector43.X -= num460 * 1f;
                                vector43.Y -= num461 * 1f;
                                int num465 = Projectile.NewProjectile(vector43.X, vector43.Y, num460, num461, num464, attackDamage_ForProjectiles7, 0f, Main.myPlayer);
                            }
                        }
                    }
                    else if (npc.ai[1] == 1f)
                    {
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                        npc.rotation = num437;
                        float num466 = 14f;
                        if (Main.expertMode)
                        {
                            num466 += 2.5f;
                        }
                        Vector2 vector44 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num467 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
                        float num468 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
                        float num469 = (float)Math.Sqrt(num467 * num467 + num468 * num468);
                        num469 = num466 / num469;
                        npc.velocity.X = num467 * num469;
                        npc.velocity.Y = num468 * num469;
                        npc.ai[1] = 2f;
                    }
                    else
                    {
                        if (npc.ai[1] != 2f)
                        {
                            return false;
                        }
                        npc.ai[2] += 1f;
                        if (Main.expertMode)
                        {
                            npc.ai[2] += 0.5f;
                        }
                        if (npc.ai[2] >= 50f)
                        {
                            npc.velocity.X *= 0.93f;
                            npc.velocity.Y *= 0.93f;
                            if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                            {
                                npc.velocity.X = 0f;
                            }
                            if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                            {
                                npc.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                        }
                        if (npc.ai[2] >= 80f)
                        {
                            npc.ai[3] += 1f;
                            npc.ai[2] = 0f;
                            npc.target = 255;
                            npc.rotation = num437;
                            if (npc.ai[3] >= 6f)
                            {
                                npc.ai[1] = 0f;
                                npc.ai[3] = 0f;
                            }
                            else
                            {
                                npc.ai[1] = 1f;
                            }
                        }
                    }

                }
                else { return true; }
                return false;
            }
            return true;
        }
        public override bool PreNPCLoot(NPC npc)
        {

            int[] triplets = new int[3] { NPCID.Retinazer, NPCID.Spazmatism, mod.NPCType("Triplet") };
            if (triplets.Contains(npc.type))
            {
                for (int i = 0; i < triplets.Length; i++)
                {
                    if (triplets[i] != npc.type)
                    {
                        if (NPC.AnyNPCs(triplets[i]))
                        {
                            return false;
                        }
                    }
                }
            }


            return true;

        }
    }
}