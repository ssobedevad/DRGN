

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
    public class GalactiteThrowingAxe : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 800;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10f;
            item.value = 2000000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Scythe;
            item.useTurn = true;
            DashSpeed = 12f;
            item.useTurn = true;
            scytheThrowStyle = 1;
            scytheThrownTexture = ModContent.GetTexture("DRGN/Projectiles/Reaper/GalactiteAxeHead");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScythe"));

            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}