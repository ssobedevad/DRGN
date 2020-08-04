

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class FlareStabber : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies to jump to them and stab them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 225;
            BloodHuntRange = 200;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 11f;
            item.value = 1500000;
            item.rare = ItemRarities.FieryOrange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 8.5f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, player.whoAmI);
        }





    }
}