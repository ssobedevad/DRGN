using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    public class LunarAntJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarityID.Red;
            item.value = 4000;
        }
    }
}
