using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeEOC : GlobalNPC 
    
    
    {

        public override void AI(NPC npc)
        {

			//// eye of cthulu
			if (npc.aiStyle == 4 && DRGNModWorld.MentalMode)
			{
				bool SecondPhase = false;
				if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.12)
				{
					SecondPhase = true;
				}
				bool ThrirdPhase = false;
				if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.04)
				{
					ThrirdPhase = true;
				}
				float DashDelay = 20f;
				if (ThrirdPhase)
				{
					DashDelay = 10f;
				}
				if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
				{
					npc.TargetClosest();
				}
				bool dead = Main.player[npc.target].dead;
				float Xdiff = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
				float Ydiff = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
				float rotation = (float)Math.Atan2(Ydiff, Xdiff) + 1.57f;
				if (rotation < 0f)
				{
					rotation += 6.283f;
				}
				else if ((double)rotation > 6.283)
				{
					rotation -= 6.283f;
				}
				float RotationDifference = 0f;
				if (npc.ai[0] == 0f && npc.ai[1] == 0f)
				{
					RotationDifference = 0.02f;
				}
				if (npc.ai[0] == 0f && npc.ai[1] == 2f && npc.ai[2] > 40f)
				{
					RotationDifference = 0.05f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 0f)
				{
					RotationDifference = 0.05f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 2f && npc.ai[2] > 40f)
				{
					RotationDifference = 0.08f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 4f && npc.ai[2] > DashDelay)
				{
					RotationDifference = 0.15f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 5f)
				{
					RotationDifference = 0.05f;
				}
				if (Main.expertMode)
				{
					RotationDifference *= 1.5f;
				}
				if (ThrirdPhase && Main.expertMode)
				{
					RotationDifference = 0f;
				}
				if (npc.rotation < rotation)
				{
					if ((double)(rotation - npc.rotation) > 3.1415)
					{
						npc.rotation -= RotationDifference;
					}
					else
					{
						npc.rotation += RotationDifference;
					}
				}
				else if (npc.rotation > rotation)
				{
					if ((double)(npc.rotation - rotation) > 3.1415)
					{
						npc.rotation += RotationDifference;
					}
					else
					{
						npc.rotation -= RotationDifference;
					}
				}
				if (npc.rotation > rotation - RotationDifference && npc.rotation < rotation + RotationDifference)
				{
					npc.rotation = rotation;
				}
				if (npc.rotation < 0f)
				{
					npc.rotation += 6.283f;
				}
				else if ((double)npc.rotation > 6.283)
				{
					npc.rotation -= 6.283f;
				}
				if (npc.rotation > rotation - RotationDifference && npc.rotation < rotation + RotationDifference)
				{
					npc.rotation = rotation;
				}
				if (Main.rand.Next(5) == 0)
				{
					int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
					Main.dust[DustID].velocity.X *= 0.5f;
					Main.dust[DustID].velocity.Y *= 0.1f;
				}
				if (Main.dayTime || dead)
				{
					npc.velocity.Y -= 0.04f;
					npc.timeLeft = 10;
					return;
				}
				if (npc.ai[0] == 0f)
				{
					if (npc.ai[1] == 0f)
					{
						float Speed = 5f;
						float Acceleration = 0.04f;
						if (Main.expertMode)
						{
							Acceleration = 0.15f;
							Speed = 7f;
						}
						if (DRGNModWorld.MentalMode)
						{
							Acceleration = 0.3f;
							Speed = 9f;
						}

						Vector2 NpcPos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float XDifference = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcPos.X;
						float YDifference = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 200f - NpcPos.Y;
						float Mag = (float)Math.Sqrt(XDifference * XDifference + YDifference * YDifference);
						float Magnitude = Mag;
						Mag = Speed / Mag;
						XDifference *= Mag;
						YDifference *= Mag;
						if (npc.velocity.X < XDifference)
						{
							npc.velocity.X += Acceleration;
							if (npc.velocity.X < 0f && XDifference > 0f)
							{
								npc.velocity.X += Acceleration;
							}
						}
						else if (npc.velocity.X > XDifference)
						{
							npc.velocity.X -= Acceleration;
							if (npc.velocity.X > 0f && XDifference < 0f)
							{
								npc.velocity.X -= Acceleration;
							}
						}
						if (npc.velocity.Y < YDifference)
						{
							npc.velocity.Y += Acceleration;
							if (npc.velocity.Y < 0f && YDifference > 0f)
							{
								npc.velocity.Y += Acceleration;
							}
						}
						else if (npc.velocity.Y > YDifference)
						{
							npc.velocity.Y -= Acceleration;
							if (npc.velocity.Y > 0f && YDifference < 0f)
							{
								npc.velocity.Y -= Acceleration;
							}
						}
						npc.ai[2] += 1f;
						float Delay = 600f;
						if (Main.expertMode)
						{
							Delay *= 0.35f;
						}
						if (DRGNModWorld.MentalMode)
						{
							Delay *= 0.5f;
						}
						if (npc.ai[2] >= Delay)
						{
							npc.ai[1] = 1f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.target = 255;
							npc.netUpdate = true;
						}
						else if ((npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Magnitude < 500f) || (Main.expertMode && Magnitude < 500f))
						{
							if (!Main.player[npc.target].dead)
							{
								npc.ai[3] += 1f;
							}
							float Delay2 = 110f;
							if (Main.expertMode)
							{
								Delay2 *= 0.4f;
							}
							if (DRGNModWorld.MentalMode)
							{
								Delay2 *= 0.5f;
							}

							if (npc.ai[3] >= Delay2)
							{
								npc.ai[3] = 0f;
								npc.rotation = rotation;
								float Speed2 = 5f;
								if (Main.expertMode)
								{
									Speed2 = 6f;
								}
								if (DRGNModWorld.MentalMode)
								{
									Speed2 = 7.5f;
								}
								float XDifference2 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcPos.X;
								float YDifference2 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcPos.Y;
								float Mag2 = (float)Math.Sqrt(XDifference2 * XDifference2 + YDifference2 * YDifference2);
								Mag2 = Speed2 / Mag2;
								Vector2 position = NpcPos;
								Vector2 NpcPos2 = default(Vector2);
								NpcPos2.X = XDifference2 * Mag2;
								NpcPos2.Y = YDifference2 * Mag2;
								position.X += NpcPos2.X * 10f;
								position.Y += NpcPos2.Y * 10f;
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									int EyeSpawn = NPC.NewNPC((int)position.X, (int)position.Y, 5);
									Main.npc[EyeSpawn].velocity.X = NpcPos2.X;
									Main.npc[EyeSpawn].velocity.Y = NpcPos2.Y;
									if (Main.netMode == NetmodeID.Server && EyeSpawn < 200)
									{
										NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, EyeSpawn);
									}
								}
								Main.PlaySound(SoundID.NPCHit, (int)position.X, (int)position.Y);

								for (int n = 0; n < 10; n++)
								{
									Dust.NewDust(position, 20, 20, 5, NpcPos2.X * 0.4f, NpcPos2.Y * 0.4f);
								}
							}
						}
					}
					else if (npc.ai[1] == 1f)
					{
						npc.rotation = rotation;
						float Speed3 = 6f;
						if (Main.expertMode)
						{
							Speed3 = 7f;
						}
						if (DRGNModWorld.MentalMode)
						{
							Speed3 = 8.5f;
						}
						Vector2 NpcPos3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float XDifference3 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcPos3.X;
						float YDiffence3 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcPos3.Y;
						float Mag3 = (float)Math.Sqrt(XDifference3 * XDifference3 + YDiffence3 * YDiffence3);
						Mag3 = Speed3 / Mag3;
						npc.velocity.X = XDifference3 * Mag3;
						npc.velocity.Y = YDiffence3 * Mag3;
						npc.ai[1] = 2f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
					else if (npc.ai[1] == 2f)
					{
						npc.ai[2] += 1f;
						if (npc.ai[2] >= 40f)
						{
							npc.velocity *= 0.98f;
							if (Main.expertMode)
							{
								npc.velocity *= 0.985f;
							}
							if (DRGNModWorld.MentalMode)
							{
								npc.velocity *= 0.905f;
							}

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
						int Delay3 = 150;
						if (Main.expertMode)
						{
							Delay3 = 100;
						}
						if (DRGNModWorld.MentalMode)
						{
							Delay3 = 50;
						}

						if (npc.ai[2] >= (float)Delay3)
						{
							npc.ai[3] += 1f;
							npc.ai[2] = 0f;
							npc.target = 255;
							npc.rotation = rotation;
							if (npc.ai[3] >= 3f)
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
					float Acceletration2 = 0.5f;
					if (Main.expertMode)
					{
						Acceletration2 = 0.65f;
					}
					if (DRGNModWorld.MentalMode)
					{
						Acceletration2 = 0.7f;
					}
					if ((float)npc.life < (float)npc.lifeMax * Acceletration2)
					{
						npc.ai[0] = 1f;
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
					return;
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
					if (Main.expertMode && npc.ai[1] % 20f == 0f)
					{
						float Speed5 = 5f;
						Vector2 NpcPos4 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float RandomXDash = Main.rand.Next(-200, 200);
						float RandomYDash = Main.rand.Next(-200, 200);
						float Mag3 = (float)Math.Sqrt(RandomXDash * RandomXDash + RandomYDash * RandomYDash);
						Mag3 = Speed5 / Mag3;
						Vector2 position2 = NpcPos4;
						Vector2 Center = default;
						Center.X = RandomXDash * Mag3;
						Center.Y = RandomYDash * Mag3;
						position2.X += Center.X * 10f;
						position2.Y += Center.Y * 10f;
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							int EyeSpawn2 = NPC.NewNPC((int)position2.X, (int)position2.Y, 5);
							Main.npc[EyeSpawn2].velocity.X = Center.X;
							Main.npc[EyeSpawn2].velocity.Y = Center.Y;
							if (Main.netMode == NetmodeID.Server && EyeSpawn2 < 200)
							{
								NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, EyeSpawn2);
							}
						}
						for (int k = 0; k < 10; k++)
						{
							Dust.NewDust(position2, 20, 20, 5, Center.X * 0.4f, Center.Y * 0.4f);
						}
					}
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
							Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y);
							for (int l = 0; l < 2; l++)
							{
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8);
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
							}
							for (int num35 = 0; num35 < 20; num35++)
							{
								Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
							}
							Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(-7, 7), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(0, 10), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(7, 7), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(0, -10), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(7, -7), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(-7, -7), ProjectileID.DeathLaser, npc.damage / 3, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(-10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);
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
					return;
				}
				npc.defense = 0;

				if (Main.expertMode)
				{
					if (SecondPhase)
					{
						npc.defense = -15;
					}
					if (ThrirdPhase)
					{

						npc.defense = -30;
					}
				}
				npc.damage = 40;
				if (npc.ai[1] == 0f && SecondPhase)
				{
					npc.ai[1] = 5f;
				}
				if (npc.ai[1] == 0f)
				{
					float Speed = 6f;
					float Acceleration = 0.07f;
					Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
					float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 120f - NpcCenter.Y;
					float Mag = (float)Math.Sqrt(XDiff * XDiff + YDiff * YDiff);
					if (Mag > 400f && Main.expertMode)
					{
						Speed += 1f;
						Acceleration += 0.05f;
						if (Mag > 600f)
						{
							Speed += 1f;
							Acceleration += 0.05f;
							if (Mag > 800f)
							{
								Speed += 1f;
								Acceleration += 0.05f;
							}
						}
					}
					if (DRGNModWorld.MentalMode)
					{
						Speed += 3f;
						Acceleration += 0.15f;
					}
					Mag = Speed / Mag;
					XDiff *= Mag;
					YDiff *= Mag;
					if (npc.velocity.X < XDiff)
					{
						npc.velocity.X += Acceleration;
						if (npc.velocity.X < 0f && XDiff > 0f)
						{
							npc.velocity.X += Acceleration;
						}
					}
					else if (npc.velocity.X > XDiff)
					{
						npc.velocity.X -= Acceleration;
						if (npc.velocity.X > 0f && XDiff < 0f)
						{
							npc.velocity.X -= Acceleration;
						}
					}
					if (npc.velocity.Y < YDiff)
					{
						npc.velocity.Y += Acceleration;
						if (npc.velocity.Y < 0f && YDiff > 0f)
						{
							npc.velocity.Y += Acceleration;
						}
					}
					else if (npc.velocity.Y > YDiff)
					{
						npc.velocity.Y -= Acceleration;
						if (npc.velocity.Y > 0f && YDiff < 0f)
						{
							npc.velocity.Y -= Acceleration;
						}
					}
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 200f)
					{
						npc.ai[1] = 1f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.35)
						{
							npc.ai[1] = 3f;
						}
						npc.target = 255;
						npc.netUpdate = true;
					}
					if (Main.expertMode && ThrirdPhase)
					{
						npc.TargetClosest();
						npc.netUpdate = true;
						npc.ai[1] = 3f;
						npc.ai[2] = 0f;
						npc.ai[3] -= 1000f;

					}
				}
				else if (npc.ai[1] == 1f)
				{
					Main.PlaySound(SoundID.ForceRoar, (int)npc.position.X, (int)npc.position.Y, 0);

					Projectile.NewProjectile(npc.Center, new Vector2(0, 10), ProjectileID.DeathLaser, npc.damage / 3, 0);

					Projectile.NewProjectile(npc.Center, new Vector2(0, -10), ProjectileID.DeathLaser, npc.damage / 3, 0);

					Projectile.NewProjectile(npc.Center, new Vector2(10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);

					Projectile.NewProjectile(npc.Center, new Vector2(-10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);
					npc.rotation = rotation;
					float Speed4 = 6.8f;
					if (Main.expertMode && npc.ai[3] == 1f)
					{
						Speed4 *= 1.15f;
					}
					if (Main.expertMode && npc.ai[3] == 2f)
					{
						Speed4 *= 1.3f;
					}
					if (DRGNModWorld.MentalMode)
					{
						Speed4 *= 1.2f;
					}
					Vector2 vector7 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float Xpos = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector7.X;
					float Ypos = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector7.Y;
					float num46 = (float)Math.Sqrt(Xpos * Xpos + Ypos * Ypos);
					num46 = Speed4 / num46;
					npc.velocity.X = Xpos * num46;
					npc.velocity.Y = Ypos * num46;
					npc.ai[1] = 2f;
					npc.netUpdate = true;
					if (npc.netSpam > 10)
					{
						npc.netSpam = 10;
					}
				}
				else if (npc.ai[1] == 2f)
				{
					float Speed = 40f;
					npc.ai[2] += 1f;
					if (Main.expertMode)
					{
						Speed = 50f;
					}
					if (DRGNModWorld.MentalMode)
					{
						Speed = 60f;
					}
					if (DRGNModWorld.MentalMode)
					{
						Speed = 70f;
					}
					if (npc.ai[2] >= Speed)
					{
						npc.velocity *= 0.97f;
						if (Main.expertMode)
						{
							npc.velocity *= 0.98f;
						}
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
					int Delay = 130;
					if (Main.expertMode)
					{
						Delay = 90;
					}
					if (DRGNModWorld.MentalMode)
					{
						Delay = 40;
					}
					if (npc.ai[2] >= (float)Delay)
					{
						npc.ai[3] += 1f;
						npc.ai[2] = 0f;
						npc.target = 255;
						npc.rotation = rotation;
						if (npc.ai[3] >= 3f)
						{
							npc.ai[1] = 0f;
							npc.ai[3] = 0f;
							if (Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient && (double)npc.life < (double)npc.lifeMax * 0.5)
							{
								npc.ai[1] = 3f;
								npc.ai[3] += Main.rand.Next(1, 4);
							}
							npc.netUpdate = true;
							if (npc.netSpam > 10)
							{
								npc.netSpam = 10;
							}
						}
						else
						{
							npc.ai[1] = 1f;
						}
					}
				}
				else if (npc.ai[1] == 3f)
				{
					if (npc.ai[3] == 4f && SecondPhase && npc.Center.Y > Main.player[npc.target].Center.Y)
					{
						npc.TargetClosest();
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
					else if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.TargetClosest();
						float num49 = 20f;
						Vector2 vector8 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num50 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector8.X;
						float num51 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector8.Y;
						float num52 = Math.Abs(Main.player[npc.target].velocity.X) + Math.Abs(Main.player[npc.target].velocity.Y) / 4f;
						num52 += 10f - num52;
						if (num52 < 5f)
						{
							num52 = 5f;
						}
						if (num52 > 15f)
						{
							num52 = 15f;
						}
						if (npc.ai[2] == -1f && !ThrirdPhase)
						{
							num52 *= 4f;
							num49 *= 1.3f;
						}
						if (ThrirdPhase)
						{
							num52 *= 2f;
						}
						num50 -= Main.player[npc.target].velocity.X * num52;
						num51 -= Main.player[npc.target].velocity.Y * num52 / 4f;
						num50 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						if (ThrirdPhase)
						{
							num50 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
							num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						}
						float num53 = (float)Math.Sqrt(num50 * num50 + num51 * num51);
						float num54 = num53;
						num53 = num49 / num53;
						npc.velocity.X = num50 * num53;
						npc.velocity.Y = num51 * num53;
						npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
						npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
						if (ThrirdPhase)
						{
							npc.velocity.X += (float)Main.rand.Next(-50, 51) * 0.1f;
							npc.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.1f;
							float num55 = Math.Abs(npc.velocity.X);
							float num56 = Math.Abs(npc.velocity.Y);
							if (npc.Center.X > Main.player[npc.target].Center.X)
							{
								num56 *= -1f;
							}
							if (npc.Center.Y > Main.player[npc.target].Center.Y)
							{
								num55 *= -1f;
							}
							npc.velocity.X = num56 + npc.velocity.X;
							npc.velocity.Y = num55 + npc.velocity.Y;
							npc.velocity.Normalize();
							npc.velocity *= num49;
							npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
							npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
						}
						else if (num54 < 100f)
						{
							if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
							{
								float num57 = Math.Abs(npc.velocity.X);
								float num58 = Math.Abs(npc.velocity.Y);
								if (npc.Center.X > Main.player[npc.target].Center.X)
								{
									num58 *= -1f;
								}
								if (npc.Center.Y > Main.player[npc.target].Center.Y)
								{
									num57 *= -1f;
								}
								npc.velocity.X = num58;
								npc.velocity.Y = num57;
							}
						}
						else if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
						{
							float num59 = (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) / 2f;
							float Vel = num59;
							if (npc.Center.X > Main.player[npc.target].Center.X)
							{
								Vel *= -1f;
							}
							if (npc.Center.Y > Main.player[npc.target].Center.Y)
							{
								num59 *= -1f;
							}
							npc.velocity.X = Vel;
							npc.velocity.Y = num59;
						}
						npc.ai[1] = 4f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
				}
				else if (npc.ai[1] == 4f)
				{
					if (npc.ai[2] == 0f)
					{
						Main.PlaySound(SoundID.ForceRoar, (int)npc.position.X, (int)npc.position.Y, -1);
						Projectile.NewProjectile(npc.Center, new Vector2(-7, 7), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(0, 10), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(7, 7), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(0, -10), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(7, -7), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(-7, -7), ProjectileID.DeathLaser, npc.damage / 3, 0);
						Projectile.NewProjectile(npc.Center, new Vector2(-10, 0), ProjectileID.DeathLaser, npc.damage / 3, 0);
					}
					float num61 = DashDelay;
					npc.ai[2] += 1f;
					if (npc.ai[2] == num61 && Vector2.Distance(npc.position, Main.player[npc.target].position) < 200f)
					{
						npc.ai[2] -= 1f;
					}
					if (npc.ai[2] >= num61)
					{
						npc.velocity *= 0.95f;
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
					float num62 = num61 + 13f;
					if (npc.ai[2] >= num62)
					{
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
						npc.ai[3] += 1f;
						npc.ai[2] = 0f;
						if (npc.ai[3] >= 5f)
						{
							npc.ai[1] = 0f;
							npc.ai[3] = 0f;
						}
						else
						{
							npc.ai[1] = 3f;
						}
					}
				}
				else if (npc.ai[1] == 5f)
				{
					float num63 = 600f;
					float num64 = 9f;
					float num65 = 0.3f;
					Vector2 vector9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num66 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector9.X;
					float num67 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) + num63 - vector9.Y;
					float num68 = (float)Math.Sqrt(num66 * num66 + num67 * num67);
					num68 = num64 / num68;
					num66 *= num68;
					num67 *= num68;
					if (npc.velocity.X < num66)
					{
						npc.velocity.X += num65;
						if (npc.velocity.X < 0f && num66 > 0f)
						{
							npc.velocity.X += num65;
						}
					}
					else if (npc.velocity.X > num66)
					{
						npc.velocity.X -= num65;
						if (npc.velocity.X > 0f && num66 < 0f)
						{
							npc.velocity.X -= num65;
						}
					}
					if (npc.velocity.Y < num67)
					{
						npc.velocity.Y += num65;
						if (npc.velocity.Y < 0f && num67 > 0f)
						{
							npc.velocity.Y += num65;
						}
					}
					else if (npc.velocity.Y > num67)
					{
						npc.velocity.Y -= num65;
						if (npc.velocity.Y > 0f && num67 < 0f)
						{
							npc.velocity.Y -= num65;
						}
					}
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 70f)
					{
						npc.TargetClosest();
						npc.ai[1] = 3f;
						npc.ai[2] = -1f;
						npc.ai[3] = Main.rand.Next(-3, 1);
						npc.netUpdate = true;
					}
				}
				if (ThrirdPhase && npc.ai[1] == 5f)
				{
					npc.ai[1] = 3f;
				}
			}
		}

    }
}