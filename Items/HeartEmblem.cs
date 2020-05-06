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
    public class HeartEmblem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HeartEmblem");
            Tooltip.SetDefault("increases max life by 50. ");

        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.maxStack = 1;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.autoReuse = true;

        }
        public override bool CanUseItem(Player player)
        {
            
            if (player.statLifeMax == 500) { return true; }
            //if (DRGNPlayer.heartEmblem == true) { return false; }
            else { return false; }
        }
        public override bool UseItem(Player player)


        {
            player.HealEffect(50, true);
            player.statLifeMax2 += 50;
            
            player.statLife += 50;
            DRGNPlayer.heartEmblem = true;
            return true;
        }
    }
}
