


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class SnailFlail : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 20000;
			item.rare = ItemRarityID.Blue;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 45;
			item.useTime = 45;
			item.knockBack = 7.75f;
			item.damage = 85;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("SnailFlail");
			item.shootSpeed = 11f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AntKey"));

			recipe.AddTile(ModContent.TileType<Tiles.AntsChest>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


	}
}