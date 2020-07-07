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
            Tooltip.SetDefault("Pretty Terrential");
        }

        public override void SetDefaults()
        {
            item.damage = 600;
            item.magic = true;
            
            item.useTime = 13;
            item.useAnimation = 13;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 550000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("UnstableMeteor");
            item.mana = 6;
            item.crit = 12;
            item.shootSpeed = 12;

        }
        
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 12);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 12);
            recipe.AddIngredient(mod.ItemType("FlareSpewer"));
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}


