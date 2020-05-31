﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class CloudBossBag : ModItem
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
            int choice = Main.rand.Next(3);
            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("CloudStaff"));
            }
            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("ElectroStaff"));
            }
            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("SunBook"));
            }
            player.QuickSpawnItem(mod.ItemType("CosmoOre"), Main.rand.Next(25,40));
        }

        public override int BossBagNPC => mod.NPCType("Cloud");
    }
}