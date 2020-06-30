using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace DRGN.Items
{
    public class TheSwarm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Swarm");
            Tooltip.SetDefault("Causes the ground to tremble");
        }
        public override void SetDefaults()
        {
            item.height = 16;
            item.width = 32;
            item.rare = ItemRarityID.Expert;
            item.value = 0;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 1;
        }
        public override bool CanUseItem(Player player)
        {


             bool hp = (player.statLifeMax2 >= 200);
            return (hp);
        }
        public override bool UseItem(Player player)

        {

            Swarm.StartSwarm();                                                                             
            Main.NewText("The ground is trembling", 175, 75, 255);
        
            return true;



        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 1);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}
