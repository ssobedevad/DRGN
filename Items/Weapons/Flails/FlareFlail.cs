


using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class FlareFlail : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 450000;
			item.rare = ItemRarities.FieryOrange;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 18;
			item.knockBack = 8f;
			item.damage = 235;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("FlareFlail");
			item.shootSpeed = 16f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			
			recipe.AddIngredient(mod.ItemType("LunarFlail"));
			
			recipe.AddIngredient(mod.ItemType("DragonScale"), 22);
			recipe.AddIngredient(mod.ItemType("SolariumBar"), 20);
			recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}