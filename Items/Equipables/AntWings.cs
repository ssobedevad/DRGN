﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class AntWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("They can lift an insect but im not sure about a person");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 20000;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 110;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.35f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 0.55f;
            maxAscentMultiplier = 1f;
            constantAscend = 0.2f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 6f;
            acceleration *= 1f;
        }

       
    }
}
