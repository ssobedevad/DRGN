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
    public class Webbed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Webbed");
            Description.SetDefault("You are targetted by the void snake");
            Main.debuff[Type] = true;
            longerExpertDebuff = true;
            Main.buffNoTimeDisplay[Type] = false;


        }
        public override void Update(Player player, ref int buffIndex)
        {
            
            if (player.GetModPlayer<DRGNPlayer>().voidDebuffReduced && player.buffTime[buffIndex] > 30){ player.buffTime[buffIndex] = 30; }
        }





       

    }
}
