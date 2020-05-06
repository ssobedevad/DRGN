using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items.Weapons
{
    public class CloudStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloud Staff");
            Tooltip.SetDefault("The skies are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 150;
            item.summon = true;
            item.height = 28;
            item.width = 27;
            item.useTime = 25;
            item.useAnimation = 25;
          
            item.useStyle = 1;
            
        }
        public override bool CanUseItem(Player player)
        {
          
            int maxMinions = NPC.CountNPCS(mod.NPCType("CloudSummon"));
            return (maxMinions <= player.maxMinions );
        }
        public override bool UseItem(Player player)

        {   
            player.AddBuff(mod.BuffType("CloudSummon"), 60000);
            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("CloudSummon"), 0, player.whoAmI);
            return true;


        }
        
    }
}
