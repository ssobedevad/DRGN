using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class  PoisonBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("a little sting won't do no harm");
        }

        public override void SetDefaults()
        {
            item.damage = 22;
            item.melee = true;
            item.useTurn = true;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = 11000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BladeofGrass);
            recipe.AddIngredient(mod.ItemType("TwoEvils"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}