using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class SkeletronPrime : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mecha-Exo-Skeleton");
            Tooltip.SetDefault("Increases defense by 35"+"\nMax life by 50"+"\nGrants immunity to knockback");
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
            player.GetModPlayer<DRGNPlayer>().spEquip = true;
        }


    }
}