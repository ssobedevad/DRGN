
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Projectiles.Minion
{


	public class VoidSnakeMinionHead : VoidSnakeMinionAI
	{

		public override void SetStaticDefaults()
		{ // Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
			// This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			ProjectileID.Sets.Homing[projectile.type] = true;

		}
		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void SetDefaults()
		{


			projectile.aiStyle = -1;
			projectile.tileCollide = false;
			projectile.width = 48;
			projectile.height = 48;
			projectile.penetrate = -1;
			projectile.minion = true;
			projectile.minionSlots = 0f;
			projectile.friendly = true;
		}

		






	}

	public class VoidSnakeMinionBody : VoidSnakeMinionAI
	{

		public override void SetStaticDefaults()
		{ // Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
			// This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			ProjectileID.Sets.Homing[projectile.type] = true;

		}
		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void SetDefaults()
		{

			projectile.aiStyle = -1;

			projectile.tileCollide = false;
			projectile.width = 24;
			projectile.height = 24;
			projectile.penetrate = -1;
			projectile.minion = true;
			projectile.minionSlots = 1f;
			projectile.friendly = true;

		}
	}

	public class VoidSnakeMinionTail : VoidSnakeMinionAI
	{
		public override void SetStaticDefaults()
		{ // Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
			// This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			ProjectileID.Sets.Homing[projectile.type] = true;

		}
		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void SetDefaults()
		{
			projectile.minion = true;
			projectile.aiStyle = -1;
			projectile.tileCollide = false;
			projectile.width = 24;
			projectile.height = 24;
			projectile.penetrate = -1;
			projectile.minionSlots = 0f;
			projectile.friendly = true;
		}


	}



}