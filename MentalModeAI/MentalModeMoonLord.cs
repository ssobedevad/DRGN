using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeMoonLord : GlobalNPC


    {
		public static double timeForVisualEffects;


		public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
				
				if (npc.aiStyle == 77)
				{
					if ((timeForVisualEffects += 1.0) >= 216000.0)
					{
						timeForVisualEffects = 0.0;
					}
					if (npc.ai[0] != -1f && npc.ai[0] != 2f && Main.rand.Next(200) == 0)
					{
						Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, Main.rand.Next(93, 100));
					}
					if (npc.localAI[3] == 0f)
					{
						npc.netUpdate = true;
						npc.localAI[3] = 1f;
						npc.ai[0] = -1f;
					}
					if (npc.ai[0] == -2f)
					{
						npc.dontTakeDamage = true;
						npc.ai[1]++;
						if (npc.ai[1] == 30f)
						{
							Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
						}
						if (npc.ai[1] < 60f)
						{
							MoonlordDeathDrama.RequestLight(npc.ai[1] / 30f, npc.Center);
						}
						if (npc.ai[1] == 60f)
						{
							npc.ai[1] = 0f;
							npc.ai[0] = 0f;
							if (Main.netMode != 1 && npc.type == 398)
							{
								npc.ai[2] = Main.rand.Next(3);
								npc.ai[2] = 0f;
								npc.netUpdate = true;
							}
						}
					}
					if (npc.ai[0] == -1f)
					{
						npc.dontTakeDamage = true;
						npc.ai[1]++;
						if (npc.ai[1] == 30f)
						{
							Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
						}
						if (npc.ai[1] < 60f)
						{
							MoonlordDeathDrama.RequestLight(npc.ai[1] / 30f, npc.Center);
						}
						if (npc.ai[1] == 60f)
						{
							npc.ai[1] = 0f;
							npc.ai[0] = 0f;
							if (Main.netMode != 1 && npc.type == 398)
							{
								npc.ai[2] = Main.rand.Next(3);
								npc.ai[2] = 0f;
								npc.netUpdate = true;
								int[] array5 = new int[3];
								int num1167 = 0;
								for (int num1168 = 0; num1168 < 2; num1168++)
								{
									int num1169 = NPC.NewNPC((int)npc.Center.X + num1168 * 800 - 400, (int)npc.Center.Y - 100, 397, npc.whoAmI);
									Main.npc[num1169].ai[2] = num1168;
									Main.npc[num1169].netUpdate = true;
									array5[num1167++] = num1169;
								}
								int num1170 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 400, 396, npc.whoAmI);
								Main.npc[num1170].netUpdate = true;
								array5[num1167++] = num1170;
								for (int num1171 = 0; num1171 < 3; num1171++)
								{
									Main.npc[array5[num1171]].ai[3] = npc.whoAmI;
								}
								for (int num1172 = 0; num1172 < 3; num1172++)
								{
									npc.localAI[num1172] = array5[num1172];
								}
							}
						}
					}
					if (npc.ai[0] == 0f)
					{
						npc.dontTakeDamage = true;
						npc.TargetClosest( false);
						Vector2 value8 = Main.player[npc.target].Center - npc.Center + new Vector2(0f, 130f);
						if (value8.Length() > 20f)
						{
							Vector2 desiredVelocity = Vector2.Normalize(value8 - npc.velocity) * 25f;
							Vector2 velocity = npc.velocity;
							npc.SimpleFlyMovement(desiredVelocity, 0.5f);
							npc.velocity = Vector2.Lerp(npc.velocity, velocity, 0.5f);
						}
						if (Main.netMode != 1)
						{
							bool flag74 = false;
							if (npc.localAI[0] < 0f || npc.localAI[1] < 0f || npc.localAI[2] < 0f)
							{
								flag74 = true;
							}
							else if (!Main.npc[(int)npc.localAI[0]].active || Main.npc[(int)npc.localAI[0]].type != 397)
							{
								flag74 = true;
							}
							else if (!Main.npc[(int)npc.localAI[1]].active || Main.npc[(int)npc.localAI[1]].type != 397)
							{
								flag74 = true;
							}
							else if (!Main.npc[(int)npc.localAI[2]].active || Main.npc[(int)npc.localAI[2]].type != 396)
							{
								flag74 = true;
							}
							if (flag74)
							{
								npc.life = 0;
								npc.HitEffect();
								npc.active = false;
							}
							bool flag75 = true;
							if (Main.npc[(int)npc.localAI[0]].ai[0] != -2f)
							{
								flag75 = false;
							}
							if (Main.npc[(int)npc.localAI[1]].ai[0] != -2f)
							{
								flag75 = false;
							}
							if (Main.npc[(int)npc.localAI[2]].ai[0] != -2f)
							{
								flag75 = false;
							}
							if (flag75)
							{
								npc.ai[0] = 1f;
								npc.dontTakeDamage = false;
								npc.netUpdate = true;
							}
						}
					}
					else if (npc.ai[0] == 1f)
					{
						npc.dontTakeDamage = false;
						npc.TargetClosest(faceTarget: false);
						Vector2 value9 = Main.player[npc.target].Center - npc.Center + new Vector2(0f, 130f);
						if (value9.Length() > 20f)
						{
							Vector2 desiredVelocity2 = Vector2.Normalize(value9 - npc.velocity) * 8f;
							Vector2 velocity2 = npc.velocity;
							npc.SimpleFlyMovement(desiredVelocity2, 0.5f);
							npc.velocity = Vector2.Lerp(npc.velocity, velocity2, 0.5f);
						}
					}
					else if (npc.ai[0] == 2f)
					{
						npc.dontTakeDamage = true;
						npc.velocity = Vector2.Lerp(value2: new Vector2(npc.direction, -0.5f), value1: npc.velocity, amount: 0.98f);
						npc.ai[1]++;
						if (npc.ai[1] < 60f)
						{
							MoonlordDeathDrama.RequestLight(npc.ai[1] / 60f, npc.Center);
						}
						if (npc.ai[1] == 60f)
						{
							for (int num1173 = 0; num1173 < 1000; num1173++)
							{
								Projectile projectile = Main.projectile[num1173];
								if (projectile.active && (projectile.type == 456 || projectile.type == 462 || projectile.type == 455 || projectile.type == 452 || projectile.type == 454))
								{
									projectile.Kill();
								}
							}
							for (int num1174 = 0; num1174 < 200; num1174++)
							{
								NPC nPC3 = Main.npc[num1174];
								if (nPC3.active && nPC3.type == 400)
								{
									nPC3.HitEffect(0, 9999.0);
									nPC3.active = false;
								}
							}
						}
						if (npc.ai[1] % 3f == 0f && npc.ai[1] < 580f && npc.ai[1] > 60f)
						{
							Vector2 vector145 = Utils.RandomVector2(Main.rand, -1f, 1f);
							if (vector145 != Vector2.Zero)
							{
								vector145.Normalize();
							}
							vector145 *= 20f + Main.rand.NextFloat() * 400f;
							bool flag76 = true;
							Vector2 vector146 = npc.Center + vector145;
							Point point6 = vector146.ToTileCoordinates();
							if (!WorldGen.InWorld(point6.X, point6.Y))
							{
								flag76 = false;
							}
							if (flag76 && WorldGen.SolidTile(point6.X, point6.Y))
							{
								flag76 = false;
							}
							float num1175 = Main.rand.Next(6, 19);
							float num1176 = (float)Math.PI * 2f / num1175;
							float num1177 = (float)Math.PI * 2f * Main.rand.NextFloat();
							float scaleFactor6 = 1f + Main.rand.NextFloat() * 2f;
							float num1178 = 1f + Main.rand.NextFloat();
							float fadeIn = 0.4f + Main.rand.NextFloat();
							int num1179 = Utils.SelectRandom<int>(Main.rand, 31, 229);
							if (flag76)
							{
								MoonlordDeathDrama.AddExplosion(vector146);
								for (float num1180 = 0f; num1180 < num1175 * 2f; num1180++)
								{
									Dust dust6 = Main.dust[Dust.NewDust(vector146, 0, 0, 229)];
									dust6.noGravity = true;
									dust6.position = vector146;
									dust6.velocity = Vector2.UnitY.RotatedBy(num1177 + num1176 * num1180) * scaleFactor6 * (Main.rand.NextFloat() * 1.6f + 1.6f);
									dust6.fadeIn = fadeIn;
									dust6.scale = num1178;
								}
							}
							for (float num1181 = 0f; num1181 < npc.ai[1] / 60f; num1181++)
							{
								Vector2 vector147 = Utils.RandomVector2(Main.rand, -1f, 1f);
								if (vector147 != Vector2.Zero)
								{
									vector147.Normalize();
								}
								vector147 *= 20f + Main.rand.NextFloat() * 800f;
								Vector2 vector148 = npc.Center + vector147;
								Point point7 = vector148.ToTileCoordinates();
								bool flag77 = true;
								if (!WorldGen.InWorld(point7.X, point7.Y))
								{
									flag77 = false;
								}
								if (flag77 && WorldGen.SolidTile(point7.X, point7.Y))
								{
									flag77 = false;
								}
								if (flag77)
								{
									Dust dust7 = Main.dust[Dust.NewDust(vector148, 0, 0, num1179)];
									dust7.noGravity = true;
									dust7.position = vector148;
									dust7.velocity = -Vector2.UnitY * scaleFactor6 * (Main.rand.NextFloat() * 0.9f + 1.6f);
									dust7.fadeIn = fadeIn;
									dust7.scale = num1178;
								}
							}
						}
						if (npc.ai[1] % 15f == 0f && npc.ai[1] < 480f && npc.ai[1] >= 90f && Main.netMode != 1)
						{
							Vector2 vector149 = Utils.RandomVector2(Main.rand, -1f, 1f);
							if (vector149 != Vector2.Zero)
							{
								vector149.Normalize();
							}
							vector149 *= 20f + Main.rand.NextFloat() * 400f;
							bool flag78 = true;
							Vector2 vec3 = npc.Center + vector149;
							Point point8 = vec3.ToTileCoordinates();
							if (!WorldGen.InWorld(point8.X, point8.Y))
							{
								flag78 = false;
							}
							if (flag78 && WorldGen.SolidTile(point8.X, point8.Y))
							{
								flag78 = false;
							}
							if (flag78)
							{
								float num1182 = (float)(Main.rand.Next(4) < 2).ToDirectionInt() * ((float)Math.PI / 8f + (float)Math.PI / 4f * Main.rand.NextFloat());
								Vector2 vector150 = new Vector2(0f, (0f - Main.rand.NextFloat()) * 0.5f - 0.5f).RotatedBy(num1182) * 6f;
								Projectile.NewProjectile(vec3.X, vec3.Y, vector150.X, vector150.Y, ProjectileID.BlowupSmokeMoonlord, 0, 0f);
							}
						}
						if (npc.ai[1] == 1f)
						{
							Main.PlaySound(SoundID.NPCDeath61, npc.Center);
						}
						if (npc.ai[1] >= 480f)
						{
							MoonlordDeathDrama.RequestLight((npc.ai[1] - 480f) / 120f, npc.Center);
						}
						if (npc.ai[1] >= 600f)
						{
							npc.life = 0;
							npc.HitEffect(0, 1337.0);
							npc.checkDead();
							return false;
						}
					}
					else if (npc.ai[0] == 3f)
					{
						npc.dontTakeDamage = true;
						npc.velocity = Vector2.Lerp(value2: new Vector2(npc.direction, -0.5f), value1: npc.velocity, amount: 0.98f);
						npc.ai[1]++;
						if (npc.ai[1] < 60f)
						{
							MoonlordDeathDrama.RequestLight(npc.ai[1] / 40f, npc.Center);
						}
						if (npc.ai[1] == 40f)
						{
							for (int num1183 = 0; num1183 < 1000; num1183++)
							{
								Projectile projectile2 = Main.projectile[num1183];
								if (projectile2.active && (projectile2.type == 456 || projectile2.type == 462 || projectile2.type == 455 || projectile2.type == 452 || projectile2.type == 454))
								{
									projectile2.active = false;
									if (Main.netMode != 1)
									{
										NetMessage.SendData(27, -1, -1, null, num1183);
									}
								}
							}
							for (int num1184 = 0; num1184 < 200; num1184++)
							{
								NPC nPC4 = Main.npc[num1184];
								if (nPC4.active && nPC4.type == NPCID.MoonLordFreeEye)
								{
									nPC4.active = false;
									if (Main.netMode != 1)
									{
										NetMessage.SendData(23, -1, -1, null, nPC4.whoAmI);
									}
								}
							}
							for (int num1185 = 0; num1185 < 600; num1185++)
							{
								Gore gore2 = Main.gore[num1185];
								if (gore2.active && gore2.type >= 619 && gore2.type <= 622)
								{
									gore2.active = false;
								}
							}
						}
						if (npc.ai[1] >= 60f)
						{
							for (int num1186 = 0; num1186 < 200; num1186++)
							{
								NPC nPC5 = Main.npc[num1186];
								if (nPC5.active && (nPC5.type == 400 || nPC5.type == 397 || nPC5.type == 396))
								{
									nPC5.active = false;
									if (Main.netMode != 1)
									{
										NetMessage.SendData(23, -1, -1, null, nPC5.whoAmI);
									}
								}
							}
							npc.active = false;
							if (Main.netMode != 1)
							{
								NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
							}
							NPC.LunarApocalypseIsUp = false;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(7);
							}
							return false;
						}
					}
					bool flag79 = false;
					if (npc.ai[0] == -2f || npc.ai[0] == -1f || npc.ai[0] == 2f || npc.ai[0] == 3f)
					{
						flag79 = true;
					}
					if (Main.player[npc.target].active && !Main.player[npc.target].dead)
					{
						flag79 = true;
					}
					if (!flag79)
					{
						for (int i = 0; i < 255; i++)
						{
							if (Main.player[i].active && !Main.player[i].dead)
							{
								flag79 = true;
								break;
							}
						}
					}
					if (!flag79)
					{
						npc.ai[0] = 3f;
						npc.ai[1] = 0f;
						npc.netUpdate = true;
					}
					if (!(npc.ai[0] >= 0f) || !(npc.ai[0] < 2f) || Main.netMode == 1 || !(npc.Distance(Main.player[npc.target].Center) > 2400f))
					{
						return false;
					}
					npc.ai[0] = -2f;
					npc.netUpdate = true;
					Vector2 vector151 = Main.player[npc.target].Center - Vector2.UnitY * 150f - npc.Center;
					npc.position += vector151;
					if (Main.npc[(int)npc.localAI[0]].active)
					{
						NPC nPC = Main.npc[(int)npc.localAI[0]];
						nPC.position += vector151;
						Main.npc[(int)npc.localAI[0]].netUpdate = true;
					}
					if (Main.npc[(int)npc.localAI[1]].active)
					{
						NPC nPC = Main.npc[(int)npc.localAI[1]];
						nPC.position += vector151;
						Main.npc[(int)npc.localAI[1]].netUpdate = true;
					}
					if (Main.npc[(int)npc.localAI[2]].active)
					{
						NPC nPC = Main.npc[(int)npc.localAI[2]];
						nPC.position += vector151;
						Main.npc[(int)npc.localAI[2]].netUpdate = true;
					}
					for (int num1188 = 0; num1188 < 200; num1188++)
					{
						NPC nPC6 = Main.npc[num1188];
						if (nPC6.active && nPC6.type == 400)
						{
							NPC nPC = nPC6;
							nPC.position += vector151;
							nPC6.netUpdate = true;
						}
					}
				}
				else if (npc.aiStyle == 78)
				{
					NPC.InitializeMoonLordAttacks();
					if (!Main.npc[(int)npc.ai[3]].active || Main.npc[(int)npc.ai[3]].type != NPCID.MoonLordCore)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.active = false;
					}
					bool flag80 = npc.ai[2] == 0f;
					float num1189 = -flag80.ToDirectionInt();
					npc.spriteDirection = (int)num1189;
					npc.dontTakeDamage = (npc.frameCounter >= 21.0);
					Vector2 vector152 = new Vector2(30f, 66f);
					float num1190 = 0f;
					float num1191 = 0f;
					bool flag81 = true;
					int num1192 = 0;
					if (npc.ai[0] != -2f)
					{
						float num1193 = npc.ai[0];
						npc.ai[1]++;
						int num1194 = (int)Main.npc[(int)npc.ai[3]].ai[2];
						int num1195 = (!flag80) ? 1 : 0;
						int num1196 = 0;
						int num1197 = 0;
						for (; num1196 < 5; num1196++)
						{
							num1191 = NPC.MoonLordAttacksArray[num1194, num1195, 1, num1196];
							if (!(num1191 + (float)num1197 <= npc.ai[1]))
							{
								break;
							}
							num1197 += (int)num1191;
						}
						if (num1196 == 5)
						{
							num1196 = 0;
							npc.ai[1] = 0f;
							num1191 = NPC.MoonLordAttacksArray[num1194, num1195, 1, num1196];
							num1197 = 0;
						}
						npc.ai[0] = NPC.MoonLordAttacksArray[num1194, num1195, 0, num1196];
						num1190 = (int)npc.ai[1] - num1197;
						if (npc.ai[0] != num1193)
						{
							npc.netUpdate = true;
						}
					}
					if (npc.ai[0] == -2f)
					{
						npc.damage = 80;
						num1192 = 0;
						npc.dontTakeDamage = true;
						npc.ai[1]++;
						if (npc.ai[1] >= 32f)
						{
							npc.ai[1] = 0f;
						}
						if (npc.ai[1] < 0f)
						{
							npc.ai[1] = 0f;
						}
						Vector2 center16 = Main.npc[(int)npc.ai[3]].Center;
						Vector2 value12 = center16 + new Vector2(350f * num1189, -100f);
						Vector2 vector153 = value12 - npc.Center;
						if (vector153.Length() > 20f)
						{
							vector153.Normalize();
							vector153 *= 6f;
							Vector2 velocity3 = npc.velocity;
							if (vector153 != Vector2.Zero)
							{
								npc.SimpleFlyMovement(vector153, 0.3f);
							}
							npc.velocity = Vector2.Lerp(velocity3, npc.velocity, 0.5f);
						}
					}
					else if (npc.ai[0] == 0f)
					{
						num1192 = 3;
						npc.localAI[1] -= 0.05f;
						if (npc.localAI[1] < 0f)
						{
							npc.localAI[1] = 0f;
						}
						Vector2 center17 = Main.npc[(int)npc.ai[3]].Center;
						Vector2 value13 = center17 + new Vector2(350f * num1189, -100f);
						Vector2 vector154 = value13 - npc.Center;
						if (vector154.Length() > 20f)
						{
							vector154.Normalize();
							vector154 *= 6f;
							Vector2 velocity4 = npc.velocity;
							if (vector154 != Vector2.Zero)
							{
								npc.SimpleFlyMovement(vector154, 0.3f);
							}
							npc.velocity = Vector2.Lerp(velocity4, npc.velocity, 0.5f);
						}
					}
					else if (npc.ai[0] == 1f)
					{
						num1192 = 0;
						int num1198 = 7;
						int num1199 = 4;
						if (num1190 >= (float)(num1198 * num1199 * 2))
						{
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
						}
						else if (num1190 >= (float)(num1198 * num1199))
						{
							npc.localAI[1] += 0.05f;
							if (npc.localAI[1] > 0.75f)
							{
								npc.localAI[1] = 0.75f;
							}
							float num1200 = (float)Math.PI * 2f * (num1190 % (float)(num1198 * num1199)) / (float)(num1198 * num1199) - (float)Math.PI / 2f;
							npc.localAI[0] = new Vector2((float)Math.Cos(num1200) * vector152.X, (float)Math.Sin(num1200) * vector152.Y).ToRotation();
							if (num1190 % (float)num1199 == 0f)
							{
								Vector2 value14 = new Vector2(1f * (0f - num1189), 3f);
								Vector2 vector155 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), vector152 * npc.localAI[1]);
								Vector2 vector156 = npc.Center + Vector2.Normalize(vector155) * vector152.Length() * 0.4f + value14;
								Vector2 vector157 = Vector2.Normalize(vector155) * Main.rand.Next(5,10);
								float ai = ((float)Math.PI * 2f * (float)Main.rand.NextDouble() - (float)Math.PI) / 30f + (float)Math.PI / 180f * num1189;
								Projectile.NewProjectile(vector156.X, vector156.Y, vector157.X, vector157.Y, ProjectileID.PhantasmalEye, 30, 0f, Main.myPlayer, 0f, ai);
								Projectile.NewProjectile(vector156.X, vector156.Y, vector157.X, vector157.Y, ProjectileID.PhantasmalEye, 30, 0f, Main.myPlayer, 0f, ai);
							}
						}
						else
						{
							npc.localAI[1] += 0.02f;
							if (npc.localAI[1] > 0.75f)
							{
								npc.localAI[1] = 0.75f;
							}
							float num1201 = (float)Math.PI * 2f * (num1190 % (float)(num1198 * num1199)) / (float)(num1198 * num1199) - (float)Math.PI / 2f;
							npc.localAI[0] = new Vector2((float)Math.Cos(num1201) * vector152.X, (float)Math.Sin(num1201) * vector152.Y).ToRotation();
						}
					}
					else if (npc.ai[0] == 2f)
					{
						npc.localAI[1] -= 0.05f;
						if (npc.localAI[1] < 0f)
						{
							npc.localAI[1] = 0f;
						}
						Vector2 center18 = Main.npc[(int)npc.ai[3]].Center;
						Vector2 value15 = new Vector2(220f * num1189, -60f) + center18;
						value15 += new Vector2(num1189 * 100f, -50f);
						Vector2 value16 = new Vector2(400f * num1189, -60f);
						if (num1190 < 30f)
						{
							Vector2 vector158 = value15 - npc.Center;
							if (vector158 != Vector2.Zero)
							{
								Vector2 vector159 = vector158;
								vector159.Normalize();
								npc.velocity = Vector2.SmoothStep(npc.velocity, vector159 * Math.Min(8f, vector158.Length()), 0.2f);
							}
						}
						else if (num1190 < 210f)
						{
							num1192 = 1;
							int num1202 = (int)num1190 - 30;
							if (num1202 % 30 == 0 && Main.netMode != 1)
							{
								Vector2 vector160 = new Vector2(5f * num1189, -8f);
								int num1203 = num1202 / 30;
								vector160.X += ((float)num1203 - 3.5f) * num1189 * 3f;
								vector160.Y += ((float)num1203 - 4.5f) * 1f;
								vector160 *= 1.2f;
								int num1204 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector160.X, vector160.Y, ProjectileID.PhantasmalSphere, 50, 1f, Main.myPlayer, 0f, npc.whoAmI);
							}
							Vector2 vector161 = Vector2.SmoothStep(value15, value15 + value16, (num1190 - 30f) / 180f) - npc.Center;
							if (vector161 != Vector2.Zero)
							{
								Vector2 vector162 = vector161;
								vector162.Normalize();
								npc.velocity = Vector2.Lerp(npc.velocity, vector162 * Math.Min(20f, vector161.Length()), 0.5f);
							}
						}
						else if (num1190 < 282f)
						{
							num1192 = 0;
							npc.velocity *= 0.9f;
						}
						else if (num1190 < 287f)
						{
							num1192 = 1;
							npc.velocity *= 0.9f;
						}
						else if (num1190 < 292f)
						{
							num1192 = 2;
							npc.velocity *= 0.9f;
						}
						else if (num1190 < 300f)
						{
							num1192 = 3;
							if (num1190 == 292f && Main.netMode != 1)
							{
								int num1205 = Player.FindClosest(npc.position, npc.width, npc.height);
								Vector2 velocity5 = Vector2.Normalize(Main.player[num1205].Center - (npc.Center + Vector2.UnitY * -350f));
								if (float.IsNaN(velocity5.X) || float.IsNaN(velocity5.Y))
								{
									velocity5 = Vector2.UnitY;
								}
								velocity5 *= 12f;
								for (int num1206 = 0; num1206 < 1000; num1206++)
								{
									Projectile projectile3 = Main.projectile[num1206];
									if (projectile3.active && projectile3.type == 454 && projectile3.ai[1] == (float)npc.whoAmI && projectile3.ai[0] != -1f)
									{
										projectile3.ai[0] = -1f;
										projectile3.velocity = velocity5;
										projectile3.netUpdate = true;
									}
								}
							}
							Vector2 vector163 = Vector2.SmoothStep(value15, value15 + value16, 1f - (num1190 - 270f) / 30f) - npc.Center;
							if (vector163 != Vector2.Zero)
							{
								Vector2 vector164 = vector163;
								vector164.Normalize();
								npc.velocity = Vector2.Lerp(npc.velocity, vector164 * Math.Min(14f, vector163.Length()), 0.1f);
							}
						}
						else
						{
							num1192 = 3;
							Vector2 vector165 = value15 - npc.Center;
							if (vector165 != Vector2.Zero)
							{
								Vector2 vector166 = vector165;
								vector166.Normalize();
								npc.velocity = Vector2.SmoothStep(npc.velocity, vector166 * Math.Min(8f, vector165.Length()), 0.2f);
							}
						}
					}
					else if (npc.ai[0] == 3f)
					{
						if (num1190 == 0f)
						{
							npc.TargetClosest(faceTarget: false);
							npc.netUpdate = true;
						}
						Vector2 v2 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
						npc.localAI[0] = npc.localAI[0].AngleLerp(v2.ToRotation(), 0.5f);
						npc.localAI[1] += 0.05f;
						if (npc.localAI[1] > 1f)
						{
							npc.localAI[1] = 1f;
						}
						if (num1190 == num1191 - 35f)
						{
							Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6);
						}
						if ((num1190 == num1191 - 14f || num1190 == num1191 - 7f || num1190 == num1191 - 10f || num1190 == num1191 - 3f || num1190 == num1191) && Main.netMode != 1)
						{
							Vector2 vector167 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), vector152 * npc.localAI[1]);
							Vector2 vector168 = Vector2.Normalize(v2) * 8f;
							Projectile.NewProjectile(npc.Center.X + vector167.X, npc.Center.Y + vector167.Y, vector168.X, vector168.Y, ProjectileID.PhantasmalBolt, 55, 0f, Main.myPlayer);
						}
					}
					if (flag81)
					{
						Vector2 center19 = Main.npc[(int)npc.ai[3]].Center;
						Vector2 value17 = new Vector2(220f * num1189, -60f) + center19;
						Vector2 vector169 = value17 + new Vector2(num1189 * 110f, -150f);
						Vector2 max = vector169 + new Vector2(num1189 * 370f, 150f);
						if (vector169.X > max.X)
						{
							Utils.Swap(ref vector169.X, ref max.X);
						}
						if (vector169.Y > max.Y)
						{
							Utils.Swap(ref vector169.Y, ref max.Y);
						}
						Vector2 value18 = Vector2.Clamp(npc.Center + npc.velocity, vector169, max);
						if (value18 != npc.Center + npc.velocity)
						{
							npc.Center = value18 - npc.velocity;
						}
					}
					int num1207 = num1192 * 7;
					if ((double)num1207 > npc.frameCounter)
					{
						npc.frameCounter++;
					}
					if ((double)num1207 < npc.frameCounter)
					{
						npc.frameCounter--;
					}
					if (npc.frameCounter < 0.0)
					{
						npc.frameCounter = 0.0;
					}
					if (npc.frameCounter > 21.0)
					{
						npc.frameCounter = 21.0;
					}
					int num1208 = 0;
					if (flag80)
					{
						num1208 = 0;
					}
					switch (num1208)
					{
						case 1:
							if (npc.ai[0] == 0f)
							{
								if ((npc.ai[1] += 1f) >= 20f)
								{
									npc.ai[1] = 0f;
									npc.ai[0] = 1f;
									npc.netUpdate = true;
								}
								npc.velocity = Vector2.UnitX * 4f;
							}
							else if (npc.ai[0] == 1f)
							{
								if ((npc.ai[1] += 1f) >= 20f)
								{
									npc.ai[1] = 0f;
									npc.ai[0] = 2f;
									npc.netUpdate = true;
								}
								npc.velocity = Vector2.UnitX * -4f;
							}
							else if (npc.ai[0] == 2f || npc.ai[0] == 4f)
							{
								if ((npc.ai[1] += 1f) >= 20f)
								{
									npc.ai[1] = 0f;
									npc.ai[0]++;
									npc.netUpdate = true;
								}
								npc.velocity = Vector2.UnitY * -4f * (flag80 ? 1 : (-1));
							}
							else
							{
								if (npc.ai[0] != 3f && npc.ai[0] != 5f)
								{
									break;
								}
								if ((npc.ai[1] += 1f) >= 20f)
								{
									npc.ai[1] = 0f;
									npc.ai[0]++;
									if (npc.ai[0] == 6f)
									{
										npc.ai[0] = 0f;
									}
									npc.netUpdate = true;
								}
								npc.velocity = Vector2.UnitY * 4f * (flag80 ? 1 : (-1));
							}
							break;
						case 2:
							{
								Vector2 vector170 = new Vector2(30f, 66f);
								npc.TargetClosest(faceTarget: false);
								Vector2 v3 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - npc.Center;
								float num1209 = v3.Length() / 200f;
								if (num1209 > 1f)
								{
									num1209 = 1f;
								}
								num1209 = 1f - num1209;
								num1209 *= 2f;
								if (num1209 > 1f)
								{
									num1209 = 1f;
								}
								npc.localAI[0] = v3.ToRotation();
								npc.localAI[1] = num1209;
								npc.localAI[1] = 1f;
								break;
							}
						case 3:
							{
								int num1215 = 7;
								int num1216 = 4;
								npc.ai[1]++;
								if (npc.ai[1] >= (float)(num1215 * num1216 * 10))
								{
									npc.ai[1] = 0f;
									break;
								}
								if (npc.ai[1] >= (float)(num1215 * num1216))
								{
									npc.localAI[1] -= 0.07f;
									if (npc.localAI[1] < 0f)
									{
										npc.localAI[1] = 0f;
									}
									break;
								}
								npc.localAI[1] += 0.05f;
								if (npc.localAI[1] > 0.75f)
								{
									npc.localAI[1] = 0.75f;
								}
								float num1217 = (float)Math.PI * 2f * (npc.ai[1] % (float)(num1215 * num1216)) / (float)(num1215 * num1216) - (float)Math.PI / 2f;
								npc.localAI[0] = new Vector2((float)Math.Cos(num1217) * vector152.X, (float)Math.Sin(num1217) * vector152.Y).ToRotation();
								if (npc.ai[1] % (float)num1216 == 0f)
								{
									Vector2 value21 = new Vector2(1f * (0f - num1189), 3f);
									Vector2 vector178 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), vector152 * npc.localAI[1]);
									Vector2 vector179 = npc.Center + Vector2.Normalize(vector178) * vector152.Length() * 0.4f + value21;
									Vector2 vector180 = Vector2.Normalize(vector178) * 8f;
									float ai2 = ((float)Math.PI * 2f * (float)Main.rand.NextDouble() - (float)Math.PI) / 30f + (float)Math.PI / 180f * num1189;
									Projectile.NewProjectile(vector179.X, vector179.Y, vector180.X, vector180.Y, ProjectileID.PhantasmalEye, 5, 0f, Main.myPlayer, 0f, ai2);
								}
								break;
							}
						case 4:
							{
								Vector2 center20 = Main.npc[(int)npc.ai[3]].Center;
								Vector2 value19 = new Vector2(220f * num1189, -60f) + center20;
								value19 += new Vector2(num1189 * 100f, -50f);
								Vector2 value20 = new Vector2(400f * num1189, -60f);
								npc.ai[1]++;
								if (npc.ai[1] < 30f)
								{
									Vector2 vector171 = value19 - npc.Center;
									if (vector171 != Vector2.Zero)
									{
										Vector2 vector172 = vector171;
										vector172.Normalize();
										npc.velocity = Vector2.SmoothStep(npc.velocity, vector172 * Math.Min(8f, vector171.Length()), 0.2f);
									}
								}
								else if (npc.ai[1] < 210f)
								{
									int num1210 = (int)npc.ai[1] - 30;
									if (num1210 % 30 == 0 && Main.netMode != 1)
									{
										Vector2 vector173 = new Vector2(5f * num1189, -8f);
										int num1211 = num1210 / 30;
										vector173.X += ((float)num1211 - 3.5f) * num1189 * 3f;
										vector173.Y += ((float)num1211 - 4.5f) * 1f;
										vector173 *= 1.2f;
										int num1212 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector173.X, vector173.Y, ProjectileID.PhantasmalSphere, 1, 1f, Main.myPlayer, 0f, npc.whoAmI);
									}
									Vector2 vector174 = Vector2.SmoothStep(value19, value19 + value20, (npc.ai[1] - 30f) / 180f) - npc.Center;
									if (vector174 != Vector2.Zero)
									{
										Vector2 vector175 = vector174;
										vector175.Normalize();
										npc.velocity = Vector2.Lerp(npc.velocity, vector175 * Math.Min(4f, vector174.Length()), 0.1f);
									}
								}
								else if (npc.ai[1] < 270f)
								{
									npc.velocity *= 0.9f;
								}
								else if (npc.ai[1] < 300f)
								{
									if (npc.ai[1] == 270f && Main.netMode != 1)
									{
										int num1213 = Player.FindClosest(npc.position, npc.width, npc.height);
										Vector2 velocity6 = Vector2.Normalize(Main.player[num1213].Center - (npc.Center + Vector2.UnitY * -350f));
										if (float.IsNaN(velocity6.X) || float.IsNaN(velocity6.Y))
										{
											velocity6 = Vector2.UnitY;
										}
										velocity6 *= 12f;
										for (int num1214 = 0; num1214 < 1000; num1214++)
										{
											Projectile projectile4 = Main.projectile[num1214];
											if (projectile4.active && projectile4.type == 454 && projectile4.ai[1] == (float)npc.whoAmI && projectile4.ai[0] != -1f)
											{
												projectile4.ai[0] = -1f;
												projectile4.velocity = velocity6;
												projectile4.netUpdate = true;
											}
										}
									}
									Vector2 vector176 = Vector2.SmoothStep(value19, value19 + value20, 1f - (npc.ai[1] - 270f) / 30f) - npc.Center;
									if (vector176 != Vector2.Zero)
									{
										Vector2 vector177 = vector176;
										vector177.Normalize();
										npc.velocity = Vector2.Lerp(npc.velocity, vector177 * Math.Min(14f, vector176.Length()), 0.1f);
									}
								}
								else
								{
									npc.ai[1] = 0f;
								}
								break;
							}
						case 5:
							npc.dontTakeDamage = true;
							npc.ai[1]++;
							if (npc.ai[1] >= 40f)
							{
								npc.ai[1] = 0f;
							}
							break;
					}
				}
				else if (npc.aiStyle == 79)
				{
					if (!Main.npc[(int)npc.ai[3]].active || Main.npc[(int)npc.ai[3]].type != NPCID.MoonLordCore)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.active = false;
					}
					npc.dontTakeDamage = (npc.localAI[3] >= 15f);
					npc.velocity = Vector2.Zero;
					npc.Center = Main.npc[(int)npc.ai[3]].Center + new Vector2(0f, -400f);
					Vector2 value22 = new Vector2(27f, 59f);
					float num1218 = 0f;
					float num1219 = 0f;
					int num1220 = 0;
					int num1221 = 0;
					if (npc.ai[0] >= 0f)
					{
						float num1222 = npc.ai[0];
						npc.ai[1]++;
						int num1223 = (int)Main.npc[(int)npc.ai[3]].ai[2];
						int num1224 = 2;
						int num1225 = 0;
						int num1226 = 0;
						for (; num1225 < 5; num1225++)
						{
							num1219 = NPC.MoonLordAttacksArray[num1223, num1224, 1, num1225];
							if (!(num1219 + (float)num1226 <= npc.ai[1]))
							{
								break;
							}
							num1226 += (int)num1219;
						}
						if (num1225 == 5)
						{
							num1225 = 0;
							npc.ai[1] = 0f;
							num1219 = NPC.MoonLordAttacksArray[num1223, num1224, 1, num1225];
							num1226 = 0;
						}
						npc.ai[0] = NPC.MoonLordAttacksArray[num1223, num1224, 0, num1225];
						num1218 = (int)npc.ai[1] - num1226;
						if (npc.ai[0] != num1222)
						{
							npc.netUpdate = true;
						}
					}
					if (npc.ai[0] == -3f)
					{
						npc.damage = 0;
						npc.dontTakeDamage = true;
						npc.rotation = MathHelper.Lerp(npc.rotation, (float)Math.PI / 12f, 0.07f);
						npc.ai[1]++;
						if (npc.ai[1] >= 32f)
						{
							npc.ai[1] = 0f;
						}
						if (npc.ai[1] < 0f)
						{
							npc.ai[1] = 0f;
						}
						if (npc.localAI[2] < 14f)
						{
							npc.localAI[2]++;
						}
					}
					else if (npc.ai[0] == -2f)
					{
						if (Main.npc[(int)npc.ai[3]].ai[0] == 2f)
						{
							npc.ai[0] = -3f;
							return false;
						}
						npc.damage = 80;
						npc.dontTakeDamage = true;
						npc.ai[1]++;
						if (npc.ai[1] >= 32f)
						{
							npc.ai[1] = 0f;
						}
						if (npc.ai[1] < 0f)
						{
							npc.ai[1] = 0f;
						}
						npc.ai[2]++;
						if (npc.ai[2] >= 555f)
						{
							npc.ai[2] = 0f;
						}
						if (npc.ai[2] >= 120f)
						{
							num1218 = npc.ai[2] - 120f;
							num1219 = 555f;
							num1220 = 2;
							Vector2 value23 = new Vector2(0f, 216f);
							if (num1218 == 0f && Main.netMode != 1)
							{
								Vector2 value24 = npc.Center + value23;
								for (int num1227 = 0; num1227 < 255; num1227++)
								{
									Player player6 = Main.player[num1227];
									if (player6.active && !player6.dead && Vector2.Distance(player6.Center, value24) <= 3000f)
									{
										Vector2 value25 = Main.player[npc.target].Center - value24;
										if (value25 != Vector2.Zero)
										{
											value25.Normalize();
										}
										Projectile.NewProjectile(value24.X, value24.Y, value25.X, value25.Y, 456, 0, 0f, Main.myPlayer, npc.whoAmI + 1, num1227);
									}
								}
							}
							if ((num1218 == 120f || num1218 == 180f || num1218 == 240f) && Main.netMode != 1)
							{
								for (int num1228 = 0; num1228 < 1000; num1228++)
								{
									Projectile projectile5 = Main.projectile[num1228];
									if (projectile5.active && projectile5.type == 456 && Main.player[(int)projectile5.ai[1]].FindBuffIndex(145) != -1)
									{
										Vector2 center21 = Main.player[npc.target].Center;
										int num1229 = NPC.NewNPC((int)center21.X, (int)center21.Y, 401);
										Main.npc[num1229].netUpdate = true;
										Main.npc[num1229].ai[0] = npc.whoAmI + 1;
										Main.npc[num1229].ai[1] = num1228;
									}
								}
							}
						}
					}
					else if (npc.ai[0] == 0f)
					{
						num1221 = 3;
						npc.TargetClosest(faceTarget: false);
						Vector2 v4 = Main.player[npc.target].Center - npc.Center - new Vector2(0f, -22f);
						float num1230 = v4.Length() / 500f;
						if (num1230 > 1f)
						{
							num1230 = 1f;
						}
						num1230 = 1f - num1230;
						num1230 *= 2f;
						if (num1230 > 1f)
						{
							num1230 = 1f;
						}
						npc.localAI[0] = v4.ToRotation();
						npc.localAI[1] = num1230;
						npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
					}
					if (npc.ai[0] == 1f)
					{
						if (num1218 < 180f)
						{
							npc.localAI[1] -= 0.05f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							if (num1218 >= 60f)
							{
								Vector2 center22 = npc.Center;
								int num1231 = 0;
								if (num1218 >= 120f)
								{
									num1231 = 1;
								}
								for (int num1232 = 0; num1232 < 1 + num1231; num1232++)
								{
									int num1233 = 229;
									float num1234 = 0.8f;
									if (num1232 % 2 == 1)
									{
										num1233 = 229;
										num1234 = 1.65f;
									}
									Vector2 vector181 = center22 + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * value22 / 2f;
									int num1235 = Dust.NewDust(vector181 - Vector2.One * 8f, 16, 16, num1233, npc.velocity.X / 2f, npc.velocity.Y / 2f);
									Main.dust[num1235].velocity = Vector2.Normalize(center22 - vector181) * 3.5f * (10f - (float)num1231 * 2f) / 10f;
									Main.dust[num1235].noGravity = true;
									Main.dust[num1235].scale = num1234;
									Main.dust[num1235].customData = npc;
								}
							}
						}
						else if (num1218 < num1219 - 15f)
						{
							if (num1218 == 180f && Main.netMode != 1)
							{
								npc.TargetClosest(faceTarget: false);
								Vector2 spinningpoint8 = Main.player[npc.target].Center - npc.Center;
								spinningpoint8.Normalize();
								float num1236 = -1f;
								if (spinningpoint8.X < 0f)
								{
									num1236 = 1f;
								}
								spinningpoint8 = spinningpoint8.RotatedBy((0f - num1236) * ((float)Math.PI * 2f) / 6f);
								Projectile.NewProjectile(npc.Center.X, npc.Center.Y, spinningpoint8.X, spinningpoint8.Y, 455, 75, 0f, Main.myPlayer, num1236 * ((float)Math.PI * 2f) / 540f, npc.whoAmI);
								npc.ai[2] = (spinningpoint8.ToRotation() + (float)Math.PI * 3f) * num1236;
								npc.netUpdate = true;
							}
							npc.localAI[1] += 0.05f;
							if (npc.localAI[1] > 1f)
							{
								npc.localAI[1] = 1f;
							}
							float num1237 = (npc.ai[2] >= 0f).ToDirectionInt();
							float num1238 = npc.ai[2];
							if (num1238 < 0f)
							{
								num1238 *= -1f;
							}
							num1238 += (float)Math.PI * -3f;
							num1238 += num1237 * ((float)Math.PI * 2f) / 540f;
							npc.localAI[0] = num1238;
							npc.ai[2] = (num1238 + (float)Math.PI * 3f) * num1237;
						}
						else
						{
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							num1221 = 3;
						}
					}
					else if (npc.ai[0] == 2f)
					{
						num1220 = 2;
						num1221 = 3;
						Vector2 value26 = new Vector2(0f, 216f);
						if (num1218 == 0f && Main.netMode != 1)
						{
							Vector2 value27 = npc.Center + value26;
							for (int num1239 = 0; num1239 < 255; num1239++)
							{
								Player player7 = Main.player[num1239];
								if (player7.active && !player7.dead && Vector2.Distance(player7.Center, value27) <= 3000f)
								{
									Vector2 value28 = Main.player[npc.target].Center - value27;
									if (value28 != Vector2.Zero)
									{
										value28.Normalize();
									}
									Projectile.NewProjectile(value27.X, value27.Y, value28.X, value28.Y, 456, 0, 0f, Main.myPlayer, npc.whoAmI + 1, num1239);
								}
							}
						}
						if ((num1218 == 120f || num1218 == 180f || num1218 == 240f) && Main.netMode != 1)
						{
							for (int num1240 = 0; num1240 < 1000; num1240++)
							{
								Projectile projectile6 = Main.projectile[num1240];
								if (projectile6.active && projectile6.type == 456 && Main.player[(int)projectile6.ai[1]].FindBuffIndex(145) != -1)
								{
									Vector2 center23 = Main.player[npc.target].Center;
									int num1241 = NPC.NewNPC((int)center23.X, (int)center23.Y, 401);
									Main.npc[num1241].netUpdate = true;
									Main.npc[num1241].ai[0] = npc.whoAmI + 1;
									Main.npc[num1241].ai[1] = num1240;
								}
							}
						}
					}
					else if (npc.ai[0] == 3f)
					{
						if ((double)num1218 == 1.0)
						{
							npc.TargetClosest(faceTarget: false);
							npc.netUpdate = true;
						}
						Vector2 v5 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
						npc.localAI[0] = npc.localAI[0].AngleLerp(v5.ToRotation(), 0.5f);
						npc.localAI[1] += 0.05f;
						if (npc.localAI[1] > 1f)
						{
							npc.localAI[1] = 1f;
						}
						if (num1218 == num1219 - 35f)
						{
							Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6);
						}
						if ((num1218 == num1219 - 14f || num1218 == num1219 - 7f || num1218 == num1219) && Main.netMode != 1)
						{
							Vector2 vector182 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value22 * npc.localAI[1]);
							Vector2 vector183 = Vector2.Normalize(v5) * 8f;
							Projectile.NewProjectile(npc.Center.X + vector182.X, npc.Center.Y + vector182.Y, vector183.X, vector183.Y, 462, 30, 0f, Main.myPlayer);
						}
					}
					int num1242 = num1220 * 7;
					if ((float)num1242 > npc.localAI[2])
					{
						npc.localAI[2]++;
					}
					if ((float)num1242 < npc.localAI[2])
					{
						npc.localAI[2]--;
					}
					if (npc.localAI[2] < 0f)
					{
						npc.localAI[2] = 0f;
					}
					if (npc.localAI[2] > 14f)
					{
						npc.localAI[2] = 14f;
					}
					int num1243 = num1221 * 5;
					if ((float)num1243 > npc.localAI[3])
					{
						npc.localAI[3]++;
					}
					if ((float)num1243 < npc.localAI[3])
					{
						npc.localAI[3]--;
					}
					if (npc.localAI[3] < 0f)
					{
						npc.localAI[2] = 0f;
					}
					if (npc.localAI[3] > 15f)
					{
						npc.localAI[2] = 15f;
					}
					int num1244 = 0;
					if (num1244 == 1)
					{
						Vector2 vector184 = new Vector2(27f, 59f);
						npc.TargetClosest(faceTarget: false);
						Vector2 v6 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - npc.Center;
						float num1245 = v6.Length() / 200f;
						if (num1245 > 1f)
						{
							num1245 = 1f;
						}
						num1245 = 1f - num1245;
						num1245 *= 2f;
						if (num1245 > 1f)
						{
							num1245 = 1f;
						}
						npc.localAI[0] = v6.ToRotation();
						npc.localAI[1] = num1245;
						npc.localAI[1] = 1f;
					}
					if (num1244 == 2)
					{
						Vector2 vector185 = new Vector2(27f, 59f);
						float num1246 = (float)Math.PI * 2f * ((float)timeForVisualEffects % 600f) / 600f;
						npc.localAI[0] = new Vector2((float)Math.Cos(num1246) * vector185.X, (float)Math.Sin(num1246) * vector185.Y).ToRotation();
						npc.localAI[1] = 0.75f;
						if (npc.ai[1] == 0f)
						{
							Vector2 vector186 = num1246.ToRotationVector2();
							vector186 = Vector2.One;
							Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector186.X, vector186.Y, 455, 1, 0f, Main.myPlayer, (float)Math.PI / 300f, npc.whoAmI);
						}
						npc.ai[1]++;
						if (npc.ai[1] >= 600f)
						{
							npc.ai[1] = 0f;
						}
					}
					if (num1244 == 3)
					{
						Vector2 vector187 = new Vector2(0f, 216f);
						if (npc.ai[1] == 0f)
						{
							npc.TargetClosest(faceTarget: false);
							Vector2 vector188 = Main.player[npc.target].Center - npc.Center;
							vector188.Normalize();
							Projectile.NewProjectile(npc.Center.X + vector187.X, npc.Center.Y + vector187.Y, vector188.X, vector188.Y, 456, 0, 0f, Main.myPlayer, npc.whoAmI + 1, npc.target);
						}
						npc.ai[1]++;
						if (npc.ai[1] >= 600f)
						{
							npc.ai[1] = 0f;
						}
					}
					if (num1244 == 4)
					{
						Vector2 vector189 = new Vector2(27f, 59f);
						npc.TargetClosest(faceTarget: false);
						Vector2 v7 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
						npc.localAI[0] = npc.localAI[0].AngleLerp(v7.ToRotation(), 0.5f);
						npc.localAI[1] = 1f;
						npc.ai[1]++;
						if (npc.ai[1] == 55f)
						{
							Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6);
						}
						if (npc.ai[1] == 76f || npc.ai[1] == 83f || npc.ai[1] == 90f)
						{
							Vector2 vector190 = Utils.Vector2FromElipse(elipseSizes: new Vector2(27f, 59f) * npc.localAI[1], angleVector: npc.localAI[0].ToRotationVector2());
							Vector2 vector191 = Vector2.Normalize(v7) * 8f;
							Projectile.NewProjectile(npc.Center.X + vector190.X, npc.Center.Y + vector190.Y, vector191.X, vector191.Y, 462, 5, 0f, Main.myPlayer);
						}
						if (npc.ai[1] >= 90f)
						{
							npc.ai[1] = 0f;
						}
					}
				}
				else if (npc.aiStyle == 81)
				{
					if (Main.rand.Next(420) == 0)
					{
						Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, Main.rand.Next(100, 101));
					}
					Vector2 value29 = new Vector2(30f);
					if (!Main.npc[(int)npc.ai[3]].active || Main.npc[(int)npc.ai[3]].type != 398)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.active = false;
					}
					float num1251 = 0f;
					float num1252 = 0f;
					float num1253 = npc.ai[0];
					npc.ai[1]++;
					int num1254 = 0;
					int num1255 = 0;
					for (; num1254 < 10; num1254++)
					{
						num1252 = NPC.MoonLordAttacksArray2[1, num1254];
						if (!(num1252 + (float)num1255 <= npc.ai[1]))
						{
							break;
						}
						num1255 += (int)num1252;
					}
					if (num1254 == 10)
					{
						num1254 = 0;
						npc.ai[1] = 0f;
						num1252 = NPC.MoonLordAttacksArray2[1, num1254];
						num1255 = 0;
					}
					npc.ai[0] = NPC.MoonLordAttacksArray2[0, num1254];
					num1251 = (int)npc.ai[1] - num1255;
					if (npc.ai[0] != num1253)
					{
						npc.netUpdate = true;
					}
					if (npc.ai[0] == -1f)
					{
						npc.ai[1]++;
						if (npc.ai[1] > 180f)
						{
							npc.ai[1] = 0f;
						}
						float num1256 = 1f;
						if (npc.ai[1] < 60f)
						{
							num1256 = 0.75f;
							npc.localAI[0] = 0f;
							npc.localAI[1] = (float)Math.Sin(npc.ai[1] * ((float)Math.PI * 2f) / 15f) * 0.35f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[0] = (float)Math.PI;
							}
						}
						else if (npc.ai[1] < 120f)
						{
							num1256 = 1f;
							if (npc.localAI[1] < 0.5f)
							{
								npc.localAI[1] += 0.025f;
							}
							npc.localAI[0] += (float)Math.PI / 15f;
						}
						else
						{
							num1256 = 1.15f;
							npc.localAI[1] -= 0.05f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
						}
						npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], num1256, 0.3f);
					}
					if (npc.ai[0] == 0f)
					{
						npc.TargetClosest(faceTarget: false);
						Vector2 v8 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
						npc.localAI[0] = npc.localAI[0].AngleLerp(v8.ToRotation(), 0.5f);
						npc.localAI[1] += 0.05f;
						if (npc.localAI[1] > 0.7f)
						{
							npc.localAI[1] = 0.7f;
						}
						npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
						float scaleFactor7 = 24f;
						Vector2 center25 = npc.Center;
						Vector2 center26 = Main.player[npc.target].Center;
						Vector2 value30 = center26 - center25;
						Vector2 vector192 = value30 - Vector2.UnitY * 200f;
						vector192 = Vector2.Normalize(vector192) * scaleFactor7;
						int num1257 = 30;
						npc.velocity.X = (npc.velocity.X * (float)(num1257 - 1) + vector192.X) / (float)num1257;
						npc.velocity.Y = (npc.velocity.Y * (float)(num1257 - 1) + vector192.Y) / (float)num1257;
						float num1258 = 0.25f;
						for (int num1259 = 0; num1259 < 200; num1259++)
						{
							if (num1259 != npc.whoAmI && Main.npc[num1259].active && Main.npc[num1259].type == 400 && Vector2.Distance(npc.Center, Main.npc[num1259].Center) < 150f)
							{
								if (npc.position.X < Main.npc[num1259].position.X)
								{
									npc.velocity.X -= num1258;
								}
								else
								{
									npc.velocity.X += num1258;
								}
								if (npc.position.Y < Main.npc[num1259].position.Y)
								{
									npc.velocity.Y -= num1258;
								}
								else
								{
									npc.velocity.Y += num1258;
								}
							}
						}
					}
					else if (npc.ai[0] == 1f)
					{
						if (num1251 == 0f)
						{
							npc.TargetClosest(faceTarget: false);
							npc.netUpdate = true;
						}
						npc.velocity *= 0.95f;
						if (npc.velocity.Length() < 1f)
						{
							npc.velocity = Vector2.Zero;
						}
						Vector2 v9 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
						npc.localAI[0] = npc.localAI[0].AngleLerp(v9.ToRotation(), 0.5f);
						npc.localAI[1] += 0.05f;
						if (npc.localAI[1] > 1f)
						{
							npc.localAI[1] = 1f;
						}
						if (num1251 < 20f)
						{
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1.1f, 0.2f);
						}
						else
						{
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
						}
						if (num1251 == num1252 - 35f)
						{
							Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6);
						}
						if ((num1251 == num1252 - 14f || num1251 == num1252 - 7f || num1251 == num1252) && Main.netMode != 1)
						{
							Vector2 vector193 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value29 * npc.localAI[1]);
							Vector2 vector194 = Vector2.Normalize(v9) * 8f;
							Projectile.NewProjectile(npc.Center.X + vector193.X, npc.Center.Y + vector193.Y, vector194.X, vector194.Y, ProjectileID.PhantasmalBolt, 35, 0f, Main.myPlayer);
						}
					}
					else if (npc.ai[0] == 2f)
					{
						if (num1251 < 15f)
						{
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
							npc.velocity *= 0.8f;
							if (npc.velocity.Length() < 1f)
							{
								npc.velocity = Vector2.Zero;
							}
						}
						else if (num1251 < 75f)
						{
							float num1260 = (num1251 - 15f) / 10f;
							int num1261 = 0;
							int num1262 = 0;
							switch ((int)num1260)
							{
								case 0:
									num1261 = 0;
									num1262 = 2;
									break;
								case 1:
									num1261 = 2;
									num1262 = 5;
									break;
								case 2:
									num1261 = 5;
									num1262 = 3;
									break;
								case 3:
									num1261 = 3;
									num1262 = 1;
									break;
								case 4:
									num1261 = 1;
									num1262 = 4;
									break;
								case 5:
									num1261 = 4;
									num1262 = 0;
									break;
							}
							Vector2 spinningpoint9 = Vector2.UnitY * -30f;
							Vector2 value31 = spinningpoint9.RotatedBy((float)num1261 * ((float)Math.PI * 2f) / 6f);
							Vector2 value32 = spinningpoint9.RotatedBy((float)num1262 * ((float)Math.PI * 2f) / 6f);
							Vector2 vector195 = Vector2.Lerp(value31, value32, num1260 - (float)(int)num1260);
							float value33 = vector195.Length() / 30f;
							npc.localAI[0] = vector195.ToRotation();
							npc.localAI[1] = MathHelper.Lerp(npc.localAI[1], value33, 0.5f);
							for (int num1263 = 0; num1263 < 2; num1263++)
							{
								int num1264 = Dust.NewDust(npc.Center + vector195 - Vector2.One * 4f, 0, 0, 229);
								Dust dust = Main.dust[num1264];
								dust.velocity += vector195 / 15f;
								Main.dust[num1264].noGravity = true;
							}
							if ((num1251 - 15f) % 10f == 0f && Main.netMode != 1)
							{
								Vector2 vec4 = Vector2.Normalize(vector195);
								if (vec4.HasNaNs())
								{
									vec4 = Vector2.UnitY * -1f;
								}
								vec4 *= 4f;
								int num1265 = Projectile.NewProjectile(npc.Center.X + vector195.X, npc.Center.Y + vector195.Y, vec4.X, vec4.Y, 454, 55, 0f, Main.myPlayer, 30f, npc.whoAmI);
							}
						}
						else if (num1251 < 105f)
						{
							npc.localAI[0] = npc.localAI[0].AngleLerp(npc.ai[2] - (float)Math.PI / 2f, 0.2f);
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.75f, 0.2f);
							if (num1251 == 75f)
							{
								npc.TargetClosest(faceTarget: false);
								npc.netUpdate = true;
								npc.velocity = Vector2.UnitY * -7f;
								for (int num1266 = 0; num1266 < 1000; num1266++)
								{
									Projectile projectile7 = Main.projectile[num1266];
									if (projectile7.active && projectile7.type == 454 && projectile7.ai[1] == (float)npc.whoAmI && projectile7.ai[0] != -1f)
									{
										Projectile projectile8 = projectile7;
										projectile8.velocity += npc.velocity;
										projectile7.netUpdate = true;
									}
								}
							}
							npc.velocity.Y *= 0.96f;
							npc.ai[2] = (Main.player[npc.target].Center - npc.Center).ToRotation() + (float)Math.PI / 2f;
							npc.rotation = npc.rotation.AngleTowards(npc.ai[2], (float)Math.PI / 30f);
						}
						else if (num1251 < 120f)
						{
							Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 102);
							if (num1251 == 105f)
							{
								npc.netUpdate = true;
							}
							Vector2 velocity7 = (npc.ai[2] - (float)Math.PI / 2f).ToRotationVector2() * 12f;
							npc.velocity = velocity7 * 2f;
							for (int num1267 = 0; num1267 < 1000; num1267++)
							{
								Projectile projectile9 = Main.projectile[num1267];
								if (projectile9.active && projectile9.type == 454 && projectile9.ai[1] == (float)npc.whoAmI && projectile9.ai[0] != -1f)
								{
									projectile9.ai[0] = -1f;
									projectile9.velocity = velocity7;
									projectile9.netUpdate = true;
								}
							}
						}
						else
						{
							npc.velocity *= 0.92f;
							npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
						}
					}
					else if (npc.ai[0] == 3f)
					{
						if (num1251 < 15f)
						{
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
							npc.velocity *= 0.9f;
							if (npc.velocity.Length() < 1f)
							{
								npc.velocity = Vector2.Zero;
							}
						}
						else if (num1251 < 45f)
						{
							npc.localAI[0] = 0f;
							npc.localAI[1] = (float)Math.Sin((num1251 - 15f) * ((float)Math.PI * 2f) / 15f) * 0.5f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[0] = (float)Math.PI;
							}
						}
						else if (num1251 < 185f)
						{
							if (num1251 == 45f)
							{
								npc.ai[2] = (float)(Main.rand.Next(2) == 0).ToDirectionInt() * ((float)Math.PI * 2f) / 40f;
								npc.netUpdate = true;
							}
							if ((num1251 - 15f - 30f) % 40f == 0f)
							{
								npc.ai[2] *= 0.95f;
							}
							npc.localAI[0] += npc.ai[2];
							npc.localAI[1] += 0.05f;
							if (npc.localAI[1] > 1f)
							{
								npc.localAI[1] = 1f;
							}
							Vector2 vector196 = npc.localAI[0].ToRotationVector2() * value29 * npc.localAI[1];
							float scaleFactor8 = MathHelper.Lerp(8f, 20f, (num1251 - 15f - 30f) / 140f);
							npc.velocity = Vector2.Normalize(vector196) * scaleFactor8;
							npc.rotation = npc.rotation.AngleLerp(npc.velocity.ToRotation() + (float)Math.PI / 2f, 0.2f);
							if ((num1251 - 15f - 30f) % 10f == 0f && Main.netMode != 1)
							{
								Vector2 vector197 = npc.Center + Vector2.Normalize(vector196) * value29.Length() * 0.4f;
								Vector2 vector198 = Vector2.Normalize(vector196) * 8f;
								float ai3 = ((float)Math.PI * 2f * (float)Main.rand.NextDouble() - (float)Math.PI) / 30f + (float)Math.PI / 180f * npc.ai[2];
								Projectile.NewProjectile(vector197.X, vector197.Y, vector198.X, vector198.Y, ProjectileID.PhantasmalEye, 55, 0f, Main.myPlayer, 0f, ai3);
							}
						}
						else
						{
							npc.velocity *= 0.88f;
							npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
						}
					}
					else
					{
						if (npc.ai[0] != 4f)
						{
							return false;
						}
						if (num1251 == 0f)
						{
							npc.TargetClosest(faceTarget: false);
							npc.netUpdate = true;
						}
						if (num1251 < 180f)
						{
							npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
							npc.localAI[1] -= 0.05f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
							npc.velocity *= 0.95f;
							if (npc.velocity.Length() < 1f)
							{
								npc.velocity = Vector2.Zero;
							}
							if (!(num1251 >= 60f))
							{
								return false;
							}
							Vector2 center27 = npc.Center;
							int num1268 = 0;
							if (num1251 >= 120f)
							{
								num1268 = 1;
							}
							for (int num1269 = 0; num1269 < 1 + num1268; num1269++)
							{
								int num1270 = 229;
								float num1271 = 0.8f;
								if (num1269 % 2 == 1)
								{
									num1270 = 229;
									num1271 = 1.65f;
								}
								Vector2 vector199 = center27 + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * value29 / 2f;
								int num1272 = Dust.NewDust(vector199 - Vector2.One * 8f, 16, 16, num1270, npc.velocity.X / 2f, npc.velocity.Y / 2f);
								Main.dust[num1272].velocity = Vector2.Normalize(center27 - vector199) * 3.5f * (10f - (float)num1268 * 2f) / 10f;
								Main.dust[num1272].noGravity = true;
								Main.dust[num1272].scale = num1271;
								Main.dust[num1272].customData = npc;
							}
						}
						else if (num1251 < num1252 - 15f)
						{
							if (num1251 == 180f && Main.netMode != 1)
							{
								npc.TargetClosest(faceTarget: false);
								Vector2 spinningpoint10 = Main.player[npc.target].Center - npc.Center;
								spinningpoint10.Normalize();
								float num1273 = -1f;
								if (spinningpoint10.X < 0f)
								{
									num1273 = 1f;
								}
								spinningpoint10 = spinningpoint10.RotatedBy((0f - num1273) * ((float)Math.PI * 2f) / 6f);
								Projectile.NewProjectile(npc.Center.X, npc.Center.Y, spinningpoint10.X, spinningpoint10.Y, ProjectileID.PhantasmalDeathray, 50, 0f, Main.myPlayer, num1273 * ((float)Math.PI * 2f) / 540f, npc.whoAmI);
								if (NPC.CountNPCS(NPCID.MoonLordFreeEye) < 6) { int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.MoonLordFreeEye);
									NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
										}
								npc.ai[2] = (spinningpoint10.ToRotation() + (float)Math.PI * 3f) * num1273;
								npc.netUpdate = true;
							}
							npc.localAI[1] += 0.05f;
							if (npc.localAI[1] > 1f)
							{
								npc.localAI[1] = 1f;
							}
							float num1274 = (npc.ai[2] >= 0f).ToDirectionInt();
							float num1275 = npc.ai[2];
							if (num1275 < 0f)
							{
								num1275 *= -1f;
							}
							num1275 += (float)Math.PI * -3f;
							num1275 += num1274 * ((float)Math.PI * 2f) / 540f;
							npc.localAI[0] = num1275;
							npc.ai[2] = (num1275 + (float)Math.PI * 3f) * num1274;
						}
						else
						{
							npc.localAI[1] -= 0.07f;
							if (npc.localAI[1] < 0f)
							{
								npc.localAI[1] = 0f;
							}
						}
					}
				}
				else { return true; }
				return false;
			}
			else { return true; }
        }
    }
}
