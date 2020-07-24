using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class AntWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("They can lift an insect but im not sure about a person");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 20000;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 110;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.35f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 0.55f;
            maxAscentMultiplier = 1f;
            constantAscend = 0.2f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 6f;
            acceleration *= 1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 20);
            recipe.AddIngredient(mod.ItemType("AntWing"), 5);
            recipe.AddRecipeGroup("DRGN:T1Wings");
            recipe.AddIngredient(mod.ItemType("ElementalJaw"),12);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
           
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("AntKey"),3);

            recipe2.AddTile(ModContent.TileType<Tiles.AntsChest>());
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        
    }
    }
}
