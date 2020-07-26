

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;
using System.Collections.Generic;
using System.Drawing.Text;

namespace DRGN.Projectiles
{
	public class BruteForce : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/BruteForceChain";
		private List<Projectile> binaryProjs  = new List<Projectile>();
		private Vector2[] offsets = new Vector2[8] { new Vector2(0, -50), new Vector2(38, -38), new Vector2(50, 0), new Vector2(38, 38), new Vector2(0, 50), new Vector2(-38, 38), new Vector2(-50, 0), new Vector2(-38, -38) };
		private int delay = -1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brute Force"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 16));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 23));
			DRGN.FlailsNPCImmunity.Add(new Vector2(projectile.type, 10));
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 22, 25));

		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.aiStyle = 15;
			projectile.GetGlobalProjectile<FlailsAI>().ChainTexture = ModContent.GetTexture(ChainTexturePath);
		}
		public override void PostAI()
		{
			
			Player player = Main.player[projectile.owner];
			float charge = projectile.GetGlobalProjectile<FlailsAI>().charge;
			if(projectile.ai[0] == 0 && player.controlUseItem)
			{ 
				if(charge > 0.125f * binaryProjs.Count)
				{
					int projid = Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<BinaryShot>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
					Main.projectile[projid].tileCollide = false;
					binaryProjs.Add(Main.projectile[projid]);
				}
					
			}
			else { delay += 1; }
			for(int i = 0; i < 8; i ++)
			{
				if (binaryProjs.Count > i)
				{
					if (delay < i * 5)
					{
						binaryProjs[i].Center = player.Center + offsets[i];
						binaryProjs[i].velocity = Vector2.Zero;
					}
					else if(binaryProjs[i].velocity == Vector2.Zero)
					{
						binaryProjs[i].velocity = Vector2.Normalize(Main.MouseWorld - binaryProjs[i].Center) * 14f;


					}
				}
			
			
			}
		}
        public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 8; i++)
			{
				if (binaryProjs.Count > i)
				{
					if (binaryProjs[i].velocity == Vector2.Zero)
					{
						binaryProjs[i].velocity = Vector2.Normalize(Main.MouseWorld - binaryProjs[i].Center) * 14f;


					}
				}


			}
		}



    }
}
