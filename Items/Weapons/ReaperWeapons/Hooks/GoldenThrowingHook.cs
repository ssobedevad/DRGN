

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Projectiles.Reaper;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class GoldenThrowingHook : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Hitting enemies leaves a hook that can be retracted with right click");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 12;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2f;
            item.value = 80000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 6.5f;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.noMelee = true;
            type = Hook;
            projectileText = ModContent.GetTexture("DRGN/Projectiles/Reaper/Hooks/GoldenHook");
            chaintext = ModContent.GetTexture("DRGN/Projectiles/Reaper/Chains/ReaperChainGold");
            item.useTurn = true;

        }




    }
}