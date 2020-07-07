
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.ObsidianWeapons
{
    public class ObsidianThrowingKnife : ModItem
    {


        public override void SetDefaults()
        {
            item.damage = 16;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shootSpeed = 18f;
            item.knockBack = 2.5f;
            item.width = 22;
            item.height = 22;
            
            item.rare = ItemRarityID.Blue;
            item.value = 650;
            item.maxStack = 999;
            item.consumable = true;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ObsidianThrowingKnife");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SharpenedObsidian"), 8);
            recipe.AddIngredient(ItemID.IronBar, 5);


            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this, 60);
            recipe.AddRecipe();
        }

    }
}