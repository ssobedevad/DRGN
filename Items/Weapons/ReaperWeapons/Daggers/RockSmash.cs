

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
    public class RockSmash : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 80;
            BloodHuntRange = 170;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 8f;
            item.value = 320000;
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 6f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), damage, knockBack,player.whoAmI);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 12);
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}