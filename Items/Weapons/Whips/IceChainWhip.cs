﻿using System;
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
    public class IceChainWhip : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("\n12 summon tag damage" + "\n5 summon crit" +"\nRains icicles on hit enemies"+ "\nYour summons will focus struck enemies");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.shoot = mod.ProjectileType("IceChainWhip");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.summon = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 28;
            item.knockBack = 2.75f;
            item.shootSpeed = 8;
            item.rare = ItemRarityID.Pink;

        }



    }
}