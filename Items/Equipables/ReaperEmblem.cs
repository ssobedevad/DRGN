using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Reflection;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{
    public class ReaperEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("15% increased reaper damage");
        }
        public override void SetDefaults()
        {
            item.value = 20000;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.15f;            
        }
    }
}