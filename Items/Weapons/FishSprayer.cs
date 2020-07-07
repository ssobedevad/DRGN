using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class FishSprayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots out jets of liquid based off what liquid you are submerged in");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            
            item.rare = ItemRarityID.Yellow;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.value = 35000;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.lavaWet) { Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("LavaBlast"), damage, knockBack, player.whoAmI); }
            else if (player.honeyWet) { Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("HoneyBlast"), damage, knockBack, player.whoAmI); }
            else if (player.wet) { Projectile.NewProjectile(position.X, position.Y, speedX, speedY , mod.ProjectileType("WaterBlast"), damage, knockBack, player.whoAmI); }
            
            
            return false;

        }


    }
}
