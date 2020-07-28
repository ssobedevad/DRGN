
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
    public class FieryOrange : ModRarity
    {
        public override void Init()
        {
            rarityID = ItemRarities.FieryOrange;
            c1 = ItemRarities.fieryOrange;
            isDynamic = true;
            c2 = ItemRarities.fieryOrange2;

        }
    }
}