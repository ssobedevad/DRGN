using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

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
            item.damage = 130;
            item.magic = true;
            
            item.useTime = 2;
            item.useAnimation = 2;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 240000;
            item.rare = ItemRarities.FieryOrange;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("FireMeteor");
            item.mana = 4;
            item.crit = 2;
            item.shootSpeed = 12;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            
            
            
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.NextFloat(-1.5f,1.5f), speedY + Main.rand.NextFloat(-1.5f, 1.5f), type, damage, knockBack, player.whoAmI);
            

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

    
