
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Shoes)]
    public class LavasparkBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lavaspark Boots");
            Tooltip.SetDefault("Allows flight, super fast running, and extra mobility on ice"
                             + "\n9 % increased movement speed"
                             + "\nProvides the ability to walk on water and lava"
                             + "\nGrants immunity to fire blocks immunity to lava");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 35000;
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Lava Waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            // Frostspark Boots
            player.accRunSpeed = 8.75f;
            player.rocketBoots = 4;
            player.moveSpeed += 0.09f;
            player.iceSkate = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostsparkBoots);
            recipe.AddIngredient(ItemID.LavaWaders);
            recipe.AddIngredient(ItemID.ObsidianRose);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 18);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}