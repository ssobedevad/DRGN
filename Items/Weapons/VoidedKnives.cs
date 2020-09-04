
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class VoidedKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws knives in an arc");
        }

        public override void SetDefaults()
        {
            item.damage = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            
            
            item.value = 100000;
            item.rare = ItemRarityID.LightPurple;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidedKnives");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numShots = Main.rand.Next(3, 5);
            Vector2[] speeds = DavesUtils.GenerateRandomisedSpread(new Vector2(speedX, speedY), 22, numShots);
            for (int i = 0; i < numShots; ++i)
            {
                Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidKey"));

            recipe.AddTile(ModContent.TileType<Tiles.VoidChest>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}