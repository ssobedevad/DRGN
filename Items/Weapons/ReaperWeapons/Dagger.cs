

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
    public class Dagger : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Allows you to right click hunted enemies to jump to them and stab them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 12;
            BloodHuntRange = 50;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1.5f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 5f;
        }

       

        

    }
}