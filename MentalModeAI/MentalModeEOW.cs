using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
	public class MentalModeEOW : GlobalNPC


	{

		public override bool PreAI(NPC npc)
		{

			//// eather of worlds

			if (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail && DRGNModWorld.MentalMode)
			{

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					if (npc.type == NPCID.EaterofWorldsBody && ((double)(npc.position.Y / 16f) < Main.worldSurface || DRGNModWorld.MentalMode))
					{
						int NpcXTile = (int)(npc.Center.X / 16f);
						int NpcYTile = (int)(npc.Center.Y / 16f);
						if (WorldGen.InWorld(NpcXTile, NpcYTile) && Main.tile[NpcXTile, NpcYTile].wall == 0 && Main.rand.Next(900) == 0)
						{
							npc.TargetClosest();
							if (Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
							{
								NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y + (float)(npc.height / 2) + npc.velocity.Y), NPCID.VileSpit, 0, 0f, 1f);
							}
						}
					}
					else if (npc.type == NPCID.EaterofWorldsHead)
					{
						int SpitCooldown = 50;
						SpitCooldown += (int)((float)npc.life / (float)npc.lifeMax * 60f * 5f);
						if (Main.rand.Next(SpitCooldown) == 0)
						{
							npc.TargetClosest();
							
							if (Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
							{
								Vector2 normVel = Vector2.Normalize(npc.velocity) * 15f;
								int projid = Projectile.NewProjectile(npc.Center,new Vector2(0, normVel.Y), ProjectileID.CursedFlameHostile, 30, 0);
								int projid2 = Projectile.NewProjectile(npc.Center, new Vector2(normVel.X, 0), ProjectileID.CursedFlameHostile, 30, 0);
								int projid3 = Projectile.NewProjectile(npc.Center,new Vector2(0, -normVel.Y), ProjectileID.CursedFlameHostile, 30, 0);
								int projid4 = Projectile.NewProjectile(npc.Center, new Vector2(-normVel.Y, 0), ProjectileID.CursedFlameHostile, 30, 0);
								Main.projectile[projid].timeLeft = 60;
								Main.projectile[projid].tileCollide = false;
								Main.projectile[projid2].timeLeft = 60;
								Main.projectile[projid2].tileCollide = false;
								Main.projectile[projid3].timeLeft = 60;
								Main.projectile[projid3].tileCollide = false;
								Main.projectile[projid4].timeLeft = 60;
								Main.projectile[projid4].tileCollide = false;
								NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2, projid3, projid4);
								
							}
						}
					}
				}
				bool Despawn = false;
				float DespawnAcceleration = 0.2f;

				if (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail)
				{
					npc.realLife = -1;
				}
				else if (npc.ai[3] > 0f)
				{
					npc.realLife = (int)npc.ai[3];
				}
				if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || (Despawn && (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
				{
					npc.TargetClosest();
				}
				if (Main.player[npc.target].dead || (Despawn && (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
				{
					npc.timeLeft = 300;
					if (Despawn)
					{
						npc.velocity.Y += DespawnAcceleration;
					}
				}




				if ((npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody) && npc.ai[0] == 0f)
				{
					if (npc.type == NPCID.EaterofWorldsHead)
					{


						if (npc.type == NPCID.EaterofWorldsHead)
						{
							npc.ai[2] = GetEaterOfWorldsSegmentsCount();
						}

						npc.ai[0] = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type + 1, npc.whoAmI);
						CopyInteractions(Main.npc[(int)npc.ai[0]]);
					}
					else if ((npc.type == NPCID.EaterofWorldsBody) && npc.ai[2] > 0f)
					{
						npc.ai[0] = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, npc.whoAmI);
						CopyInteractions(Main.npc[(int)npc.ai[0]]);
					}
					else
					{
						npc.ai[0] = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type + 1, npc.whoAmI);
						CopyInteractions(Main.npc[(int)npc.ai[0]]);
					}
					if (npc.type < NPCID.EaterofWorldsHead || npc.type > NPCID.EaterofWorldsTail)
					{
						Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
						Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
					}
					Main.npc[(int)npc.ai[0]].ai[1] = npc.whoAmI;
					Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;
					npc.netUpdate = true;
				}



				if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
				{
					if (!Main.npc[(int)npc.ai[1]].active && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.checkDead();
						npc.active = false;
						NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f);
					}
					if (npc.type == NPCID.EaterofWorldsHead && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.checkDead();
						npc.active = false;
						NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f);
					}
					if (npc.type == NPCID.EaterofWorldsTail && !Main.npc[(int)npc.ai[1]].active)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.checkDead();
						npc.active = false;
						NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f);
					}
					if (npc.type == NPCID.EaterofWorldsBody && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
					{
						npc.type = NPCID.EaterofWorldsHead;
						int whoAmI = npc.whoAmI;
						float HealthPercent = (float)npc.life / (float)npc.lifeMax;
						float OwnerNPC = npc.ai[0];
						npc.SetDefaultsKeepPlayerInteraction(npc.type);
						npc.life = (int)((float)npc.lifeMax * HealthPercent);
						npc.ai[0] = OwnerNPC;
						npc.TargetClosest();
						npc.netUpdate = true;
						npc.whoAmI = whoAmI;
						npc.alpha = 0;
					}
					if (npc.type == NPCID.EaterofWorldsBody && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
					{
						npc.type = NPCID.EaterofWorldsTail;
						int whoAmI2 = npc.whoAmI;
						float BodyHealthPercent = (float)npc.life / (float)npc.lifeMax;
						float OwnerNPCBody = npc.ai[1];
						npc.SetDefaultsKeepPlayerInteraction(npc.type);
						npc.life = (int)((float)npc.lifeMax * BodyHealthPercent);
						npc.ai[1] = OwnerNPCBody;
						npc.TargetClosest();
						npc.netUpdate = true;
						npc.whoAmI = whoAmI2;
						npc.alpha = 0;
					}
				}
				if (!npc.active && Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f);
				}

				int BodyMaxTileX = (int)(npc.position.X / 16f) - 1;
				int BodyMinTileX = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
				int BodyMaxTileY = (int)(npc.position.Y / 16f) - 1;
				int BodyMinTileY = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
				if (BodyMaxTileX < 0)
				{
					BodyMaxTileX = 0;
				}
				if (BodyMinTileX > Main.maxTilesX)
				{
					BodyMinTileX = Main.maxTilesX;
				}
				if (BodyMaxTileY < 0)
				{
					BodyMaxTileY = 0;
				}
				if (BodyMinTileY > Main.maxTilesY)
				{
					BodyMinTileY = Main.maxTilesY;
				}
				bool Collide = false;

				if (!Collide)
				{
					Vector2 vector = default(Vector2);
					for (int x = BodyMaxTileX; x < BodyMinTileX; x++)
					{
						for (int y = BodyMaxTileY; y < BodyMinTileY; y++)
						{
							if (Main.tile[x, y] == null || ((!Main.tile[x, y].nactive() || (!Main.tileSolid[Main.tile[x, y].type] && (!Main.tileSolidTop[Main.tile[x, y].type] || Main.tile[x, y].frameY != 0))) && Main.tile[x, y].liquid <= 64))
							{
								continue;
							}
							vector.X = x * 16;
							vector.Y = y * 16;
							if (npc.position.X + (float)npc.width > vector.X && npc.position.X < vector.X + 16f && npc.position.Y + (float)npc.height > vector.Y && npc.position.Y < vector.Y + 16f)
							{
								Collide = true;

							}
						}
					}
				}
				if (!Collide && (npc.type == NPCID.EaterofWorldsHead))
				{
					Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
					int ReqDistToPlayer = 650;
					bool CloseToPlayer = true;
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active)
						{
							Rectangle rectangle2 = new Rectangle((int)Main.player[i].position.X - ReqDistToPlayer, (int)Main.player[i].position.Y - ReqDistToPlayer, ReqDistToPlayer * 2, ReqDistToPlayer * 2);
							if (rectangle.Intersects(rectangle2))
							{
								CloseToPlayer = false;
								break;
							}
						}
					}
					if (CloseToPlayer)
					{
						Collide = true;
					}
				}


				float MaxSpeed = 8f;
				float Acceleration = 0.07f;

				if (npc.type == NPCID.EaterofWorldsHead)
				{
					MaxSpeed = 10f;
					Acceleration = 0.07f;
					if (Main.expertMode)
					{
						MaxSpeed = 12f;
						Acceleration = 0.15f;
					}
					if (DRGNModWorld.MentalMode)
					{
						MaxSpeed += 4f;
						Acceleration += 0.05f;
					}
				}


				Vector2 NPCCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float PlayerXpos = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
				float PlayerYpos = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);

				PlayerXpos = (int)(PlayerXpos / 16f) * 16;
				PlayerYpos = (int)(PlayerYpos / 16f) * 16;
				NPCCenter.X = (int)(NPCCenter.X / 16f) * 16;
				NPCCenter.Y = (int)(NPCCenter.Y / 16f) * 16;
				PlayerXpos -= NPCCenter.X;
				PlayerYpos -= NPCCenter.Y;

				float Mag = (float)Math.Sqrt(PlayerXpos * PlayerXpos + PlayerYpos * PlayerYpos);
				if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
				{
					try
					{
						NPCCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						PlayerXpos = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - NPCCenter.X;
						PlayerYpos = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - NPCCenter.Y;
					}
					catch
					{
					}
					npc.rotation = (float)Math.Atan2(PlayerYpos, PlayerXpos) + 1.57f;
					Mag = (float)Math.Sqrt(PlayerXpos * PlayerXpos + PlayerYpos * PlayerYpos);
					int Width = npc.width;

					if (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail)
					{
						Width = (int)((float)Width * npc.scale);
					}


					Mag = (Mag - (float)Width) / Mag;
					PlayerXpos *= Mag;
					PlayerYpos *= Mag;
					npc.velocity = Vector2.Zero;
					npc.position.X += PlayerXpos;
					npc.position.Y += PlayerYpos;

				}
				else
				{
					if (!Collide)
					{
						npc.TargetClosest();
						npc.velocity.Y += 0.11f;
						if (npc.velocity.Y > MaxSpeed)
						{
							npc.velocity.Y = MaxSpeed;
						}
						if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)MaxSpeed * 0.4)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X -= Acceleration * 1.1f;
							}
							else
							{
								npc.velocity.X += Acceleration * 1.1f;
							}
						}
						else if (npc.velocity.Y == MaxSpeed)
						{
							if (npc.velocity.X < PlayerXpos)
							{
								npc.velocity.X += Acceleration;
							}
							else if (npc.velocity.X > PlayerXpos)
							{
								npc.velocity.X -= Acceleration;
							}
						}
						else if (npc.velocity.Y > 4f)
						{
							if (npc.velocity.X < 0f)
							{
								npc.velocity.X += Acceleration * 0.9f;
							}
							else
							{
								npc.velocity.X -= Acceleration * 0.9f;
							}
						}
					}
					else
					{

						Mag = (float)Math.Sqrt(PlayerXpos * PlayerXpos + PlayerYpos * PlayerYpos);
						float AbsPlayerX = Math.Abs(PlayerXpos);
						float AbsPlayerY = Math.Abs(PlayerYpos);
						float VelocityMult = MaxSpeed / Mag;
						PlayerXpos *= VelocityMult;
						PlayerYpos *= VelocityMult;
						bool ToDespawn = false;
						if ((npc.type == NPCID.EaterofWorldsHead) && ((!Main.player[npc.target].ZoneCorrupt && !Main.player[npc.target].ZoneCrimson) || Main.player[npc.target].dead))
						{
							ToDespawn = true;
						}

						if (ToDespawn)
						{
							bool ToKill = true;
							for (int i = 0; i < 255; i++)
							{
								if (Main.player[i].active && !Main.player[i].dead && Main.player[i].ZoneCorrupt)
								{
									ToKill = false;
								}
							}
							if (ToKill)
							{
								if (Main.netMode != NetmodeID.MultiplayerClient && (double)(npc.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
								{
									npc.active = false;
									int PreviousNPC = (int)npc.ai[0];
									while (PreviousNPC > 0 && PreviousNPC < 200 && Main.npc[PreviousNPC].active && Main.npc[PreviousNPC].aiStyle == npc.aiStyle)
									{
										int PreviousPreviousNPC = (int)Main.npc[PreviousNPC].ai[0];
										Main.npc[PreviousNPC].active = false;
										npc.life = 0;
										if (Main.netMode == NetmodeID.Server)
										{
											NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, PreviousNPC);
										}
										PreviousNPC = PreviousPreviousNPC;
									}
									if (Main.netMode == NetmodeID.Server)
									{
										NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
									}
								}
								PlayerXpos = 0f;
								PlayerYpos = MaxSpeed;
							}
						}
						bool DontAccelerateTowardsPlayer = false;


						if (!DontAccelerateTowardsPlayer)
						{
							Acceleration += 0.1f;
							if ((npc.velocity.X > 0f && PlayerXpos > 0f) || (npc.velocity.X < 0f && PlayerXpos < 0f) || (npc.velocity.Y > 0f && PlayerYpos > 0f) || (npc.velocity.Y < 0f && PlayerYpos < 0f))
							{
								if (npc.velocity.X < PlayerXpos)
								{
									npc.velocity.X += Acceleration;
								}
								else if (npc.velocity.X > PlayerXpos)
								{
									npc.velocity.X -= Acceleration;
								}
								if (npc.velocity.Y < PlayerYpos)
								{
									npc.velocity.Y += Acceleration;
								}
								else if (npc.velocity.Y > PlayerYpos)
								{
									npc.velocity.Y -= Acceleration;
								}
								if ((double)Math.Abs(PlayerYpos) < (double)MaxSpeed * 0.2 && ((npc.velocity.X > 0f && PlayerXpos < 0f) || (npc.velocity.X < 0f && PlayerXpos > 0f)))
								{
									if (npc.velocity.Y > 0f)
									{
										npc.velocity.Y += Acceleration * 3f;
									}
									else
									{
										npc.velocity.Y -= Acceleration * 3f;
									}
								}
								if ((double)Math.Abs(PlayerXpos) < (double)MaxSpeed * 0.2 && ((npc.velocity.Y > 0f && PlayerYpos < 0f) || (npc.velocity.Y < 0f && PlayerYpos > 0f)))
								{
									if (npc.velocity.X > 0f)
									{
										npc.velocity.X += Acceleration * 3f;
									}
									else
									{
										npc.velocity.X -= Acceleration * 3f;
									}
								}
							}
							else if (AbsPlayerX > AbsPlayerY)
							{
								if (npc.velocity.X < PlayerXpos)
								{
									npc.velocity.X += Acceleration * 1.2f;
								}
								else if (npc.velocity.X > PlayerXpos)
								{
									npc.velocity.X -= Acceleration * 1.2f;
								}
								if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)MaxSpeed * 0.5)
								{
									if (npc.velocity.Y > 0f)
									{
										npc.velocity.Y += Acceleration;
									}
									else
									{
										npc.velocity.Y -= Acceleration;
									}
								}
							}
							else
							{
								if (npc.velocity.Y < PlayerYpos)
								{
									npc.velocity.Y += Acceleration * 1.1f;
								}
								else if (npc.velocity.Y > PlayerYpos)
								{
									npc.velocity.Y -= Acceleration * 1.1f;
								}
								if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)MaxSpeed * 0.5)
								{
									if (npc.velocity.X > 0f)
									{
										npc.velocity.X += Acceleration;
									}
									else
									{
										npc.velocity.X -= Acceleration;
									}
								}
							}
						}
					}
					npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
					if (npc.type == NPCID.EaterofWorldsHead)
					{
						if (Collide)
						{
							if (npc.localAI[0] != 1f)
							{
								npc.netUpdate = true;
							}
							npc.localAI[0] = 1f;
						}
						else
						{
							if (npc.localAI[0] != 0f)
							{
								npc.netUpdate = true;
							}
							npc.localAI[0] = 0f;
						}
						if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
						{
							npc.netUpdate = true;
						}
					}

				}
				if (npc.type < NPCID.EaterofWorldsHead || npc.type > NPCID.EaterofWorldsTail || (npc.type != NPCID.EaterofWorldsHead && (npc.type == NPCID.EaterofWorldsHead || Main.npc[(int)npc.ai[1]].alpha >= 85)))
				{
					return false;
				}
				if (npc.alpha > 0 && npc.life > 0)
				{
					for (int DustNumber = 0; DustNumber < 2; DustNumber++)
					{
						int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 14, 0f, 0f, 100, default(Color), 2f);
						Main.dust[DustID].noGravity = true;
						Main.dust[DustID].noLight = true;
					}
				}
				if ((npc.position - npc.oldPosition).Length() > 2f)
				{
					npc.alpha -= 42;
					if (npc.alpha < 0)
					{
						npc.alpha = 0;
					}
				}
				return false;
			}
			return true;


		}
		public void CopyInteractions(NPC npc)
		{
			for (int i = 0; i < npc.playerInteraction.Length; i++)
			{
				npc.playerInteraction[i] = npc.playerInteraction[i];
			}

		}
		public static int GetEaterOfWorldsSegmentsCount()
		{
			
			
			return 70;
		}

	}
}