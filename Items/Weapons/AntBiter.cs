using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class AntBiter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly flails ants at enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
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
            item.shoot = mod.ProjectileType("AntBiter");
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 25);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
