using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class ToxicFrog : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Fly");
            Tooltip.SetDefault("Increases the effectiveness of the melting debuff"+"\nGrants increased defense in the jungle"+"\nImmunity to all thing toxic");
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
            player.GetModPlayer<DRGNPlayer>().tfEquip = true;
        }


    }
}