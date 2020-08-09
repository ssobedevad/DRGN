

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class GalactiteThrowingAxe : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 305;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10f;
            item.value = 2000000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Scythe;
            item.useTurn = true;
            DashSpeed = 10.5f;
            item.useTurn = true;
            scytheThrowStyle = 1;
            projectileText = ModContent.GetTexture("DRGN/Projectiles/Reaper/GalactiteAxeHead");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScythe"));
            recipe.AddIngredient(mod.ItemType("FlareReaver"));
            recipe.AddIngredient(mod.ItemType("DragonFlySlasher"));
            recipe.AddIngredient(mod.ItemType("LunarReaver"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("GalacticCurse"), 120);
            Projectile.NewProjectile(target.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"), damage, 0f, player.whoAmI, target.Center.Y - 10);
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), damage, 0f, player.whoAmI);
        }


    }
}