
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class FrogBossBag : ModItem
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
            item.rare = 11;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
           
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ToxicFlesh"),45);
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("ToxicRifle"));
            }
            
            
            player.QuickSpawnItem(mod.ItemType("EarthenOre"), Main.rand.Next(35, 50));
            player.QuickSpawnItem(mod.ItemType("ToxicFlesh"), Main.rand.Next(15, 30));
        }

        public override int BossBagNPC => mod.NPCType("ToxicFrog");
    }
}