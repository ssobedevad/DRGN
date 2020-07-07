
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class VoidShuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws shurikens that create void on enemy hit");
        }

        public override void SetDefaults()
        {
            item.damage = 270;
            item.useStyle = 1;
            item.useAnimation = 5;
            item.useTime = 5;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.width = 22;
            item.height = 22;
            item.scale = 1f;
            item.value = 950000;
            item.rare = ItemRarityID.Purple;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidShuriken");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FlareShuriken"));
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 18);

            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}