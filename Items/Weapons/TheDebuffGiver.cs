using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class  TheDebuffGiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("one touch from this and you're off");
        }

        public override void SetDefaults()
        {
            item.damage = 38;
            item.melee = true;
            item.useTurn = true;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = 80000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FieryGreatsword);
            recipe.AddIngredient(mod.ItemType("DungeonsWrath"));
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}