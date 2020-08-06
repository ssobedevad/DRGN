using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class LunarThistle : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Shoots a lunar thistle");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.magic = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 7f;
            item.value = 280000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("LunarThistleHead");
            item.mana = 16;

            item.shootSpeed = 34f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarFragment"),25);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 10);
            recipe.AddIngredient(ItemID.LunarBar,10);
            recipe.AddIngredient(mod.ItemType("BeetleThorn"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


