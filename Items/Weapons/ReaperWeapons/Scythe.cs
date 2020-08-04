

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
    public class Scythe : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 18;
            
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Scythe;
            item.useTurn = true;
            DashSpeed = 5.5f;
            item.useTurn = true;
            scytheThrowStyle = 0;
        }





    }
}