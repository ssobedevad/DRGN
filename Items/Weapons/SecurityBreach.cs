using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class SecurityBreach : ModItem
    {

        public override void SetStaticDefaults()
        {
           
            Tooltip.SetDefault("Shoots an explosive ball of binary");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 40;
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
            item.shoot = mod.ProjectileType("SecurityBreach");
            item.mana = 8;
            
            item.shootSpeed = 12f;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
           
            recipe.AddIngredient(mod.ItemType("TechnoBar"),18);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}


