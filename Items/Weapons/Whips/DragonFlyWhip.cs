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
using DRGN.Rarities;

namespace DRGN.Items.Weapons.Whips
{
    public class DragonFlyWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n40 summon tag damage" + "\n15 summon crit" + "\nHitting enemies creates dragonflies" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 26;
            item.useTime = 26;
            item.width = 18;
            item.height = 18;
            item.value = 110000;
            item.shoot = mod.ProjectileType("DragonFlyWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            
            item.damage = 125;
            item.knockBack = 5f;
            item.shootSpeed = 8;
            item.rare = ItemRarities.DarkBlue;

        }
        public override void HoldItem(Player player)
        {
            item.autoReuse = player.GetModPlayer<DRGNPlayer>().WhipAutoswing;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ElementalJaw>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DragonFlyDust>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DragonFlyWing>(), 12);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
