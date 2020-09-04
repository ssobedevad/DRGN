using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DRGN.Tiles;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class FlameSceptre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 18000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("FireOrb");
            item.mana = 20;
            item.shootSpeed = 8f;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 18);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 22);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


