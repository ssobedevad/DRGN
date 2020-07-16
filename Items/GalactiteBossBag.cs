
using DRGN.Items.Equipables;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using DRGN.NPCs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class GalactiteBossBag : ModItem
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

            int choice = Main.rand.Next(13);

            if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<GalacticaBar>(), Main.rand.Next(25, 50));
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<GalacticOrbs>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<GalacticStaff>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<GalacticWhip>());
            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ItemType<GalactiteBow>());
            }
            else if (choice == 5)
            {
                player.QuickSpawnItem(ItemType<GalactiteChain>());
            }
            else if (choice == 6)
            {
                player.QuickSpawnItem(ItemType<GalactiteRifle>());
            }
            else if (choice == 7)
            {
                player.QuickSpawnItem(ItemType<GalaxySlicer>() );
            }
            else if (choice == 8)
            {
                player.QuickSpawnItem(ItemType<GalacticYoyo>());
            }
            else if (choice == 9)
            {
                player.QuickSpawnItem(ItemType<GalactiteShield>());
            }
            else if (choice == 10)
            {
                player.QuickSpawnItem(ItemType<GalactiteBrawlerGloves>());
            }
            else if (choice == 11)
            {
                player.QuickSpawnItem(ItemType<GalactiteWings>());
            }

            else if (choice == 12)
            {
                player.QuickSpawnItem(ItemType<GalactiteBoosters>());
            }
           

            player.QuickSpawnItem(ItemType<GalacticaOre>(), Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemType<GalacticEssence>(), Main.rand.Next(25, 50));
            player.QuickSpawnItem(ItemType<GalacticScale>(), Main.rand.Next(25, 50));
        }

        public override int BossBagNPC => NPCType<GalacticGuardian>();
    }
}