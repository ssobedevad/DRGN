using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class SkeletronPrime : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mecha-Exo-Skeleton");
            Tooltip.SetDefault("Taking damage increases defense by 50% for 10 seconds"+"\nBeing hit while this is active resets the duration and increases the defense bonus by 10%"+"\nUp to a maxiumum of 250% increased defense");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = ItemRarities.Mental;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().spEquip = true;
        }


    }
}