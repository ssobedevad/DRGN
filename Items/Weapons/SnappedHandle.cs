using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
	public class SnappedHandle : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Keep it maybe something useful can be made from it");
		}

		public override void SetDefaults() 
		{
			item.damage = 2;
			item.melee = true;
            
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle =   1;
			item.knockBack = 6;
			item.value = 10;
			item.rare = ItemRarityID.Gray;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
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