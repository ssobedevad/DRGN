using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
	public class ShadeDagger : ModItem
	{

		public override void SetDefaults()
		{
			item.damage = 25;
			item.melee = true;
			item.useTime = 16;
			item.useTurn = true;
			item.useAnimation = 16;
			item.useStyle = 1;
			item.knockBack = 7/5f;
			item.value = 15000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("ShadowDagger"));
			recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}