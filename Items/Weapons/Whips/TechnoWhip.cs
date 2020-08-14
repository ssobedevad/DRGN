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
    public class TechnoWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n10 summon tag damage" + "\n4 summon crit" + "\nInflicts giltched" + "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 30;
            
            item.value = 18000;
            item.shoot = mod.ProjectileType("TechnoWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 45;
            item.knockBack = 2.75f;
            item.shootSpeed = 8;
            item.rare = ItemRarityID.LightPurple;

        }
        public override void HoldItem(Player player)
        {
            item.autoReuse = player.GetModPlayer<DRGNPlayer>().WhipAutoswing;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ModContent.ItemType<TechnoBar>(), 12);


            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
