using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilTome : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 36;
            item.noMelee = true;
            item.magic = true;
            item.mana = 12;
            item.useTime = 8;
            item.UseSound = SoundID.Item5;
            item.useStyle = 5;
            item.shootSpeed = 8f;
            item.useAnimation = 32;
            item.shoot = mod.ProjectileType("Crystil");
            item.autoReuse = true;
            item.value = 96000;
            item.rare = ItemRarityID.Purple;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vel = new Vector2(speedX, speedY);
            vel = DavesUtils.Rotate(vel, MathHelper.ToRadians(Main.rand.NextFloat(-30f, 30f)));
            Projectile.NewProjectile(position,vel, item.shoot, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
