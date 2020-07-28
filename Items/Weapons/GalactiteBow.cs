using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class GalactiteBow : ModItem
    {

       

        public override void SetDefaults()
        {
            item.damage = 152;
            item.ranged = true;

            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 2850000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 16f;
            item.crit = 2;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 14);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 10);
            recipe.AddIngredient(mod.ItemType("GalacticScale"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 12;

            Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.NextFloat(-5, 5), speedY + Main.rand.NextFloat(-5, 5), type, damage, knockBack, player.whoAmI); 
            



            return false;
        }
    }
}