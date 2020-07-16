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
    public class CritCounter : ModBuff
    {
        
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Crit Counter");
            Description.SetDefault("");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            
            
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            int critCountReset = player.GetModPlayer<DRGNPlayer>().critCountResetable;
            
            
            string tooltip = ($"You have hit {critCountReset} Crits without being Hit.");
            tip = tooltip;
           
            rare = ItemRarityID.Expert;
        
        }







    }
}
