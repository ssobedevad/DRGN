
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class VoidBossBag : ModItem
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
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            int choice = Main.rand.Next(6);
            
            
             if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<VoidScythe>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<VoidPick>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<VoidChain>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<VoidSpear>());

            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ItemType<VoidSnakeStaff>());
            }
            else if (choice == 5)
            {
                player.QuickSpawnItem(ItemType<VoidSnakeWhip>());
            }




            player.QuickSpawnItem(mod.ItemType("VoidOre"), Main.rand.Next(40,60));
            player.QuickSpawnItem(mod.ItemType("VoidSoul"), Main.rand.Next(40, 60));
        }

        public override int BossBagNPC => mod.NPCType("VoidSnakeHead");
    }
}