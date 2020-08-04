

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
    public class AntShank : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies to jump to them and stab them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 34;
            BloodHuntRange = 90;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3.5f;
            item.value = 80000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 7f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), damage, knockBack, player.whoAmI);
        }





    }
}