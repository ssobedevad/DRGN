﻿using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class WallOfFlesh : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Screaming Mouth");
            Tooltip.SetDefault("Being Hit pushes nearby enemies away");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.expert = true;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().wofEquip = true;
        }


    }
}