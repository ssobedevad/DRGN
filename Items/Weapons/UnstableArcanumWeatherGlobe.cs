using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class UnstableArcanumWeatherGlobe : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("PrettyTerrential");
        }

        public override void SetDefaults()
        {
            item.damage = 355;
            item.magic = true;
            
            item.useTime = 15;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 500000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("UnstableMeteor");
            item.mana = 10;
            item.crit = 25;
            item.shootSpeed = 12;

        }
        
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
            recipe.AddIngredient(mod.ItemType("SoulOfTheBlueMoon"));
            
            recipe.AddTile(mod.TileType("InterGalacticAnvil"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}


