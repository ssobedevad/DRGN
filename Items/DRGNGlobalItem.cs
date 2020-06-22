﻿using DRGN.Items.Equipables.MentalModeDrops;
using DRGN.NPCs.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    
    public class DRGNGlobalItem : GlobalItem
    {
        public override bool UseItem(Item item, Player player)
        {

            if (item.type == ItemID.MechanicalEye && DRGNModWorld.MentalMode)
            {
                if (!Main.dayTime)
                {

                    Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, 125);
                        NPC.SpawnOnPlayer(player.whoAmI, 126);
                        NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Triplet"));
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 125f);
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 126f);
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, (float)ModContent.NPCType<Triplet>());
                    }
                    return true;
                }

            }
            return true;
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
			

		   if (context == "bossBag" && DRGNModWorld.MentalMode)
			{
				
				if (arg == ModContent.ItemType<SerpentBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Desertserpent>()); }
				else if (arg == ItemID.KingSlimeBossBag) { player.QuickSpawnItem(ModContent.ItemType<KingSlime>()); }
				else if (arg == ItemID.EyeOfCthulhuBossBag) { player.QuickSpawnItem(ModContent.ItemType<EyeOfCthulu>()); }
				else if (arg == ItemID.EaterOfWorldsBossBag) { player.QuickSpawnItem(ModContent.ItemType<EaterOfWorlds>()); }
				else if (arg == ItemID.BrainOfCthulhuBossBag) { player.QuickSpawnItem(ModContent.ItemType<BrainOfCthulu>()); }
				else if (arg == ModContent.ItemType<FrogBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.ToxicFrog>()); }
				else if (arg == ItemID.QueenBeeBossBag) { player.QuickSpawnItem(ModContent.ItemType<QueenBee>()); }
				else if (arg == ItemID.SkeletronBossBag) { player.QuickSpawnItem(ModContent.ItemType<Skeletron>()); }
				else if (arg == ModContent.ItemType<AntsBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.QueenAnt>()); }
				else if (arg == ItemID.WallOfFleshBossBag) { player.QuickSpawnItem(ModContent.ItemType<WallOfFlesh>()); }
				else if (arg == ModContent.ItemType<FishBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.IceFish>()); }
				else if (arg == ItemID.TwinsBossBag) { player.QuickSpawnItem(ModContent.ItemType<Twins>()); }
				else if (arg == ItemID.SkeletronPrimeBossBag) { player.QuickSpawnItem(ModContent.ItemType<SkeletronPrime>()); }
				else if (arg == ItemID.DestroyerBossBag) { player.QuickSpawnItem(ModContent.ItemType<Destroyer>()); }
				else if (arg == ItemID.PlanteraBossBag) { player.QuickSpawnItem(ModContent.ItemType<Plantera>()); }
				else if (arg == ItemID.GolemBossBag) { player.QuickSpawnItem(ModContent.ItemType<Golem>()); }
				else if (arg == ModContent.ItemType<CloudBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.Cloud>()); }
				else if (arg == ItemID.CultistBossBag) { player.QuickSpawnItem(ModContent.ItemType<Cultists>()); }
				else if (arg == ItemID.MoonLordBossBag) { player.QuickSpawnItem(ModContent.ItemType<MoonLord>()); }
				else if (arg == ModContent.ItemType<DragonFlyBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.DragonFly>()); }
				else if (arg == ModContent.ItemType<DragonBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.FireDragon>()); }
				else if (arg == ModContent.ItemType<VoidBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.VoidSnake>()); }






			}
        }
    }
}
