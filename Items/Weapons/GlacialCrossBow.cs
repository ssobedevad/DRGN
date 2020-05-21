using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class GlacialCrossBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into frostburn arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.noMelee = true;
            item.ranged = true;
           
            item.rare = 5;
            item.width = 58;
            item.height = 26;
            item.useTime = 19;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 25f;
            item.useAnimation = 19;
            item.shoot = mod.ProjectileType("ElectroStaffBolt");
            item.useAmmo = AmmoID.Arrow;
            item.value = 100000;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.FrostArrow, damage, knockBack, player.whoAmI);
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
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 20);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 10);
            recipe.AddIngredient(mod.ItemType("AntCrossBow"));

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
