using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace DRGN.Items.Weapons.Whips
{
    public class Nunchucks : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Show off your ancient ninja skills");
        }
        public override void SetDefaults()
        {

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.useTime = 20;
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.shoot = mod.ProjectileType("Nunchucks");
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.melee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.damage = 35;
            item.knockBack = 1;
            item.shootSpeed = 8;
            item.rare = ItemRarityID.Yellow;

        }



    }
}
