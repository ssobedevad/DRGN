


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.Flails
{
	public class FlamingAnchor : ModItem
	{
		public override void SetDefaults()
		{

			item.value = 20000;
			item.rare = ItemRarityID.Blue;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 60;
			item.useTime = 60;
			item.knockBack = 5.5f;
			item.damage = 21;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("FlamingAnchor");
			item.shootSpeed = 11f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			DRGN.FlailItem.Add(item.type);
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 12);

			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}