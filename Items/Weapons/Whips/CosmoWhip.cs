using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Tiles;

namespace DRGN.Items.Weapons.Whips
{
    public class CosmoWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n20 summon tag damage" + "\n10 summon crit" + "\nHitting enemies creates lunar fragments" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 28;
            item.useTime = 28;
            item.width = 18;
            item.height = 18;
            item.value = 75000;
            item.shoot = mod.ProjectileType("CosmoWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 62;
            item.knockBack = 4f;
            item.shootSpeed = 8;
            item.rare = ItemRarityID.Cyan;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmoBar>(), 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
