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
    public class FrogStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frog Staff");
            Tooltip.SetDefault("The frogs are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 12;
            item.summon = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("ToxicFrogMinion");
            item.shoot = mod.ProjectileType("ToxicFrogMinion");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int number = player.ownedProjectileCounts[mod.ProjectileType("ToxicFrogMinion")] + 1;
            int previous = -1;
            
            for (int i = 0; i < Main.projectile.Length; i++)
            { if (Main.projectile[i].type == mod.ProjectileType("ToxicFrogMinion") && Main.projectile[i].active) { if (Main.projectile[i].ai[0] == (number - 1)) { previous = i;} } }
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            int projid =Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI,number,previous);
            for (int i = 0; i < Main.projectile.Length; i++)
            { if (Main.projectile[i].type == mod.ProjectileType("ToxicFrogMinion") && Main.projectile[i].active) { if (Main.projectile[i].ai[0] == (number - 1)) { Main.projectile[i].localAI[1] = projid; } } }
            Main.projectile[projid].localAI[1] = -1;
            return false;
        }

    }
}
