using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class SolarFlare : ModItem
    {
        

        public override void SetDefaults()
        {
            item.damage = 145;
            item.melee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 13;
            item.value = 308000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 3;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("SolarFlareProj");
            
            item.shootSpeed = 15;
        }
       
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Daybreak, 600);
            if (target.boss == true)
                player.AddBuff(mod.BuffType("BossSlayer"), 360);
            
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar , 20);
            recipe.AddIngredient(mod.ItemType("SpaceSaber"));
            recipe.AddIngredient(mod.ItemType("TerrorBlade"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}