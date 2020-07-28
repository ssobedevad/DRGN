
using DRGN.Rarities;

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ID.Colors;

namespace DRGN.Rarities
{
    public class DarkBlue : ModRarity
    {
        public override void Init()
        {
            rarityID = ItemRarities.DarkBlue;
            c1 = ItemRarities.darkBlue;
            
        }
    }
}