using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class MagmaticCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into flare arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 333;
            item.noMelee = true;
            item.ranged = true;
           
            
            item.width = 58;
            item.height = 26;
            item.useTime = 17;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;
            item.shootSpeed = 45f;
            item.useAnimation = 17;
            item.shoot = mod.ProjectileType("FlareArrow");
            item.useAmmo = AmmoID.Arrow;
            item.value = 450000;
            item.rare = ItemRarityID.Red;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("FlareArrow"), damage, knockBack, player.whoAmI);
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
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 10);
            recipe.AddIngredient(mod.ItemType("DragonFlyCrossBow"));

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
