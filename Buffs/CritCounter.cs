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
            Player player = Main.player[Main.myPlayer];
            int critCountReset = player.GetModPlayer<DRGNPlayer>().critCountResetable;
            float tooltipBonusDamage = player.GetModPlayer<DRGNPlayer>().eowEquip ?  critCountReset* 0.5f : player.GetModPlayer<DRGNPlayer>().fdEquip? 0 : -1f;
            float tooltipBonusLifesteal = player.GetModPlayer<DRGNPlayer>().bocEquip ? critCountReset * 0.02f : player.GetModPlayer<DRGNPlayer>().fdEquip ? 0 : -1f;
            int tooltipBonusDefense = -1;
            int tooltipBonusMaxHealth = -1;
            if (player.GetModPlayer<DRGNPlayer>().fdEquip) { tooltipBonusDamage += critCountReset; tooltipBonusLifesteal += critCountReset * 0.04f; tooltipBonusDefense = critCountReset; tooltipBonusMaxHealth = critCountReset; }
            string tooltip = ($"You have hit {critCountReset} Crits without being Hit." +( tooltipBonusDamage>= 0f? $"\nThis increases your damage by {tooltipBonusDamage} %." : "") + (tooltipBonusLifesteal >= 0f? $"\nThis increases your lifesteal by { tooltipBonusLifesteal} %." : "") + (tooltipBonusDefense != -1 ? $"\nThis increases your defense by { tooltipBonusDefense}." : "") + (tooltipBonusMaxHealth != -1 ? $"\nThis increases your max life by { tooltipBonusMaxHealth}." : ""));
            tip = tooltip;
           
            rare = ItemRarityID.Expert;
        
        }







    }
}
