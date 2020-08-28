using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Dusts
{
	public class ShadeDust : ModDust
	{
		public override bool MidUpdate(Dust dust)
		{
			if (!dust.noGravity)
			{
				dust.velocity.Y += 0.05f;
			}

			if (dust.noLight)
			{
				return false;
			}

			float strength = dust.scale * 1.4f;
			if (strength > 1f)
			{
				strength = 1f;
			}
			Lighting.AddLight(dust.position, 0.1f * strength, 0.2f * strength, 0.7f * strength);
			return false;
		}

	}
}