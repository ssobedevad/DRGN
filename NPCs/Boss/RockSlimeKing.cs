using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    public class RockSlimeKing : ModNPC
	{
		int phase;
		bool shot = false;
		float HealthInc = 1f;
		bool NeedScale = false;
		bool ToHide = false;
		public override void SetStaticDefaults()
		{

			Main.npcFrameCount[npc.type] = 6;
		}
		public override void SetDefaults()
		{
			npc.lifeMax = 10000;
			npc.height = 120;
			npc.width = 180;
			npc.aiStyle = -1;
			npc.damage = 15;
			npc.defense = 25;
			npc.boss = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 100;
			npc.knockBackResist = 0f;


		}
		public override void SendExtraAI(BinaryWriter writer)
		{



			writer.Write(phase);
			
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{



			phase = reader.ReadInt32();
			

		}
		public override void AI()
		{
			HealthInc = 1f;
			NeedScale = false;
			ToHide = false;
			phase = 0;
			if(npc.life < npc.lifeMax * 0.15f)
			{ phase = 5; }
			else if (npc.life < npc.lifeMax * 0.3f)
			{ phase = 4; }
			else if (npc.life < npc.lifeMax * 0.45f)
			{ phase = 3; }
			else if (npc.life < npc.lifeMax * 0.6f)
			{ phase = 2; }
			else if (npc.life < npc.lifeMax * 0.8f)
			{ phase = 1; }

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
			
			if (Main.player[npc.target].dead )
			{
				npc.TargetClosest();
				if (Main.player[npc.target].dead)
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
			if (phase != 5)
			{
				ControlTP();
				npc.dontTakeDamage = (npc.hide = ToHide);
			}
			if (phase == 5)
			{
				npc.rotation = npc.velocity.ToRotation();
				
				npc.noTileCollide = true;
				npc.noGravity = true;
				if (npc.velocity == Vector2.Zero)
				{
					npc.velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 14;
				}
				else if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1 && npc.velocity.Y > -0.1 && npc.velocity.Y < 0.1) { npc.velocity = Vector2.Zero; }
				else { npc.velocity *= 0.98f; }
			}
			else if (npc.velocity.Y == 0f)
			{
				if (!shot)
				{
					shot = true;
					if (phase == 2)
					{
						for (int i = 0; i < 3; i++)
						{
							int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-4, -2), -6 + Main.rand.NextFloat(-3, 3), ProjectileID.GreekFire1, npc.damage / 2, 0);
							int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(2, 4), -6 + Main.rand.NextFloat(-3, 3), ProjectileID.GreekFire1, npc.damage / 2, 0);
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
						}
					}
					else if (phase == 3)
					{
							int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-4, -2), Main.rand.NextFloat(-3, 3), mod.ProjectileType("EmeraldBouncy"), npc.damage / 2, 0);
							int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(2, 4), Main.rand.NextFloat(-3, 3), mod.ProjectileType("EmeraldBouncy"), npc.damage / 2, 0);
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2);
					}
					else if (phase == 4)
					{
						int projid = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-8, -4), Main.rand.NextFloat(-5, 5), mod.ProjectileType("DiamondBouncy"), npc.damage / 2, 0);
						int projid2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(4, 8), Main.rand.NextFloat(-5, 5), mod.ProjectileType("DiamondBouncy"), npc.damage / 2, 0);
						int projid3 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(-8, -4), Main.rand.NextFloat(-5, 5), mod.ProjectileType("DiamondBouncy"), npc.damage / 2, 0);
						int projid4= Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.NextFloat(4, 8), Main.rand.NextFloat(-5, 5), mod.ProjectileType("DiamondBouncy"), npc.damage / 2, 0);
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid, projid2,projid3,projid4);
					}


				}
				npc.velocity.X *= 0.8f;
				if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
				{	
					npc.velocity.X = 0f;
				}

				if (!NeedScale)
				{
					
					
					if(phase != 5)
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
					}
					if (npc.ai[0] >= 0f)
					{
						shot = false;
						npc.netUpdate = true;
						npc.TargetClosest();
						if (phase == 5)
						{
							npc.noTileCollide = false; npc.noGravity = false;
							npc.ai[0] = -200f;



						}
						else
						{
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

			int id3 = Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, 255, new Color(0, 80, 255, 80), npc.scale * 1.2f);
			Main.dust[id3].noGravity = true;
			Dust dust = Main.dust[id3];
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
				int SlimeType = mod.NPCType("RockSlime");
				if (Main.expertMode)
				{
					SlimeType = mod.NPCType("RockSlime");
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
		private void ControlTP()
		{
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
					Gore.NewGore(npc.Center + new Vector2(-40f, -npc.height / 2), npc.velocity, GoreID.KingSlimeCrown);
				}
				if (npc.ai[0] >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
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
						int id = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, DustID.Stone, new Color(78, 136, 255, 80), 2f);
						Main.dust[id].noGravity = true;
						dust = Main.dust[id];
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
					int id2 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, DustID.Stone, new Color(78, 136, 255, 80), 2f);
					Main.dust[id2].noGravity = true;
					dust = Main.dust[id2];
					dust.velocity *= 2f;
				}
			}
			
		}
	


        public override void FindFrame(int frameHeight)
        {
            npc.frame.Width = 206;
            npc.frame.Height = 136;
            int frame = 0;
            int style = phase;

			if (npc.velocity.Y != 0f)
			{
				if (frame < 4)
				{
					frame = 4;
					npc.frameCounter = 0.0;
				}
				if ((npc.frameCounter += 1.0) >= 4.0)
				{
					frame = 5;
				}

			}
			else
			{
				frame = 1;
			}
			


			npc.frame.Y = 136 * frame;
            npc.frame.X = style * 206;

        }


        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.StoneBlock, Main.rand.Next(1, 5));

            Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(1, 4));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
			Texture2D text = ModContent.GetTexture(Texture);
			Vector2 vect = npc.Bottom - Main.screenPosition - new Vector2(103 , 134);
			spriteBatch.Draw(text,vect,npc.frame, drawColor);
			Texture2D text2 = ModContent.GetTexture("DRGN/NPCs/Boss/RockSlimeCrown");
			
			spriteBatch.Draw(text2, vect, null, drawColor);
			return false;
        }

    }
}