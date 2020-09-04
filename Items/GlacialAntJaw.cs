using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    public class GlacialAntJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarityID.LightRed;
            item.value = 3000;
        }
    }
}
