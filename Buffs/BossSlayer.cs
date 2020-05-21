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
    public class BossSlayer : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("BossSlayer");
            Description.SetDefault("Courage within you grants you strength");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;


        }
        public override void Update(Player player, ref int buffIndex)
        {
           
            if ((Main.rand.Next(0, 7) == 1))
            {
                player.statLife += 1;
                player.HealEffect(1);
            }
        }
              
        
    
      
       
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            time += 5;
            return true;
        }

    }
}
