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
    public class VoidBuff : ModBuff
    {
      
       
        
        
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Void Buff");
            
            Main.debuff[Type] = true;
            
           


        }
        public override void Update( NPC npc, ref int buffIndex)
        {
            if (DRGNPlayer.VoidEffect[npc.whoAmI] == null || DRGNPlayer.VoidEffect[npc.whoAmI] < 1 )
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Bottom.Y + 30, mod.NPCType("VoidBuffNPCEffect"), 0, npc.whoAmI);
                
                DRGNPlayer.VoidEffect[npc.whoAmI] = 1;

            }
            if (npc.buffTime[buffIndex] == 0)
            {
                DRGNPlayer.VoidEffect[npc.whoAmI] = 0;

            }
            


        
        
        
        }



        public override bool ReApply(NPC npc, int time, int buffIndex)
        {

            DRGNPlayer.VoidEffect[npc.whoAmI] += 1;


            return true;
        }

    }
}
