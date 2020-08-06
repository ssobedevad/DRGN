using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class RockMonarch : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rock gauntlet");
            Tooltip.SetDefault("Standing still boosts defense by 20 and true melee damage by 20%");
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
           if(player.velocity == Vector2.Zero) { player.GetModPlayer<DRGNPlayer>().rmEquip = true; }
        }


    }
}