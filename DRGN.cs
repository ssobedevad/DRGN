using Terraria.ModLoader;
using Terraria;
using System;
using System.Collections.Generic;
using Terraria.ID;

namespace DRGN
{
    public class DRGN : Mod
    {
        
          public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                
                
                bossChecklist.Call("AddBoss", 0.5f, NPCType("DesertSerpent"), this, "Desert Serpent", (Func<bool>)(() => DRGNModWorld.downedSerpent), ItemType("SnakeHead"), new List<int> { }, new List<int> { ItemType("SnakeScale"), ItemType("ToxicFang"), ItemID.Cactus }, "Use a [i:" + ItemType("SnakeHead") + "] in the Day.");
                bossChecklist.Call("AddBoss", 4.5f, NPCType("ToxicFrog"), this, "Toxic Frog", (Func<bool>)(() => DRGNModWorld.downedToxicFrog), ItemType("FrogClaw"), new List<int> { }, new List<int> { ItemType("ToxicFlesh"), ItemType("Lobber"),ItemType("ThePlague"), ItemType("ThrowingTongue"), ItemType("ToxicRifle") }, "Use a [i:" + ItemType("FrogClaw") + "] in the Day on the surface Jungle.");
                bossChecklist.Call("AddBoss", 6.5f, NPCType("IceFish"), this, "Ice Fish", (Func<bool>)(() => DRGNModWorld.downedIceFish), ItemType("FrozenFishFood"), new List<int> { }, new List<int> { ItemType("GlacialShard"), ItemType("IceSpear") }, "Use a [i:" + ItemType("FrozenFishFood") + "] in the Ice Biome.");
                bossChecklist.Call("AddBoss", 11.5f, NPCType("Cloud"), this, "Cloud", (Func<bool>)(() => DRGNModWorld.downedCloud), ItemType("CelestialSundial"), new List<int> { }, new List<int> { ItemType("CloudStaff"), ItemType("ElectroStaff") }, "Use a [i:" + ItemType("CelestialSundial") + "] in Space.");
                bossChecklist.Call("AddBoss", 15f, NPCType("FireDragon"), this, "Fire Dragon", (Func<bool>)(() => DRGNModWorld.downedDragon), ItemType("FireDragonEgg"), new List<int> { }, new List<int> { ItemType("SolariumOre"), ItemType("DragonScale"), ItemType("SunBook") }, "Use a [i:" + ItemType("FireDragonEgg") + "] in the Dragon's Lair.");
                bossChecklist.Call("AddBoss", 16f, NPCType("VoidSnakeHead"), this, "Void Snake", (Func<bool>)(() => DRGNModWorld.downedVoidSnake), ItemType("VoidFlesh"), new List<int> { }, new List<int> { ItemType("VoidOre"), ItemType("VoidBar"), ItemType("VoidSpear"), ItemType("VoidScythe"), ItemType("VoidBlade"), ItemType("VoidSilk") }, "Use a [i:" + ItemType("VoidFlesh") + "] anywhere and anytime.");
            }
        }
    }
    
	
}