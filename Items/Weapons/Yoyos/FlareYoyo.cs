


using DRGN.Projectiles.Yoyos;
using DRGN.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class FlareYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of flare" + "\nShoots out sparks at nearby enemies" + "\nCreates flare explosions");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 12f;
			item.damage = 295;
			item.rare = ItemRarityID.Red;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 550000;
			item.shoot = mod.ProjectileType("FlareYoyo");
		}

		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes)
		// These are the ones that reduce damage of a melee weapon




		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			
			recipe.AddIngredient(ItemType<SolariumBar>(), 10);
			recipe.AddIngredient(ItemType<DragonScale>(), 10);
			
			recipe.AddTile(TileType<InterGalacticAnvilTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}