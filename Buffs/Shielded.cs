using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Buffs
{
    public class Shielded : ModBuff
    {
        
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shielded");
            Description.SetDefault("");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
       

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            int defenseLevel = player.GetModPlayer<DRGNPlayer>().defenseLevel;
            int defenseInc = 50 + ( 10 * defenseLevel);


            string tooltip = ($"You have {defenseInc}% increased defense");
            tip = tooltip;

            rare = ItemRarityID.Expert;

        }







    }
}
