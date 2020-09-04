
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilKnives : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 42;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 12;
            item.useTime = 12;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.value = 120000;
            item.rare = ItemRarityID.Purple;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true; 
            item.autoReuse = true; 
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("CrystilKnife");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numShots = Main.rand.Next(2, 4);
            Vector2[] speeds = DavesUtils.GenerateRandomisedSpread(new Vector2(speedX, speedY), 12, numShots);
            for (int i = 0; i < numShots; ++i)
            {
                Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 8);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}