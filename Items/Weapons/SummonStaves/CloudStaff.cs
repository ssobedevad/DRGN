using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Tiles;

namespace DRGN.Items.Weapons.SummonStaves
{
    public class CloudStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloud Staff");
            Tooltip.SetDefault("The skies are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 75;
            item.summon = true;
            item.rare = ItemRarityID.Cyan;
            item.value = 100000;
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("CloudSummon");
            item.shoot = mod.ProjectileType("CloudSummon");
            item.useStyle = 1;
            item.noMelee = true;

        }
       
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmoBar>(), 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
