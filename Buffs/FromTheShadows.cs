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
    public class FromTheShadows : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("From the shadows");
            Description.SetDefault("The next hit will restore your health by its base damage and be dodged");
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;



        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (DRGNPlayer.NinjaSuit && (DRGNPlayer.dodgeCounter == DRGNPlayer.dodgeCounterMax))
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
