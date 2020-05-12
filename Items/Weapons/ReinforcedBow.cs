using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class ReinforcedBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Finally some accuracy");
        }

        public override void SetDefaults()
        {
            item.damage = 14;
            item.ranged = true;

            item.useTime = 38;
            item.useAnimation = 38;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 14;
        }
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar,12);
            recipe.AddIngredient(mod.ItemType("SturdyBow"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}