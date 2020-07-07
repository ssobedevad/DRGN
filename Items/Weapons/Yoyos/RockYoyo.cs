


using DRGN.Projectiles.Yoyos;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class RockYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of rocks" + "\nShoots out rocks at nearby enemies");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 5f;
			item.damage = 100;
			item.rare = ItemRarityID.Lime;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 110000;
			item.shoot = mod.ProjectileType("RockYoyo");
		}

		
	}
}