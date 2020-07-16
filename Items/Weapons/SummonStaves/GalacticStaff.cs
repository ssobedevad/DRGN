using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class GalacticStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Summons a galactic drone to fight for you");
        }
        public override void SetDefaults()
        {
            item.damage = 1050;
            item.summon = true;
            item.rare = ItemRarityID.Purple;
            item.value = 2500000;
            item.useTime = 15;
            item.useAnimation = 15;
            item.buffType = mod.BuffType("GalacticDrones");
            item.shoot = mod.ProjectileType("GalacticDrone");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            return true;
        }
        public override void AddRecipes()
        {

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 16);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 12);
            recipe.AddIngredient(mod.ItemType("GalacticScale"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
