﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Yoyos
{
	public class MechaYoyo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// The following sets are only applicable to yoyo that use aiStyle 99.
			// YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 6.5f;
			// YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 295f;
			// YoyosTopSpeed is top speed of the yoyo projectile. 
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13.5f;
		}

		public override void SetDefaults()
		{


			projectile.width = 20;
			projectile.height = 20;

			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			
		}


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f)), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, projectile.owner);
        }

    }
}