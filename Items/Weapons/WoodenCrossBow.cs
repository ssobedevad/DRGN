using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class WoodenCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into flaming arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 9;
            item.noMelee = true;
            item.ranged = true;
           
            item.rare = 2;
            item.width = 58;
            item.height = 26;
            item.useTime = 32;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 25f;
            item.useAnimation = 32;
            item.shoot = mod.ProjectileType("ElectroStaffBolt");
            item.useAmmo = AmmoID.Arrow;
            item.value = 1000;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), ProjectileID.FireArrow, damage, knockBack, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI);
            }
          
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.WhiteString);
            

            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
