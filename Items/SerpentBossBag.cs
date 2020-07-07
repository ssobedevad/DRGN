
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class SerpentBossBag : ModItem
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

            int choice = Main.rand.Next(5);

            if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<SnakeHeadThrown>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<SnakeSlayer>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<SnakeStaff>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<SnakeWhip>());
            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ItemType<ToxicFang>());
            }

            player.QuickSpawnItem(mod.ItemType("SnakeScale"), 45);
            player.QuickSpawnItem(ItemID.Cactus, 45);
        }

        public override int BossBagNPC => mod.NPCType("DesertSerpent");
    }
}