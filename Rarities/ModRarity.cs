
using DRGN.Rarities;

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ID.Colors;

namespace DRGN.Rarities
{
    public class ModRarity
    {
        public int rarityID = 0;
        public Color c1 = Color.Black;
        public bool isDynamic = false;
        public Color c2 = Color.Black;
        public int rangeSize = 1;
        public bool usesDiscoRGB;
        
        public virtual void Init() { }
        public void Load()
        {
          


            Init();
            if (rarityID != 0)
            {
                if (usesDiscoRGB)
                { AddDiscoRGB(rarityID); }
                else if (rangeSize <= 1)
                {
                    AddModRarity(rarityID, c1, isDynamic, c2);


                }
                else if (rangeSize > 1)
                {

                    AddModRarityAsRange(rarityID, c1, rangeSize, isDynamic, c2);

                }


            }
        }
        public void AddDiscoRGB(int rarityID)
        {

            DRGN._usesDiscoRGB.Add(rarityID);
            

        }
        public void AddModRarity(int rarityID, Color c1, bool isDynamic = false, Color c2 = new Color())
        {

            DRGN._rarities.Add(rarityID, c1);
            if (isDynamic)
            { DRGN._dynamicRaritiesColor.Add(rarityID, c2); }

        }

        public void AddModRarityAsRange(int startingRarityID, Color c1, int RangeSize = 3, bool isDynamic = false, Color c2 = new Color())
        {
            for (int i = 0; i < RangeSize; i++)
            {
                DRGN._rarities.Add(startingRarityID + i, c1);
                if (isDynamic)
                { DRGN._dynamicRaritiesColor.Add(startingRarityID + i, c2); }
            }

        }
    }
}
