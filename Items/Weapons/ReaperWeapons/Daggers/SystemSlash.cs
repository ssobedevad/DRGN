

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Daggers
{
    public class SystemSlash : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 60;
            BloodHuntRange = 160;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4.5f;
            item.value = 220000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 8f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 90);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}