

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
    public class GalacticShank : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 450;
            BloodHuntRange = 220;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 15f;
            item.value = 5000000;
            item.rare = ItemRarities.VoidPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 11f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("GalacticCurse"), 120);
            Projectile.NewProjectile(target.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"),damage, 0f, player.whoAmI, target.Center.Y - 10);
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), damage, 0f,player.whoAmI);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InterdimensionalImpaler"));
            recipe.AddIngredient(mod.ItemType("FlareStabber"));
            recipe.AddIngredient(mod.ItemType("DragonFlyKnife"));
            recipe.AddIngredient(mod.ItemType("LunarShank"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}