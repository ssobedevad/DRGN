using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class HellFireCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into HellFire arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 16;
            item.noMelee = true;
            item.ranged = true;
           
            
            item.width = 58;
            item.height = 26;
            item.useTime = 20;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;
            item.shootSpeed = 25f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("ElectroStaffBolt");
            item.useAmmo = AmmoID.Arrow;
            item.value = 28000;
            item.rare = ItemRarityID.Orange;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), ProjectileID.HellfireArrow, damage, knockBack, player.whoAmI);
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
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            
            recipe.AddIngredient(mod.ItemType("EarthenCrossBow"));

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
