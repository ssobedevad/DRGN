using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
namespace DRGN.Items
{
    public class CursedHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Heart");
            Tooltip.SetDefault("Used to activate Mental Mode"+"\nAlso activates expert mode");
            
            


        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.rare = ItemRarityID.Expert;
            item.value = 0;
            
            item.useStyle = 4;
            
            item.useTime = 5;
            item.useAnimation = 5;
            item.height = 30;
            item.width = 32;
            item.consumable = true;
            

        }
        
        public override bool UseItem(Player player)


        {

            Main.expertMode = true;
           DRGNModWorld.MentalMode = true; Main.NewText("Sanity is beyond your grasp", 255, 180, 180); 

            return true;
        }


    }
}
