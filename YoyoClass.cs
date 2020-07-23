

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{

    public class YoyoClass : GlobalItem
    {
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if (ItemID.Sets.Yoyo[item.type])
            {
                add += player.GetModPlayer<DRGNPlayer>().YoyoDamageInc;

            }
            if(DRGN.FlailItem.Contains(item.type))
            {
                add += player.GetModPlayer<DRGNPlayer>().FlailDamageInc;
            }

        }
        public override void GetWeaponCrit(Item item, Player player, ref int crit)
        {
            if (ItemID.Sets.Yoyo[item.type])
            {
                crit += player.GetModPlayer<DRGNPlayer>().YoyoBonusCrit;

            }
            if (DRGN.FlailItem.Contains(item.type))
            {
                crit += player.GetModPlayer<DRGNPlayer>().FlailBonusCrit;
            }
        }

    }
}
