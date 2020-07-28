using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class OmniBow : ModItem
    {
      
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into flare arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 112;
            item.ranged = true;
            
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 750000;
            item.rare = ItemRarities.VoidPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 22;
            item.crit = 3;
        }
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"),35);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 35);
            recipe.AddIngredient(mod.ItemType("FlareSpitter"));
            recipe.AddIngredient(mod.ItemType("ToxicRifle"));
            
            recipe.AddIngredient(mod.ItemType("ElementalAntJaws"));
            recipe.AddIngredient(mod.ItemType("Lobber"));
            recipe.AddIngredient(mod.ItemType("MagmaticHuntingRifle"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), mod.ProjectileType("FlareArrow"), damage, knockBack, player.whoAmI);
                }
                else { Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI); }
            }



            return false;
        }
    }
}