
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class IceSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Freezes enemies on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 65;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 24;
            item.shootSpeed = 9f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 5;
            item.value = Item.sellPrice(silver: 10);

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("IceSpearProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 15);
            
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}