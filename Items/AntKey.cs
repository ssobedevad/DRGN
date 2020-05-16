using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;

namespace DRGN.Items
{
    public class AntKey : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant Key");
            Tooltip.SetDefault("Opens a locked ant chest");

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.rare = 8;
            item.value = 1000;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.useTime = 25;
        }
        public override bool UseItem(Player player)

        {
           for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    WorldGen.PlaceChest((int)(player.Center.X / 16)-50 + i, (int)(player.Center.Y / 16) - 50 + j, (ushort)mod.TileType("AntsChest"), false, 1);//mod.TileType("AntsChest"));
                    if (Main.tile[(int)(player.Center.X / 16) - 50 + i, (int)(player.Center.Y / 16) - 50 + j].active()) { return false; }
                }
            }                                                                                             //WorldGen.PlaceChest((int)(player.Center.X / 16),(int)(player.Center.Y / 16),1,false,0);
                                                                                                              //WorldGen.Place2x2((int)(player.Center.X / 16), (int)(player.Center.Y / 16), (ushort)mod.TileType("AntsChest"), 0);




            return true;


        }

    }
}
