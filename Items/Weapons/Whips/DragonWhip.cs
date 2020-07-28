using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Weapons.Whips
{
    public class DragonWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n75 summon tag damage" + "\n25 summon crit" + "\nHitting enemies creates sparks and flares" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 24;
            item.useTime = 24;
            item.width = 18;
            item.height = 18;
            item.value = 300000;
            item.shoot = mod.ProjectileType("DragonWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 185;
            item.knockBack = 6f;
            item.shootSpeed = 8;
            item.rare = ItemRarities.FieryOrange;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 12);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
