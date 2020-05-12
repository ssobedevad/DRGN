using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{

    public class EssenceofExpert : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Essence of Expert");
            Tooltip.SetDefault("Grants a second chance when you need it");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 100000;
            item.expert = true;
            item.accessory = true;
            
        }


    }
}