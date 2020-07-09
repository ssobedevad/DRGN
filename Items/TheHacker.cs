using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class TheHacker : ModItem
    {
        public override void SetStaticDefaults()
        {
           
            Tooltip.SetDefault("Glows ");
        }

        public override void SetDefaults()
        {
            item.damage = 22;

            item.melee = true;

            item.useTurn = true;
            item.rare = ItemRarityID.LightPurple;
            item.value = 10000;
            
            item.useTime = 25;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            
            item.useAnimation = 25;
            item.pick = 195;
            item.axe = 60;
            item.tileBoost += 2;
        }
        public override void UpdateInventory(Player player)
        {

            if (player.HeldItem == item) { player.pickSpeed *= 1.5f; }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
