using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class StarBeeMiniGun : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots different bees based on Bee cloak tier");
        }

        public override void SetDefaults()
        {
            item.damage = 85;
            item.magic = true;
            item.mana = 8;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1000000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = ProjectileID.Bee;
            
            item.shootSpeed = 10;
            item.crit = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeeGun);
            recipe.AddIngredient(ItemID.WaspGun);
            recipe.AddIngredient(mod.ItemType("LunarStar"),30);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
     

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<DRGNPlayer>().protectorsVeil)
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-1, 1), speedY + Main.rand.Next(-1, 1), mod.ProjectileType("OmegaStarBee"), damage, knockBack, player.whoAmI, 90);
            else if (player.GetModPlayer<DRGNPlayer>().beeVeil)
            {
                for (int i = 0; i < Main.rand.Next(2, 5); i++)
                {

                    Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-1, 1), speedY + Main.rand.Next(-1, 1), mod.ProjectileType("StarBee"), damage, knockBack, player.whoAmI, 90);

                }
            }
            else
            {
                for (int i = 0; i < Main.rand.Next(1, 3); i++)
                {

                    Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-1, 1), speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI, 90);

                }
            }


            return false;
        }
    }
}