using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace DRGN.Projectiles
{
	public abstract class ThornWeapon : ModProjectile
	{
		private Vector2 velocity;
		public bool head = false;
		public int Length;
		public int fadeSpeed;
		public int BodyType;
		public int partOffset = 34;
		public override void SetDefaults()
		{

			
			
			projectile.friendly = true;
			projectile.magic = true;
			
			projectile.penetrate = -1;
			SafeSetDefaults();
			projectile.aiStyle = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		public virtual void SafeSetDefaults() { }

		public override void AI()
		{
			if (velocity == Vector2.Zero) { velocity = projectile.velocity; projectile.velocity = Vector2.Zero; }

			projectile.rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
			
			if (projectile.ai[0] <= Length && head)
			{






				if (Main.myPlayer == projectile.owner)
				{






					int number = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X, velocity.Y, BodyType, projectile.damage, projectile.knockBack, projectile.owner);
					NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, number);
					projectile.ai[0] += 1;
					projectile.position += Vector2.Normalize(velocity)*partOffset;
				}

				return;
			}



			projectile.alpha += fadeSpeed;

			if (projectile.alpha >= 230)
			{
				projectile.Kill();
			}


		}
	}

}