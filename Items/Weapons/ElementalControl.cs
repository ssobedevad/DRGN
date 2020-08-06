using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class ElementalControl : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots all the different dungeon caster projectiles");
        }
        private int randShot;
        public override void SetDefaults()
        {
            item.damage = 125;
            item.magic = true;
            item.mana = 12;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AntBiterJaws");
            
            item.shootSpeed = 16;
            randShot = 1;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (randShot == 1)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY,ProjectileID.InfernoFriendlyBolt, damage, knockBack, player.whoAmI);
                randShot = 2;
            }
            else if (randShot == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY , ProjectileID.LostSoulFriendly, damage, knockBack, player.whoAmI);
                randShot = 3;
            }
            else if (randShot == 3)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY , ProjectileID.ShadowBeamFriendly, damage, knockBack, player.whoAmI);
                randShot = 4;
            }
            else if (randShot == 4)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.UnholyTridentFriendly, damage, knockBack, player.whoAmI);
                randShot = 1;
            }
            if (randShot > 4 || randShot < 1) { randShot = 1; }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.UnholyTrident);
            recipe.AddIngredient(ItemID.ShadowbeamStaff);
            recipe.AddIngredient(ItemID.InfernoFork);
            recipe.AddIngredient(ItemID.SpectreStaff);
            recipe.AddIngredient(mod.ItemType("DemonSoul"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}