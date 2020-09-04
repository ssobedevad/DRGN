using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class FlamingStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a friendly fireball");
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.summon = true;
            item.rare = ItemRarityID.Orange;
            item.value = 10000;
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("FireballMinion");
            item.shoot = mod.ProjectileType("LivingFireballMinion");
            item.useStyle = ItemUseStyleID.SwingThrow;
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

            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 12);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
