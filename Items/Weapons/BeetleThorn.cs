using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class BeetleThorn : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Shoots a beetle thorn");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 42;
            item.magic = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 28000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("BeetleThornHead");
            item.mana = 14;

            item.shootSpeed = 34f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Vilethorn);
            recipe.AddIngredient(ItemID.CrystalVileShard);
            recipe.AddIngredient(ItemID.NettleBurst);
            recipe.AddIngredient(ItemID.BeetleHusk,10);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


