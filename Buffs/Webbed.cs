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
            Main.debuff[Type] = true;
            longerExpertDebuff = true;
            Main.buffNoTimeDisplay[Type] = false;


        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.controlUp = false;
            player.controlDown = false;
            player.controlLeft = false;
            player.controlRight = false;
            player.controlJump = false;
        }





       

    }
}
