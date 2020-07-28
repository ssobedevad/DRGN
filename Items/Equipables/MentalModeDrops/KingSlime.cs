using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{
    
    public class KingSlime : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Sigil");
            Tooltip.SetDefault("Grants a permanent bonus of 8 critical armor penetration and 5% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = ItemRarities.Mental;
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.maxStack = 1;

        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<DRGNPlayer>().ksEquip;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().ksEquip = true;
            return true;
        }


    }
}