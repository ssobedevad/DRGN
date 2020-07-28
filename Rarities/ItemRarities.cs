


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.ID;
using System.Linq;
using Terraria.ModLoader.Config;

namespace DRGN.Rarities
{
    public class ItemRarities : GlobalItem
    {
        

        public const short DarkBlue = -50;
        public const short FieryOrange = -49;
        public const short VoidPurple = -48;
        public const short GalacticRainbow = -47;
        public const short Mental = -100;

        
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.expert || item.rare >= GalacticRainbow && item.rare < -13 ) { tooltips[0].overrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB); }
            else if (DRGN._rarities.ContainsKey(item.rare))
            {
                
                tooltips[0].overrideColor = DRGN._rarities[item.rare];
            }
            
            else if (item.rare >= Mental && item.rare < DarkBlue) { tooltips[0].overrideColor = new AnimatedColor(Color.Blue, Color.Yellow).GetColor(); }
        }






    }
}
