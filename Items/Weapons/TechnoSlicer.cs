﻿using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Buffs;

namespace DRGN.Items.Weapons
{
    public class TechnoSlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Inflicts bugged to enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 65;
            item.melee = true;
            item.useTime = 21;
            item.useAnimation = 21;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.value = 50000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            
            item.useTurn = true;

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 90);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<TechnoBar>(), 12);
            


            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}