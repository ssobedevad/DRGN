
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class DragonFlyBossBag : ModItem
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
            
           
            int choice = Main.rand.Next(4);

            if (choice == 0)
            {
                player.QuickSpawnItem(ItemType<DragonFlyStaff>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ItemType<DragonFlyWhip>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ItemType<DragonFlySlicer>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ItemType<TheDragonFly>());
            }


            player.QuickSpawnItem(mod.ItemType("GalacticEssence"));
            
            player.QuickSpawnItem(mod.ItemType("GalacticScale"));
            


            player.QuickSpawnItem(mod.ItemType("DragonFlyWing"), 45);
            player.QuickSpawnItem(mod.ItemType("DragonFlyDust"), 45);
        }

        public override int BossBagNPC => mod.NPCType("DragonFly");
    }
}