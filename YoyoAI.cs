

using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.ModBrowser;

namespace DRGN
{

    public class YoyoAI : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
		
		public override bool PreAI(Projectile projectile)
        {
			if (projectile.aiStyle == 99)
			{
				projectile.aiStyle = -2;
				projectile.tileCollide = false;
				projectile.velocity = Vector2.Zero;
			}
			
			if(projectile.aiStyle == -2)
			{ 
				
                Player player = Main.player[projectile.owner];
				player.itemAnimation = 1;
				player.heldProj = projectile.whoAmI;
                int Timeleft = (int)(ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] * 80);
                float range = ProjectileID.Sets.YoyosMaximumRange[projectile.type];
                if (player.yoyoString) { range = range *1.25f + 30f;  }
                float topSpeed = ProjectileID.Sets.YoyosTopSpeed[projectile.type];
                if (Timeleft < -1) { projectile.timeLeft = 6; }
                else 
                {
                    if (projectile.timeLeft > Timeleft) { projectile.timeLeft = Timeleft; }
					else if (projectile.timeLeft <= 5) { Retract(projectile, player,range);projectile.timeLeft = 4; }
                }
                if ((!(DRGN.YoyoSkill1.Current) && !DRGN.YoyoSkill2.Current) || player.dead || (Vector2.Distance(player.Center,projectile.Center) > range*1.5f)) { Retract(projectile,player,range); }
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
				if (DRGN.YoyoSkill1.Current && DRGN.YoyoSkill2.Current)
				{ Oval(projectile, topSpeed, range); }
				
				else if (DRGN.YoyoSkill1.Current && !DRGN.YoyoSkill2.Current)
				{ Spin(projectile, topSpeed, range); }
				else
				{ Idle(projectile, player, topSpeed, range); }
				if (!(DRGN.YoyoSkill1.Current))
				{   
					projectile.ai[0] = 0;
					
				}
				if(!DRGN.YoyoSkill2.Current)
				{
					projectile.ai[1] = 0;

				}
				if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
				{
					player.ChangeDir(1);
					projectile.direction = 1;
				}
				else
				{
					player.ChangeDir(-1);
					projectile.direction = -1;
				}














				if (projectile.type == 603 && projectile.owner == Main.myPlayer)
				{
					projectile.localAI[1] += 1f;
					if (projectile.localAI[1] >= 6f)
					{
						float num3 = 400f;
						Vector2 velocity = projectile.velocity;
						Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						vector.Normalize();
						vector *= (float)Main.rand.Next(10, 41) * 0.1f;
						if (Main.rand.Next(3) == 0)
						{
							vector *= 2f;
						}
						velocity *= 0.25f;
						velocity += vector;
						for (int j = 0; j < 200; j++)
						{
							if (Main.npc[j].CanBeChasedBy(this))
							{
								float num4 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
								float num5 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
								float num6 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num4) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num5);
								if (num6 < num3 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
								{
									num3 = num6;
									velocity.X = num4;
									velocity.Y = num5;
									velocity -= projectile.Center;
									velocity.Normalize();
									velocity *= 8f;
								}
							}
						}
						velocity *= 0.8f;
						Projectile.NewProjectile(projectile.Center.X - velocity.X, projectile.Center.Y - velocity.Y, velocity.X, velocity.Y, ProjectileID.TerrarianBeam, projectile.damage, projectile.knockBack, projectile.owner);
						projectile.localAI[1] = 0f;
					}
				}





			}
            else
            {
                return true;
            }
            return false;
        }
		private static Vector2 Rotate(Vector2 v, float radians)
		{
			double ca = Math.Cos(radians);
			double sa = Math.Sin(radians);
			return new Vector2((float)(ca * v.X - sa * v.Y), (float)(sa * v.X + ca * v.Y));
		}

		public void Retract(Projectile proj , Player player , float range)
		{
			Vector2 restingPoint = player.Center;
			proj.rotation += 0.45f;
			Vector2 diff = restingPoint - proj.Center;
			float Mag = Vector2.Distance(restingPoint, proj.Center);
			if(Mag < 20) { proj.Kill();player.itemAnimation = 0;
				for (int i = 0; i < Main.projectile.Length; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
					{
						if (Main.projectile[i].counterweight)
						{
							Main.projectile[i].Kill();
						}

					}
				}
			}
			if (Vector2.Distance(player.Center, proj.Center) > range * 1.5f)
			{
				if (Mag > 150) { diff *= 150 / Mag; }
                
			}
			else
			{
				if (Mag > 50) { diff *= 50 / Mag; }
			}
			player.itemAnimation = 1;
			
			proj.velocity = (proj.velocity * 10f + diff) / 11f;
		}
        public void Idle(Projectile proj , Player player , float topSpeed , float maxRange)
        {
            if(proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
			float YDiff = Main.MouseWorld.Y - player.Center.Y;
			if(YDiff < 5) { YDiff = 5; }
			if(YDiff > maxRange / 2f) { YDiff = maxRange / 2f; }
			float ProjYDiff = proj.Center.Y - player.Center.Y;
			if (ProjYDiff > maxRange / 1.8f) { proj.velocity.Y *= 0.9f; }
			Vector2 restingPoint = player.Center + new Vector2(10 * player.direction + Main.rand.Next(-3,4), YDiff + Main.rand.Next(-3, 4));
            proj.rotation += 0.45f;
            Vector2 diff = restingPoint - proj.Center;
            float Mag = Vector2.Distance(restingPoint, proj.Center);
            diff *= topSpeed / Mag;
            proj.velocity = (proj.velocity * 40f + diff) / 41f;







        }
		public void Spin(Projectile proj, float topSpeed, float maxRange)
		{
			Player player = Main.player[proj.owner];
			proj.ai[0] += 0.15f;
			if (proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
			float extensionLength = Vector2.Distance(Main.MouseWorld, player.Center);
			if(extensionLength > maxRange) { extensionLength = maxRange; }
			
			Vector2 restingPoint = player.Center + new Vector2((float)Math.Sin(proj.ai[0]) * extensionLength + (10 * player.direction), (float)Math.Cos(proj.ai[0]) * extensionLength);
			proj.rotation += 0.2f;
            if (proj.ai[0] < 2f)
            {
                Vector2 diff = restingPoint - proj.Center;
                float Mag = Vector2.Distance(restingPoint, proj.Center);
                diff *= (topSpeed * (extensionLength/maxRange) * 3) / Mag;
                proj.velocity = diff;
            }
            else
            {
                proj.Center = restingPoint;
            }








		}
		public void Oval(Projectile proj, float topSpeed, float maxRange)
		{
			Player player = Main.player[proj.owner];
			proj.ai[1] += 0.25f;
			if (proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
			float extensionLength = Vector2.Distance(Main.MouseWorld, player.Center);
			if (extensionLength > maxRange * 0.4f) { extensionLength = maxRange * 0.4f; }

			Vector2 SpinnyPlace = new Vector2((float)Math.Sin(proj.ai[1]) * extensionLength + (extensionLength), (float)Math.Cos(proj.ai[1]) * (extensionLength/3));
			float rotatyBoi = (Main.MouseWorld - player.Center).ToRotation();
			
			
			SpinnyPlace = Rotate(SpinnyPlace, rotatyBoi);
			SpinnyPlace += player.Center;
			proj.rotation += 0.2f;
			if (proj.ai[1] < 2f)
			{
				Vector2 diff = SpinnyPlace - proj.Center;
				float Mag = Vector2.Distance(SpinnyPlace, proj.Center);
				diff *= (topSpeed * (extensionLength / maxRange) * 3) / Mag;
				proj.velocity = diff;
			}
			else
			{
				proj.Center = SpinnyPlace;
			}








		}
		public override bool PreDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
			if (projectile.aiStyle == -2)
			{
				Player player = Main.player[projectile.owner];
				Vector2 vector = player.MountedCenter  + new Vector2(player.direction * 10 , -10 );
				vector.Y += Main.player[projectile.owner].gfxOffY;
				float num2 = projectile.Center.X - vector.X;
				float num3 = projectile.Center.Y - vector.Y;
				Math.Sqrt(num2 * num2 + num3 * num3);
				float Rotation = (float)Math.Atan2(num3, num2) - 1.57f;
				if (!projectile.counterweight)
				{
					int num5 = -1;
					if (projectile.position.X + (float)(projectile.width / 2) < Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
					{
						num5 = 1;
					}
					num5 *= -1;
					Main.player[projectile.owner].itemRotation = (float)Math.Atan2(num3 * (float)num5, num2 * (float)num5);
				}
				bool flag = true;
				if (num2 == 0f && num3 == 0f)
				{
					flag = false;
				}
				else
				{
					float num6 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
					num6 = 12f / num6;
					num2 *= num6;
					num3 *= num6;
					vector.X -= num2 * 0.1f;
					vector.Y -= num3 * 0.1f;
					num2 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
					num3 = projectile.position.Y + (float)projectile.height * 0.5f - vector.Y;
				}
				while (flag)
				{
					float speed = 12f;
					float mag = (float)Math.Sqrt(num2 * num2 + num3 * num3);
					float mag2 = mag;
					if (float.IsNaN(mag) || float.IsNaN(mag2))
					{
						flag = false;
						continue;
					}
					if (mag < 20f)
					{
						speed = mag - 8f;
						flag = false;
					}
					mag = 12f / mag;
					num2 *= mag;
					num3 *= mag;
					vector.X += num2;
					vector.Y += num3;
					num2 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
					num3 = projectile.position.Y + (float)projectile.height * 0.1f - vector.Y;
					if (mag2 > 12f)
					{
						float num10 = 0.3f;
						float num11 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
						if (num11 > 16f)
						{
							num11 = 16f;
						}
						num11 = 1f - num11 / 16f;
						num10 *= num11;
						num11 = mag2 / 80f;
						if (num11 > 1f)
						{
							num11 = 1f;
						}
						num10 *= num11;
						if (num10 < 0f)
						{
							num10 = 0f;
						}
						num10 *= num11;
						num10 *= 0.5f;
						if (num3 > 0f)
						{
							num3 *= 1f + num10;
							num2 *= 1f - num10;
						}
						else
						{
							num11 = Math.Abs(projectile.velocity.X) / 3f;
							if (num11 > 1f)
							{
								num11 = 1f;
							}
							num11 -= 0.5f;
							num10 *= num11;
							if (num10 > 0f)
							{
								num10 *= 2f;
							}
							num3 *= 1f + num10;
							num2 *= 1f - num10;
						}
					}
					Rotation = (float)Math.Atan2(num3, num2) - 1.57f;
					Color white = Color.White;
					white.A = (byte)((float)(int)white.A * 0.4f);
					white = TryApplyingPlayerStringColor(Main.player[projectile.owner].stringColor, white);
					float num12 = 0.8f * (Vector2.Distance(Main.player[projectile.owner].Center,vector) /(Vector2.Distance(Main.player[projectile.owner].Center, projectile.Center)*1.4f));
					white = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), white);
					Color color = new Color((byte)((float)(int)white.R * num12), (byte)((float)(int)white.G * num12), (byte)((float)(int)white.B * num12), (byte)((float)(int)white.A * num12));
					Texture2D text = Main.fishingLineTexture;
					Vector2 pos = new Vector2(vector.X - Main.screenPosition.X + (float)text.Width * 0.5f, vector.Y - Main.screenPosition.Y + (float)text.Height * 0.5f) - new Vector2(6f, 0f);
					spriteBatch.Draw(text, pos, new Rectangle(0, 0, text.Width, (int)speed), color, Rotation, new Vector2((float)text.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0);
					
				}
				
				Texture2D text2 = Main.projectileTexture[projectile.type];
				spriteBatch.Draw(text2, projectile.Center - Main.screenPosition, new Rectangle(0, 0, text2.Width, text2.Height), Color.White, projectile.rotation, new Vector2 (projectile.width/2,projectile.height /2 ), 1f, SpriteEffects.None, 0);

				
			}
			else { return true; }
			return false;
		}
		
		private static Color TryApplyingPlayerStringColor(int playerStringColor, Microsoft.Xna.Framework.Color stringColor)
		{
			if (playerStringColor > 0)
			{
				stringColor = WorldGen.paintColor(playerStringColor);
				if (stringColor.R < 75)
				{
					stringColor.R = 75;
				}
				if (stringColor.G < 75)
				{
					stringColor.G = 75;
				}
				if (stringColor.B < 75)
				{
					stringColor.B = 75;
				}
				switch (playerStringColor)
				{
					case 13:
						stringColor = new Color(20, 20, 20);
						break;
					case 0:
					case 14:
						stringColor = new Color(200, 200, 200);
						break;
					case 28:
						stringColor = new Color(163, 116, 91);
						break;
					case 27:
						stringColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
						break;
				}
				stringColor.A = (byte)((float)(int)stringColor.A * 0.4f);
			}
			return stringColor;
		}
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if(projectile.aiStyle == -2)
			{ Counterweight(target.Center, damage, knockback, Main.player[projectile.owner] , projectile); }
        }
        public void Counterweight(Vector2 hitPos, int dmg, float kb , Player player , Projectile projectile)
		{
			if (!player.yoyoGlove && player.counterWeight <= 0)
			{
				return;
			}
			int playerYoyoType = -1;
			int ExistingProjNum = 0;
			int CounterWeightNum = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
				{
					if (Main.projectile[i].counterweight)
					{
						CounterWeightNum++;
					}
					else if (Main.projectile[i].aiStyle == -2)
					{
						ExistingProjNum++;
						playerYoyoType = i;
					}
				}
			}
			if (player.yoyoGlove && ExistingProjNum < 2)
			{
				if (playerYoyoType >= 0)
				{
					Vector2 num3 = hitPos - player.Center;
					num3.Normalize();
					num3 *= 16f;
					Projectile.NewProjectile(player.Center.X, player.Center.Y, num3.X, num3.Y, Main.projectile[playerYoyoType].type, Main.projectile[playerYoyoType].damage, Main.projectile[playerYoyoType].knockBack, player.whoAmI, 1f);
				}
			}
			else if (CounterWeightNum < ExistingProjNum)
			{
				Vector2 Velocity = hitPos - player.Center;
				Velocity.Normalize();
				Velocity *= 16f;
				float knockback = (kb + 6f) / 2f;
				if (CounterWeightNum > 0)
				{
					int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y, Velocity.X, Velocity.Y, player.counterWeight, (int)((double)dmg * 0.8), knockback, player.whoAmI, 1f);
					Main.projectile[projid].timeLeft = projectile.timeLeft;
				}
				else
				{
					int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y, Velocity.X, Velocity.Y, player.counterWeight, (int)((double)dmg * 0.8), knockback, player.whoAmI);
					Main.projectile[projid].timeLeft = projectile.timeLeft;
				}
			}
		}



	}

}