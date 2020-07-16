
using DRGN.Buffs.Minion;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class TechnoBossBag : ModItem
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
            
            int choice = Main.rand.Next(10);

            if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<TechnoWhip>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<TechnoShuriken>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<TechnoSlicer>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<TechnoSpear>());
            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ItemType<GlitchHunter>());
            }
            else if (choice == 5)
            {
                player.QuickSpawnItem(ItemType<SourceCode>());
            }
            else if (choice == 6)
            {
                player.QuickSpawnItem(ItemType<SourceThrow>());
            }
            else if (choice == 7)
            {
                player.QuickSpawnItem(ItemType<SecurityBreach>());
            }
            else if (choice == 8)
            {
                player.QuickSpawnItem(ItemType<BinaryStaff>());
            }

            else if (choice == 9)
            {
                player.QuickSpawnItem(ItemType<TheBug>());
            }


            player.QuickSpawnItem(mod.ItemType("TechnoOre"), Main.rand.Next(55, 75));
           
        }

        public override int BossBagNPC => mod.NPCType("TheVirus");
    }
}