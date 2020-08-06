


using DRGN.Projectiles.Yoyos;
using DRGN.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class CosmoYo : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of space" + "\nShoots out lunar shards at nearby enemies");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 6f;
			item.damage = 118;
			item.rare = ItemRarityID.Cyan;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 130000;
			item.shoot = mod.ProjectileType("CosmoYo");
		}

		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes)
		// These are the ones that reduce damage of a melee weapon




		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemType<CosmoBar>(), 12);
			recipe.AddIngredient(ItemID.SpectreBar, 12);
			recipe.AddTile(TileType<InterGalacticAnvilTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}