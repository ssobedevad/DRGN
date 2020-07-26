


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class BeetleSmasher : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 80000;
			item.rare = ItemRarityID.Yellow;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 30;
			item.useTime = 30;
			item.knockBack = 6f;
			item.damage = 145;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("BeetleSmasher");
			item.shootSpeed = 16f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FlowerPow);
			recipe.AddIngredient(ItemID.DaoofPow);
			recipe.AddIngredient(ItemID.Sunfury);
			recipe.AddIngredient(ItemID.BeetleHusk, 10);
			recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 10);
			recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}