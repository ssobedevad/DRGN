using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
	public class RustedBlade : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("kinda good kinda bad");
		}

		public override void SetDefaults() 
		{
            item.useTurn = true;
            item.damage = 8;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle =   1;
			item.knockBack = 6;
			item.value = 100;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("SnappedHandle"));
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}