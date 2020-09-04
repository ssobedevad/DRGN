
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilShuriken : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 94;
            item.useStyle = 1;
            item.useAnimation = 14;
            item.useTime = 14;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.value = 250000;
            item.rare = ItemRarityID.Purple;
            item.thrown = true;
            item.noMelee = true; 
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("CrystilShuriken");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}