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
    public class Bugged : ModBuff
    {




        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bugged");
            Description.SetDefault("Being hit releases binary");
            Main.debuff[Type] = true;
            longerExpertDebuff = true;



        }
        public override void Update(NPC npc, ref int buffIndex)
        {


            int dustid = Dust.NewDust(npc.position, npc.height, npc.width, 107);
            Main.dust[dustid].noGravity = true;



        }
        





    }
}
