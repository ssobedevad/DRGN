


using DRGN.Projectiles.Yoyos;
using DRGN.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class VoidYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of the void" + "\nShoots out galactic stars at nearby enemies" + "\nCreates galactic explosions");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 15f;
			item.damage = 345;
			item.rare = ItemRarityID.Purple;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 1050000;
			item.shoot = mod.ProjectileType("VoidYoyo");
		}

		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes)
		// These are the ones that reduce damage of a melee weapon




		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);


			recipe.AddIngredient(ItemType<DragonFlyYoyo>());
			recipe.AddIngredient(ItemType<FlareYoyo>());
			recipe.AddIngredient(ItemType<CosmoYo>());
			recipe.AddIngredient(ItemType<ToxicYoyo>());
			recipe.AddIngredient(ItemType<RockYoyo>());
			recipe.AddIngredient(ItemType<GlacialYoyo>());
			recipe.AddIngredient(ItemType<SnakeFangYoyo>());
			recipe.AddIngredient(ItemID.Terrarian);
			recipe.AddIngredient(ItemID.TheEyeOfCthulhu);
			recipe.AddIngredient(ItemID.Kraken);
			recipe.AddIngredient(ItemType<VoidBar>(),10);
			recipe.AddTile(TileType<InterGalacticAnvilTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}