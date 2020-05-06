using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class ElectroStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Zapp");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.mana = 5;
            item.rare = 5;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 25f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("ElectroStaffBolt");
            item.value = 100000;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


           Projectile.NewProjectile(player.Center, new Vector2(speedX,speedY), mod.ProjectileType("ElectroStaffBolt"), (int)(item.damage * player.magicDamage), 1f, player.whoAmI, -2, 1);
          
            return false;
        }

    }
}
