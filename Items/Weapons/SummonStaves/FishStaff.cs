﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class FishStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Staff");
            Tooltip.SetDefault("The fish are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 30;
            item.summon = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("IceFishMinion");
            item.shoot = mod.ProjectileType("IceFishMinion");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            return true;
        }

    }
}