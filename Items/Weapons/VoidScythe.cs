
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class VoidScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Absolutely shreds");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 30;
            item.shootSpeed = 40f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 5;
            item.value = Item.sellPrice(silver: 10);
            item.value = 1000000;
            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidScytheProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 18);
            recipe.AddIngredient(mod.ItemType("VoidBrick"), 15);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}