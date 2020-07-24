using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Linq;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class EaterOfWorlds : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormy Headband");
            Tooltip.SetDefault("Crits on bosses increase damage by 0.25% up to 100 times but resets upon taking damage");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.expert = true;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().eowEquip = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int[] types = new int[3] { ModContent.ItemType<BrainOfCthulu>(), ModContent.ItemType<EaterOfWorlds>(), ModContent.ItemType<FireDragon>() };
            for (int i = 3; i < 3 + 5 + player.extraAccessorySlots; i++)
            { if (player.armor[i] != null && i != slot) { if (types.Contains(player.armor[i].type)) { return false; } } }
            return true;
        }

    }
}