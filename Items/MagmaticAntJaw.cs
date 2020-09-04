using DRGN.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    public class MagmaticAntJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarities.FieryOrange;
            item.value = 6000;
        }
    }
}
