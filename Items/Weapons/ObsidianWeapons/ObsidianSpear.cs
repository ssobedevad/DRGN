
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.ObsidianWeapons
{
	public class ObsidianSpear : ModItem
	{


		public override void SetDefaults()
		{
			item.damage = 24;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 26;
			item.useTime = 26;
			item.shootSpeed = 5f;
			item.knockBack = 5.5f;
			item.width = 32;
			item.height = 32;
			
			item.rare = ItemRarityID.Orange;
			item.value = 5000;
			item.crit = 2;
			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("ObsidianSpear");
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("SharpenedObsidian"), 50);
			recipe.AddIngredient(ItemID.IronBar, 20);


			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}