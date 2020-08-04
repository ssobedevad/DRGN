

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class VoidScythe : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 300;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 9f;
            item.value = 950000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Scythe;
            item.useTurn = true;
            DashSpeed = 10.5f;
            item.useTurn = true;
            scytheThrowStyle = 0;
           
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 18);

            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}