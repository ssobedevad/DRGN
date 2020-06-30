﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class AntStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant Staff");
            Tooltip.SetDefault("The ants are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 19;
            item.summon = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("QueenAntMinion");
            item.shoot = mod.ProjectileType("AntBarrel");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("AntBarrel")] > 0)
            {

                return false;



            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            
                return true;
            
        }

    }
}
