

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class FlareFlail : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/FlareFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FlareFlail"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 18));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 29));
			DRGN.FlailsNPCImmunity.Add(new Vector2(projectile.type, 6));
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 28, 32));

		}

		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.aiStyle = 15;
			projectile.GetGlobalProjectile<FlailsAI>().ChainTexture = ModContent.GetTexture(ChainTexturePath);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);
		}
		public override void PostAI()
		{

			
			float num21 = 400f;
			NPC nPC = null;
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] >= 10f)
				{
					projectile.localAI[0] = 9f;
					for (int i = 0; i < 200; i++)
					{
						NPC nPC2 = Main.npc[i];
						if (nPC2.CanBeChasedBy(this))
						{
							float num22 = projectile.Distance(nPC2.Center);
							if (!(num22 >= num21) && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
							{
								nPC = nPC2;
								num21 = num22;
							}
						}
					}
				}
				if (nPC != null)
				{
					projectile.localAI[0] = 0f;
					float scaleFactor = 12f;
					Vector2 center = projectile.Center;
					Vector2 velocity = Vector2.Normalize(nPC.Center - center) * scaleFactor;
					Projectile.NewProjectile(center, velocity, mod.ProjectileType("Spark"), (int)((double)projectile.damage / 1.5), projectile.knockBack / 2f, Main.myPlayer);
				}
			}
		}



	}
}
