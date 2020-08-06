using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.NPCs;

namespace DRGN.Buffs
{
    public class MarkedForDeath : ModBuff
    {




        public override void SetDefaults()
        {
            DisplayName.SetDefault("Marked for death");

            Main.debuff[Type] = true;




        }


        


        public override void Update(NPC npc, ref int buffIndex)
        {
            int dustid = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PurpleCrystalShard);
            Main.dust[dustid].noGravity = true;
        }

    }
}
