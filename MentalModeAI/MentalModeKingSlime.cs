using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
	public class MentalModeKingSlime : GlobalNPC


	{

		public override void AI(NPC npc)
		{

			if (npc.aiStyle == 15 && DRGNModWorld.MentalMode)
			{
				float HealthInc = 1f;
				bool NeedScale = false;
				bool ToHide = false;


				npc.aiAction = 0;
				if (npc.ai[3] == 0f && npc.life > 0)
				{
					npc.ai[3] = npc.lifeMax;
				}
				if (npc.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.ai[0] = -100f;
					npc.localAI[3] = 1f;
					npc.TargetClosest();
					npc.netUpdate = true;
				}
				int MaxDistanceToPlayer = 3000;
				if (Main.player[npc.target].dead || Vector2.Distance(npc.Center, Main.player[npc.target].Center) > (float)MaxDistanceToPlayer)
				{
					npc.TargetClosest();
					if (Main.player[npc.target].dead || Vector2.Distance(npc.Center, Main.player[npc.target].Center) > (float)MaxDistanceToPlayer)
					{
						npc.timeLeft = 10;
						if (Main.player[npc.target].Center.X < npc.Center.X)
						{
							npc.direction = 1;
						}
						else
						{
							npc.direction = -1;
						}
						if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] != 5f)
						{
							npc.netUpdate = true;
							npc.ai[2] = 0f;
							npc.ai[0] = 0f;
							npc.ai[1] = 5f;
							npc.localAI[1] = Main.maxTilesX * 16;
							npc.localAI[2] = Main.maxTilesY * 16;
						}
					}
				}
				if (!Main.player[npc.target].dead && npc.timeLeft > 10 && npc.ai[2] >= 300f && npc.ai[1] < 5f && npc.velocity.Y == 0f)
				{
					npc.ai[2] = 0f;
					npc.ai[0] = 0f;
					npc.ai[1] = 5f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.TargetClosest(faceTarget: false);
						Point NpcCenterTile = npc.Center.ToTileCoordinates();
						Point PlayerCenterTile = Main.player[npc.target].Center.ToTileCoordinates();
						Vector2 DistanceToPlayer = Main.player[npc.target].Center - npc.Center;
						int RandomOffset = 10;
						int OffsetUsedForCalculatingNpcDistance = 0;
						int YOffSet = 7;
						int TeleportCD = 0;
						bool Teleport = false;
						if (npc.localAI[0] >= 360f || DistanceToPlayer.Length() > 2000f)
						{
							if (npc.localAI[0] >= 360f)
							{
								npc.localAI[0] = 360f;
							}
							Teleport = true;
							TeleportCD = 100;
						}
						while (!Teleport && TeleportCD < 100)
						{
							TeleportCD++;
							int RandomisedPlayerTileX = Main.rand.Next(PlayerCenterTile.X - RandomOffset, PlayerCenterTile.X + RandomOffset + 1);
							int RandomisedPlayerTileY = Main.rand.Next(PlayerCenterTile.Y - RandomOffset, PlayerCenterTile.Y + 1);
							if ((RandomisedPlayerTileY >= PlayerCenterTile.Y - YOffSet && RandomisedPlayerTileY <= PlayerCenterTile.Y + YOffSet && RandomisedPlayerTileX >= PlayerCenterTile.X - YOffSet && RandomisedPlayerTileX <= PlayerCenterTile.X + YOffSet) || (RandomisedPlayerTileY >= NpcCenterTile.Y - OffsetUsedForCalculatingNpcDistance && RandomisedPlayerTileY <= NpcCenterTile.Y + OffsetUsedForCalculatingNpcDistance && RandomisedPlayerTileX >= NpcCenterTile.X - OffsetUsedForCalculatingNpcDistance && RandomisedPlayerTileX <= NpcCenterTile.X + OffsetUsedForCalculatingNpcDistance) || Main.tile[RandomisedPlayerTileX, RandomisedPlayerTileY].nactive())
							{
								continue;
							}
							int RandPlayerTileY = RandomisedPlayerTileY;
							int YExtraOffset = 0;
							if (Main.tile[RandomisedPlayerTileX, RandPlayerTileY].nactive() && Main.tileSolid[Main.tile[RandomisedPlayerTileX, RandPlayerTileY].type] && !Main.tileSolidTop[Main.tile[RandomisedPlayerTileX, RandPlayerTileY].type])
							{
								YExtraOffset = 1;
							}
							else
							{
								for (; YExtraOffset < 150 && RandPlayerTileY + YExtraOffset < Main.maxTilesY; YExtraOffset++)
								{
									int TileToCheckY = RandPlayerTileY + YExtraOffset;
									if (Main.tile[RandomisedPlayerTileX, TileToCheckY].nactive() && Main.tileSolid[Main.tile[RandomisedPlayerTileX, TileToCheckY].type] && !Main.tileSolidTop[Main.tile[RandomisedPlayerTileX, TileToCheckY].type])
									{
										YExtraOffset--;
										break;
									}
								}
							}
							RandomisedPlayerTileY += YExtraOffset;
							bool TileisValid = true;
							if (TileisValid && Main.tile[RandomisedPlayerTileX, RandomisedPlayerTileY].lava())
							{
								TileisValid = false;
							}
							if (TileisValid && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
							{
								TileisValid = false;
							}
							if (TileisValid)
							{
								npc.localAI[1] = RandomisedPlayerTileX * 16 + 8;
								npc.localAI[2] = RandomisedPlayerTileY * 16 + 16;
								Teleport = true;
								break;
							}
						}
						if (TeleportCD >= 100)
						{
							Vector2 bottom = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
							npc.localAI[1] = bottom.X;
							npc.localAI[2] = bottom.Y;
						}
					}
				}
				if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0) || Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 160f)
				{
					npc.ai[2]++;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.localAI[0]++;
					}
				}
				else if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.localAI[0]--;
					if (npc.localAI[0] < 0f)
					{
						npc.localAI[0] = 0f;
					}
				}
				if (npc.timeLeft < 10 && (npc.ai[0] != 0f || npc.ai[1] != 0f))
				{
					npc.ai[0] = 0f;
					npc.ai[1] = 0f;
					npc.netUpdate = true;
					NeedScale = false;
				}
				Dust dust;
				if (npc.ai[1] == 5f)
				{
					NeedScale = true;
					npc.aiAction = 1;
					npc.ai[0]++;
					HealthInc = MathHelper.Clamp((60f - npc.ai[0]) / 60f, 0f, 1f);
					HealthInc = 0.5f + HealthInc * 0.5f;

					if (npc.ai[0] >= 60f)
					{
						ToHide = true;
					}
					if (npc.ai[0] == 60f)
					{
						Gore.NewGore(npc.Center + new Vector2(-40f, -npc.height / 2), npc.velocity, 734);
					}
					if (npc.ai[0] >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2] - 400);
						npc.ai[1] = 6f;
						npc.ai[0] = 0f;
						npc.netUpdate = true;
					}
					if (Main.netMode == NetmodeID.MultiplayerClient && npc.ai[0] >= 120f)
					{
						npc.ai[1] = 6f;
						npc.ai[0] = 0f;
					}
					if (!ToHide)
					{
						for (int num247 = 0; num247 < 10; num247++)
						{
							int DustID3 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
							Main.dust[DustID3].noGravity = true;
							dust = Main.dust[DustID3];
							dust.velocity *= 0.5f;
						}
					}
				}
				else if (npc.ai[1] == 6f)
				{
					NeedScale = true;
					npc.aiAction = 0;
					npc.ai[0]++;
					HealthInc = MathHelper.Clamp(npc.ai[0] / 30f, 0f, 1f);
					HealthInc = 0.5f + HealthInc * 0.5f;

					if (npc.ai[0] >= 30f && Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.ai[1] = 0f;
						npc.ai[0] = 0f;
						npc.netUpdate = true;
						npc.TargetClosest();
					}
					if (Main.netMode == NetmodeID.MultiplayerClient && npc.ai[0] >= 60f)
					{
						npc.ai[1] = 0f;
						npc.ai[0] = 0f;
						npc.TargetClosest();
					}
					for (int j = 0; j < 10; j++)
					{
						int DustID2 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, 255, 80), 2f);
						Main.dust[DustID2].noGravity = true;
						dust = Main.dust[DustID2];
						dust.velocity *= 2f;
					}
				}
				npc.dontTakeDamage = (npc.hide = ToHide);
				if (npc.velocity.Y == 0f)
				{
					if (npc.ai[0] >= -120 && npc.ai[0] <= -80)
					{
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-10, -8), -12 + Main.rand.Next(-5, 5), ProjectileID.SpikedSlimeSpike, npc.damage / 2, 0);
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(8, 10), -12 + Main.rand.Next(-5, 5), ProjectileID.SpikedSlimeSpike, npc.damage / 2, 0);
					}
					npc.velocity.X *= 0.8f;
					if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
					{
						npc.velocity.X = 0f;
					}
					if (!NeedScale)
					{
						npc.ai[0] += 2f;
						if ((double)npc.life < (double)npc.lifeMax * 0.8)
						{
							npc.ai[0] += 2f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.6)
						{
							npc.ai[0] += 3f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.4)
						{
							npc.ai[0] += 4f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.2)
						{
							npc.ai[0] += 5f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.1)
						{
							npc.ai[0] += 6f;
						}
						if (npc.ai[0] >= 0f)
						{
							npc.netUpdate = true;
							npc.TargetClosest();
							if (npc.ai[1] == 3f)
							{
								npc.velocity.Y = -13f;
								npc.velocity.X += 3.5f * (float)npc.direction;
								npc.ai[0] = -200f;
								npc.ai[1] = 0f;
							}
							else if (npc.ai[1] == 2f)
							{
								npc.velocity.Y = -6f;
								npc.velocity.X += 4.5f * (float)npc.direction;
								npc.ai[0] = -120f;
								npc.ai[1] += 1f;
							}
							else
							{
								npc.velocity.Y = -8f;
								npc.velocity.X += 4f * (float)npc.direction;
								npc.ai[0] = -120f;
								npc.ai[1] += 1f;
							}
						}
						else if (npc.ai[0] >= -30f)
						{
							npc.aiAction = 1;
						}
					}
				}
				else if (npc.target < 255)
				{
					float num251 = 3f;

					if ((npc.direction == 1 && npc.velocity.X < num251) || (npc.direction == -1 && npc.velocity.X > 0f - num251))
					{
						if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
						{
							npc.velocity.X += 0.2f * (float)npc.direction;
						}
						else
						{
							npc.velocity.X *= 0.93f;
						}
					}
				}

				int DustID = Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, 255, new Color(0, 80, 255, 80), npc.scale * 1.2f);
				Main.dust[DustID].noGravity = true;
				dust = Main.dust[DustID];
				dust.velocity *= 0.5f;
				if (npc.life <= 0)
				{
					return;
				}
				float HealthPercent = (float)npc.life / (float)npc.lifeMax;
				HealthPercent = HealthPercent * 0.5f + 0.75f;
				HealthPercent *= HealthInc;
				if (HealthPercent != npc.scale)
				{
					npc.position.X += npc.width / 2;
					npc.position.Y += npc.height;
					npc.scale = HealthPercent;
					npc.width = (int)(98f * npc.scale);
					npc.height = (int)(92f * npc.scale);
					npc.position.X -= npc.width / 2;
					npc.position.Y -= npc.height;
				}
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					return;
				}
				int num254 = (int)((double)npc.lifeMax * 0.05);
				if (!((float)(npc.life + num254) < npc.ai[3]))
				{
					return;
				}
				npc.ai[3] = npc.life;
				int RandomSlimeNumber = Main.rand.Next(4, 8);
				for (int i = 0; i < RandomSlimeNumber; i++)
				{
					int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
					int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
					int SlimeType = NPCID.BlueSlime;
					if (Main.expertMode)
					{
						SlimeType = NPCID.SlimeSpiked;
					}
					int NpcNumber = NPC.NewNPC(x, y, SlimeType);
					Main.npc[NpcNumber].SetDefaults(SlimeType);
					Main.npc[NpcNumber].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
					Main.npc[NpcNumber].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
					Main.npc[NpcNumber].ai[0] = -1000 * Main.rand.Next(3);
					Main.npc[NpcNumber].ai[1] = 0f;
					if (Main.netMode == NetmodeID.Server && NpcNumber < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NpcNumber);
					}
				}
			}


		}

	}
}