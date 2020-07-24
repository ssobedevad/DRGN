

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class SnailFlail : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/SnailFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snail Flail"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 16));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 23));
			
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 22, 26));

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
		



	}
}
