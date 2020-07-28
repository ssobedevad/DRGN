using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class GalacticShower : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Rains galactic stars on enemies");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 350;
            item.magic = true;

            item.useTime = 12;
            item.useAnimation = 12;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = ItemRarities.GalacticRainbow;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GalactiteStarLaser");
            item.mana = 8;

            item.shootSpeed = 18f;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 startPos = new Vector2(position.X, position.Y - 800);
                startPos += new Vector2(Main.rand.NextFloat(-100, 100), Main.rand.NextFloat(-100, 100));

                Projectile.NewProjectile(startPos, findVel(startPos), mod.ProjectileType("GalactiteStarLaser"), damage, knockBack, player.whoAmI);
            }

            return false;
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
        private Vector2 findVel(Vector2 startPos)
        {
            Vector2 projVel = Main.MouseWorld - startPos;
            float magnitude = Magnitude(projVel);
            if (magnitude > item.shootSpeed)
            {
                projVel *= item.shootSpeed / magnitude;
            }
            return projVel;



        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }

}


