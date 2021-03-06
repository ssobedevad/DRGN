﻿using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{

    public class AmuletOfSouls : ModItem
    {


        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Increases damage per soul by 0.01");
        }

        public override void SetDefaults()
        {

            item.value = 45000;
            item.rare = ItemRarityID.Green;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().damageIncPerSoul += 0.01f; 
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}