﻿


using DRGN.Projectiles.Yoyos;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Yoyos
{
	public class GlacialYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A ball of ice" + "\nShoots out ice shards at nearby enemies");


			ItemID.Sets.Yoyo[item.type] = true;

		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 3f;
			item.damage = 50;
			item.rare = ItemRarityID.Pink;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = 44000;
			item.shoot = mod.ProjectileType("GlacialYoyo");
		}

		// Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes)
		// These are the ones that reduce damage of a melee weapon




		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemType<GlacialBar>(), 12);
			recipe.AddIngredient(ItemType<GlacialShard>(), 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}