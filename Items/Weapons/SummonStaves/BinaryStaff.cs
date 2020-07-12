using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class BinaryStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Summons friendly binary to protect you");
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.summon = true;
            item.rare = ItemRarityID.LightPurple;
            item.value = 25000;
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("BinaryBoi");
            item.shoot = mod.ProjectileType("BinaryBoi");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }
        

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;
            Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("BinaryBoi"), damage, knockBack, player.whoAmI, 0,1);
            return true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<TechnoBar>(), 12);
            


            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
