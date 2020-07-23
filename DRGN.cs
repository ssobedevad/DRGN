using Terraria;

using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;


using DRGN.UI;
using DRGN.Items.Weapons;
using DRGN.Items;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;

namespace DRGN
{
    public class DRGN : Mod
    {
        public static ModHotKey TimeWarpHotkey;
       
        internal RevivalBar RevivalBar;
        internal EngineerAmmoBar EngineerAmmoBar;
        internal DodgeBar DodgeBar;
        internal EngineerGun EngineerGun;
        internal DisassembleUI disassembleUI;
        private UserInterface _revivalCooldownBar;
        private UserInterface _engineerAmmoBar;
        private UserInterface _dodgeCooldownBar;
        private UserInterface _EngineerGun;
        public UserInterface _DisUI;
        
        public static List<Vector2> FlailsRangeMult = new List<Vector2>();
        public static List<Vector2> FlailsTopSpeed = new List<Vector2>();
        public static List<Vector3> FlailsMinPlayerDists = new List<Vector3>();
        public static List<Vector2> FlailsNPCImmunity = new List<Vector2>();
        public static List<int> FlailItem = new List<int>();
        
        public override void PostSetupContent()
        {

            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                
                
                bossChecklist.Call("AddBoss", 1.5f, NPCType("DesertSerpent"), this, "Desert Serpent", (Func<bool>)(() => DRGNModWorld.downedSerpent), ItemType("SnakeHead"), new List<int> { }, new List<int> { ModContent.ItemType<SnakeScale>(), ModContent.ItemType<ToxicFang>(), ModContent.ItemType<SnakeHeadThrown>(), ModContent.ItemType<SnakeSlayer>(), ModContent.ItemType<SnakeStaff>(), ModContent.ItemType<SnakeWhip>(), ItemID.Cactus }, "Use a [i:" + ItemType("SnakeHead") + "] in the Day.");
                bossChecklist.Call("AddEvent", 1f, NPCType("Ant"), this, "The Swarm Pre EoW/BoC", (Func<bool>)(() => DRGNModWorld.SwarmKilled), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                
                bossChecklist.Call("AddBoss", 4.5f, NPCType("ToxicFrog"), this, "Toxic Frog", (Func<bool>)(() => DRGNModWorld.downedToxicFrog), ItemType("FrogClaw"), new List<int> { }, new List<int> { ModContent.ItemType<ToxicFlesh>(), ModContent.ItemType<Lobber>(), ModContent.ItemType<ToxicRifle>(), ModContent.ItemType<ThrowingTongue>(), ModContent.ItemType<ThePlague>(), ModContent.ItemType<TongueSword>(), ModContent.ItemType<TongueWhip>(), ModContent.ItemType<FrogStaff>(), ModContent.ItemType<EarthenOre>() }, "Use a [i:" + ItemType("FrogClaw") + "] in the Day on the surface Jungle.");
                bossChecklist.Call("AddBoss", 6f, NPCType("QueenAnt"), this, "Queen Ant", (Func<bool>)(() => DRGNModWorld.downedQueenAnt), ItemType("AntsCall"), new List<int> { }, new List<int> { ModContent.ItemType<AntEssence>(), ModContent.ItemType<AntJaw>(), ModContent.ItemType<AntBiter>(), ModContent.ItemType<AntJaws>(), ModContent.ItemType<AntSlicer>(), ModContent.ItemType<ElementalAntWhip>(), ModContent.ItemType<AntStaff>() }, "Use a [i:" + ItemType("AntsCall") + "] in the ant nest.");
                bossChecklist.Call("AddEvent", 6.1f, NPCType("ElectricAnt"), this, "The Swarm Post Queen Ant", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostQA), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 6.5f, NPCType("IceFish"), this, "Ice Fish", (Func<bool>)(() => DRGNModWorld.downedIceFish), ItemType("FrozenFishFood"), new List<int> { }, new List<int> { ModContent.ItemType<GlacialShard>(), ModContent.ItemType<IceChains>(), ModContent.ItemType<IceChainWhip>(), ModContent.ItemType<IceSpear>(), ModContent.ItemType<IcicleBlaster>(), ModContent.ItemType<IcicleSlicer>(), ModContent.ItemType<ArcticHuntingRifle>(), ModContent.ItemType<FishStaff>(), ModContent.ItemType<GlacialOre>() }, "Use a [i:" + ItemType("FrozenFishFood") + "] in the Ice Biome.");
                bossChecklist.Call("AddBoss", 8f, NPCType("TheVirus"), this, "The Virus", (Func<bool>)(() => DRGNModWorld.downedTheVirus), ItemType("UnstableBatteries"), new List<int> { }, new List<int> { ModContent.ItemType<TechnoOre>(), ModContent.ItemType<TechnoWhip>(), ModContent.ItemType<TechnoSpear>(), ModContent.ItemType<TechnoSlicer>(), ModContent.ItemType<TechnoShuriken>(), ModContent.ItemType<SourceCode>(), ModContent.ItemType<SourceThrow>(), ModContent.ItemType<SecurityBreach>(), ModContent.ItemType<BinaryStaff>() }, "Use a [i:" + ItemType("UnstableBatteries") + "] anywhere and anytime.");
                bossChecklist.Call("AddEvent", 8f, NPCType("FlyingAnt"), this, "The Swarm Post Mechanical Boss", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMechBoss), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 11.5f, NPCType("Cloud"), this, "Cloud", (Func<bool>)(() => DRGNModWorld.downedCloud), ItemType("CelestialSundial"), new List<int> { }, new List<int> { ModContent.ItemType<CloudStaff>(), ModContent.ItemType<ElectroStaff>(), ModContent.ItemType<SunBook>(), ModContent.ItemType<CosmoOre>(), ModContent.ItemType<CosmoBlade>(), ModContent.ItemType<CosmoSpear>(), ModContent.ItemType<CosmoWhip>() }, "Use a [i:" + ItemType("CelestialSundial") + "] in Space.");
                bossChecklist.Call("AddEvent", 14.4f, NPCType("DragonFlyMini"), this, "The Swarm Post Moonlord", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMoonlord), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntCrawlerScale"), ItemType("DragonFlyWing"), ItemType("DragonFliesCall"), ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing"), ItemType("DragonFliesCall") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 14.5f, NPCType("DragonFly"), this, "DragonFly", (Func<bool>)(() => DRGNModWorld.downedDragonFly), ItemType("DragonFliesCall"), new List<int> { }, new List<int> { ModContent.ItemType<DragonFlyDust>(), ModContent.ItemType<DragonFlyWing>(), ModContent.ItemType<GalacticScale>(), ModContent.ItemType<GalacticEssence>(), ModContent.ItemType<TheDragonFly>(), ModContent.ItemType<DragonFlySlicer>(), ModContent.ItemType<DragonFlyStaff>(), ModContent.ItemType<DragonFlyWhip>() }, "Use a [i:" + ItemType("DragonFliesCall") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 15f, NPCType("FireDragon"), this, "Fire Dragon", (Func<bool>)(() => DRGNModWorld.downedDragon), ItemType("FireDragonEgg"), new List<int> { }, new List<int> { ModContent.ItemType<SolariumOre>(), ModContent.ItemType<DragonScale>(), ModContent.ItemType<DragonPick>(), ModContent.ItemType<DragonStaff>(), ModContent.ItemType<DragonSpear>(), ModContent.ItemType<MagmaticHuntingRifle>(), ModContent.ItemType<DragonWhip>() }, "Use a [i:" + ItemType("FireDragonEgg") + "] in the Dragon's Lair.");
                bossChecklist.Call("AddBoss", 16f, NPCType("VoidSnakeHead"), this, "Void Snake", (Func<bool>)(() => DRGNModWorld.downedVoidSnake), ItemType("VoidFlesh"), new List<int> { }, new List<int> { ModContent.ItemType<VoidOre>(), ModContent.ItemType<VoidBar>(), ModContent.ItemType<VoidSpear>(), ModContent.ItemType<VoidScythe>(), ModContent.ItemType<VoidSoul>(), ModContent.ItemType<VoidSnakeStaff>(), ModContent.ItemType<VoidSnakeWhip>() }, "Use a [i:" + ItemType("VoidFlesh") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 17f, NPCType("GalacticGuardian"), this, "Galactic Guardian", (Func<bool>)(() => DRGNModWorld.downedGalacticGuardian), ItemType("GalacticTotem"), new List<int> { }, new List<int> { ModContent.ItemType<GalacticaOre>(), ModContent.ItemType<GalacticaBar>(), ModContent.ItemType<GalacticOrbs>(), ModContent.ItemType<GalacticShower>(), ModContent.ItemType<GalacticStaff>(), ModContent.ItemType<GalacticWhip>(), ModContent.ItemType<GalacticYoyo>(), ModContent.ItemType<GalactiteChain>(), ModContent.ItemType<GalactiteRifle>(), ModContent.ItemType<GalactiteThrowingAxe>(), ModContent.ItemType<GalaxySlicer>(), ModContent.ItemType<GalactiteBow>() }, "Use a [i:" + ItemType("GalacticTotem") + "] anywhere and anytime.");
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HellstoneBar,15);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion,3);
            recipe.AddIngredient(ItemID.LavaBucket,5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.LavaCharm);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.ObsidianSkull);
            recipe.AddIngredient(ItemID.LavaBucket, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.ObsidianRose);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.GoldCoin);
            recipe.AddIngredient(ItemID.GoldBar,5);
            recipe.AddIngredient(ItemID.Bone,5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.GoldenKey);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Cloud,10);
            recipe.AddIngredient(ItemID.RainCloud, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 5);
            recipe.AddIngredient(ItemID.Feather, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.LuckyHorseshoe);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddIngredient(ItemID.PinkDye);
            
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(ItemID.PinkGel,20);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Book, 20);
            recipe.AddIngredient(ItemID.WaterBucket,5);
            recipe.AddIngredient(this.ItemType("ToxicFang"));           
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(ItemID.WaterBolt);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SunplateBlock, 10);
            recipe.AddIngredient(ItemID.Cloud, 5);
            recipe.AddIngredient(ItemID.RainCloud, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.SkyMill);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ManaCrystal, 10);
            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.CelestialMagnet);
            recipe.AddRecipe();

        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Phasesaber", new int[]
            {
        ItemID.PurplePhasesaber,
        ItemID.RedPhasesaber,
        ItemID.BluePhasesaber,
        ItemID.GreenPhasesaber,
        ItemID.WhitePhasesaber,
        ItemID.YellowPhasesaber,
            });
            RecipeGroup.RegisterGroup("DRGN:Phasesabers", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Hardmode Bar", new int[]
            {
        ItemID.AdamantiteBar,
       
        ItemID.TitaniumBar,
            });
            RecipeGroup.RegisterGroup("DRGN:T3HmB", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 2 Hardmode Bar", new int[]
           {
        ItemID.MythrilBar,

        ItemID.OrichalcumBar,
           });
            RecipeGroup.RegisterGroup("DRGN:T2HmB", group);
            
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 2 Repeater", new int[]
           {
        ItemID.MythrilRepeater,

        ItemID.OrichalcumRepeater,
           });
            RecipeGroup.RegisterGroup("DRGN:T2Rep", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 1 Repeater", new int[]
           {
        ItemID.CobaltRepeater,

        ItemID.PalladiumRepeater,
           });
            RecipeGroup.RegisterGroup("DRGN:T1Rep", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Repeater", new int[]
           {
        ItemID.TitaniumRepeater,

        ItemID.AdamantiteRepeater,
           });
            RecipeGroup.RegisterGroup("DRGN:T3Rep", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardmode Forge", new int[]
            {
        ItemID.AdamantiteForge,

        ItemID.TitaniumForge,
            });
            RecipeGroup.RegisterGroup("DRGN:T3Forge", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardmode Anvil", new int[]
            {
        ItemID.MythrilAnvil,

        ItemID.OrichalcumAnvil,
            });
            RecipeGroup.RegisterGroup("DRGN:HmAnvil", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 1 wings", new int[]
            {
        ItemID.AngelWings,

        ItemID.DemonWings,
            });
            RecipeGroup.RegisterGroup("DRGN:T1Wings", group);
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 12 wings", new int[]
            {
        ItemID.WingsSolar,
         ItemID.WingsNebula,
          ItemID.WingsVortex,
           ItemID.WingsStardust,

            });
            RecipeGroup.RegisterGroup("DRGN:T12Wings", group);
        }
        public override void Load()
        {
            FlailItem.Add(5011);
            FlailItem.Add(5012);
            FlailItem.Add(ItemID.BallOHurt);
            FlailItem.Add(ItemID.TheMeatball);
            FlailItem.Add(ItemID.BlueMoon);
            FlailItem.Add(ItemID.Sunfury);
            FlailItem.Add(ItemID.DaoofPow);
            FlailItem.Add(ItemID.FlowerPow);
            FlailItem.Add(4272);


            FlailsRangeMult.Add(new Vector2(947,13));
            FlailsRangeMult.Add(new Vector2(948, 13));
            FlailsRangeMult.Add(new Vector2(25, 15));
            FlailsRangeMult.Add(new Vector2(154, 15));
            FlailsRangeMult.Add(new Vector2(26, 15));
            FlailsRangeMult.Add(new Vector2(35, 15));
            FlailsRangeMult.Add(new Vector2(63, 13));
            FlailsRangeMult.Add(new Vector2(757, 13));
            FlailsRangeMult.Add(new Vector2(247, 13));


            FlailsTopSpeed.Add(new Vector2(947, 12));
            FlailsTopSpeed.Add(new Vector2(948, 12));
            FlailsTopSpeed.Add(new Vector2(25, 14));
            FlailsTopSpeed.Add(new Vector2(154, 15));
            FlailsTopSpeed.Add(new Vector2(26, 16));
            FlailsTopSpeed.Add(new Vector2(35, 17));
            FlailsTopSpeed.Add(new Vector2(63, 21));
            FlailsTopSpeed.Add(new Vector2(757, 22));
            FlailsTopSpeed.Add(new Vector2(247, 23));

              
            FlailsNPCImmunity.Add(new Vector2(63, 15));
            FlailsNPCImmunity.Add(new Vector2(757, 15));
            FlailsNPCImmunity.Add(new Vector2(247, 15));


            FlailsMinPlayerDists.Add(new Vector3(947, 8 , 13));
            FlailsMinPlayerDists.Add(new Vector3(948, 8, 13));
            FlailsMinPlayerDists.Add(new Vector3(25, 10, 15));
            FlailsMinPlayerDists.Add(new Vector3(154, 11, 16));
            FlailsMinPlayerDists.Add(new Vector3(26, 12, 16));
            FlailsMinPlayerDists.Add(new Vector3(35, 14, 18));
            FlailsMinPlayerDists.Add(new Vector3(63, 20, 24));
            FlailsMinPlayerDists.Add(new Vector3(757, 22, 26));




            TimeWarpHotkey = RegisterHotKey("Time Warp", "Q");
           
            RevivalBar = new RevivalBar();
            RevivalBar.Activate();
            EngineerAmmoBar = new EngineerAmmoBar();
            EngineerAmmoBar.Activate();
            DodgeBar = new DodgeBar();
            DodgeBar.Activate();
            EngineerGun = new EngineerGun();
            EngineerGun.Activate();
            disassembleUI = new DisassembleUI();
            disassembleUI.Activate();
            _EngineerGun = new UserInterface();
            _EngineerGun.SetState(EngineerGun);
            _engineerAmmoBar = new UserInterface();
            _engineerAmmoBar.SetState(EngineerAmmoBar);
            _revivalCooldownBar = new UserInterface();
            _revivalCooldownBar.SetState(RevivalBar);
            _dodgeCooldownBar = new UserInterface();
            _dodgeCooldownBar.SetState(DodgeBar);
            _DisUI = new UserInterface();
            _DisUI.SetState(null);

        }
        public override void Unload()
        {
            TimeWarpHotkey = null;
          
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _revivalCooldownBar?.Update(gameTime);
            RevivalBar?.Update(gameTime);
            _engineerAmmoBar?.Update(gameTime);
            EngineerAmmoBar?.Update(gameTime);
            _dodgeCooldownBar?.Update(gameTime);
            DodgeBar?.Update(gameTime);
            _EngineerGun?.Update(gameTime);
            EngineerGun?.Update(gameTime);
            _DisUI?.Update(gameTime);
            disassembleUI?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "DRGN:Resouce Bars",
                    delegate
                    {
                        _revivalCooldownBar.Draw(Main.spriteBatch, new GameTime());
                        _dodgeCooldownBar.Draw(Main.spriteBatch, new GameTime());
                        _EngineerGun.Draw(Main.spriteBatch, new GameTime());
                        _engineerAmmoBar.Draw(Main.spriteBatch, new GameTime());
                        _DisUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
    
	
}