using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Linq;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class FireDragon : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragons Wrath");
            Tooltip.SetDefault("Crits increase damage by 1%, lifesteal by 0.04%, defense by 1 and max life by 1"+"\nResets upon taking damage" + "\nGrants the ability to dodge");
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
            player.GetModPlayer<DRGNPlayer>().fdEquip = true;
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