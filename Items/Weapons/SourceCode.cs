using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons
{
    public class SourceCode : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a stream of binary");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.noMelee = true;
            item.magic = true;
            item.mana = 10;

           
            item.useTime = 11;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;
            item.shootSpeed = 6f;
            item.useAnimation = 11;
            item.shoot = mod.ProjectileType("BinaryShot");
            item.autoReuse = true;
            item.value = 78000;
            item.rare = ItemRarityID.LightPurple;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

           
            
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.NextFloat(-1f, 1f), speedY + Main.rand.NextFloat(-1f,1f), item.shoot, damage, knockBack, player.whoAmI);
            

            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpellTome);
            
            recipe.AddIngredient(mod.ItemType("TechnoBar"),10);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
