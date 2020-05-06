using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
	public class MossyBowWood : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Keep it maybe something useful can be made from it");
		}

		public override void SetDefaults() 
		{
			item.damage = 2;
			item.ranged = true;
		
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle =   4;
			item.knockBack = 6;
			item.value = 1;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.noMelee = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}