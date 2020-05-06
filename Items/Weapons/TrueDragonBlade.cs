using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class TrueDragonBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault(" that strange glow ain't good for no ones health");
        }

        public override void SetDefaults()
        {
            item.damage = 440;
            item.melee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 25;
            item.value = 1000000;
            item.rare = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 35;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("TrueDragonBladeProj");
            
            item.shootSpeed = 16;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            
            target.AddBuff(BuffID.Daybreak, 600);
            if (target.boss == true )
                player.AddBuff(mod.BuffType("BossSlayer"), 360);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"),30);
            recipe.AddIngredient(mod.ItemType("DragonBlade"));
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
      
    }
}