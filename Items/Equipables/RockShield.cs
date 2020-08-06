using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{

    public class RockShield : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rock Shield");
            Tooltip.SetDefault("Increases defense by 20 and max life by 100 but reduces movement speed by 35%" + "\nIncreases fall speed by 50%");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 50000;
            item.rare = ItemRarityID.Green;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.statDefense += 20;
            player.statLifeMax2 += 100;
            player.runAcceleration *= 0.65f;
            player.maxRunSpeed *= 0.65f;
            player.maxFallSpeed *= 1.5f;
        }


    }
}