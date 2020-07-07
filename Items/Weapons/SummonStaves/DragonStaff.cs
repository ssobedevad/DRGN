using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class DragonStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Staff");
            Tooltip.SetDefault("The dragon will protect you");
        }
        public override void SetDefaults()
        {
            item.damage = 185;
            item.summon = true;
            item.rare = ItemRarityID.Red;
            item.value = 400000;
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("DragonMinion");
            item.shoot = mod.ProjectileType("DragonMinion");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            if (player.ownedProjectileCounts[mod.ProjectileType("DragonMinion")] > 0)
            {
                for (int i = 0; i < Main.projectile.Length; i ++)
                { if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("DragonMinion") && Main.projectile[i].ai[1] < player.maxMinions && Main.projectile[i].owner == player.whoAmI) { Main.projectile[i].ai[1] += 1; } }
            }
            else
            {

                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI,damage,1);
            }


            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 12);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
