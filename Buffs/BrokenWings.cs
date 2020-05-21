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
    public class BrokenWings : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Broken wings");
            Description.SetDefault("You cannot fly");
            Main.debuff[Type] = true;
           
            Main.buffNoTimeDisplay[Type] = false;


        }
        public override void Update(Player player, ref int buffIndex)
        {

            player.wingTime = 0;
            player.velocity *= 0.75f;
        }





        

    }
}
