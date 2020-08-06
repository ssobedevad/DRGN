


using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class VoidFlail : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 750000;
			item.rare = ItemRarities.VoidPurple;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 16;
			item.useTime = 16;
			item.knockBack = 10f;
			item.damage = 405;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("VoidFlail");
			item.shootSpeed = 16f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(mod.ItemType("FlareFlail"));

			recipe.AddIngredient(mod.ItemType("VoidSoul"), 22);
			recipe.AddIngredient(mod.ItemType("VoidBar"), 20);
			recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}