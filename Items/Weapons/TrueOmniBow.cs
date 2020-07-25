using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class TrueOmniBow : ModItem
    {
     
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("pretty cool");
        }

        public override void SetDefaults()
        {
            item.damage = 145;
            item.ranged = true;
            
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1050000;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 25;
            item.crit = 5;
        }
       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddIngredient(mod.ItemType("OmniBow"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
       

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            for (int i = 0; i < 6; i++)
            {
               
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI);
               
            }


            return false;
        }
    }
}