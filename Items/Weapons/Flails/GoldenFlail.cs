


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class GoldenFlail : ModItem
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
			item.knockBack = 5f;
			item.damage = 22;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("GoldenFlail");
			item.shootSpeed = 11f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar,12);
			
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}