using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ToxicFang : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Sprays toxic fangs");
        }

        public override void SetDefaults()
        {
            item.damage = 14;
            item.noMelee = true;
            item.magic = true;
            
            item.mana = 12;
            
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
           
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("ToxicFang");
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"),12);
            recipe.AddIngredient(ItemID.Cactus,20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
