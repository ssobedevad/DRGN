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
    public class VoidSnakeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Snake Staff");
            Tooltip.SetDefault("The void snake will protect you");
        }
        public override void SetDefaults()
        {
            item.damage = 500;
            item.summon = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("VoidSnakeMinion");
            item.shoot = 1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("VoidSnakeMinionBody")] < player.maxMinions)
            { return true; }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            
            if (player.ownedProjectileCounts[mod.ProjectileType("VoidSnakeMinionHead")] <= 0 )
            { 

                int HeadID = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("VoidSnakeMinionHead"), damage, knockBack, player.whoAmI,-2);
                


            }
            else
            { 
                for (int i = 0; i < Main.projectile.Length; i++) { if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("VoidSnakeMinionTail") && Main.projectile[i].owner == player.whoAmI) { Main.projectile[i].type = mod.ProjectileType("VoidSnakeMinionBody");int TailID = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("VoidSnakeMinionTail"), damage, knockBack, player.whoAmI, i);break;  }  } 
            
            
            }
            return false;
        }

    }
}
