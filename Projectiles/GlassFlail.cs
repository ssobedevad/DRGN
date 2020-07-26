

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class GlassFlail : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/GlassFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass Flail"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 16));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 25));

			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 24, 28));

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
			if (projectile.GetGlobalProjectile<FlailsAI>().charge >= 1f)
			{ Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<GlassShatter>(), projectile.damage / 2, projectile.knockBack / 2); }
			Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<GlassShatter>(), projectile.damage/2, projectile.knockBack/2);
		}




    }
}
