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
    public class Revival : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Revival");
            Description.SetDefault("The next fatal hit will restore you to full health");
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;



        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<DRGNPlayer>().secondlife && player.GetModPlayer<DRGNPlayer>().lifeCounter == player.GetModPlayer<DRGNPlayer>().lifeCounterMax)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }







    }
}
