using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
	public class MentalModeBOC : GlobalNPC


	{

		public override bool PreAI(NPC npc)
		{
			if (npc.aiStyle == 54 && DRGNModWorld.MentalMode)
			{
				npc.TargetClosest(true);
				Main.player[npc.target].ZoneCrimson = true;
				
				NPC.crimsonBoss = npc.whoAmI;
				if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
				{
					npc.localAI[0] = 1f;
					for (int NumFloaters = 0; NumFloaters < 35; NumFloaters++)
					{
						float NpcCenterX = npc.Center.X;
						float NpcCenterY = npc.Center.Y;
						NpcCenterX += (float)Main.rand.Next(-npc.width, npc.width);
						NpcCenterY += (float)Main.rand.Next(-npc.height, npc.height);
						int FloaterID = NPC.NewNPC((int)NpcCenterX, (int)NpcCenterY, NPCID.Creeper, 0);
						Main.npc[FloaterID].velocity = new Vector2((float)Main.rand.Next(-30, 31) * 0.1f, (float)Main.rand.Next(-30, 31) * 0.1f);
						Main.npc[FloaterID].netUpdate = true;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.TargetClosest(true);
                    
					int DespawnDistance = 6000;
					if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)DespawnDistance)
					{
						npc.active = false;
						npc.life = 0;
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0);
						}
					}
				}
				if (npc.ai[0] < 0f)
				{
					if (npc.localAI[2] == 0f)
					{
						Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y, 1);
						npc.localAI[2] = 1f;
						Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 392, 1f);
						Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 393, 1f);
						Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 394, 1f);
						Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 395, 1f);
						for (int i = 0; i < 20; i++)
						{
							Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
							
						}
						Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
					}
					
					npc.dontTakeDamage = false;
					npc.knockBackResist = 0.3f;
					npc.TargetClosest(true);
					Vector2 NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
					float XDiff = Main.player[npc.target].Center.X - NpcCenter.X;
					float YDiff = Main.player[npc.target].Center.Y - NpcCenter.Y;
					float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
					float Speed = 16f;
					Mag = Speed / Mag;
					XDiff *= Mag;
					YDiff *= Mag;
					npc.velocity.X = (npc.velocity.X * 50f + XDiff) / 51f;
					npc.velocity.Y = (npc.velocity.Y * 50f + YDiff) / 51f;
					if (npc.ai[0] == -1f)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							npc.localAI[1] += 1f;
							if (npc.localAI[1] >= (float)(60 + Main.rand.Next(120)))
							{
								npc.localAI[1] = 0f;
								npc.TargetClosest(true);
								int NumberOfCycles = 0;
								int PlayerCenterTileX;
								int PlayerCenterTileY;
								while (true)
								{
									NumberOfCycles++;
									PlayerCenterTileX = (int)Main.player[npc.target].Center.X / 16;
									PlayerCenterTileY = (int)Main.player[npc.target].Center.Y / 16;
									if (Main.rand.Next(2) == 0)
									{
										PlayerCenterTileX += Main.rand.Next(2, 20);
									}
									else
									{
										PlayerCenterTileX -= Main.rand.Next(2, 20);
									}
									if (Main.rand.Next(2) == 0)
									{
										PlayerCenterTileY += Main.rand.Next(2, 20);
									}
									else
									{
										PlayerCenterTileY -= Main.rand.Next(2, 20);
									}
									if (!WorldGen.SolidTile(PlayerCenterTileX, PlayerCenterTileY))
									{
										break;
									}
									if (NumberOfCycles > 80)
									{
										return false;
									}
								}
								npc.ai[0] = -2f;
								npc.ai[1] = (float)PlayerCenterTileX;
								npc.ai[2] = (float)PlayerCenterTileY;
								npc.netUpdate = true;
								return false;
							}
						}
					}
					else
					{
						if (npc.ai[0] == -2f)
						{
							npc.velocity *= 1.15f;
							npc.alpha += 25;
							if (npc.alpha >= 255)
							{
								npc.alpha = 255;
								npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
								npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
								Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 8);
								npc.ai[0] = -3f;
								return false;
							}
						}
						else
						{
							if (npc.ai[0] == -3f)
							{
								npc.alpha -= 25;
								if (npc.alpha <= 0)
								{
									npc.alpha = 0;
									npc.ai[0] = -1f;
									return false;
								}
							}
						}
					}
				}
				else
				{
					npc.TargetClosest(true);
					Vector2 NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
					float XDiff = Main.player[npc.target].Center.X - NpcCenter.X;
					float YDiff = Main.player[npc.target].Center.Y - NpcCenter.Y;
					float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
					float Speed = 6f;
					if (Mag < Speed)
					{
						npc.velocity.X = XDiff;
						npc.velocity.Y = YDiff;
					}
					else
					{
						Mag = Speed / Mag;
						npc.velocity.X = XDiff * Mag;
						npc.velocity.Y = YDiff * Mag;
					}
					if (npc.ai[0] == 0f)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							int NumberofCreepers = 0;
							for (int i = 0; i < 200; i++)
							{
								if (Main.npc[i].active && Main.npc[i].type == NPCID.Creeper)
								{
									NumberofCreepers++;
								}
							}
							if (NumberofCreepers == 0)
							{
								npc.ai[0] = -1f;
								npc.localAI[1] = 0f;
								npc.alpha = 0;
								npc.netUpdate = true;
							}
							npc.localAI[1] += 1f;
							if (npc.localAI[1] >= (float)(60 + Main.rand.Next(300)))
							{
								npc.localAI[1] = 0f;
								npc.TargetClosest(true);
								int Attempts = 0;
								int PlayerXTile;
								int PlayerYTile;
								while (true)
								{
									Attempts++;
									PlayerXTile = (int)Main.player[npc.target].Center.X / 16;
									PlayerYTile = (int)Main.player[npc.target].Center.Y / 16;
									PlayerXTile += Main.rand.Next(-20, 21);
									PlayerYTile += Main.rand.Next(-20, 21);
									if (!WorldGen.SolidTile(PlayerXTile, PlayerYTile) && Collision.CanHit(new Vector2((float)(PlayerXTile * 16), (float)(PlayerYTile * 16)), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
									{
										break;
									}
									if (Attempts > 100)
									{
										return false;
									}
								}
								npc.ai[0] = 1f;
								npc.ai[1] = (float)PlayerXTile;
								npc.ai[2] = (float)PlayerYTile;
								npc.netUpdate = true;
								return false;
							}
						}
					}
					else
					{
						if (npc.ai[0] == 1f)
						{
							npc.alpha += 5;
							if (npc.alpha >= 255)
							{
								Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 8);
								npc.alpha = 255;
								npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
								npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
								npc.ai[0] = 2f;
								return false;
							}
						}
						else
						{
							if (npc.ai[0] == 2f)
							{
								npc.alpha -= 5;
								if (npc.alpha <= 0)
								{
									npc.alpha = 0;
									npc.ai[0] = 0f;
									return false;
								}
							}
						}
					}
				}
			}
			else
			{
				if (npc.aiStyle == 55)
				{
					if (NPC.crimsonBoss < 0)
					{
						npc.active = false;
						npc.netUpdate = true;
						return false;
					}
					if (npc.ai[0] == 0f)
					{
						Vector2 NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
						float XDiff = Main.npc[NPC.crimsonBoss].Center.X - NpcCenter.X;
						float YDiff = Main.npc[NPC.crimsonBoss].Center.Y - NpcCenter.Y;
						float Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
						if (Mag > 100f)
						{
							Mag = 8f / Mag;
							XDiff *= Mag;
							YDiff *= Mag;
							npc.velocity.X = (npc.velocity.X * 15f + XDiff) / 16f;
							npc.velocity.Y = (npc.velocity.Y * 15f + YDiff) / 16f;
							return false;
						}
						if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 8f)
						{
							npc.velocity.Y = npc.velocity.Y * 1.05f;
							npc.velocity.X = npc.velocity.X * 1.05f;
						}
						if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.Next(200) == 0)
						{
							npc.TargetClosest(true);
							NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
							XDiff = Main.player[npc.target].Center.X - NpcCenter.X;
							YDiff = Main.player[npc.target].Center.Y - NpcCenter.Y;
							Mag = (float)Math.Sqrt((double)(XDiff * XDiff + YDiff * YDiff));
							Mag = 16f / Mag;
							npc.velocity.X = XDiff * Mag;
							npc.velocity.Y = YDiff * Mag;
							Projectile.NewProjectile(npc.Center, npc.velocity, ProjectileID.GoldenShowerHostile, npc.damage/3, 0);
							npc.ai[0] = 1f;
							npc.netUpdate = true;
							return false;
						}
					}
					else
					{
						Vector2 NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
						float BrainDiffX = Main.npc[NPC.crimsonBoss].Center.X - NpcCenter.X;
						float BrainDiffY = Main.npc[NPC.crimsonBoss].Center.Y - NpcCenter.Y;
						float Mag = (float)Math.Sqrt((double)(BrainDiffX * BrainDiffX + BrainDiffY * BrainDiffY));
						if (Mag > 700f || npc.justHit)
						{
							npc.ai[0] = 0f;
							return false;
						}
					}
				}

			}
			if (DRGNModWorld.MentalMode && (npc.aiStyle == 54 || npc.aiStyle == 55))
			{ return false; }
			else { return true; }
		}
        public override void NPCLoot(NPC npc)
        {
			if (DRGNModWorld.MentalMode && npc.aiStyle == 55)
			{
				
					Projectile.NewProjectile(npc.Center, npc.velocity, ProjectileID.CultistBossIceMist, npc.damage/3, 0);
				
			}
		}
		
	}
}