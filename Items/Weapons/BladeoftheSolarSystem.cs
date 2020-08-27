using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class BladeoftheSolarSystem : ModItem
    {
        public override void SetStaticDefaults()
        {
             DisplayName.SetDefault("Blade of the Solar System");          
        }

        public override void SetDefaults()
        {
            item.damage = 245;
            item.melee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 16;
            item.value = 125000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 4;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("SolarProj");
           
            item.shootSpeed = 11;
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
            recipe.AddIngredient(ItemID.Meowmere);
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddIngredient(mod.ItemType("SolarFlare"));
            recipe.AddIngredient(mod.ItemType("LunarFlare"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
       
    }
}