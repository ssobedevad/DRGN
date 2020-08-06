


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.ID;

using Terraria.ModLoader.Config;

namespace DRGN.Rarities
{
    public class ItemRarities : GlobalItem
    {
        public static Color darkBlue = new Color(30, 43, 82);
        public static Color darkBlue2 = new Color(80, 143, 158);
        public static Color fieryOrange = new Color(177, 38, 3);
        public static Color fieryOrange2 = new Color(247, 142, 4);
        public static Color voidPurple = new Color(96, 34, 186);
        public static Color mental = new Color(146, 4, 247);
        public static Color mental2 = new Color(4, 247, 168);

        public const short DarkBlue = 12;
        public const short FieryOrange = 13;
        public const short VoidPurple = 14;
        public const short GalacticRainbow = 15;
        
        public const short Mental = -10;

        
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            
            if (item.expert || DRGN._usesDiscoRGB.Contains(item.rare)) { tooltips[0].overrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB); }
            else if (DRGN._dynamicRaritiesColor.ContainsKey(item.rare))
            {
                tooltips[0].overrideColor = Color.Lerp(DRGN._rarities[item.rare], DRGN._dynamicRaritiesColor[item.rare], DRGN.ColorCounter);
            }
            else if (DRGN._rarities.ContainsKey(item.rare))
            {
                
                tooltips[0].overrideColor = DRGN._rarities[item.rare];
            }
            if(item.rare == Mental)
            {
                tooltips.Add(new TooltipLine(mod, "DRGN:Mental", "Mental"));               
                tooltips[tooltips.Count - 1].overrideColor = Color.Lerp(mental, mental2, DRGN.ColorCounter);
            }
            


        }






    }
}
