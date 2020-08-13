
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ShadowDemonBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased movement speed and acceleration" +"\n+5 Reaper Crit");
        }

        public override void SetDefaults()
        {            
            item.value = 600;
            item.rare = ItemRarityID.Blue;
            item.defense = 3;

        }
        public override void UpdateEquip(Player player)
        {
           
            player.runAcceleration *= 1.1f;
            player.maxRunSpeed *= 1.1f;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 5;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 4);
            recipe.AddIngredient(ItemID.Ebonwood, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.Vertebrae, 4);
            recipe2.AddIngredient(ItemID.Shadewood, 8);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }

    }
}
