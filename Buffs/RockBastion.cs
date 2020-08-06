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
    public class RockBastion : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rock Bastion");
            Description.SetDefault("+20 defense and +20% true melee damage");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }


        







    }
}
