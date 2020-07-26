using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class IcePick : ModItem
    {
       
        public override void SetDefaults()
        {
            item.damage = 18;

            item.melee = true;

            item.useTurn = true;
            item.rare = ItemRarityID.Pink;
            item.value = 8000;

            item.useTime = 28;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;

            item.useAnimation = 28;
            item.pick = 195;
            item.tileBoost += 2;
            
            
        }
        public override void UpdateInventory(Player player)
        {

            if (player.HeldItem == item) { player.pickSpeed *= 2f; }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("GlacialBar"), 18);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
