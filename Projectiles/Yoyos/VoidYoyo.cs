using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Yoyos
{
	public class VoidYoyo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// The following sets are only applicable to yoyo that use aiStyle 99.
			// YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
			// YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 655f;
			// YoyosTopSpeed is top speed of the yoyo projectile. 
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 22f;
		}
		int shootCD = 0;
		public override void SetDefaults()
		{


			projectile.width = 28;
			projectile.height = 28;

			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			shootCD = 0;
		}

		public override void PostAI()
		{
			shootCD += 1;
			if (shootCD == 10)
			{
				Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<VoidedExplosion>(), projectile.damage, projectile.knockBack, projectile.owner);

			}
			if (shootCD >= 20 && VelocityToTarget() != Vector2.Zero)
			{
				int projid = Projectile.NewProjectile(projectile.Center, VelocityToTarget(), ModContent.ProjectileType<UnstableMeteor>(), projectile.damage, projectile.knockBack, projectile.owner);
				Main.projectile[projid].timeLeft = 20;
				shootCD = 0;
			}


		}
		private Vector2 VelocityToTarget()
		{
			int target = Target();
			if (target == -1) { return Vector2.Zero; }
			NPC targetNPC = Main.npc[target];
			Vector2 shootTo = targetNPC.Center - projectile.Center;
			shootTo = Vector2.Normalize(shootTo);
			shootTo *= 20;
			return shootTo;
		}

		private int Target()
		{
			int target = -1;
			int targetMag = 500;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(this, false))
				{

					float DistanceProjtoNpc = Vector2.Distance(Main.npc[i].Center, projectile.Center);

					if ((DistanceProjtoNpc < targetMag))
					{
						targetMag = (int)DistanceProjtoNpc;

						target = i;

					}


				}

			}
			return target;


		}


	}
}