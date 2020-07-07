
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.ObsidianWeapons
{
    public class ObsidianThrowingAxe : ModItem
    {


        public override void SetDefaults()
        {
            item.damage = 32;
            item.useStyle = 1;
            item.useAnimation = 34;
            item.useTime = 34;
            item.shootSpeed = 12f;
            item.knockBack = 10f;
            item.width = 22;
            item.height = 22;
            item.scale = 1f;
            item.rare = ItemRarityID.Orange;
            item.value = 650;
            item.maxStack = 999;
            item.consumable = true;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ObsidianThrowingAxe");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SharpenedObsidian"), 4);
            recipe.AddIngredient(ItemID.IronBar, 2);


            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this, 35);
            recipe.AddRecipe();
        }

    }
}