
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class RockBossBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;

            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {


            if (DRGNModWorld.MentalMode)
            {
                player.QuickSpawnItem(mod.ItemType("RockMonarch"));
            }

           player.QuickSpawnItem(mod.ItemType("RockShield"));

           player.QuickSpawnItem(mod.ItemType("Flint"), Main.rand.Next(45, 80));
            player.QuickSpawnItem(mod.ItemType("SharpenedObsidian"), Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Ruby,  Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Emerald, Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Diamond, Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Sapphire, Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Topaz, Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Amethyst, Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemID.Amber, Main.rand.Next(25, 50));
        }

        public override int BossBagNPC => mod.NPCType("RockSlimeKing");
    }
}