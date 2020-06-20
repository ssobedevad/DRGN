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
    public class CritCounterSlimeShield : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Slime Shield Cooldown Counter");
            Description.SetDefault("");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {


        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.player[Main.myPlayer];
            int numCrits = 100 - player.GetModPlayer<DRGNPlayer>().critCountNoreduce;
            string tooltip;
            if (numCrits <= 0)
            { tooltip = ("Slime Shield Activated"); }
            else
            {
                tooltip = ($"You need {numCrits} more Crits to activat the slime shield.");
            }
            tip = tooltip;

            rare = ItemRarityID.Expert;

        }







    }
}
