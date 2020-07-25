using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
	public class TornBook : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Keep it maybe something useful can be made from it");
		}

		public override void SetDefaults() 
		{
			item.damage = 4;
			item.magic = true;
            
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle =   5;
			item.knockBack = 2f;
			item.value = 10;
			item.rare = ItemRarityID.Gray;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileID.Spark;
            item.mana = 2;
            
            item.shootSpeed = 16;
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