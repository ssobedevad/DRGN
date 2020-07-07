using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class MarshmallowRifle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Uses Marshmallows as ammo" + "\nSticks to hit enemies");
        }
        public override void SetDefaults()
        {
            item.damage = 35;
            item.ranged = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 30000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("Marshmallow");
            item.useAmmo = ItemID.Marshmallow;
            item.shootSpeed = 16;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextBool();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 55;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-0.2f, 0.2f), type, damage, knockBack, player.whoAmI);
            
            return false;
        }
        
    }
}