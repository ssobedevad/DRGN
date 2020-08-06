using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{

    public class BloodCore : ModItem
    {


        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Increases bloodhunt range by 50");
        }

        public override void SetDefaults()
        {
           
            item.value = 35000;
            item.rare = ItemRarityID.Green;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 50;
        }
        

    }
}