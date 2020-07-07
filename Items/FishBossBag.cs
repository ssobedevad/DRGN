
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class FishBossBag : ModItem
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
            item.rare = ItemRarityID.Purple;
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
            int choice = Main.rand.Next(7);

            if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<IcicleBlaster>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<IcicleSlicer>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<IceChainWhip>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<IceChains>());
            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ItemType<FishStaff>());
            }
            else if (choice == 5)
            {
                player.QuickSpawnItem(ItemType<ArcticHuntingRifle>());
            }
            else if (choice == 6)
            {
                player.QuickSpawnItem(ItemType<IceSpear>());
            }


            player.QuickSpawnItem(mod.ItemType("GlacialOre"), 45);
            player.QuickSpawnItem(mod.ItemType("GlacialShard"),45);
        }

        public override int BossBagNPC => mod.NPCType("IceFish");
    }
}