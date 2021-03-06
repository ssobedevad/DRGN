﻿using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class SunBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoot a laser beam that ignores immunity frames");
        }

        public override void SetDefaults()
        {
            item.damage = 43;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.mana = 12;
            
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("SunRay");
            item.value = 308000;
            item.rare = ItemRarityID.Red;
        }

        
    }
}
