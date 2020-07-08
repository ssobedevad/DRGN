using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Yoyos
{
	public class HellThrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// The following sets are only applicable to yoyo that use aiStyle 99.
			// YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 4.2f;
			// YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 200f;
			// YoyosTopSpeed is top speed of the yoyo projectile. 
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 9.8f;
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

		public override void PostAI()
		{
			
			BurnNearbyTargets();


		}
		private void BurnNearbyTargets()
		{
			for(int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(this, false))
				{

					float DistanceProjtoNpc = Vector2.Distance(Main.npc[i].Center, projectile.Center);

					if ((DistanceProjtoNpc < 100))
					{

						NPC targetNPC = Main.npc[i];
						targetNPC.AddBuff(BuffID.OnFire, 60);

					}


				}

			}
			
			
		}

		


		


	}
}