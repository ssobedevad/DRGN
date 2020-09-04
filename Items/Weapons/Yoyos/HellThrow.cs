


using DRGN.Projectiles.Yoyos;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class HellThrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of hellfire" + "\nIgnites nearby enemies");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 2f;
			item.damage = 18;
			item.rare = ItemRarityID.Orange;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 16000;
			item.shoot = mod.ProjectileType("HellThrow");
		}

		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes)
		// These are the ones that reduce damage of a melee weapon




		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.HellstoneBar, 12);
			recipe.AddIngredient(mod.ItemType("AshenWood"), 12);
			recipe.AddIngredient(mod.ItemType("FlareCrystal"), 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}