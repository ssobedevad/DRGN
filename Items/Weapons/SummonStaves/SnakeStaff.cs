using System;
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
    public class SnakeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snake Staff");
            Tooltip.SetDefault("The snakes are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 9;
            item.summon = true;
            
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("DesertSnakeMinion");
            item.shoot = mod.ProjectileType("DesertSnakeMinion");
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
