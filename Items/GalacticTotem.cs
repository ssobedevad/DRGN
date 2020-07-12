using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.NPCs.Boss;
using Terraria.Localization;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items
{
    public class GalacticTotem : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Awakes the galactic guardian");
        }
        public override void SetDefaults()
        {
            item.height = 32;
            item.width = 32;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.rare = ItemRarityID.Expert;
            item.value = 1000;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

            
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("GalacticGuardian"));
            return (!alreadySpawned);
        }
        public override bool UseItem(Player player)

        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("GalacticGuardian")); // Spawn the boss within a range of the player. 
            }
            else
            { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
       
    }
}
