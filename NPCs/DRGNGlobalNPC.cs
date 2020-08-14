using DRGN.Buffs;
using DRGN.Items;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using Microsoft.Xna.Framework;
using Steamworks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs
{

    class DRGNGlobalNPC : GlobalNPC
    {
        public static int[] invaders;
        public int voidBuffLevel = 0;
        public override bool InstancePerEntity => true;       
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (DRGNModWorld.MentalMode && !npc.boss || npc.modNPC == null)
            {
                npc.damage = (int)(npc.damage * 1.3f);
                npc.defense = (int)(npc.defense * 1.4f);
                npc.lifeMax = (int)(npc.lifeMax * 1.6f);
                npc.value = (int)(npc.value * 3);


                if (NPC.downedMoonlord && !npc.boss)
                {
                    npc.lifeMax *= 2;
                    npc.defense *= 2;
                }

                if (npc.type == NPCID.Plantera || npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                { npc.lifeMax = (int)(npc.lifeMax * 1.5f); npc.life = npc.lifeMax; npc.damage = (int)(npc.damage * 1.2f); }


            }

            if (DRGNModWorld.MentalMode && npc.boss)
            {
                npc.defense = (int)(npc.defense * (1.3f * Main.ActivePlayersCount));
            }
            npc.netUpdate = true;
        }



        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            //If the custom invasion is up and the invasion has reached the spawn pos
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                //Clear pool so that only the stuff you want spawns
                pool.Clear();
                
                //key = NPC ID | value = spawn weight
                //pool.add(key, value)

                //For every ID inside the invader array in our CustomInvasion file
                foreach (int i in invaders)
                {
                    pool.Add(i, 1f); //Add it to the pool with the same weight of 1
                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            //Change spawn stuff if invasion up and invasion at spawn
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                spawnRate = 100; //Higher the number, the more spawns
                maxSpawns = 10000; //Max spawns of NPCs depending on NPC value
            }
            if (NPC.downedMoonlord) { spawnRate = (int)(spawnRate * 0.3); maxSpawns = maxSpawns * 3; }
        }

        //Adding to the AI of an NPC
        public override void PostAI(NPC npc)
        {
            //Changes NPCs so they do not despawn when invasion up and invasion at spawn
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                npc.timeLeft = 1000;
            }
            if(!npc.HasBuff(mod.BuffType("VoidBuff")) && voidBuffLevel > 0)
            { voidBuffLevel = 0; }
        }


        public override void NPCLoot(NPC npc)
        {
            // We check several things that filter out bosses and critters, as well as the depth that the npc died at. 
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && npc.position.Y > Main.rockLayer * 16.0 && npc.value > 0f && Main.rand.NextBool(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 2 : 1, 30))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("FrogClaw"));
                }
            }
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && npc.position.Y > Main.rockLayer * 16.0 && npc.value > 0f && Main.rand.NextBool(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 2 : 1, 5))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle && DRGNModWorld.downedToxicFrog)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("ToxicFlesh"), DRGNModWorld.MentalMode ? 3 : 1);
                }
            }
            int rand = Main.rand.Next(1, 5);
            if (npc.type == NPCID.Golem && !Main.expertMode) { if (rand == 1) { Item.NewItem(npc.getRect(), ModContent.ItemType<RockSpear>()); } else if(rand == 2) { Item.NewItem(npc.getRect(), ModContent.ItemType<RockWhip>()); } if (rand == 3) { Item.NewItem(npc.getRect(), ModContent.ItemType<RockSprayer>()); } if (rand == 4) { Item.NewItem(npc.getRect(), ModContent.ItemType<CelestialSundial>()); } else if (rand == 5) { Item.NewItem(npc.getRect(), ModContent.ItemType<RockYoyo>()); } }
            if (npc.type == NPCID.CultistBoss && DRGNModWorld.MentalMode) { Item.NewItem(npc.getRect(), ItemID.CultistBossBag,Main.ActivePlayersCount); }
            if ((npc.type == NPCID.Drippler || npc.type == NPCID.BloodZombie) && Main.rand.NextBool(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 2 : 1, 50)) { Item.NewItem(npc.getRect(), mod.ItemType("BloodCore")); }
            if ((npc.type == NPCID.CaveBat || npc.type == NPCID.UndeadMiner || npc.type == NPCID.MotherSlime || npc.type == NPCID.UndeadViking || npc.type == NPCID.IceBat || npc.type == NPCID.GraniteFlyer || npc.type == NPCID.GraniteGolem) && Main.rand.NextBool(DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 2 : 1, 50)) { Item.NewItem(npc.getRect(), mod.ItemType("SoulContainer")); }
            if (DRGNModWorld.SwarmUp)
            {
                //Gets IDs of invaders from CustomInvasion file

                //If npc type equal to invader's ID decrement size to progress invasion
                if (npc.type == mod.NPCType("Ant") || npc.type == mod.NPCType("FireAnt") || npc.type == mod.NPCType("ElectricAnt"))
                {
                    Main.invasionSize -= 1;
                }
                else if (npc.type == mod.NPCType("FlyingAnt"))
                {
                    Main.invasionSize -= 2;
                }
                else if (npc.type == mod.NPCType("AntCrawlerHead") || npc.type == mod.NPCType("DragonFlyMini"))
                {
                    Main.invasionSize -= 4;
                }

            }
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && NPC.downedMoonlord && npc.value > 0f && Main.rand.NextBool(DRGNModWorld.MentalMode? 4 : Main.expertMode ? 2 : 1, 15))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("MagmaticEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("EarthenEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("GlacialEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSkyHeight)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("LunarEssence"));
                }
            }
            if (npc.type == NPCID.Golem && DRGNModWorld.LihzahrdOre == false)
            {
                Main.NewText("The Jungle has condensed", 85, 85, 5);
               
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 600, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.LihzahrdOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("LihzahrdOre"));




                }


            }

            if (npc.type == NPCID.MoonLordCore && DRGNModWorld.LuminiteOre == false)
            {
                Main.NewText("The Lunar Being has fallen and your world has been blessed", 180, 180, 180);
                Item.NewItem(npc.getRect(), mod.ItemType("LunarBlessing"),Main.ActivePlayersCount);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 600, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.LuminiteOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), TileID.LunarOre);




                }


            }
            if (npc.type == mod.NPCType("ToxicFrog") && DRGNModWorld.EarthenOre == false)
            {

                Main.NewText("The earth has released its energy", 0, 255, 100);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 200, Main.maxTilesY - 600);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.EarthenOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("EarthenOre"));




                }
            }
            if (npc.type == mod.NPCType("TheVirus") && DRGNModWorld.TechnoOre == false)
            {

                Main.NewText("Technology is infecting your world", 0, 40, 0);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 200);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.TechnoOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("TechnoOre"));




                }
            }
            if (npc.type == mod.NPCType("Cloud") && DRGNModWorld.CosmoOre == false)
            {

                Main.NewText("The sun and moon have condensed", 180, 180, 255);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 600, Main.maxTilesY - 600);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.CosmoOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("CosmoOre"));




                }
            }
            if (npc.type == mod.NPCType("VoidSnakeHead") && DRGNModWorld.VoidOre == false)
            {

                Main.NewText("The void has collapsed", 0, 255, 100);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 700, Main.maxTilesY);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.VoidOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("VoidOre"));
                    




                }
            }
            if (npc.type == mod.NPCType("GalacticGuardian") && DRGNModWorld.GalactiteOre == false)
            {

                Main.NewText("A galaxy has formed in your world", 200, 200, 200);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 700, Main.maxTilesY);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);



                    DRGNModWorld.GalactiteOre = true;

                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(10, 15), (ushort)mod.TileType("GalacticaOre"));




                }
            }
            if (npc.type == mod.NPCType("IceFish") && DRGNModWorld.GlacialOre == false)
            {
                Main.NewText("The ancient being has fallen granting your world its power", 0, 100, 255);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);



                    DRGNModWorld.GlacialOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("GlacialOre"));




                }
            }
            if (npc.type == mod.NPCType("FireDragon") && DRGNModWorld.SolariumOre == false)
            {
                Main.NewText("Hell has risen releasing power into the depths of your world", 255, 50, 50);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next(Main.maxTilesY - 400, Main.maxTilesY);
                    int X = Main.rand.Next(10, Main.maxTilesX - 10);



                    DRGNModWorld.SolariumOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("SolariumOre"));




                }

            }

        }
       
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (npc.HasBuff(mod.BuffType("Bugged")) && Main.rand.NextBool(npc.boss? 1 : 3 , 12) && (projectile.type != mod.ProjectileType("BinaryShot")|| projectile.ai[0] != -1)) { Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-10f,10f), Main.rand.NextFloat(-10f, 10f)), mod.ProjectileType("BinaryShot"), damage/3, knockback, projectile.owner , -1); }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
        
            if (npc.HasBuff(mod.BuffType("Bugged")) && Main.rand.NextBool(npc.boss ? 1 : 3, 12)) { Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f)), mod.ProjectileType("BinaryShot"), damage/2, knockback, player.whoAmI , -1); }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {

            if (npc.HasBuff(ModContent.BuffType<Melting>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= Main.player[npc.FindClosestPlayer()].GetModPlayer<DRGNPlayer>().tfEquip? 60 : 30;
                damage = Main.player[Main.myPlayer].GetModPlayer<DRGNPlayer>().tfEquip ? 10 : 5 ;
            }

            else if (npc.HasBuff(ModContent.BuffType<Burning>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 90;
                damage = 15;
            }
            else if (npc.HasBuff(ModContent.BuffType<GalacticCurse>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 10000;
                damage = 5000;
            }
            else if (npc.HasBuff(ModContent.BuffType<Shocked>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 60;
                damage = 10;
            }
            else if (npc.HasBuff(ModContent.BuffType<VoidBuff>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.TargetClosest(false);
                Player player = Main.player[npc.target];
                int maxHPDamage = (int)(npc.lifeMax * 0.00000025f);
                int maxHPlifeRegen = (int)(npc.lifeMax * 0.000005f);
                if (player.GetModPlayer<DRGNPlayer>().voidArmorSet)
                { maxHPDamage *= 3; maxHPlifeRegen *= 3; }
                if(player.GetModPlayer<DRGNPlayer>().vsEquip)
                { maxHPDamage *= 2; maxHPlifeRegen *= 2; }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 1000 * voidBuffLevel + (maxHPlifeRegen * voidBuffLevel)/3;
                damage = 100 * voidBuffLevel + (maxHPDamage * voidBuffLevel)/3;
            }

        }
    }
}