
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Shoes)]
    public class GalactiteBoosters : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactite Boosters");
            Tooltip.SetDefault("Allows flight, ultra fast running, and extra mobility on ice"
                             + "\n15 % increased movement speed"
                             + "\nProvides the ability to walk on water and lava"
                             + "\nGrants immunity to fire blocks immunity to lava");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 150000;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Lava Waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            // Frostspark Boots
            player.accRunSpeed = 15f;
            player.rocketBoots = 4;
            player.moveSpeed += 0.15f;
            player.iceSkate = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LavasparkBoots"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}