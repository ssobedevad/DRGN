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
    public class VoidBuff : ModBuff
    {


        
        
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Void Buff");
            
            Main.debuff[Type] = true;
            
           


        }
       




        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.GetGlobalNPC<DRGNGlobalNPC>().voidBuffLevel += 1;


            if (time > npc.buffTime[buffIndex])
            { npc.buffTime[buffIndex] = time; }
            return false;
        }

    }
}
