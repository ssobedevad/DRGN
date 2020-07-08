using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Tiles;

namespace DRGN.Items.Weapons
{
    public class VoidedOrbs : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoots orbs that split on contact");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("VoidedOrb");
            item.mana = 9;
            
            item.shootSpeed = 16;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidKey"));
            
            recipe.AddTile(ModContent.TileType<Tiles.VoidChest>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


