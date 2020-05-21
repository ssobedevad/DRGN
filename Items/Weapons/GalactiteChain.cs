using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class GalactiteChain : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Reality tearing Chain");
        }

        public override void SetDefaults()
        {
            item.damage = 390;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.thrown = true;
            item.channel = true; //Channel so that you can held the weapon [Important]

            item.rare = 12;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("GalactiteChain");
            item.value = 100000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonTail"));
            recipe.AddIngredient(mod.ItemType("VoidChain"));
            recipe.AddIngredient(mod.ItemType("IceChains"));
            recipe.AddIngredient(mod.ItemType("ThrowingTongue"));
            recipe.AddIngredient(mod.ItemType("TheDragonFly"));
            recipe.AddIngredient(ItemID.ChainGuillotines);
            recipe.AddIngredient(mod.ItemType("GalacticaBar") ,10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
