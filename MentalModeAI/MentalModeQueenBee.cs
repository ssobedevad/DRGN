using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeQueenBee : GlobalNPC


	{
		
		private static int spikyBallCD = 30;

		public override void AI(NPC npc)
		{
			if (npc.aiStyle == 43 && DRGNModWorld.MentalMode)
			{
				if (spikyBallCD > 0) { spikyBallCD -= 1; }
				if (npc.target == -1 || Main.player[npc.target].dead || !Main.player[npc.target].active)
				{
					npc.TargetClosest(true);
				}
				if (npc.ai[0] == -1f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					float Mode = npc.ai[1];
					int RandomNum;
					do
					{
						RandomNum = Main.rand.Next(3);
						if (RandomNum == 1)
						{
							RandomNum = 2;
						}
						else
						{
							if (RandomNum == 2)
							{
								RandomNum = 3;
							}
						}
					}
					while ((float)RandomNum == Mode);
					npc.ai[0] = (float)RandomNum;
					npc.ai[1] = 0f;
					npc.ai[2] = 0f;
				}
				if (npc.ai[0] == 0f)
				{
					if (npc.ai[1] > 4f && npc.ai[1] % 2f == 0f)
					{
						npc.ai[0] = -1f;
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.netUpdate = true;
					}
					else
					{
						if (npc.ai[1] % 2f == 0f)  // through player
						{
							npc.TargetClosest(true);
							if (Math.Abs(npc.position.Y + (float)(npc.height / 2) - (Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))) < 20f)
							{
								npc.localAI[0] = 1f;
								npc.ai[1] += 1f;
								npc.ai[2] = 0f;
								float Speed = 26f;
								Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
								float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
								float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
								Mag = Speed / Mag;
								npc.velocity.X = XDiff * Mag;
								npc.velocity.Y = YDiff * Mag;
								npc.spriteDirection = npc.direction;
								Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
								
								
							}
							else
							{
								
								npc.localAI[0] = 0f;
								if (npc.position.Y + (float)(npc.height / 2) < Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))
								{
									npc.velocity.Y = npc.velocity.Y + 0.5f;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - 0.5f;
								}
								if (npc.velocity.Y < -20f)
								{
									npc.velocity.Y = -20f;
								}
								if (npc.velocity.Y > 20f)
								{
									npc.velocity.Y = 20f;
								}
								if (Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) > 600f)
								{
									npc.velocity.X = npc.velocity.X + 0.5f * (float)npc.direction;
								}
								else
								{
									
									if (Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) < 500f)
									{
										npc.velocity.X = npc.velocity.X - 0.5f * (float)npc.direction;
									}
									else
									{
										npc.velocity.X = npc.velocity.X * 0.6f;
									}
								}
								if (npc.velocity.X < -20f)
								{
									npc.velocity.X = -20f;
								}
								if (npc.velocity.X > 20f)
								{
									npc.velocity.X = 20f;
								}
								npc.spriteDirection = npc.direction;
							}
						}
						else
						{
							
							if (spikyBallCD == 0)
							{
								Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.ThornBall, npc.damage / 3, 0);
								spikyBallCD = 30;
							}
							if (npc.velocity.X < 0f)
							{
								npc.direction = -1;
							}
							else
							{
								npc.direction = 1;
							}
							npc.spriteDirection = npc.direction;
							int SideOfPlayer = 1;
							if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
							{
								SideOfPlayer = -1;
							}
							if (npc.direction == SideOfPlayer && Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) > 600f)
							{
								npc.ai[2] = 1f;
							}
							if (npc.ai[2] == 1f)
							{
								npc.TargetClosest(true);
								npc.spriteDirection = npc.direction;
								npc.localAI[0] = 0f;
								npc.velocity *= 0.9f;
								if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < 0.1)
								{
									npc.ai[2] = 0f;
									npc.ai[1] += 1f;
								}
							}
							else
							{
								npc.localAI[0] = 1f;
							}
						}
					}
				}
				if (npc.ai[0] == 2f) // top of player
				{
					
					npc.TargetClosest(true);
					npc.spriteDirection = npc.direction;
					float Speed = 18f;
					float Acceleration = 0.16f;
					Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
					float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 200f - NpcCenter.Y;
					float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
					if (Mag < 200f)
					{
						npc.ai[0] = 1f;
						npc.ai[1] = 0f;
						npc.netUpdate = true;
					}
					else
					{
						Mag = Speed / Mag;
						if (npc.velocity.X < XDiff)
						{
							npc.velocity.X = npc.velocity.X + Acceleration;
							if (npc.velocity.X < 0f && XDiff > 0f)
							{
								npc.velocity.X = npc.velocity.X + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.X > XDiff)
							{
								npc.velocity.X = npc.velocity.X - Acceleration;
								if (npc.velocity.X > 0f && XDiff < 0f)
								{
									npc.velocity.X = npc.velocity.X - Acceleration;
								}
							}
						}
						if (npc.velocity.Y < YDiff)
						{
							npc.velocity.Y = npc.velocity.Y + Acceleration;
							if (npc.velocity.Y < 0f && YDiff > 0f)
							{
								npc.velocity.Y = npc.velocity.Y + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.Y > YDiff)
							{
								npc.velocity.Y = npc.velocity.Y - Acceleration;
								if (npc.velocity.Y > 0f && YDiff < 0f)
								{
									npc.velocity.Y = npc.velocity.Y - Acceleration;
								}
							}
						}
					}
				}
				if (npc.ai[0] == 1f)
				{
					
					npc.localAI[0] = 0f;
					npc.TargetClosest(true);
					Vector2 NpcCenterDir = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
					Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
					float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
					float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
					if (Collision.CanHit(NpcCenterDir, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
					{
						npc.ai[1] += 1f;
						if (npc.ai[1] > 40f)
						{
							npc.ai[1] = 0f;
							npc.ai[2] += 1f;
							Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y, 1);
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								for (int i = 0; i < 5; i++)
								{
									Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.Next(-500, 500), -600), Vector2.Zero, ProjectileID.BeeHive, npc.damage, 0);
								}
								
								
							}
						}
					}
					if (Mag > 200f || !Collision.CanHit(new Vector2(NpcCenterDir.X, NpcCenterDir.Y - 30f), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
					{
						
						float Speed = 16f;
						float Acceleration = 0.2f;
						NpcCenter = NpcCenterDir;
						XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
						YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
						Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
						Mag = Speed / Mag;
						if (npc.velocity.X < XDiff)
						{
							npc.velocity.X = npc.velocity.X + Acceleration;
							if (npc.velocity.X < 0f && XDiff > 0f)
							{
								npc.velocity.X = npc.velocity.X + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.X > XDiff)
							{
								npc.velocity.X = npc.velocity.X - Acceleration;
								if (npc.velocity.X > 0f && XDiff < 0f)
								{
									npc.velocity.X = npc.velocity.X - Acceleration;
								}
							}
						}
						if (npc.velocity.Y < YDiff)
						{
							npc.velocity.Y = npc.velocity.Y + Acceleration;
							if (npc.velocity.Y < 0f && YDiff > 0f)
							{
								npc.velocity.Y = npc.velocity.Y + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.Y > YDiff)
							{
								npc.velocity.Y = npc.velocity.Y - Acceleration;
								if (npc.velocity.Y > 0f && YDiff < 0f)
								{
									npc.velocity.Y = npc.velocity.Y - Acceleration;
								}
							}
						}
					}
					else
					{
						npc.velocity *= 0.9f;
					}
					npc.spriteDirection = npc.direction;
					if (npc.ai[2] > 5f)
					{
						npc.ai[0] = -1f;
						npc.ai[1] = 1f;
						npc.netUpdate = true;
					}
				}
				if (npc.ai[0] == 3f)
				{
					
					float Speed = 4f;
					float Acceleration = 0.05f;
					Vector2 NpcCenterDir = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
					Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
					float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - NpcCenter.Y;
					float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
					npc.ai[1] += 1f;
					if (npc.ai[1] % 30f == 29f && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(NpcCenterDir, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
					{
						Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 17);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							for (int i = 0; i < 3; i++)
							{
								float StingerSpeed = 12f;
								float XDiffProj = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - NpcCenterDir.X + Main.rand.NextFloat()*80 - Main.rand.NextFloat()* 80;
								float YDiffProj = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - NpcCenterDir.Y + Main.rand.NextFloat() * 80 - Main.rand.NextFloat() * 80;
								float StingerMag = (float)Math.Sqrt((double)(XDiffProj * XDiffProj + YDiffProj * YDiffProj));
								StingerMag = StingerSpeed / StingerMag;
								XDiffProj *= StingerMag;
								YDiffProj *= StingerMag;
								int Damage = npc.damage / 4;
								int Type = 55;

								int ProjID = Projectile.NewProjectile(NpcCenterDir.X, NpcCenterDir.Y, XDiffProj, YDiffProj, Type, Damage, 0f);
								Main.projectile[ProjID].timeLeft = 300;
							}
						}
					}
					if (!Collision.CanHit(new Vector2(NpcCenterDir.X, NpcCenterDir.Y - 30f), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
					{
						
						Speed = 14f;
						Acceleration = 0.1f;
						NpcCenter = NpcCenterDir;
						XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
						YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
						Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
						Mag = Speed / Mag;
						if (npc.velocity.X < XDiff)
						{
							npc.velocity.X = npc.velocity.X + Acceleration;
							if (npc.velocity.X < 0f && XDiff > 0f)
							{
								npc.velocity.X = npc.velocity.X + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.X > XDiff)
							{
								npc.velocity.X = npc.velocity.X - Acceleration;
								if (npc.velocity.X > 0f && XDiff < 0f)
								{
									npc.velocity.X = npc.velocity.X - Acceleration;
								}
							}
						}
						if (npc.velocity.Y < YDiff)
						{
							npc.velocity.Y = npc.velocity.Y + Acceleration;
							if (npc.velocity.Y < 0f && YDiff > 0f)
							{
								npc.velocity.Y = npc.velocity.Y + Acceleration;
							}
						}
						else
						{
							if (npc.velocity.Y > YDiff)
							{
								npc.velocity.Y = npc.velocity.Y - Acceleration;
								if (npc.velocity.Y > 0f && YDiff < 0f)
								{
									npc.velocity.Y = npc.velocity.Y - Acceleration;
								}
							}
						}
					}
					else
					{
						if (Mag > 100f)
						{
							npc.TargetClosest(true);
							npc.spriteDirection = npc.direction;
							Mag = Speed / Mag;
							if (npc.velocity.X < XDiff)
							{
								npc.velocity.X = npc.velocity.X + Acceleration;
								if (npc.velocity.X < 0f && XDiff > 0f)
								{
									npc.velocity.X = npc.velocity.X + Acceleration * 2f;
								}
							}
							else
							{
								if (npc.velocity.X > XDiff)
								{
									npc.velocity.X = npc.velocity.X - Acceleration;
									if (npc.velocity.X > 0f && XDiff < 0f)
									{
										npc.velocity.X = npc.velocity.X - Acceleration * 2f;
									}
								}
							}
							if (npc.velocity.Y < YDiff)
							{
								npc.velocity.Y = npc.velocity.Y + Acceleration;
								if (npc.velocity.Y < 0f && YDiff > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + Acceleration * 2f;
								}
							}
							else
							{
								if (npc.velocity.Y > YDiff)
								{
									npc.velocity.Y = npc.velocity.Y - Acceleration;
									if (npc.velocity.Y > 0f && YDiff < 0f)
									{
										npc.velocity.Y = npc.velocity.Y - Acceleration * 2f;
									}
								}
							}
						}
					}
					if (npc.ai[1] > 800f)
					{
						
						npc.ai[0] = -1f;
						npc.ai[1] = 3f;
						npc.netUpdate = true;
						return;
					}
				}
			}
			
		}

	}
}