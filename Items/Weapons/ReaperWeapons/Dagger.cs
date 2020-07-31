

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
            
            Tooltip.SetDefault("Allows you to right click hunted enemies to teleport to them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 14;
            BloodHunt = new BloodHunt(300, 1);
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.crit = 4;
            item.useTurn = true;

        }

       

        

    }
}