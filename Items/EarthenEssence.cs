using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
namespace DRGN.Items
{
    public class EarthenEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Essence");
            Tooltip.SetDefault("A fragment of the earth");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 13;
            item.value = 10000;
            item.height = 22;
            item.width = 22;

        }


    }
}
