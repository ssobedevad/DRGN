
using DRGN.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class CosmoSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Creates lunar fragments");
        }

        public override void SetDefaults()
        {
            item.damage = 76;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 11f;
            item.knockBack = 6.5f;
            
            item.value = 85000;
            item.rare = ItemRarityID.Cyan;
            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("CosmoSpearProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmoBar>(), 14);
            recipe.AddIngredient(ItemID.SpectreBar, 14);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}