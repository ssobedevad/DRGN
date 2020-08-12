using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{

    public class SoulContainer : ModItem
    {


        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Increases maximum soul storage by 10");
        }

        public override void SetDefaults()
        {

            item.value = 45000;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 10;
        }


    }
}