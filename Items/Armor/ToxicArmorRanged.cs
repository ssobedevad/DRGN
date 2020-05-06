
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Visor");
            Tooltip.SetDefault("35% increased ranged damage.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 1000;
            item.rare = 2;
            item.defense = 5;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage = (float)(1.35 * player.rangedDamage);
            
        }


            public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 12);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }


    
}
