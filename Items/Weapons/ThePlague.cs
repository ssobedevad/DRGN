using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ThePlague : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("ribbit");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.noMelee = true;
            item.magic = true;
            item.useAnimation = 20;
            item.mana = 12;
            
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.reuseDelay = 30;
            item.useStyle = 5;
            item.shoot = ProjectileType<ExplodingFrog>();
            item.shootSpeed = 0;
            item.useAnimation = 20;

            item.value = 18000;
            item.rare = ItemRarityID.Green;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld,Vector2.Zero, type, damage, knockBack, player.whoAmI);
        
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 12);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
