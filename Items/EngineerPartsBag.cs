
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class EngineerPartsBag : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer Parts Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 11;
            
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            List<int> PossibleCog = new List<int>();
            List<int> PossibleScrew = new List<int>();
            List<int> PossiblePlate = new List<int>();
            List<int> PossiblePipe = new List<int>();
            PossibleCog.Add(mod.ItemType("WoodenCog"));
            PossiblePipe.Add(mod.ItemType("WoodenDowel"));
            PossiblePlate.Add(mod.ItemType("WoodenPlate"));
            PossibleScrew.Add(mod.ItemType("WoodenFixing"));
            if (NPC.downedBoss1) {
                PossibleCog.Add(mod.ItemType("Cog"));
                PossiblePipe.Add(mod.ItemType("MetalPipe"));
                PossiblePlate.Add(mod.ItemType("MetalPlate"));
                PossibleScrew.Add(mod.ItemType("Screw"));
            }
            if (NPC.downedBoss3) {
                PossibleCog.Add(mod.ItemType("GoldenCog"));
                PossiblePipe.Add(mod.ItemType("GoldenPipe"));
                PossiblePlate.Add(mod.ItemType("GoldenPlate"));
                PossibleScrew.Add(mod.ItemType("GoldenScrew"));
            }
            if (DRGNModWorld.downedIceFish) {
                PossibleCog.Add(mod.ItemType("IcyCog"));
                PossiblePipe.Add(mod.ItemType("IcyPipe"));
                PossiblePlate.Add(mod.ItemType("IcyPlate"));
                PossibleScrew.Add(mod.ItemType("IcyScrew"));
            }
            if (NPC.downedPlantBoss) {
                PossibleCog.Add(mod.ItemType("PlantenCog"));
                PossiblePipe.Add(mod.ItemType("PlantenPipe"));
                PossiblePlate.Add(mod.ItemType("PlantenPlate"));
                PossibleScrew.Add(mod.ItemType("PlantenScrew"));
            }
            if (NPC.downedMoonlord) {
                PossibleCog.Add(mod.ItemType("LunarCog"));
                PossiblePipe.Add(mod.ItemType("LunarPipe"));
                PossiblePlate.Add(mod.ItemType("LunarPlate"));
                PossibleScrew.Add(mod.ItemType("LunarScrew"));
            }
            if (DRGNModWorld.downedDragonFly) {
                PossibleCog.Add(mod.ItemType("InsectiumCog"));
                PossiblePipe.Add(mod.ItemType("InsectiumPipe"));
                PossiblePlate.Add(mod.ItemType("InsectiumPlate"));
                PossibleScrew.Add(mod.ItemType("InsectiumScrew"));
            }
            if (DRGNModWorld.downedDragon) {
                PossibleCog.Add(mod.ItemType("FlariumCog"));
                PossiblePipe.Add(mod.ItemType("FlariumPipe"));
                PossiblePlate.Add(mod.ItemType("FlariumPlate"));
                PossibleScrew.Add(mod.ItemType("FlariumScrew"));
            }
            if (DRGNModWorld.downedVoidSnake) {
                PossibleCog.Add(mod.ItemType("VoidCog"));
                PossiblePipe.Add(mod.ItemType("VoidPipe"));
                PossiblePlate.Add(mod.ItemType("VoidPlate"));
                PossibleScrew.Add(mod.ItemType("VoidScrew"));
            }
            int[] CogOptions = PossibleCog.ToArray();
            int[] PipeOptions = PossiblePipe.ToArray();
            int[] PlateOptions = PossiblePlate.ToArray();
            int[] ScrewOptions = PossibleScrew.ToArray();


            player.QuickSpawnItem(Main.rand.Next(CogOptions), Main.rand.Next(player.GetModPlayer<DRGNPlayer>().engineerQuestNum,player.GetModPlayer<DRGNPlayer>().engineerQuestNum + 3));
            player.QuickSpawnItem(Main.rand.Next(PipeOptions), Main.rand.Next(player.GetModPlayer<DRGNPlayer>().engineerQuestNum,player.GetModPlayer<DRGNPlayer>().engineerQuestNum + 3));
            player.QuickSpawnItem(Main.rand.Next(PlateOptions), Main.rand.Next(player.GetModPlayer<DRGNPlayer>().engineerQuestNum,player.GetModPlayer<DRGNPlayer>().engineerQuestNum + 3));
            player.QuickSpawnItem(Main.rand.Next(ScrewOptions), Main.rand.Next(player.GetModPlayer<DRGNPlayer>().engineerQuestNum,player.GetModPlayer<DRGNPlayer>().engineerQuestNum + 3));
            
        }
        public override int BossBagNPC => NPCID.BlueSlime;
    }
}