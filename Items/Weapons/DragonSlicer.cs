using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class DragonSlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Releases sparks on enemy hit");
        }

        public override void SetDefaults()
        {
            item.damage = 260;
            item.melee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 20;
            item.value = 750000;
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 28;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("DragonSlicerProj");
            
            item.shootSpeed = 9;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Daybreak, 600);
            if (target.boss == true)
                player.AddBuff(mod.BuffType("BossSlayer"), 360);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"),30);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
            recipe.AddIngredient(mod.ItemType("MeowMeow"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
       
    }
}