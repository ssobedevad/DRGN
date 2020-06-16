using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeSkeletronPrime : GlobalNPC


    {

		private static int[] limbs = new int[4] { NPCID.PrimeVice, NPCID.PrimeSaw, NPCID.PrimeLaser, NPCID.PrimeCannon };
		private const int totalLimbs = 5;
		private static int shootCD;
        public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
				if (npc.aiStyle == 32)
				{
					
					
					int missingLimbs = totalLimbs;
					for (int i = 0; i < Main.npc.Length; i++)
					{ if (limbs.Contains(Main.npc[i].type) && Main.npc[i].active) { missingLimbs -= 1; } }
					
					if (shootCD > 0) { shootCD -= 1; }
					npc.damage = npc.defDamage;
					npc.defense = npc.defDefense;
					if (npc.ai[0] == 0f && Main.netMode != 1)
					{
						npc.TargetClosest();
						npc.ai[0] = 1f;
						int Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, NPCID.PrimeCannon, npc.whoAmI);
						Main.npc[Newnpc].ai[0] = -1f;
						Main.npc[Newnpc].ai[1] = npc.whoAmI;
						Main.npc[Newnpc].target = npc.target;
						Main.npc[Newnpc].netUpdate = true;
						Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, NPCID.PrimeSaw, npc.whoAmI);
						Main.npc[Newnpc].ai[0] = 1f;
						Main.npc[Newnpc].ai[1] = npc.whoAmI;
						Main.npc[Newnpc].target = npc.target;
						Main.npc[Newnpc].netUpdate = true;
						Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, NPCID.PrimeVice, npc.whoAmI);
						Main.npc[Newnpc].ai[0] = -1f;
						Main.npc[Newnpc].ai[1] = npc.whoAmI;
						Main.npc[Newnpc].target = npc.target;
						Main.npc[Newnpc].ai[3] = 150f;
						Main.npc[Newnpc].netUpdate = true;
						Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, NPCID.PrimeLaser, npc.whoAmI);
						Main.npc[Newnpc].ai[0] = 1f;
						Main.npc[Newnpc].ai[1] = npc.whoAmI;
						Main.npc[Newnpc].target = npc.target;
						Main.npc[Newnpc].netUpdate = true;
						Main.npc[Newnpc].ai[3] = 150f;
						Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, NPCID.PrimeLaser, npc.whoAmI);
						Main.npc[Newnpc].ai[0] = 0f;
						Main.npc[Newnpc].ai[1] = npc.whoAmI;
						Main.npc[Newnpc].target = npc.target;
						Main.npc[Newnpc].netUpdate = true;
						Main.npc[Newnpc].ai[3] = 150f;
					}
					if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
					{
						npc.TargetClosest();
						if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
						{
							npc.ai[1] = 3f;
						}
					}
					if (Main.dayTime && npc.ai[1] != 3f && npc.ai[1] != 2f)
					{
						npc.ai[1] = 2f;
						Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
					}
					if (npc.ai[1] == 0f)
					{
						npc.ai[2] += 1f;
						if (npc.ai[2] >= 300f)
						{
							npc.ai[2] = 0f;
							npc.ai[1] = 1f;
							npc.TargetClosest();
							npc.netUpdate = true;
						}
						Vector2 npCCenter = npc.Center;
						if (Collision.CanHit(npCCenter, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && shootCD == 0)
						{
							float Speed = 10f;
							
							float Xdiff = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - npCCenter.X + (float)Main.rand.Next(-20, 21);
							float Ydiff = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - npCCenter.Y + (float)Main.rand.Next(-20, 21);
							float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
							Mag = Speed / Mag;
							Xdiff *= Mag;
							Ydiff *= Mag;
							Vector2 ShootVel = new Vector2(Xdiff * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, Ydiff * 1f + (float)Main.rand.Next(-50, 51) * 0.01f);
							ShootVel.Normalize();
							ShootVel *= Speed;
							ShootVel += npc.velocity;
							Xdiff = ShootVel.X;
							Ydiff = ShootVel.Y;
							int dmg = npc.damage / 3;
							int ProjType = 270;
							npCCenter+= ShootVel * 5f;
							int ProjID = Projectile.NewProjectile(npCCenter.X, npCCenter.Y, Xdiff, Ydiff, ProjType, dmg, 0f, Main.myPlayer, -1f);
							Main.projectile[ProjID].timeLeft = 300;
							shootCD = 40;
						}
						npc.rotation = npc.velocity.X / 15f;
						float num471 = 0.1f;
						float num472 = 2f;
						float num473 = 0.1f;
						float num474 = 8f;
						if (Main.expertMode)
						{
							num471 = 0.03f;
							num472 = 4f;
							num473 = 0.07f;
							num474 = 9.5f;
						}
						if (npc.position.Y > Main.player[npc.target].position.Y - 200f)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.98f;
							}
							npc.velocity.Y -= num471;
							if (npc.velocity.Y > num472)
							{
								npc.velocity.Y = num472;
							}
						}
						else if (npc.position.Y < Main.player[npc.target].position.Y - 500f)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.98f;
							}
							npc.velocity.Y += num471;
							if (npc.velocity.Y < 0f - num472)
							{
								npc.velocity.Y = 0f - num472;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.98f;
							}
							npc.velocity.X -= num473;
							if (npc.velocity.X > num474)
							{
								npc.velocity.X = num474;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.98f;
							}
							npc.velocity.X += num473;
							if (npc.velocity.X < 0f - num474)
							{
								npc.velocity.X = 0f - num474;
							}
						}
					}
					else if (npc.ai[1] == 1f)
					{
						npc.defense *= 2;
						npc.damage *= 2;
						npc.ai[2] += 1f;
						int limbType = Main.rand.Next(limbs);
						if (missingLimbs > 0)
						{
							if (Main.rand.Next(400 / missingLimbs) == 1)
							{
								int Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, limbType, npc.whoAmI);
								if ( limbType == NPCID.PrimeCannon || limbType == NPCID.PrimeVice)
								{
									Main.npc[Newnpc].ai[0] = -1f;
								}
								if (limbType == NPCID.PrimeLaser || limbType == NPCID.PrimeVice)
								{
									Main.npc[Newnpc].ai[3] = 150f;

								}
								Main.npc[Newnpc].ai[1] = npc.whoAmI;
								Main.npc[Newnpc].target = npc.target;
								Main.npc[Newnpc].netUpdate = true;

							}
						}
						else if (missingLimbs == 5)
						{
							int Newnpc = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, Main.rand.Next(limbs), npc.whoAmI);
							if (limbType == NPCID.PrimeCannon || limbType == NPCID.PrimeVice)
							{
								Main.npc[Newnpc].ai[0] = -1f;
							}
							if (limbType == NPCID.PrimeLaser || limbType == NPCID.PrimeVice)
							{
								Main.npc[Newnpc].ai[3] = 150f;

							}
							Main.npc[Newnpc].ai[1] = npc.whoAmI;
							Main.npc[Newnpc].target = npc.target;
							Main.npc[Newnpc].netUpdate = true;
						}
						

						if (npc.ai[2] == 2f)
						{
							Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
						}
						if (npc.ai[2] >= 400f)
						{
							npc.ai[2] = 0f;
							npc.ai[1] = 0f;
						}
						npc.rotation += (float)npc.direction * 0.3f;
						Vector2 vector45 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num475 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector45.X;
						float num476 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector45.Y;
						float Mag = (float)Math.Sqrt(num475 * num475 + num476 * num476);
						float Speed = 2f;
						
							Speed = 8f;
							if (Mag > 150f)
							{
								Speed *= 1.05f;
							}
							if (Mag > 200f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 250f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 300f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 350f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 400f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 450f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 500f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 550f)
							{
								Speed *= 1.1f;
							}
							if (Mag > 600f)
							{
								Speed *= 1.1f;
							}
						
						Mag = Speed / Mag;
						npc.velocity.X = num475 * Mag;
						npc.velocity.Y = num476 * Mag;
					}
					else if (npc.ai[1] == 2f)
					{
						npc.damage = 1000;
						npc.defense = 9999;
						npc.rotation += (float)npc.direction * 0.3f;
						Vector2 vector46 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num479 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector46.X;
						float num480 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector46.Y;
						float num481 = (float)Math.Sqrt(num479 * num479 + num480 * num480);
						float num482 = 10f;
						num482 += num481 / 100f;
						if (num482 < 8f)
						{
							num482 = 8f;
						}
						if (num482 > 32f)
						{
							num482 = 32f;
						}
						num481 = num482 / num481;
						npc.velocity.X = num479 * num481;
						npc.velocity.Y = num480 * num481;
					}
					else if (npc.ai[1] == 3f)
					{
						npc.velocity.Y += 0.1f;
						if (npc.velocity.Y < 0f)
						{
							npc.velocity.Y *= 0.95f;
						}
						npc.velocity.X *= 0.95f;
						npc.timeLeft = npc.timeLeft > 500 ? 500 : npc.timeLeft - 1;
					}
				}
				else if (npc.aiStyle == 33)
				{
					Vector2 vector47 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num483 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector47.X;
					float num484 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector47.Y;
					float num485 = (float)Math.Sqrt(num483 * num483 + num484 * num484);
					if (npc.ai[2] != 99f)
					{
						if (num485 > 800f)
						{
							npc.ai[2] = 99f;
						}
					}
					else if (num485 < 400f)
					{
						npc.ai[2] = 0f;
					}
					npc.spriteDirection = -(int)npc.ai[0];
					if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != 32)
					{
						npc.ai[2] += 10f;
						if (npc.ai[2] > 50f || Main.netMode != 2)
						{
							npc.life = -1;
							npc.HitEffect();
							npc.active = false;
						}
					}
					if (npc.ai[2] == 99f)
					{
						if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.96f;
							}
							npc.velocity.Y -= 0.1f;
							if (npc.velocity.Y > 8f)
							{
								npc.velocity.Y = 8f;
							}
						}
						else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.96f;
							}
							npc.velocity.Y += 0.1f;
							if (npc.velocity.Y < -8f)
							{
								npc.velocity.Y = -8f;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.96f;
							}
							npc.velocity.X -= 0.5f;
							if (npc.velocity.X > 12f)
							{
								npc.velocity.X = 12f;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.96f;
							}
							npc.velocity.X += 0.5f;
							if (npc.velocity.X < -12f)
							{
								npc.velocity.X = -12f;
							}
						}
					}
					else if (npc.ai[2] == 0f || npc.ai[2] == 3f)
					{
						if (Main.npc[(int)npc.ai[1]].ai[1] == 3f)
						{
							npc.timeLeft=(npc.timeLeft>10)?10:npc.timeLeft-1;
						}
						if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
						{
							npc.TargetClosest();
							if (Main.player[npc.target].dead)
							{
								npc.velocity.Y += 0.1f;
								if (npc.velocity.Y > 16f)
								{
									npc.velocity.Y = 16f;
								}
							}
							else
							{
								Vector2 vector48 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								float num486 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector48.X;
								float num487 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector48.Y;
								float num488 = (float)Math.Sqrt(num486 * num486 + num487 * num487);
								num488 = 7f / num488;
								num486 *= num488;
								num487 *= num488;
								npc.rotation = (float)Math.Atan2(num487, num486) - 1.57f;
								if (npc.velocity.X > num486)
								{
									if (npc.velocity.X > 0f)
									{
										npc.velocity.X *= 0.97f;
									}
									npc.velocity.X -= 0.05f;
								}
								if (npc.velocity.X < num486)
								{
									if (npc.velocity.X < 0f)
									{
										npc.velocity.X *= 0.97f;
									}
									npc.velocity.X += 0.05f;
								}
								if (npc.velocity.Y > num487)
								{
									if (npc.velocity.Y > 0f)
									{
										npc.velocity.Y *= 0.97f;
									}
									npc.velocity.Y -= 0.05f;
								}
								if (npc.velocity.Y < num487)
								{
									if (npc.velocity.Y < 0f)
									{
										npc.velocity.Y *= 0.97f;
									}
									npc.velocity.Y += 0.05f;
								}
							}
							npc.ai[3] += 1f;
							if (npc.ai[3] >= 600f)
							{
								npc.ai[2] = 0f;
								npc.ai[3] = 0f;
								npc.netUpdate = true;
							}
						}
						else
						{
							npc.ai[3] += 1f;
							if (npc.ai[3] >= 300f)
							{
								npc.ai[2] += 1f;
								npc.ai[3] = 0f;
								npc.netUpdate = true;
							}
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 320f)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.04f;
								if (npc.velocity.Y > 3f)
								{
									npc.velocity.Y = 3f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 260f)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.04f;
								if (npc.velocity.Y < -3f)
								{
									npc.velocity.Y = -3f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X -= 0.3f;
								if (npc.velocity.X > 12f)
								{
									npc.velocity.X = 12f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 250f)
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X += 0.3f;
								if (npc.velocity.X < -12f)
								{
									npc.velocity.X = -12f;
								}
							}
						}
						Vector2 vector49 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num489 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector49.X;
						float num490 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector49.Y;
						float num491 = (float)Math.Sqrt(num489 * num489 + num490 * num490);
						npc.rotation = (float)Math.Atan2(num490, num489) + 1.57f;
					}
					else if (npc.ai[2] == 1f)
					{
						Vector2 vector50 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num492 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector50.X;
						float num493 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector50.Y;
						float num494 = (float)Math.Sqrt(num492 * num492 + num493 * num493);
						npc.rotation = (float)Math.Atan2(num493, num492) + 1.57f;
						npc.velocity.X *= 0.95f;
						npc.velocity.Y -= 0.1f;
						if (npc.velocity.Y < -8f)
						{
							npc.velocity.Y = -8f;
						}
						if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 200f)
						{
							npc.TargetClosest();
							npc.ai[2] = 2f;
							vector50 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							num492 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector50.X;
							num493 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector50.Y;
							num494 = (float)Math.Sqrt(num492 * num492 + num493 * num493);
							num494 = 22f / num494;
							npc.velocity.X = num492 * num494;
							npc.velocity.Y = num493 * num494;
							npc.netUpdate = true;
						}
					}
					else if (npc.ai[2] == 2f)
					{
						if (npc.position.Y > Main.player[npc.target].position.Y || npc.velocity.Y < 0f)
						{
							npc.ai[2] = 3f;
						}
					}
					else if (npc.ai[2] == 4f)
					{
						npc.TargetClosest();
						Vector2 vector51 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num495 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector51.X;
						float num496 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector51.Y;
						float num497 = (float)Math.Sqrt(num495 * num495 + num496 * num496);
						num497 = 7f / num497;
						num495 *= num497;
						num496 *= num497;
						if (npc.velocity.X > num495)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.97f;
							}
							npc.velocity.X -= 0.05f;
						}
						if (npc.velocity.X < num495)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.97f;
							}
							npc.velocity.X += 0.05f;
						}
						if (npc.velocity.Y > num496)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.97f;
							}
							npc.velocity.Y -= 0.05f;
						}
						if (npc.velocity.Y < num496)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.97f;
							}
							npc.velocity.Y += 0.05f;
						}
						npc.ai[3] += 1f;
						if (npc.ai[3] >= 600f)
						{
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.netUpdate = true;
						}
						vector51 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						num495 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector51.X;
						num496 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector51.Y;
						num497 = (float)Math.Sqrt(num495 * num495 + num496 * num496);
						npc.rotation = (float)Math.Atan2(num496, num495) + 1.57f;
					}
					else if (npc.ai[2] == 5f && ((npc.velocity.X > 0f && npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2)) || (npc.velocity.X < 0f && npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))))
					{
						npc.ai[2] = 0f;
					}
				}



				// prime cannon
				else if (npc.aiStyle == 34)
				{
					npc.spriteDirection = -(int)npc.ai[0];
					Vector2 vector52 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num498 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector52.X;
					float num499 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector52.Y;
					float num500 = (float)Math.Sqrt(num498 * num498 + num499 * num499);
					if (npc.ai[2] != 99f)
					{
						if (num500 > 800f)
						{
							npc.ai[2] = 99f;
						}
					}
					else if (num500 < 400f)
					{
						npc.ai[2] = 0f;
					}
					if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != 32)
					{
						npc.ai[2] += 10f;
						if (npc.ai[2] > 50f || Main.netMode != 2)
						{
							npc.life = -1;
							npc.HitEffect();
							npc.active = false;
						}
					}
					if (npc.ai[2] == 99f)
					{
						if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.96f;
							}
							npc.velocity.Y -= 0.1f;
							if (npc.velocity.Y > 8f)
							{
								npc.velocity.Y = 8f;
							}
						}
						else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.96f;
							}
							npc.velocity.Y += 0.1f;
							if (npc.velocity.Y < -8f)
							{
								npc.velocity.Y = -8f;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.96f;
							}
							npc.velocity.X -= 0.5f;
							if (npc.velocity.X > 12f)
							{
								npc.velocity.X = 12f;
							}
						}
						if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.96f;
							}
							npc.velocity.X += 0.5f;
							if (npc.velocity.X < -12f)
							{
								npc.velocity.X = -12f;
							}
						}
					}
					else if (npc.ai[2] == 0f || npc.ai[2] == 3f)
					{
						if (Main.npc[(int)npc.ai[1]].ai[1] == 3f)
						{
							npc.timeLeft=(npc.timeLeft>10)?10:npc.timeLeft-1;;
						}
						if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
						{
							npc.TargetClosest();
							npc.TargetClosest();
							if (Main.player[npc.target].dead)
							{
								npc.velocity.Y += 0.1f;
								if (npc.velocity.Y > 16f)
								{
									npc.velocity.Y = 16f;
								}
							}
							else
							{
								Vector2 vector53 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								float num501 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector53.X;
								float num502 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector53.Y;
								float num503 = (float)Math.Sqrt(num501 * num501 + num502 * num502);
								num503 = 12f / num503;
								num501 *= num503;
								num502 *= num503;
								npc.rotation = (float)Math.Atan2(num502, num501) - 1.57f;
								if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 2f)
								{
									npc.rotation = (float)Math.Atan2(num502, num501) - 1.57f;
									npc.velocity.X = num501;
									npc.velocity.Y = num502;
									npc.netUpdate = true;
								}
								else
								{
									npc.velocity *= 0.97f;
								}
								npc.ai[3] += 1f;
								if (npc.ai[3] >= 600f)
								{
									npc.ai[2] = 0f;
									npc.ai[3] = 0f;
									npc.netUpdate = true;
								}
							}
						}
						else
						{
							npc.ai[3] += 1f;
							if (npc.ai[3] >= 300f)
							{
								npc.ai[2] += 1f;
								npc.ai[3] = 0f;
								npc.netUpdate = true;
							}
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 300f)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.1f;
								if (npc.velocity.Y > 3f)
								{
									npc.velocity.Y = 3f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230f)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.1f;
								if (npc.velocity.Y < -3f)
								{
									npc.velocity.Y = -3f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) + 250f)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.94f;
								}
								npc.velocity.X -= 0.3f;
								if (npc.velocity.X > 9f)
								{
									npc.velocity.X = 9f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2))
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.94f;
								}
								npc.velocity.X += 0.2f;
								if (npc.velocity.X < -8f)
								{
									npc.velocity.X = -8f;
								}
							}
						}
						Vector2 vector54 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num504 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector54.X;
						float num505 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector54.Y;
						float num506 = (float)Math.Sqrt(num504 * num504 + num505 * num505);
						npc.rotation = (float)Math.Atan2(num505, num504) + 1.57f;
					}
					else if (npc.ai[2] == 1f)
					{
						if (npc.velocity.Y > 0f)
						{
							npc.velocity.Y *= 0.9f;
						}
						Vector2 vector55 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num507 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 280f * npc.ai[0] - vector55.X;
						float num508 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector55.Y;
						float num509 = (float)Math.Sqrt(num507 * num507 + num508 * num508);
						npc.rotation = (float)Math.Atan2(num508, num507) + 1.57f;
						npc.velocity.X = (npc.velocity.X * 5f + Main.npc[(int)npc.ai[1]].velocity.X) / 6f;
						npc.velocity.X += 0.5f;
						npc.velocity.Y -= 0.5f;
						if (npc.velocity.Y < -9f)
						{
							npc.velocity.Y = -9f;
						}
						if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 280f)
						{
							npc.TargetClosest();
							npc.ai[2] = 2f;
							vector55 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							num507 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector55.X;
							num508 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector55.Y;
							num509 = (float)Math.Sqrt(num507 * num507 + num508 * num508);
							num509 = 20f / num509;
							npc.velocity.X = num507 * num509;
							npc.velocity.Y = num508 * num509;
							npc.netUpdate = true;
						}
					}
					else if (npc.ai[2] == 2f)
					{
						if (npc.position.Y > Main.player[npc.target].position.Y || npc.velocity.Y < 0f)
						{
							if (npc.ai[3] >= 4f)
							{
								npc.ai[2] = 3f;
								npc.ai[3] = 0f;
							}
							else
							{
								npc.ai[2] = 1f;
								npc.ai[3] += 1f;
							}
						}
					}
					else if (npc.ai[2] == 4f)
					{
						Vector2 vector56 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num510 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector56.X;
						float num511 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector56.Y;
						float num512 = (float)Math.Sqrt(num510 * num510 + num511 * num511);
						npc.rotation = (float)Math.Atan2(num511, num510) + 1.57f;
						npc.velocity.Y = (npc.velocity.Y * 5f + Main.npc[(int)npc.ai[1]].velocity.Y) / 6f;
						npc.velocity.X += 0.5f;
						if (npc.velocity.X > 12f)
						{
							npc.velocity.X = 12f;
						}
						if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 500f || npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) + 500f)
						{
							npc.TargetClosest();
							npc.ai[2] = 5f;
							vector56 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							num510 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector56.X;
							num511 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector56.Y;
							num512 = (float)Math.Sqrt(num510 * num510 + num511 * num511);
							num512 = 17f / num512;
							npc.velocity.X = num510 * num512;
							npc.velocity.Y = num511 * num512;
							npc.netUpdate = true;
						}
					}
					else if (npc.ai[2] == 5f && npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
					{
						if (npc.ai[3] >= 4f)
						{
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
						}
						else
						{
							npc.ai[2] = 4f;
							npc.ai[3] += 1f;
						}
					}
				}

				//prime saw
				else if (npc.aiStyle == 35)
				{
					npc.spriteDirection = -(int)npc.ai[0];
					if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != 32)
					{
						npc.ai[2] += 10f;
						if (npc.ai[2] > 50f || Main.netMode != 2)
						{
							npc.life = -1;
							npc.HitEffect();
							npc.active = false;
						}
					}
					if (npc.ai[2] == 0f)
					{
						if (Main.npc[(int)npc.ai[1]].ai[1] == 3f)
						{
							npc.timeLeft=(npc.timeLeft>10)?10:npc.timeLeft-1;;
						}
						if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
						{
							npc.localAI[0] += 2f;
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.07f;
								if (npc.velocity.Y > 6f)
								{
									npc.velocity.Y = 6f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100f)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.07f;
								if (npc.velocity.Y < -6f)
								{
									npc.velocity.Y = -6f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X -= 0.1f;
								if (npc.velocity.X > 8f)
								{
									npc.velocity.X = 8f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X += 0.1f;
								if (npc.velocity.X < -8f)
								{
									npc.velocity.X = -8f;
								}
							}
						}
						else
						{
							npc.ai[3] += 1f;
							if (npc.ai[3] >= 600f)
							{
								npc.localAI[0] = 0f;
								npc.ai[2] = 1f;
								npc.ai[3] = 0f;
								npc.netUpdate = true;
							}
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 150f)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.04f;
								if (npc.velocity.Y > 3f)
								{
									npc.velocity.Y = 3f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 150f)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.04f;
								if (npc.velocity.Y < -3f)
								{
									npc.velocity.Y = -3f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) + 200f)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X -= 0.2f;
								if (npc.velocity.X > 8f)
								{
									npc.velocity.X = 8f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) + 160f)
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X += 0.2f;
								if (npc.velocity.X < -8f)
								{
									npc.velocity.X = -8f;
								}
							}
						}
						Vector2 vector57 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num513 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector57.X;
						float num514 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector57.Y;
						float num515 = (float)Math.Sqrt(num513 * num513 + num514 * num514);
						npc.rotation = (float)Math.Atan2(num514, num513) + 1.57f;
						if (Main.netMode != 1)
						{
							npc.localAI[0] += 1f;
							if (npc.localAI[0] > 140f)
							{
								npc.localAI[0] = 0f;
								float num516 = 12f;
								int num517 = 0;
								int num518 = 102;
								num515 = num516 / num515;
								num513 = (0f - num513) * num515;
								num514 = (0f - num514) * num515;
								num513 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num514 += (float)Main.rand.Next(-40, 41) * 0.01f;
								vector57.X += num513 * 4f;
								vector57.Y += num514 * 4f;
								int num519 = Projectile.NewProjectile(vector57.X, vector57.Y, num513, num514, num518, num517, 0f, Main.myPlayer);
							}
						}
					}
					else
					{
						if (npc.ai[2] != 1f)
						{
							return false ;
						}
						npc.ai[3] += 1f;
						if (npc.ai[3] >= 300f)
						{
							npc.localAI[0] = 0f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.netUpdate = true;
						}
						Vector2 vector58 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num520 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector58.X;
						float num521 = Main.npc[(int)npc.ai[1]].position.Y - vector58.Y;
						num521 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 80f - vector58.Y;
						float num522 = (float)Math.Sqrt(num520 * num520 + num521 * num521);
						num522 = 6f / num522;
						num520 *= num522;
						num521 *= num522;
						if (npc.velocity.X > num520)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.9f;
							}
							npc.velocity.X -= 0.04f;
						}
						if (npc.velocity.X < num520)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.9f;
							}
							npc.velocity.X += 0.04f;
						}
						if (npc.velocity.Y > num521)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.9f;
							}
							npc.velocity.Y -= 0.08f;
						}
						if (npc.velocity.Y < num521)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.9f;
							}
							npc.velocity.Y += 0.08f;
						}
						npc.TargetClosest();
						vector58 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						num520 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector58.X;
						num521 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector58.Y;
						num522 = (float)Math.Sqrt(num520 * num520 + num521 * num521);
						npc.rotation = (float)Math.Atan2(num521, num520) - 1.57f;
						if (Main.netMode != 1)
						{
							npc.localAI[0] += 1f;
							if (npc.localAI[0] > 20f)
							{
								npc.localAI[0] = 0f;
								float num523 = 10f;
								int num524 = 0;
								int num525 = 102;
								num522 = num523 / num522;
								num520 *= num522;
								num521 *= num522;
								num520 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num521 += (float)Main.rand.Next(-40, 41) * 0.01f;
								vector58.X += num520 * 4f;
								vector58.Y += num521 * 4f;
								int num526 = Projectile.NewProjectile(vector58.X, vector58.Y, num520, num521, num525, num524, 0f, Main.myPlayer);
							}
						}
					}
				}
				else if (npc.aiStyle == 36)
				{
					if (npc.ai[0] == 0)
					{ npc.spriteDirection = -1; }
					else
					{
						npc.spriteDirection = -(int)npc.ai[0];
					}
					if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != 32)
					{
						npc.ai[2] += 10f;
						if (npc.ai[2] > 50f || Main.netMode != 2)
						{
							npc.life = -1;
							npc.HitEffect();
							npc.active = false;
						}
					}
					if (npc.ai[2] == 0f || npc.ai[2] == 3f)
					{
						if (Main.npc[(int)npc.ai[1]].ai[1] == 3f)
						{
							npc.timeLeft=(npc.timeLeft>10)?10:npc.timeLeft-1;;
						}
						if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
						{
							npc.localAI[0] += 3f;
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.07f;
								if (npc.velocity.Y > 6f)
								{
									npc.velocity.Y = 6f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100f)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.07f;
								if (npc.velocity.Y < -6f)
								{
									npc.velocity.Y = -6f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X -= 0.1f;
								if (npc.velocity.X > 8f)
								{
									npc.velocity.X = 8f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X += 0.1f;
								if (npc.velocity.X < -8f)
								{
									npc.velocity.X = -8f;
								}
							}
						}
						else
						{
							npc.ai[3] += 1f;
							if (npc.ai[3] >= 800f)
							{
								npc.ai[2] += 1f;
								npc.ai[3] = 0f;
								npc.netUpdate = true;
							}
							if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - (50f * npc.ai[0])+ 50)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y -= 0.1f;
								if (npc.velocity.Y > 3f)
								{
									npc.velocity.Y = 3f;
								}
							}
							else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - (50f * npc.ai[0])+ 50)
							{
								if (npc.velocity.Y < 0f)
								{
									npc.velocity.Y *= 0.96f;
								}
								npc.velocity.Y += 0.1f;
								if (npc.velocity.Y < -3f)
								{
									npc.velocity.Y = -3f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 180f * npc.ai[0])
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X -= 0.14f;
								if (npc.velocity.X > 8f)
								{
									npc.velocity.X = 8f;
								}
							}
							if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 180f * npc.ai[0])
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X *= 0.96f;
								}
								npc.velocity.X += 0.14f;
								if (npc.velocity.X < -8f)
								{
									npc.velocity.X = -8f;
								}
							}
						}
						npc.TargetClosest();
						Vector2 vector59 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num527 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector59.X;
						float num528 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector59.Y;
						float num529 = (float)Math.Sqrt(num527 * num527 + num528 * num528);
						npc.rotation = (float)Math.Atan2(num528, num527) - 1.57f;
						if (Main.netMode != 1)
						{
							npc.localAI[0] += 1f;
							if (npc.localAI[0] > 80f)
							{
								npc.localAI[0] = 0f;
								float num530 = 8f;
								int num531 = 25;
								int num532 = 100;
								num529 = num530 / num529;
								num527 *= num529;
								num528 *= num529;
								num527 += (float)Main.rand.Next(-40, 41) * 0.05f;
								num528 += (float)Main.rand.Next(-40, 41) * 0.05f;
								vector59.X += num527 * 8f;
								vector59.Y += num528 * 8f;
								int num533 = Projectile.NewProjectile(vector59.X, vector59.Y, num527, num528, num532, num531, 0f, Main.myPlayer);
							}
						}
					}
					else
					{
						if (npc.ai[2] != 1f)
						{
							return false;
						}
						npc.ai[3] += 1f;
						if (npc.ai[3] >= 200f)
						{
							npc.localAI[0] = 0f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.netUpdate = true;
						}
						Vector2 vector60 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num534 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 350f - vector60.X;
						float num535 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 20f - vector60.Y;
						float num536 = (float)Math.Sqrt(num534 * num534 + num535 * num535);
						num536 = 7f / num536;
						num534 *= num536;
						num535 *= num536;
						if (npc.velocity.X > num534)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X *= 0.9f;
							}
							npc.velocity.X -= 0.1f;
						}
						if (npc.velocity.X < num534)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X *= 0.9f;
							}
							npc.velocity.X += 0.1f;
						}
						if (npc.velocity.Y > num535)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y *= 0.9f;
							}
							npc.velocity.Y -= 0.03f;
						}
						if (npc.velocity.Y < num535)
						{
							if (npc.velocity.Y < 0f)
							{
								npc.velocity.Y *= 0.9f;
							}
							npc.velocity.Y += 0.03f;
						}
						npc.TargetClosest();
						vector60 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						num534 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector60.X;
						num535 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector60.Y;
						num536 = (float)Math.Sqrt(num534 * num534 + num535 * num535);
						npc.rotation = (float)Math.Atan2(num535, num534) - 1.57f;
						if (Main.netMode == 1)
						{
							npc.localAI[0] += 1f;
							if (npc.localAI[0] > 80f)
							{
								npc.localAI[0] = 0f;
								float num537 = 10f;
								int num538 = 25;
								int num539 = 100;
								num536 = num537 / num536;
								num534 *= num536;
								num535 *= num536;
								num534 += (float)Main.rand.Next(-40, 41) * 0.05f;
								num535 += (float)Main.rand.Next(-40, 41) * 0.05f;
								vector60.X += num534 * 8f;
								vector60.Y += num535 * 8f;
								int num540 = Projectile.NewProjectile(vector60.X, vector60.Y, num534, num535, num539, num538, 0f, Main.myPlayer);
							}
						}
					}
				}
				else { return true; }
				return false;



			}
			return true;
		}
    }
}