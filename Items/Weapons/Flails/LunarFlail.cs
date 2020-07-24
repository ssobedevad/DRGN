


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class LunarFlail : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 250000;
			item.rare = ItemRarityID.Red;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 22;
			item.useTime = 22;
			item.knockBack = 6f;
			item.damage = 195;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("LunarFlail");
			item.shootSpeed = 16f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Flairon);
			recipe.AddIngredient(ItemID.LunarBar,10);
			recipe.AddIngredient(mod.ItemType("BeetleSmasher"));
			recipe.AddIngredient(mod.ItemType("GalacticEssence"),8);
			recipe.AddIngredient(mod.ItemType("LunarFragment"),20);
			recipe.AddIngredient(mod.ItemType("CosmoBar"), 14);
			recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}