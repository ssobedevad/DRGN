using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Tiles;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class GalacticOrbs : ModItem
    {

        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Shoots powerful orbs that split on contact and home");
        }

        public override void SetDefaults()
        {
            item.damage = 500;
            item.magic = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GalacticOrb");
            item.mana = 20;

            item.shootSpeed = 16;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 14);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 10);
            recipe.AddIngredient(mod.ItemType("GalacticScale"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


