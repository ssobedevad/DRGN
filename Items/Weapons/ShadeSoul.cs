using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Tiles;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class ShadeSoul : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 10;
            item.magic = true;

            item.useTime = 35;
            item.useAnimation = 35;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 18000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("ShadeSoul");
            item.mana = 25;
            item.shootSpeed = 14f;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowSoul"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


