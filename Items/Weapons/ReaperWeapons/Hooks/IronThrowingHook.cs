

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
    public class IronThrowingHook : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Hitting enemies leaves a hook that can be retracted with right click");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 6;
            
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1.5f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 7.5f;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.noMelee = true;
            type = Hook;
            projectileText = ModContent.GetTexture("DRGN/Projectiles/Reaper/Hooks/IronHook");
            chaintext = ModContent.GetTexture("DRGN/Projectiles/Reaper/Chains/ReaperChainIron");
            item.useTurn = true;
            DashSpeed = 1.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("IronBar", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}