using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class BeastSlayer3000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rips to the bone");
        }

        public override void SetDefaults()
        {
            item.damage = 98;
            item.ranged = true;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 70000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 22;
            item.crit = 11;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.Tsunami);
            recipe.AddIngredient(ItemID.CandyCornRifle);
            recipe.AddIngredient(ItemID.JackOLanternLauncher);
            recipe.AddIngredient(ItemID.StakeLauncher);
            recipe.AddIngredient(ItemID.EldMelter);
            recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddIngredient(ItemID.SnowmanCannon);
            recipe.AddIngredient(mod.ItemType("AimOfGods"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


            
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY+1 ,type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY-1, type, damage, knockBack, player.whoAmI);


            return false;
        }
    }
}