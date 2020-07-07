using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeCultist : GlobalNPC


    {



        public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
                if (npc.aiStyle == 84)
                {
                    if (npc.ai[0] != -1f && Main.rand.Next(1000) == 0)
                    {
                        Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, Main.rand.Next(88, 92));
                    }
                    bool i = Main.expertMode;
                    bool flag = npc.life <= npc.lifeMax / 2;
                    int num = 120;
                    int attackDamage_ForProjectiles = npc.damage / 3;
                    
                        num = 90;
                    

                    num -= 30;

                    int num2 = 10;
                    int num3 = 5;
                    int attackDamage_ForProjectiles2 = npc.damage / 3;
                    
                       
                    

                    

                    int num4 = 80;
                    int attackDamage_ForProjectiles3 = npc.damage / 3;
                    
                        num4 = 40;
                    

                    num4 -= 20;

                    int num5 = 30;
                    int num6 = 2;
                    
                    int num7 = 20;
                    int num8 = 3;
                    bool flag2 = npc.type == NPCID.CultistBoss;
                    bool flag3 = false;
                    bool flag4 = false;
                    if (flag)
                    {
                        npc.defense = (int)((float)npc.defDefense * 0.65f);
                    }
                    if (!flag2)
                    {

                        npc.ai[0] = Main.npc[(int)npc.ai[3]].ai[0];
                        npc.ai[1] = Main.npc[(int)npc.ai[3]].ai[1];
                        if (npc.ai[0] == 5f)
                        {

                        }
                        else
                        {
                            flag3 = true;
                            flag4 = true;
                        }
                    }
                    else if (npc.ai[0] == 5f && npc.ai[1] >= 120f && npc.ai[1] < 420f && npc.justHit)
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[3] += 1f;
                        npc.velocity = Vector2.Zero;
                        npc.netUpdate = true;
                        List<int> list = new List<int>();
                        for (int j = 0; j < 200; j++)
                        {
                            if (Main.npc[j].active && Main.npc[j].type == NPCID.CultistBossClone && Main.npc[j].ai[3] == (float)npc.whoAmI)
                            {
                                list.Add(j);
                            }
                        }
                        
                        foreach (int item in list)
                        {
                            NPC nPC = Main.npc[item];
                            
                        }
                        Main.projectile[(int)npc.ai[2]].ai[1] = -1f;
                        Main.projectile[(int)npc.ai[2]].netUpdate = true;
                    }
                    Vector2 center = npc.Center;
                    Player player = Main.player[npc.target];
                    if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
                    {
                        npc.TargetClosest(faceTarget: false);
                        player = Main.player[npc.target];
                        npc.netUpdate = true;
                    }
                    if (player.dead || Vector2.Distance(player.Center, center) > 5600f)
                    {
                        npc.life = 0;
                        npc.HitEffect();
                        npc.active = false;
                        if (Main.netMode != 1)
                        {
                            NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f);
                        }
                        new List<int>().Add(npc.whoAmI);
                        for (int k = 0; k < 200; k++)
                        {
                            if (Main.npc[k].active && Main.npc[k].type == NPCID.CultistBossClone && Main.npc[k].ai[3] == (float)npc.whoAmI)
                            {
                                Main.npc[k].life = 0;
                                Main.npc[k].HitEffect();
                                Main.npc[k].active = false;
                                if (Main.netMode != 1)
                                {
                                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f);
                                }
                            }
                        }
                    }
                    float num10 = npc.ai[3];
                    if (npc.localAI[0] == 0f)
                    {
                        Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 89);
                        npc.localAI[0] = 1f;
                        npc.alpha = 255;
                        npc.rotation = 0f;
                        if (Main.netMode != 1)
                        {
                            npc.ai[0] = -1f;
                            npc.netUpdate = true;
                        }
                    }
                    if (npc.ai[0] == -1f)
                    {
                        npc.alpha -= 5;
                        if (npc.alpha < 0)
                        {
                            npc.alpha = 0;
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= 420f)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        else if (npc.ai[1] > 360f)
                        {
                            npc.velocity *= 0.95f;
                            if (npc.localAI[2] != 13f)
                            {
                                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 105);
                            }
                            npc.localAI[2] = 13f;
                        }
                        else if (npc.ai[1] > 300f)
                        {
                            npc.velocity = -Vector2.UnitY;
                            npc.localAI[2] = 10f;
                        }
                        else if (npc.ai[1] > 120f)
                        {
                            npc.localAI[2] = 1f;
                        }
                        else
                        {
                            npc.localAI[2] = 0f;
                        }
                        flag3 = true;
                        flag4 = true;
                    }
                    if (npc.ai[0] == 0f)
                    {
                        if (npc.ai[1] == 0f)
                        {
                            npc.TargetClosest(faceTarget: false);
                        }
                        npc.localAI[2] = 10f;
                        int num11 = Math.Sign(player.Center.X - center.X);
                        if (num11 != 0)
                        {
                            npc.direction = (npc.spriteDirection = num11);
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= 40f && flag2)
                        {
                            int num12 = 0;
                            if (flag)
                            {
                                switch ((int)npc.ai[3])
                                {
                                    case 0:
                                        num12 = 0;
                                        break;
                                    case 1:
                                        num12 = 1;
                                        break;
                                    case 2:
                                        num12 = 0;
                                        break;
                                    case 3:
                                        num12 = 5;
                                        break;
                                    case 4:
                                        num12 = 0;
                                        break;
                                    case 5:
                                        num12 = 3;
                                        break;
                                    case 6:
                                        num12 = 0;
                                        break;
                                    case 7:
                                        num12 = 5;
                                        break;
                                    case 8:
                                        num12 = 0;
                                        break;
                                    case 9:
                                        num12 = 2;
                                        break;
                                    case 10:
                                        num12 = 0;
                                        break;
                                    case 11:
                                        num12 = 3;
                                        break;
                                    case 12:
                                        num12 = 0;
                                        break;
                                    case 13:
                                        num12 = 4;
                                        npc.ai[3] = -1f;
                                        break;
                                    default:
                                        npc.ai[3] = -1f;
                                        break;
                                }
                            }
                            else
                            {
                                switch ((int)npc.ai[3])
                                {
                                    case 0:
                                        num12 = 0;
                                        break;
                                    case 1:
                                        num12 = 1;
                                        break;
                                    case 2:
                                        num12 = 0;
                                        break;
                                    case 3:
                                        num12 = 2;
                                        break;
                                    case 4:
                                        num12 = 0;
                                        break;
                                    case 5:
                                        num12 = 3;
                                        break;
                                    case 6:
                                        num12 = 0;
                                        break;
                                    case 7:
                                        num12 = 1;
                                        break;
                                    case 8:
                                        num12 = 0;
                                        break;
                                    case 9:
                                        num12 = 2;
                                        break;
                                    case 10:
                                        num12 = 0;
                                        break;
                                    case 11:
                                        num12 = 4;
                                        npc.ai[3] = -1f;
                                        break;
                                    default:
                                        npc.ai[3] = -1f;
                                        break;
                                }
                            }
                            int maxValue = 6;
                            if (npc.life < npc.lifeMax / 3)
                            {
                                maxValue = 4;
                            }
                            if (npc.life < npc.lifeMax / 4)
                            {
                                maxValue = 3;
                            }
                            if (i && flag && Main.rand.Next(maxValue) == 0 && num12 != 0 && num12 != 4 && num12 != 5 && NPC.CountNPCS(523) < 10)
                            {
                                num12 = 6;
                            }
                            if (num12 == 0)
                            {
                                float num13 = (float)Math.Ceiling((player.Center + new Vector2(0f, -100f) - center).Length() / 50f);
                                if (num13 == 0f)
                                {
                                    num13 = 1f;
                                }
                                List<int> list2 = new List<int>();
                                int num14 = 0;
                                list2.Add(npc.whoAmI);
                                for (int l = 0; l < 200; l++)
                                {
                                    if (Main.npc[l].active && Main.npc[l].type == NPCID.CultistBossClone && Main.npc[l].ai[3] == (float)npc.whoAmI)
                                    {
                                        list2.Add(l);
                                    }
                                }
                                bool flag5 = list2.Count % 2 == 0;
                                foreach (int item2 in list2)
                                {
                                    NPC nPC2 = Main.npc[item2];
                                    Vector2 center2 = nPC2.Center;
                                    float num15 = (float)((num14 + flag5.ToInt() + 1) / 2) * ((float)Math.PI * 2f) * 0.4f / (float)list2.Count;
                                    if (num14 % 2 == 1)
                                    {
                                        num15 *= -1f;
                                    }
                                    if (list2.Count == 1)
                                    {
                                        num15 = 0f;
                                    }
                                    Vector2 value = new Vector2(0f, -1f).RotatedBy(num15) * new Vector2(300f, 200f);
                                    Vector2 value2 = player.Center + value - center2;
                                    nPC2.ai[0] = 1f;
                                    nPC2.ai[1] = num13 * 2f;
                                    nPC2.velocity = value2 / num13;
                                    if (npc.whoAmI >= nPC2.whoAmI)
                                    {
                                        nPC2.position -= nPC2.velocity;
                                    }
                                    nPC2.netUpdate = true;
                                    num14++;
                                }
                            }
                            switch (num12)
                            {
                                case 1:
                                    npc.ai[0] = 3f;
                                    npc.ai[1] = 0f;
                                    break;
                                case 2:
                                    npc.ai[0] = 2f;
                                    npc.ai[1] = 0f;
                                    break;
                                case 3:
                                    npc.ai[0] = 4f;
                                    npc.ai[1] = 0f;
                                    break;
                                case 4:
                                    npc.ai[0] = 5f;
                                    npc.ai[1] = 0f;
                                    break;
                            }
                            if (num12 == 5)
                            {
                                npc.ai[0] = 7f;
                                npc.ai[1] = 0f;
                            }
                            if (num12 == 6)
                            {
                                npc.ai[0] = 8f;
                                npc.ai[1] = 0f;
                            }
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 1f)
                    {
                        flag3 = true;
                        npc.localAI[2] = 10f;
                        if ((float)(int)npc.ai[1] % 2f != 0f && npc.ai[1] != 1f)
                        {
                            npc.position -= npc.velocity;
                        }
                        npc.ai[1] -= 1f;
                        if (npc.ai[1] <= 0f)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 2f)
                    {
                        npc.localAI[2] = 11f;
                        Vector2 vec = Vector2.Normalize(player.Center - center);
                        if (vec.HasNaNs())
                        {
                            vec = new Vector2(npc.direction, 0f);
                        }
                        if (npc.ai[1] >= 4f && flag2 && (int)(npc.ai[1] - 4f) % num == 0)
                        {
                            if (Main.netMode != 1)
                            {
                                List<int> list3 = new List<int>();
                                for (int m = 0; m < 200; m++)
                                {
                                    if (Main.npc[m].active && Main.npc[m].type == 440 && Main.npc[m].ai[3] == (float)npc.whoAmI)
                                    {
                                        list3.Add(m);
                                    }
                                }
                                foreach (int item3 in list3)
                                {
                                    NPC nPC3 = Main.npc[item3];
                                    Vector2 center3 = nPC3.Center;
                                    int num16 = Math.Sign(player.Center.X - center3.X);
                                    if (num16 != 0)
                                    {
                                        nPC3.direction = (nPC3.spriteDirection = num16);
                                    }
                                    if (Main.netMode != 1)
                                    {
                                        vec = Vector2.Normalize(player.Center - center3 + player.velocity * 20f);
                                        if (vec.HasNaNs())
                                        {
                                            vec = new Vector2(npc.direction, 0f);
                                        }
                                        Vector2 vector = center3 + new Vector2(npc.direction * 30, 12f);
                                        for (int n = 0; n < 1; n++)
                                        {
                                            Vector2 spinninpoint = vec * (6f + (float)Main.rand.NextDouble() * 4f);
                                            spinninpoint = spinninpoint.RotatedByRandom(0.52359879016876221);
                                            Projectile.NewProjectile(vector.X, vector.Y, spinninpoint.X, spinninpoint.Y, 468, 18, 0f, Main.myPlayer);
                                        }
                                    }
                                }
                            }
                            if (Main.netMode != 1)
                            {
                                vec = Vector2.Normalize(player.Center - center + player.velocity * 20f);
                                if (vec.HasNaNs())
                                {
                                    vec = new Vector2(npc.direction, 0f);
                                }
                                Vector2 vector2 = npc.Center + new Vector2(npc.direction * 30, 12f);
                                for (int num17 = 0; num17 < 1; num17++)
                                {
                                    Vector2 vector3 = vec * 4f;
                                    Projectile.NewProjectile(vector2.X, vector2.Y, vector3.X, vector3.Y, 464, attackDamage_ForProjectiles, 0f, Main.myPlayer, 0f, 1f);
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= (float)(4 + num))
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 3f)
                    {
                        npc.localAI[2] = 11f;
                        Vector2 vec2 = Vector2.Normalize(player.Center - center);
                        if (vec2.HasNaNs())
                        {
                            vec2 = new Vector2(npc.direction, 0f);
                        }
                        if (npc.ai[1] >= 4f && flag2 && (int)(npc.ai[1] - 4f) % num2 == 0)
                        {
                            if ((int)(npc.ai[1] - 4f) / num2 == 2)
                            {
                                List<int> list4 = new List<int>();
                                for (int num18 = 0; num18 < 200; num18++)
                                {
                                    if (Main.npc[num18].active && Main.npc[num18].type == 440 && Main.npc[num18].ai[3] == (float)npc.whoAmI)
                                    {
                                        list4.Add(num18);
                                    }
                                }
                                if (Main.netMode != 1)
                                {
                                    foreach (int item4 in list4)
                                    {
                                        NPC nPC4 = Main.npc[item4];
                                        Vector2 center4 = nPC4.Center;
                                        int num19 = Math.Sign(player.Center.X - center4.X);
                                        if (num19 != 0)
                                        {
                                            nPC4.direction = (nPC4.spriteDirection = num19);
                                        }
                                        if (Main.netMode != 1)
                                        {
                                            vec2 = Vector2.Normalize(player.Center - center4 + player.velocity * 20f);
                                            if (vec2.HasNaNs())
                                            {
                                                vec2 = new Vector2(npc.direction, 0f);
                                            }
                                            Vector2 vector4 = center4 + new Vector2(npc.direction * 30, 12f);
                                            for (int num20 = 0; num20 < 1; num20++)
                                            {
                                                Vector2 spinninpoint2 = vec2 * (6f + (float)Main.rand.NextDouble() * 4f);
                                                spinninpoint2 = spinninpoint2.RotatedByRandom(0.52359879016876221);
                                                Projectile.NewProjectile(vector4.X, vector4.Y, spinninpoint2.X, spinninpoint2.Y, 468, 18, 0f, Main.myPlayer);
                                            }
                                        }
                                    }
                                }
                            }
                            int num21 = Math.Sign(player.Center.X - center.X);
                            if (num21 != 0)
                            {
                                npc.direction = (npc.spriteDirection = num21);
                            }
                            if (Main.netMode != 1)
                            {
                                vec2 = Vector2.Normalize(player.Center - center + player.velocity * 20f);
                                if (vec2.HasNaNs())
                                {
                                    vec2 = new Vector2(npc.direction, 0f);
                                }
                                Vector2 vector5 = npc.Center + new Vector2(npc.direction * 30, 12f);
                                for (int num22 = 0; num22 < 1; num22++)
                                {
                                    Vector2 spinninpoint3 = vec2 * (6f + (float)Main.rand.NextDouble() * 4f);
                                    spinninpoint3 = spinninpoint3.RotatedByRandom(0.52359879016876221);
                                    Projectile.NewProjectile(vector5.X, vector5.Y, spinninpoint3.X, spinninpoint3.Y, 467, attackDamage_ForProjectiles2, 0f, Main.myPlayer);
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= (float)(4 + num2 * num3))
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 4f)
                    {
                        if (flag2)
                        {
                            npc.localAI[2] = 12f;
                        }
                        else
                        {
                            npc.localAI[2] = 11f;
                        }
                        if (npc.ai[1] == 20f && flag2 && Main.netMode != 1)
                        {
                            List<int> list5 = new List<int>();
                            for (int num23 = 0; num23 < 200; num23++)
                            {
                                if (Main.npc[num23].active && Main.npc[num23].type == 440 && Main.npc[num23].ai[3] == (float)npc.whoAmI)
                                {
                                    list5.Add(num23);
                                }
                            }
                            foreach (int item5 in list5)
                            {
                                NPC nPC5 = Main.npc[item5];
                                Vector2 center5 = nPC5.Center;
                                int num24 = Math.Sign(player.Center.X - center5.X);
                                if (num24 != 0)
                                {
                                    nPC5.direction = (nPC5.spriteDirection = num24);
                                }
                                if (Main.netMode != 1)
                                {
                                    Vector2 vec3 = Vector2.Normalize(player.Center - center5 + player.velocity * 20f);
                                    if (vec3.HasNaNs())
                                    {
                                        vec3 = new Vector2(npc.direction, 0f);
                                    }
                                    Vector2 vector6 = center5 + new Vector2(npc.direction * 30, 12f);
                                    for (int num25 = 0; num25 < 1; num25++)
                                    {
                                        Vector2 spinninpoint4 = vec3 * (6f + (float)Main.rand.NextDouble() * 4f);
                                        spinninpoint4 = spinninpoint4.RotatedByRandom(0.52359879016876221);
                                        Projectile.NewProjectile(vector6.X, vector6.Y, spinninpoint4.X, spinninpoint4.Y, 468, 18, 0f, Main.myPlayer);
                                    }
                                }
                            }
                            if ((int)(npc.ai[1] - 20f) % num4 == 0)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 100f, 0f, 0f, 465, attackDamage_ForProjectiles3, 0f, Main.myPlayer);
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= (float)(20 + num4))
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 5f)
                    {
                        npc.localAI[2] = 10f;
                        if (Vector2.Normalize(player.Center - center).HasNaNs())
                        {
                            new Vector2(npc.direction, 0f);
                        }
                        if (npc.ai[1] >= 0f && npc.ai[1] < 30f)
                        {
                            flag3 = true;
                            flag4 = true;
                            float num26 = (npc.ai[1] - 0f) / 30f;
                            npc.alpha = (int)(num26 * 255f);
                        }
                        else if (npc.ai[1] >= 30f && npc.ai[1] < 90f)
                        {
                            if (npc.ai[1] == 30f && Main.netMode != 1 && flag2)
                            {
                                npc.localAI[1] += 1f;
                                Vector2 spinningpoint = new Vector2(180f, 0f);
                                List<int> list6 = new List<int>();
                                for (int num27 = 0; num27 < 200; num27++)
                                {
                                    if (Main.npc[num27].active && Main.npc[num27].type == 440 && Main.npc[num27].ai[3] == (float)npc.whoAmI)
                                    {
                                        list6.Add(num27);
                                    }
                                }
                                int num28 = 6 - list6.Count;
                                if (num28 > 2)
                                {
                                    num28 = 2;
                                }
                                int num29 = list6.Count + num28 + 1;
                                float[] array = new float[num29];
                                for (int num30 = 0; num30 < array.Length; num30++)
                                {
                                    array[num30] = Vector2.Distance(npc.Center + spinningpoint.RotatedBy((float)num30 * ((float)Math.PI * 2f) / (float)num29 - (float)Math.PI / 2f), player.Center);
                                }
                                int num31 = 0;
                                for (int num32 = 1; num32 < array.Length; num32++)
                                {
                                    if (array[num31] > array[num32])
                                    {
                                        num31 = num32;
                                    }
                                }
                                num31 = ((num31 >= num29 / 2) ? (num31 - num29 / 2) : (num31 + num29 / 2));
                                int num33 = num28;
                                for (int num34 = 0; num34 < array.Length; num34++)
                                {
                                    if (num31 != num34)
                                    {
                                        Vector2 center6 = npc.Center + spinningpoint.RotatedBy((float)num34 * ((float)Math.PI * 2f) / (float)num29 - (float)Math.PI / 2f);
                                        if (num33-- > 0)
                                        {
                                            int num35 = NPC.NewNPC((int)center6.X, (int)center6.Y + npc.height / 2, 440, npc.whoAmI);
                                            Main.npc[num35].ai[3] = npc.whoAmI;
                                            Main.npc[num35].netUpdate = true;
                                            Main.npc[num35].localAI[1] = npc.localAI[1];
                                        }
                                        else
                                        {
                                            int num36 = list6[-num33 - 1];
                                            Main.npc[num36].Center = center6;
                                            NetMessage.SendData(23, -1, -1, null, num36);
                                        }
                                    }
                                }
                                npc.ai[2] = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, 490, 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                                npc.Center += spinningpoint.RotatedBy((float)num31 * ((float)Math.PI * 2f) / (float)num29 - (float)Math.PI / 2f);
                                npc.netUpdate = true;
                                list6.Clear();
                            }
                            flag3 = true;
                            flag4 = true;
                            npc.alpha = 255;
                            if (flag2)
                            {
                                Vector2 value3 = Main.projectile[(int)npc.ai[2]].Center;
                                value3 -= npc.Center;
                                if (value3 == Vector2.Zero)
                                {
                                    value3 = -Vector2.UnitY;
                                }
                                value3.Normalize();
                                if (Math.Abs(value3.Y) < 0.77f)
                                {
                                    npc.localAI[2] = 11f;
                                }
                                else if (value3.Y < 0f)
                                {
                                    npc.localAI[2] = 12f;
                                }
                                else
                                {
                                    npc.localAI[2] = 10f;
                                }
                                int num37 = Math.Sign(value3.X);
                                if (num37 != 0)
                                {
                                    npc.direction = (npc.spriteDirection = num37);
                                }
                            }
                            else
                            {
                                Vector2 value4 = Main.projectile[(int)Main.npc[(int)npc.ai[3]].ai[2]].Center;
                                value4 -= npc.Center;
                                if (value4 == Vector2.Zero)
                                {
                                    value4 = -Vector2.UnitY;
                                }
                                value4.Normalize();
                                if (Math.Abs(value4.Y) < 0.77f)
                                {
                                    npc.localAI[2] = 11f;
                                }
                                else if (value4.Y < 0f)
                                {
                                    npc.localAI[2] = 12f;
                                }
                                else
                                {
                                    npc.localAI[2] = 10f;
                                }
                                int num38 = Math.Sign(value4.X);
                                if (num38 != 0)
                                {
                                    npc.direction = (npc.spriteDirection = num38);
                                }
                            }
                        }
                        else if (npc.ai[1] >= 90f && npc.ai[1] < 120f)
                        {
                            flag3 = true;
                            flag4 = true;
                            float num39 = (npc.ai[1] - 90f) / 30f;
                            npc.alpha = 255 - (int)(num39 * 255f);
                        }
                        else if (npc.ai[1] >= 120f && npc.ai[1] < 420f)
                        {
                            flag4 = true;
                            npc.alpha = 0;
                            if (flag2)
                            {
                                Vector2 value5 = Main.projectile[(int)npc.ai[2]].Center;
                                value5 -= npc.Center;
                                if (value5 == Vector2.Zero)
                                {
                                    value5 = -Vector2.UnitY;
                                }
                                value5.Normalize();
                                if (Math.Abs(value5.Y) < 0.77f)
                                {
                                    npc.localAI[2] = 11f;
                                }
                                else if (value5.Y < 0f)
                                {
                                    npc.localAI[2] = 12f;
                                }
                                else
                                {
                                    npc.localAI[2] = 10f;
                                }
                                int num40 = Math.Sign(value5.X);
                                if (num40 != 0)
                                {
                                    npc.direction = (npc.spriteDirection = num40);
                                }
                            }
                            else
                            {
                                Vector2 value6 = Main.projectile[(int)Main.npc[(int)npc.ai[3]].ai[2]].Center;
                                value6 -= npc.Center;
                                if (value6 == Vector2.Zero)
                                {
                                    value6 = -Vector2.UnitY;
                                }
                                value6.Normalize();
                                if (Math.Abs(value6.Y) < 0.77f)
                                {
                                    npc.localAI[2] = 11f;
                                }
                                else if (value6.Y < 0f)
                                {
                                    npc.localAI[2] = 12f;
                                }
                                else
                                {
                                    npc.localAI[2] = 10f;
                                }
                                int num41 = Math.Sign(value6.X);
                                if (num41 != 0)
                                {
                                    npc.direction = (npc.spriteDirection = num41);
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= 420f)
                        {
                            flag4 = true;
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 6f)
                    {
                        npc.localAI[2] = 13f;
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= 120f)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 7f)
                    {
                        npc.localAI[2] = 11f;
                        Vector2 vec4 = Vector2.Normalize(player.Center - center);
                        if (vec4.HasNaNs())
                        {
                            vec4 = new Vector2(npc.direction, 0f);
                        }
                        if (npc.ai[1] >= 4f && flag2 && (int)(npc.ai[1] - 4f) % num5 == 0)
                        {
                            if ((int)(npc.ai[1] - 4f) / num5 == 2)
                            {
                                List<int> list7 = new List<int>();
                                for (int num42 = 0; num42 < 200; num42++)
                                {
                                    if (Main.npc[num42].active && Main.npc[num42].type == NPCID.CultistBossClone && Main.npc[num42].ai[3] == (float)npc.whoAmI)
                                    {
                                        list7.Add(num42);
                                    }
                                }
                                foreach (int item6 in list7)
                                {
                                    NPC nPC6 = Main.npc[item6];
                                    Vector2 center7 = nPC6.Center;
                                    int num43 = Math.Sign(player.Center.X - center7.X);
                                    if (num43 != 0)
                                    {
                                        nPC6.direction = (nPC6.spriteDirection = num43);
                                    }
                                    if (Main.netMode != 1)
                                    {
                                        vec4 = Vector2.Normalize(player.Center - center7 + player.velocity * 20f);
                                        if (vec4.HasNaNs())
                                        {
                                            vec4 = new Vector2(npc.direction, 0f);
                                        }
                                        Vector2 vector7 = center7 + new Vector2(npc.direction * 30, 12f);
                                        for (int num44 = 0; (float)num44 < 5f; num44++)
                                        {
                                            Vector2 spinninpoint5 = vec4 * (6f + (float)Main.rand.NextDouble() * 4f);
                                            spinninpoint5 = spinninpoint5.RotatedByRandom(1.2566370964050293);
                                            Projectile.NewProjectile(vector7.X, vector7.Y, spinninpoint5.X, spinninpoint5.Y, 468, 18, 0f, Main.myPlayer);
                                        }
                                    }
                                }
                            }
                            int num45 = Math.Sign(player.Center.X - center.X);
                            if (num45 != 0)
                            {
                                npc.direction = (npc.spriteDirection = num45);
                            }
                            if (Main.netMode != 1)
                            {
                                vec4 = Vector2.Normalize(player.Center - center + player.velocity * 20f);
                                if (vec4.HasNaNs())
                                {
                                    vec4 = new Vector2(npc.direction, 0f);
                                }
                                Vector2 vector8 = npc.Center + new Vector2(npc.direction * 30, 12f);
                                float scaleFactor = 8f;
                                float num46 = (float)Math.PI * 2f / 25f;
                                for (int num47 = 0; (float)num47 < 5f; num47++)
                                {
                                    Vector2 spinningpoint2 = vec4 * scaleFactor;
                                    spinningpoint2 = spinningpoint2.RotatedBy(num46 * (float)num47 - ((float)Math.PI * 2f / 5f - num46) / 2f);
                                    float ai = (Main.rand.NextFloat() - 0.5f) * 0.3f * ((float)Math.PI * 2f) / 60f;
                                    int num48 = NPC.NewNPC((int)vector8.X, (int)vector8.Y + 7, 522, 0, 0f, ai, spinningpoint2.X, spinningpoint2.Y);
                                    Main.npc[num48].velocity = spinningpoint2;
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= (float)(4 + num5 * num6))
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[0] == 8f)
                    {
                        npc.localAI[2] = 13f;
                        if (npc.ai[1] >= 4f && flag2 && (int)(npc.ai[1] - 4f) % num7 == 0)
                        {
                            List<int> list8 = new List<int>();
                            for (int num49 = 0; num49 < 200; num49++)
                            {
                                if (Main.npc[num49].active && Main.npc[num49].type == 440 && Main.npc[num49].ai[3] == (float)npc.whoAmI)
                                {
                                    list8.Add(num49);
                                }
                            }
                            int num50 = list8.Count + 1;
                            if (num50 > 3)
                            {
                                num50 = 3;
                            }
                            int num51 = Math.Sign(player.Center.X - center.X);
                            if (num51 != 0)
                            {
                                npc.direction = (npc.spriteDirection = num51);
                            }
                            if (Main.netMode != 1)
                            {
                                for (int num52 = 0; num52 < num50; num52++)
                                {
                                    Point point = npc.Center.ToTileCoordinates();
                                    Point point2 = Main.player[npc.target].Center.ToTileCoordinates();
                                    Vector2 vector9 = Main.player[npc.target].Center - npc.Center;
                                    int num53 = 20;
                                    int num54 = 3;
                                    int num55 = 7;
                                    int num56 = 2;
                                    int num57 = 0;
                                    bool flag6 = false;
                                    if (vector9.Length() > 2000f)
                                    {
                                        flag6 = true;
                                    }
                                    while (!flag6 && num57 < 100)
                                    {
                                        num57++;
                                        int num58 = Main.rand.Next(point2.X - num53, point2.X + num53 + 1);
                                        int num59 = Main.rand.Next(point2.Y - num53, point2.Y + num53 + 1);
                                        if ((num59 < point2.Y - num55 || num59 > point2.Y + num55 || num58 < point2.X - num55 || num58 > point2.X + num55) && (num59 < point.Y - num54 || num59 > point.Y + num54 || num58 < point.X - num54 || num58 > point.X + num54) && !Main.tile[num58, num59].nactive())
                                        {
                                            bool flag7 = true;
                                            if (flag7 && Collision.SolidTiles(num58 - num56, num58 + num56, num59 - num56, num59 + num56))
                                            {
                                                flag7 = false;
                                            }
                                            if (flag7)
                                            {
                                                NPC.NewNPC(num58 * 16 + 8, num59 * 16 + 8, 523, 0, npc.whoAmI);
                                                flag6 = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        npc.ai[1] += 1f;
                        if (npc.ai[1] >= (float)(4 + num7 * num8))
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] += 1f;
                            npc.velocity = Vector2.Zero;
                            npc.netUpdate = true;
                        }
                    }
                    if (!flag2)
                    {
                        npc.ai[3] = num10;
                    }
                    npc.dontTakeDamage = flag3;
                    npc.chaseable = !flag4;
                    return false;
                }
                else return true;

            }
            return true;
        }
    }
}
