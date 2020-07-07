using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class DragonBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("got a dragon for a handle");
        }

        public override void SetDefaults()
        {
            item.damage = 325;
            item.melee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 25;
            item.value = 550000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 32;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("DragonBladeProj");
            
            item.shootSpeed = 12;
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

            recipe.AddIngredient(mod.ItemType("VoidBar"), 30);
            recipe.AddIngredient(mod.ItemType("DragonSlicer"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
   
       
    }
}