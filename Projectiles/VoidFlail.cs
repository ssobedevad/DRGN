

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class VoidFlail : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/VoidFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Flail"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 20));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 31));
			DRGN.FlailsNPCImmunity.Add(new Vector2(projectile.type, 5));
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 30, 34));

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
			Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("VoidExplosion"), projectile.damage, 0f, projectile.owner);
			Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), mod.ProjectileType("VoidedArrow"), projectile.damage, 0f, projectile.owner,1);
		}
		



	}
}
