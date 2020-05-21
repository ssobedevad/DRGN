using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class AntCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into ant arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 18;
            item.noMelee = true;
            item.ranged = true;
           
            item.rare = 5;
            item.width = 58;
            item.height = 26;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 25f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("ElectroStaffBolt");
            item.useAmmo = AmmoID.Arrow;
            item.value = 100000;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), mod.ProjectileType("AntArrow"), damage, knockBack, player.whoAmI);
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
            recipe.AddIngredient(mod.ItemType("AntJaw"), 20);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 10);
            recipe.AddIngredient(mod.ItemType("EarthenCrossBow"));

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
