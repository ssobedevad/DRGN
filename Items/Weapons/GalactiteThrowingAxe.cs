
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class GalactiteThrowingAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws an axe of the galaxies");
        }

        public override void SetDefaults()
        {
            item.damage = 800;
            item.useStyle = 5;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 40f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.value = 1050000;
            item.rare = ItemRarityID.Purple;
            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("GalactiteAxeHead");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScythe"));

            recipe.AddIngredient(mod.ItemType("GalactiteBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}