using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Tiles;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class ShadeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 16;
            item.magic = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 18000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("ShadeBlast");
            item.mana = 18;
            item.shootSpeed = 6f;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowStaff"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


