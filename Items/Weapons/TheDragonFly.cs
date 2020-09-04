using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Rarities;
namespace DRGN.Items.Weapons
{
    public class TheDragonFly : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a dragonfly that rapidly flails at enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.thrown = true;
            item.channel = true; //Channel so that you can held the weapon [Important]

            
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("TheDragonFly");
            item.value = 408000;
            item.rare = ItemRarities.DarkBlue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntBiter"));
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"),25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
