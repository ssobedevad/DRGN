using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class MagmaticSpike : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Shoots a Magmatic Spike");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.magic = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 8f;
            item.value = 540000;
            item.rare = ItemRarities.FieryOrange;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("MagmaticSpikeHead");
            item.mana = 20;

            item.shootSpeed = 34f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 25);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 10);
            
            recipe.AddIngredient(mod.ItemType("LunarThistle"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

}


