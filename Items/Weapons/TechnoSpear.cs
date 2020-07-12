
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class TechnoSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Freezes enemies on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 66;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 16;
            item.useTime = 16;
            item.shootSpeed = 9f;
            item.knockBack = 4.5f;
            
            
            item.value = 55000;
            item.rare = ItemRarityID.LightPurple;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("TechnoSpear");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 15);

            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}