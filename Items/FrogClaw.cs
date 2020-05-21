using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items
{
    public class FrogClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frog Claw");
            Tooltip.SetDefault("Ribbit "+"\n Drops from underground jungle enemies");
        }
        public override void SetDefaults()
        {
            item.height = 22;
            item.width = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

            bool jngBiome = player.ZoneJungle;
            bool day = Main.dayTime;
            bool surface = player.ZoneOverworldHeight;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("ToxicFrog"));
            return (!alreadySpawned && jngBiome && surface && day);
        }
        public override bool UseItem(Player player)

        {

            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("ToxicFrog")); // Spawn the boss within a range of the player. 
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
       
    }
}
