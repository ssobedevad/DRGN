
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class CrystilBossBag : ModItem
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
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void OpenBossBag(Player player)
        {
            int rand = Main.rand.Next(1, 11);
            if (rand == 1)
            {player.QuickSpawnItem(mod.ItemType("CrystilBow")); }
            else if (rand == 2)
            { player.QuickSpawnItem(mod.ItemType("CrystilShuriken")); }
            else if (rand == 3)
            { player.QuickSpawnItem(mod.ItemType("CrystilSword")); }
            else if (rand == 4)
            { player.QuickSpawnItem(mod.ItemType("CrystilDagger")); }
            else if (rand == 5)
            { player.QuickSpawnItem(mod.ItemType("CrystilFlyingKnife")); }
            else if (rand == 6)
            { player.QuickSpawnItem(mod.ItemType("CrystilHook")); }
            else if (rand == 7)
            { player.QuickSpawnItem(mod.ItemType("CrystilKnives")); }
            else if (rand == 8)
            { player.QuickSpawnItem(mod.ItemType("CrystilScythe")); }
            else if (rand == 9)
            { player.QuickSpawnItem(mod.ItemType("CrsytilStaff")); }
            else if (rand == 10)
            { player.QuickSpawnItem(mod.ItemType("CrsytilTome")); }
            player.QuickSpawnItem(mod.ItemType("CrystilOre"), Main.rand.Next(20, 40));
        }
        public override int BossBagNPC => mod.NPCType("Crystil");
    }
}