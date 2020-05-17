﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class AntsBossBag : ModItem
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
            int choice = Main.rand.Next(7);

            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("AntJaws"), 45);
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("AntBiter"));
            }


            player.QuickSpawnItem(mod.ItemType("AntJaw"), 45);
            player.QuickSpawnItem(mod.ItemType("AntEssence"), 45);
        }

        public override int BossBagNPC => mod.NPCType("QueenAnt");
    }
}