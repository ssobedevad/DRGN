using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    public class ToxicAntJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarityID.Green;
            item.value = 2000;
        }
    }
}
