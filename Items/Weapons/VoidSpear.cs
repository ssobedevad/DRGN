
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class VoidSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Powered by the void");
        }

        public override void SetDefaults()
        {
            item.damage = 235;
            item.useStyle = 5;
            item.useAnimation = 12;
            item.useTime = 18;
            item.shootSpeed = 14.7f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 5;
            item.value = 1000000;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidSlash");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 8);
            recipe.AddIngredient(mod.ItemType("VoidBrick"), 12);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}