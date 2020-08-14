using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace DRGN.Items.Weapons.Whips
{
    public class SnakeWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Hisss" + "\n4 summon tag damage" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.width = 18;
            item.height = 18;
            item.value = 1000;
            item.shoot = mod.ProjectileType("SnakeWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 11;
            item.knockBack = 2;
            item.shootSpeed = 8;
            item.rare = ItemRarityID.Blue;

        }
        public override void HoldItem(Player player)
        {
            item.autoReuse = player.GetModPlayer<DRGNPlayer>().WhipAutoswing;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 12);
            recipe.AddIngredient(ItemID.Cactus, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
