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
    public class AntEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant Essence");
            Tooltip.SetDefault("The essence of the ants ruler");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarityID.Orange;
            item.value = 1000;
            item.height = 22;
            item.width = 22;

        }


    }
}
