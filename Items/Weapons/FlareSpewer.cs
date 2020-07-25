using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Items.Weapons
{
    public class FlareSpewer : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Spews flares");
        }

        public override void SetDefaults()
        {
            item.damage = 180;
            item.magic = true;
            
            item.useTime = 3;
            item.useAnimation = 3;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 240000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("FireMeteor");
            item.mana = 3;
            item.crit = 2;
            item.shootSpeed = 12;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            
            
            
                Projectile.NewProjectile(position.X, position.Y, speedX + ((float)Main.rand.Next(-100, 100) / 100), speedY + ((float)Main.rand.Next(-100, 100)/100), type, damage, knockBack, player.whoAmI);
            

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 35);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 35);
            recipe.AddIngredient(mod.ItemType("SoulOfTheBlueMoon"));
            
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}

    
