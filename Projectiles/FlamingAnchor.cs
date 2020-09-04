

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class FlamingAnchor : ModProjectile
	{
		private const string ChainTexturePath = "DRGN/Projectiles/FlamingAnchorChain";

		public override void SetStaticDefaults()
		{			
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 14));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 20));
			DRGN.FlailsNPCImmunity.Add(new Vector2(projectile.type, 15));
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 18, 22));
		}
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.aiStyle = 15;
			projectile.GetGlobalProjectile<FlailsAI>().ChainTexture = ModContent.GetTexture(ChainTexturePath);
		}
		public override void PostAI()
		{
			projectile.rotation = Vector2.Normalize(projectile.Center - Main.player[projectile.owner].Center).ToRotation() + MathHelper.ToRadians(45f);

		}
	}
}
