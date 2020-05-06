using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class TrueArcanumWeatherGlobe : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Rains down purest green");
        }

        public override void SetDefaults()
        {
            item.damage = 600;
            item.magic = true;
            
            item.useTime = 10;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1000000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("TrueUnstableMeteor");
            item.mana = 14;
            item.crit = 30;
            item.shootSpeed = 12;

        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 30);
            recipe.AddIngredient(mod.ItemType("UnstableArcanumWeatherGlobe"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

}


