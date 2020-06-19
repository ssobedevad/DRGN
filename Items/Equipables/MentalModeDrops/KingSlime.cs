using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{
    
    public class KingSlime : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Shield");
            Tooltip.SetDefault("Hitting 100 crits grants a dhield of flaming gel that blocks projectiles");
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
            player.GetModPlayer<DRGNPlayer>().ksEquip = true;
        }


    }
}