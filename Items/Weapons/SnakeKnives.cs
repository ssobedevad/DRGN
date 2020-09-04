
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class SnakeKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws snake fangs in an arc");
        }

        public override void SetDefaults()
        {
            item.damage = 8;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 12f;
            item.knockBack = 3.5f;


            item.value = 1000;
            item.rare = ItemRarityID.Blue;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("SnakeKnife");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numShots = Main.rand.Next(3, 5);
            Vector2[] speeds = DavesUtils.GenerateRandomisedSpread(new Vector2(speedX, speedY), 15, numShots);
            for (int i = 0; i < numShots; ++i)
            {
                Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SnakeScale>(), 14);
            recipe.AddIngredient(ItemID.Cactus, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
