using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class VoidCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into void arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 666;
            item.noMelee = true;
            item.ranged = true;
           
            item.rare = 5;
            item.width = 58;
            item.height = 26;
            item.useTime = 14;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;
            item.shootSpeed = 45f;
            item.useAnimation = 14;
            item.shoot = mod.ProjectileType("VoidArrow");
            item.useAmmo = AmmoID.Arrow;
            item.value = 100000;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("VoidArrow"), damage, knockBack, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            }
          
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 10);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);
            recipe.AddIngredient(mod.ItemType("MagmaticCrossBow"));

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
