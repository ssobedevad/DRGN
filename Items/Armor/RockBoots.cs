
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class RockBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Rock boots");
            Tooltip.SetDefault("Standing still grants + 75 defense");
        }

        public override void SetDefaults()
        {
            
            item.value = 35000;
            item.rare = ItemRarityID.Lime;
            item.defense = 20;

        }
        public override void UpdateEquip(Player player)
        {


            if(player.velocity == Vector2.Zero)
            { player.statDefense += 75; }


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
