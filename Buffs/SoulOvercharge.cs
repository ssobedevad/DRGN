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
    public class SoulOvercharge : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Soul Overcharge");
            Description.SetDefault("");                 
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult += 0.01f * player.GetModPlayer<ReaperPlayer>().soulOverchargeLevel;
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            int critCountReset = player.GetModPlayer<ReaperPlayer>().soulOverchargeLevel;


            string tooltip = ($"You have {critCountReset} excess soul power.");
            tip = tooltip;

            rare = ItemRarityID.Expert;

        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            if(player.buffTime[buffIndex] > 180 * player.GetModPlayer<ReaperPlayer>().maxSouls2) { player.buffTime[buffIndex] = 180 * player.GetModPlayer<ReaperPlayer>().maxSouls2; }
            return false;
        }







    }
}
