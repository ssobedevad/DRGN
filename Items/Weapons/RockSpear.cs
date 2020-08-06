
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
	public class RockSpear : ModItem
	{


		public override void SetDefaults()
		{
			item.damage = 82;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 26;
			item.useTime = 26;
			item.shootSpeed = 5f;
			item.knockBack = 5.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.value = 88000;
			item.rare = ItemRarityID.Lime;
			item.crit = 1;
			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("RockSpear");
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 8);
			recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}