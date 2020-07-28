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
    public class GalacticWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n250 summon tag damage" + "\n65 summon crit" + "\nCreates galactic explosions on enemies" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 18;
            item.useTime = 18;
            
            item.value = 1500000;
            item.shoot = mod.ProjectileType("GalacticWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 750;
            item.knockBack = 10f;
            item.shootSpeed = 8;
            item.rare = ItemRarities.GalacticRainbow;

        }
        public override void AddRecipes()
        {
            
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 14);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 10);
            recipe.AddIngredient(mod.ItemType("GalacticScale"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
