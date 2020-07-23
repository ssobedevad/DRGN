

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Mono.CompilerServices.SymbolWriter;


namespace DRGN.Projectiles
{
	public class LunarFlail : ModProjectile
	{
		// The folder path to the flail chain sprite
		private const string ChainTexturePath = "DRGN/Projectiles/LunarFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("LunarFlail"); // Set the projectile name to Example Flail Ball
			DRGN.FlailsRangeMult.Add(new Vector2(projectile.type, 17));
			DRGN.FlailsTopSpeed.Add(new Vector2(projectile.type, 27));
			DRGN.FlailsNPCImmunity.Add(new Vector2(projectile.type, 8));
			DRGN.FlailsMinPlayerDists.Add(new Vector3(projectile.type, 26, 30));
			
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
			Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("LunarExplosion"), projectile.damage, 0f, projectile.owner);
		}


        
	}
}
