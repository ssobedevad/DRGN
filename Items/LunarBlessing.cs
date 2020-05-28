﻿using System;
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
    public class LunarBlessing : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Blessing");
            Tooltip.SetDefault("Grants you a permanent extra accessory slot");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 13;
            item.value = 0;
            item.useStyle = 5;
            item.consumable = true;
            item.useTime = 1;
            item.useAnimation = 1;
            item.height = 32;
            item.width = 32;
            item.noUseGraphic = true;

        }
        public override bool CanUseItem(Player player)
        {

            
            if (player.GetModPlayer<DRGNPlayer>().lunarBlessing == false) { return true; }
            else { return false ; }
            
        }
        public override bool UseItem(Player player)


        {
            player.extraAccessorySlots += 1;





            player.GetModPlayer<DRGNPlayer>().lunarBlessing = true;

            return true;
        }


    }
}