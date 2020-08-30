using DRGN.Items;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.ReaperWeapons.Scythes;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using DRGN.Prefixes.Accessories;
using DRGN.Rarities;
using DRGN.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Utilities;
using static Terraria.ID.Colors;

namespace DRGN
{
    public class DRGN : Mod
    {
        public static ModHotKey TimeWarpHotkey;
        public static List<ushort> wallsForTreeGrow = new List<ushort>();
        internal RevivalBar RevivalBar;
        public static float ColorCounter = 0f;
        private static bool directionOfChange = false;
        
        internal DodgeBar DodgeBar;
        internal ReaperSoulBar ReaperSoulBar;
        public int hoverButton = -1;
        public int[] selectedButtons = new int[3] { 2, 5, 9 };
        public bool canCreate = false;
        public string name = "";
        public KeyboardState oldState = Keyboard.GetState();
        private UserInterface _reaperSoulBar;
        private UserInterface _revivalCooldownBar;
        public Dictionary<int , bool> mentalModeWlds = new Dictionary<int, bool>();
        private UserInterface _dodgeCooldownBar;
        private static MyUIWorldSelect myUIWorldSelect = new MyUIWorldSelect();
        public static Dictionary<string , bool> worldInfo = new Dictionary<string, bool>();
        public bool usesVanillaWorldGen = false;
        public static List<int> meleePrefixes = new List<int>();
        public List<string> wldId = new List<string>();

        public static List<Vector2> FlailsRangeMult = new List<Vector2>();
        public static List<Vector2> FlailsTopSpeed = new List<Vector2>();
        public static List<Vector3> FlailsMinPlayerDists = new List<Vector3>();
        public static List<Vector2> FlailsNPCImmunity = new List<Vector2>();
        public static List<int> FlailItem = new List<int>();

        public static Dictionary<int, Color> _rarities = new Dictionary<int, Color>();
        public static Dictionary<int, Color> _dynamicRaritiesColor = new Dictionary<int, Color>();
        public static List<int> _usesDiscoRGB = new List<int>();
        public static List<int> _isFixedRarity = new List<int>();
        public static int MaxRarity = 11;
        public static Dictionary<int, int> _itemTextRarities = new Dictionary<int, int>();


        public override void PostSetupContent()
        {

            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {


                bossChecklist.Call("AddBoss", 2.5f, NPCType("DesertSerpent"), this, "Desert Serpent", (Func<bool>)(() => DRGNModWorld.downedSerpent), ItemType("SnakeHead"), new List<int> { }, new List<int> { ModContent.ItemType<SnakeScale>(), ModContent.ItemType<ToxicFang>(), ModContent.ItemType<SnakeHeadThrown>(), ModContent.ItemType<SnakeSlayer>(), ModContent.ItemType<SnakeStaff>(), ModContent.ItemType<SnakeWhip>(), ItemID.Cactus }, "Use a [i:" + ItemType("SnakeHead") + "] in the Day.");
                bossChecklist.Call("AddEvent", 1f, NPCType("Ant"), this, "The Swarm Pre QA", (Func<bool>)(() => DRGNModWorld.SwarmKilled), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 2.5f, NPCType("RockSlimeKing"), this, "Rock Monarch", (Func<bool>)(() => DRGNModWorld.downedRockMonarch), ItemType("RockCrown"), new List<int> { }, new List<int> { ModContent.ItemType<Flint>(), ModContent.ItemType<SharpenedObsidian>(), ItemID.Ruby, ItemID.Sapphire, ItemID.Diamond, ItemID.Topaz, ItemID.Amethyst, ItemID.Amber, ItemID.Emerald, }, "Use a [i:" + ItemType("RockCrown") + "] underground.");
                bossChecklist.Call("AddBoss", 4.5f, NPCType("ToxicFrog"), this, "Toxic Frog", (Func<bool>)(() => DRGNModWorld.downedToxicFrog), ItemType("FrogClaw"), new List<int> { }, new List<int> { ModContent.ItemType<ToxicFlesh>(), ModContent.ItemType<Lobber>(), ModContent.ItemType<ToxicRifle>(), ModContent.ItemType<ThrowingTongue>(), ModContent.ItemType<ThePlague>(), ModContent.ItemType<TongueSword>(), ModContent.ItemType<TongueWhip>(), ModContent.ItemType<FrogStaff>(), ModContent.ItemType<EarthenOre>() }, "Use a [i:" + ItemType("FrogClaw") + "] in the Day on the surface Jungle.");
                bossChecklist.Call("AddBoss", 6f, NPCType("QueenAnt"), this, "Queen Ant", (Func<bool>)(() => DRGNModWorld.downedQueenAnt), ItemType("AntsCall"), new List<int> { }, new List<int> { ModContent.ItemType<AntEssence>(), ModContent.ItemType<AntJaw>(), ModContent.ItemType<AntBiter>(), ModContent.ItemType<AntJaws>(), ModContent.ItemType<AntSlicer>(), ModContent.ItemType<ElementalAntWhip>(), ModContent.ItemType<AntStaff>() }, "Use a [i:" + ItemType("AntsCall") + "] on the surface.");
                bossChecklist.Call("AddEvent", 6.1f, NPCType("ElectricAnt"), this, "The Swarm Post Queen Ant", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostQA), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 6.5f, NPCType("IceFish"), this, "Ice Fish", (Func<bool>)(() => DRGNModWorld.downedIceFish), ItemType("FrozenFishFood"), new List<int> { }, new List<int> { ModContent.ItemType<GlacialShard>(), ModContent.ItemType<IceChains>(), ModContent.ItemType<IceChainWhip>(), ModContent.ItemType<IceSpear>(), ModContent.ItemType<IcicleBlaster>(), ModContent.ItemType<IcicleSlicer>(), ModContent.ItemType<ArcticHuntingRifle>(), ModContent.ItemType<FishStaff>(), ModContent.ItemType<GlacialOre>() }, "Use a [i:" + ItemType("FrozenFishFood") + "] in the Ice Biome.");
                bossChecklist.Call("AddBoss", 9.5f, NPCType("TheVirus"), this, "The Virus", (Func<bool>)(() => DRGNModWorld.downedTheVirus), ItemType("UnstableBatteries"), new List<int> { }, new List<int> { ModContent.ItemType<TechnoOre>(), ModContent.ItemType<TechnoWhip>(), ModContent.ItemType<TechnoSpear>(), ModContent.ItemType<TechnoSlicer>(), ModContent.ItemType<TechnoShuriken>(), ModContent.ItemType<SourceCode>(), ModContent.ItemType<SourceThrow>(), ModContent.ItemType<SecurityBreach>(), ModContent.ItemType<BinaryStaff>() }, "Use a [i:" + ItemType("UnstableBatteries") + "] anywhere and anytime.");
                bossChecklist.Call("AddEvent", 8f, NPCType("FlyingAnt"), this, "The Swarm Post Mechanical Boss", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMechBoss), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 11.5f, NPCType("Cloud"), this, "Big Angry Nimbus", (Func<bool>)(() => DRGNModWorld.downedCloud), ItemType("CelestialSundial"), new List<int> { }, new List<int> { ModContent.ItemType<CloudStaff>(), ModContent.ItemType<ElectroStaff>(), ModContent.ItemType<SunBook>(), ModContent.ItemType<CosmoOre>(), ModContent.ItemType<CosmoBlade>(), ModContent.ItemType<CosmoSpear>(), ModContent.ItemType<CosmoWhip>() }, "Use a [i:" + ItemType("CelestialSundial") + "] in Space.");
                bossChecklist.Call("AddEvent", 14.4f, NPCType("DragonFlyMini"), this, "The Swarm Post Moonlord", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMoonlord), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntCrawlerScale"), ItemType("DragonFlyWing"), ItemType("DragonFliesCall"), ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing"), ItemType("DragonFliesCall") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 14.5f, NPCType("DragonFly"), this, "DragonFly", (Func<bool>)(() => DRGNModWorld.downedDragonFly), ItemType("DragonFliesCall"), new List<int> { }, new List<int> { ModContent.ItemType<DragonFlyDust>(), ModContent.ItemType<DragonFlyWing>(), ModContent.ItemType<GalacticScale>(), ModContent.ItemType<GalacticEssence>(), ModContent.ItemType<TheDragonFly>(), ModContent.ItemType<DragonFlySlicer>(), ModContent.ItemType<DragonFlyStaff>(), ModContent.ItemType<DragonFlyWhip>() }, "Use a [i:" + ItemType("DragonFliesCall") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 15f, NPCType("FireDragon"), this, "Fire Dragon", (Func<bool>)(() => DRGNModWorld.downedDragon), ItemType("FireDragonEgg"), new List<int> { }, new List<int> { ModContent.ItemType<SolariumOre>(), ModContent.ItemType<DragonScale>(), ModContent.ItemType<DragonPick>(), ModContent.ItemType<DragonStaff>(), ModContent.ItemType<DragonSpear>(), ModContent.ItemType<MagmaticHuntingRifle>(), ModContent.ItemType<DragonWhip>() }, "Use a [i:" + ItemType("FireDragonEgg") + "] in the Dragon's Lair.");
                bossChecklist.Call("AddBoss", 16f, NPCType("VoidSnakeHead"), this, "Void Snake", (Func<bool>)(() => DRGNModWorld.downedVoidSnake), ItemType("VoidFlesh"), new List<int> { }, new List<int> { ModContent.ItemType<VoidOre>(), ModContent.ItemType<VoidBar>(), ModContent.ItemType<VoidSpear>(), ModContent.ItemType<VoidScythe>(), ModContent.ItemType<VoidSoul>(), ModContent.ItemType<VoidSnakeStaff>(), ModContent.ItemType<VoidSnakeWhip>() }, "Use a [i:" + ItemType("VoidFlesh") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 17f, NPCType("GalacticGuardian"), this, "Galactic Guardian", (Func<bool>)(() => DRGNModWorld.downedGalacticGuardian), ItemType("GalacticTotem"), new List<int> { }, new List<int> { ModContent.ItemType<GalacticaOre>(), ModContent.ItemType<GalacticaBar>(), ModContent.ItemType<GalacticOrbs>(), ModContent.ItemType<GalacticShower>(), ModContent.ItemType<GalacticStaff>(), ModContent.ItemType<GalacticWhip>(), ModContent.ItemType<GalacticYoyo>(), ModContent.ItemType<GalactiteChain>(), ModContent.ItemType<GalactiteRifle>(), ModContent.ItemType<GalactiteThrowingAxe>(), ModContent.ItemType<GalaxySlicer>(), ModContent.ItemType<GalactiteBow>() }, "Use a [i:" + ItemType("GalacticTotem") + "] anywhere and anytime.");
            }
        }
        public override void PostUpdateEverything()
        {
            ColorCounter += directionOfChange ? 0.02f : -0.02f;
            ColorCounter = MathHelper.Clamp(ColorCounter, 0, 1);
            if (ColorCounter >= 1) directionOfChange = false;
            if (ColorCounter <= 0) directionOfChange = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion, 3);
            recipe.AddIngredient(ItemID.LavaBucket, 5);
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
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.GoldenKey);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Cloud, 10);
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
            recipe.SetResult(ItemID.PinkGel, 20);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Book, 20);
            recipe.AddIngredient(ItemID.WaterBucket, 5);
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
            Terraria.RecipeGroup group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Phasesaber", new int[]
            {
        ItemID.PurplePhasesaber,
        ItemID.RedPhasesaber,
        ItemID.BluePhasesaber,
        ItemID.GreenPhasesaber,
        ItemID.WhitePhasesaber,
        ItemID.YellowPhasesaber,
            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:Phasesabers", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Hardmode Bar", new int[]
            {
        ItemID.AdamantiteBar,

        ItemID.TitaniumBar,
            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T3HmB", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 2 Hardmode Bar", new int[]
           {
        ItemID.MythrilBar,

        ItemID.OrichalcumBar,
           });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T2HmB", group);

            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 2 Repeater", new int[]
           {
        ItemID.MythrilRepeater,

        ItemID.OrichalcumRepeater,
           });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T2Rep", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 1 Repeater", new int[]
           {
        ItemID.CobaltRepeater,

        ItemID.PalladiumRepeater,
           });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T1Rep", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 3 Repeater", new int[]
           {
        ItemID.TitaniumRepeater,

        ItemID.AdamantiteRepeater,
           });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T3Rep", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardmode Forge", new int[]
            {
        ItemID.AdamantiteForge,

        ItemID.TitaniumForge,
            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T3Forge", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardmode Anvil", new int[]
            {
        ItemID.MythrilAnvil,

        ItemID.OrichalcumAnvil,
            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:HmAnvil", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 1 wings", new int[]
            {
        ItemID.AngelWings,

        ItemID.DemonWings,
            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T1Wings", group);
            group = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tier 12 wings", new int[]
            {
        ItemID.WingsSolar,
         ItemID.WingsNebula,
          ItemID.WingsVortex,
           ItemID.WingsStardust,

            });
            Terraria.RecipeGroup.RegisterGroup("DRGN:T12Wings", group);
        }
        public override void Load()
        {
            Init();

            meleePrefixes.Add(1);
            meleePrefixes.Add(2);
            meleePrefixes.Add(3);
            meleePrefixes.Add(4);
            meleePrefixes.Add(5);
            meleePrefixes.Add(6);
            meleePrefixes.Add(7);
            meleePrefixes.Add(8);
            meleePrefixes.Add(9);
            meleePrefixes.Add(10);
            meleePrefixes.Add(11);
            meleePrefixes.Add(12);
            meleePrefixes.Add(13);
            meleePrefixes.Add(14);
            meleePrefixes.Add(15);
            meleePrefixes.Add(36);
            meleePrefixes.Add(37);
            meleePrefixes.Add(38);
            meleePrefixes.Add(39);
            meleePrefixes.Add(40);
            meleePrefixes.Add(41);
            meleePrefixes.Add(42);
            meleePrefixes.Add(43);
            meleePrefixes.Add(44);
            meleePrefixes.Add(45);
            meleePrefixes.Add(46);
            meleePrefixes.Add(47);
            meleePrefixes.Add(48);
            meleePrefixes.Add(49);
            meleePrefixes.Add(50);
            meleePrefixes.Add(51);
            meleePrefixes.Add(53);
            meleePrefixes.Add(54);
            meleePrefixes.Add(55);
            meleePrefixes.Add(56);
            meleePrefixes.Add(57);
            meleePrefixes.Add(59);
            meleePrefixes.Add(60);
            meleePrefixes.Add(61);
            meleePrefixes.Add(81);
            meleePrefixes.Add(PrefixType("Mental"));

            FlailItem.Add(5011);
            FlailItem.Add(5012);
            FlailItem.Add(ItemID.BallOHurt);
            FlailItem.Add(ItemID.TheMeatball);
            FlailItem.Add(ItemID.BlueMoon);
            FlailItem.Add(ItemID.Sunfury);
            FlailItem.Add(ItemID.DaoofPow);
            FlailItem.Add(ItemID.FlowerPow);
            FlailItem.Add(4272);


            FlailsRangeMult.Add(new Vector2(947, 13));
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


            FlailsMinPlayerDists.Add(new Vector3(947, 8, 13));
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
            ReaperSoulBar = new ReaperSoulBar();
            ReaperSoulBar.Activate();
            DodgeBar = new DodgeBar();
            DodgeBar.Activate();


            _reaperSoulBar = new UserInterface();
            _reaperSoulBar.SetState(ReaperSoulBar);

            _revivalCooldownBar = new UserInterface();
            _revivalCooldownBar.SetState(RevivalBar);
            _dodgeCooldownBar = new UserInterface();
            _dodgeCooldownBar.SetState(DodgeBar);


        }
        public override void Unload()
        {
            TimeWarpHotkey = null;


        }
        public override void UpdateUI(GameTime gameTime)
        {
            _revivalCooldownBar?.Update(gameTime);
            RevivalBar?.Update(gameTime);

            _dodgeCooldownBar?.Update(gameTime);
            DodgeBar?.Update(gameTime);
            _reaperSoulBar?.Update(gameTime);
            ReaperSoulBar?.Update(gameTime);

            
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
                        _revivalCooldownBar.Draw(Terraria.Main.spriteBatch, new GameTime());
                        _dodgeCooldownBar.Draw(Terraria.Main.spriteBatch, new GameTime());
                        _reaperSoulBar.Draw(Terraria.Main.spriteBatch, new GameTime());

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }


        public void Init()
        {
            On.Terraria.WorldGen.UpdateWorld += WorldGen_UpdateWorld;
            On.Terraria.Player.beeType += Player_beeType;
            On.Terraria.Player.beeDamage += Player_beeDamage;
            On.Terraria.Player.beeKB += Player_beeKB;
            On.Terraria.Main.DrawMenu += Main_DrawMenu;
            On.Terraria.Main.DrawProj += Main_DrawProj;
            On.Terraria.Player.Counterweight += Player_Counterweight;
            On.Terraria.Main.MouseText += MouseText;
            On.Terraria.ItemText.NewText += NewText;
            On.Terraria.ItemText.Update += TextUpdate;
            On.Terraria.Item.Prefix += UpdateRarity;
            On.Terraria.Main.DrawInterface_Resources_Life += Main_DrawInterface_Resources_Life;
            On.Terraria.Main.DrawInterface_Resources_Mana += Main_DrawInterface_Resources_Mana;
            RarityInit();
        }
        private void WorldGen_UpdateWorld(On.Terraria.WorldGen.orig_UpdateWorld orig)
        {
            orig();
            int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
            int y = WorldGen.genRand.Next((int)Main.worldSurface - 1, Main.maxTilesY - 20);
            int yUp = y - 1;
            if (yUp < 10)
            {
                yUp = 10;
            }
            if (Main.tile[x, y] != null && Main.tile[x, y].liquid <= 32 && Main.tile[x, y].nactive())
            {
                if (Main.tile[x, y].type == 60)
                {
                    if (Main.hardMode && WorldGen.genRand.Next(10) == 0)
                    {
                        bool create = true;
                        int off = 80;
                        for (int i = x - off; i < x + off; i += 2)
                        {
                            for (int j = y - off; j < y + off; j += 2)
                            {
                                if (i > 1 && i < Main.maxTilesX - 2 && j > 1 && j < Main.maxTilesY - 2 && Main.tile[i, j].active() && Main.tile[i, j].type == TileType("ManaFruit"))
                                {
                                    create = false;
                                    break;
                                }
                            }
                        }
                        if (create)
                        {
                            PlaceJungleTile(x, yUp, (ushort)TileType("ManaFruit"), WorldGen.genRand.Next(3));
                            WorldGen.SquareTileFrame(x, yUp, true);
                            WorldGen.SquareTileFrame(x + 1, yUp + 1, true);
                            if (Main.tile[x, yUp].type == TileType("ManaFruit") && Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, x, yUp, 4);
                            }
                        }

                    }
                }
            }
        }
        public void PlaceJungleTile(int X2, int Y2, ushort type, int styleX)
        {

            for (int a = 0; a < 20; a++)
            {
                if (X2 < 5 || X2 > Main.maxTilesX - 5 || Y2 < 5 || Y2 > Main.maxTilesY - 5)
                {
                    return;
                }
                bool place = true;
                for (int i = X2 - 1; i < X2 + 1; i++)
                {
                    for (int j = Y2 - 1; j < Y2 + 1; j++)
                    {
                        if (Main.tile[i, j] == null)
                        {
                            Main.tile[i, j] = new Tile();
                        }
                        if (Main.tile[i, j].active() && Main.tile[i, j].type != 61 && Main.tile[i, j].type != 62 && Main.tile[i, j].type != 69 && Main.tile[i, j].type != 74 && (type != 236 || Main.tile[i, j].type != 233) && (type != 238 || Main.tile[i, j].type != 233) && (Main.tile[i, j].type != 185 || Main.tile[i, j].frameY != 0))
                        {
                            place = false;
                        }
                        if (type == 98 && Main.tile[i, j].liquid > 0)
                        {
                            place = false;
                        }
                    }
                    if (Main.tile[i, Y2 + 1] == null)
                    {
                        Main.tile[i, Y2 + 1] = new Tile();
                    }
                    if (!WorldGen.SolidTile(i, Y2 + 1) || Main.tile[i, Y2 + 1].type != 60)
                    {
                        place = false;
                    }
                }
                if (place)
                {
                    short typeX = (short)(36 * styleX);
                    Main.tile[X2 - 1, Y2 - 1].active(active: true);
                    Main.tile[X2 - 1, Y2 - 1].frameY = 0;
                    Main.tile[X2 - 1, Y2 - 1].frameX = typeX;
                    Main.tile[X2 - 1, Y2 - 1].type = type;
                    Main.tile[X2, Y2 - 1].active(active: true);
                    Main.tile[X2, Y2 - 1].frameY = 0;
                    Main.tile[X2, Y2 - 1].frameX = (short)(18 + typeX);
                    Main.tile[X2, Y2 - 1].type = type;
                    Main.tile[X2 - 1, Y2].active(active: true);
                    Main.tile[X2 - 1, Y2].frameY = (short)(18);
                    Main.tile[X2 - 1, Y2].frameX = typeX;
                    Main.tile[X2 - 1, Y2].type = type;
                    Main.tile[X2, Y2].active(active: true);
                    Main.tile[X2, Y2].frameY = (short)(18);
                    Main.tile[X2, Y2].frameX = (short)(18 + typeX);
                    Main.tile[X2, Y2].type = type;
                    break;
                }
                X2 += WorldGen.genRand.Next(-5, 5);
                Y2 += WorldGen.genRand.Next(-5, 5);
            }

        }

        private float Player_beeKB(On.Terraria.Player.orig_beeKB orig, Player self, float KB)
        {
            if (self.GetModPlayer<DRGNPlayer>().qbEquip) { return 1f + KB * 1.2f;}
            orig(self, KB);
            return KB;
        }

        private int Player_beeDamage(On.Terraria.Player.orig_beeDamage orig, Player self, int dmg)
        {
            if (self.GetModPlayer<DRGNPlayer>().qbEquip) { return dmg + Main.rand.Next(2, 8); }
            orig(self , dmg);
            return dmg;
        }

        private int Player_beeType(On.Terraria.Player.orig_beeType orig, Player self)
        {
            if (self.GetModPlayer<DRGNPlayer>().qbEquip && Main.rand.NextBool(1, 25)) { return ProjectileID.Beenade; }
            else
            {
                orig(self);
            }
            return 181;
        }
        private bool isValidWorldName(string name)
        { 
            if(name == "") { return false; }
            foreach (WorldFileData wfd in Main.WorldList)
            {
                if(wfd.Name == name) { return false; }

            }
            return true;     
        }
        private void DoOnClick(int buttonID)
        {
            if (buttonID == 0)
            {
                Main.menuMode = 6;
                Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
            }
            else if (buttonID < 4) { selectedButtons[0] = buttonID; }
            else if (buttonID == 4)
            {
            }
            else if (buttonID < 8) { selectedButtons[1] = buttonID; }
            else if (buttonID == 8)
            {
                if (isValidWorldName(name))
                {
                    WorldGen.WorldGenParam_Evil = (selectedButtons[0] == 1 ? 0 : selectedButtons[0] == 2 ? -1 : 1);
                    if (selectedButtons[2] == 9)
                    {
                        Main.maxTilesX = 4200;
                        Main.maxTilesY = 1200;
                    }
                    else if (selectedButtons[2] == 10)
                    {
                        Main.maxTilesX = 6400;
                        Main.maxTilesY = 1800;
                    }
                    else
                    {
                        Main.maxTilesX = 8400;
                        Main.maxTilesY = 2400;
                    }
                    WorldGen.setWorldSize();
                    Main.expertMode = selectedButtons[1] != 5;
                    Main.menuMode = 10;
                    Main.worldName = name;
                    worldInfo.Add(name,false);
                    Main.ActiveWorldFileData = WorldFile.CreateMetadata(Main.worldName, SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault, Main.expertMode);
                    WorldGen.CreateNewWorld(null);
                    if (selectedButtons[1] == 7) { DRGNModWorld.MentalMode = true; worldInfo[name] = true; }
                }
            }
            else { selectedButtons[2] = buttonID; }

        }
        private void CheckHover(int buttonID, Rectangle positon)
        {
            if (positon.Contains((Main.MouseWorld - Main.screenPosition).ToPoint()))
            {
                if (hoverButton != buttonID)
                { Main.PlaySound(SoundID.MenuOpen, -1, -1, 1); hoverButton = buttonID; }
                
                string text = buttonID == 0 ? "Back" : buttonID == 1 ? "Corruption" : buttonID == 2 ? "Random" : buttonID == 3 ? "Crimson" : buttonID == 4 ? "World Name" : buttonID == 5 ? "Normal" : buttonID == 6 ? "Expert" : buttonID == 7 ? "Mental" : buttonID == 8 ? "Create World" : buttonID == 9 ? "Small World" : buttonID == 10 ? "Medium World" : "Large World";
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText,text, Main.MouseWorld - Main.screenPosition, Color.White, 0f, new Vector2(-22, 16), new Vector2(1f, 1f));
                if(buttonID >= 5 && buttonID <= 7)
                {
                    string text2 = buttonID == 5 ? "Your Journey Begins..." : buttonID == 6 ? "Fortune & Glory, Kid." : "It will be fun they said";
                    string text3 = buttonID == 5 ? "(The standard Terraria Experience)" : buttonID == 6 ? "(Far Greater Difficulty & Loot)" : "(Not just stat changes I promise)";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, text2, Main.MouseWorld - Main.screenPosition, Color.White, 0f, new Vector2(-22, -4), new Vector2(1f, 1f));
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, text3, Main.MouseWorld - Main.screenPosition, Color.White, 0f, new Vector2(-22, -24), new Vector2(1f, 1f));


                }
                if (Main.mouseLeft)
                { DoOnClick(buttonID); }
            }
            else if (hoverButton == buttonID) { hoverButton = -1; }
        }
        private void HandleButtons(Vector2 pos, Main self)
        {
            int posX = (int)pos.X;
            int posY = (int)pos.Y;
            Vector2 textpos = new Vector2(posX + 20, posY + 74);
            Vector2 textpos2 = new Vector2(posX + 20, posY + 274);
            Rectangle[] buttonPos = new Rectangle[12];
            buttonPos[0] = new Rectangle(posX + 20, posY + 12, 60, 52);
            buttonPos[1] = new Rectangle(posX + 100, posY + 12, 80, 52);
            buttonPos[2] = new Rectangle(posX + 200, posY + 12, 80, 52);
            buttonPos[3] = new Rectangle(posX + 300, posY + 12, 80, 52);
            buttonPos[4] = new Rectangle(posX + 20, posY + 74, 360, 52);
            buttonPos[5] = new Rectangle(posX + 20, posY + 134, 80, 52);
            buttonPos[6] = new Rectangle(posX + 120, posY + 134, 80, 52);
            buttonPos[7] = new Rectangle(posX + 220, posY + 134, 80, 52);
            buttonPos[8] = new Rectangle(posX + 320, posY + 134, 60, 52);
            buttonPos[9] = new Rectangle(posX + 400, posY + 8, 32, 44);
            buttonPos[10] = new Rectangle(posX + 400, posY + 68, 32, 44);
            buttonPos[11] = new Rectangle(posX + 400, posY + 128, 32, 44);
            for (int i = 0; i < buttonPos.Length; i++)
            {
                if (i == hoverButton || selectedButtons.Contains(i))
                {
                    Texture2D glowText = ModContent.GetTexture("DRGN/UI/WorldGenUI/Glow/" + i);
                    Main.spriteBatch.Draw(glowText, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                }
                               
            }
            for (int i = 0; i < buttonPos.Length; i++)
            {
                CheckHover(i, buttonPos[i]);
            }
            Main.hasFocus = true;
            name = getInputText(name);

            string add = " ";
            self.textBlinkerCount++;
            if (self.textBlinkerCount >= 20)
            {
                if (self.textBlinkerState == 0)
                {
                    self.textBlinkerState = 1;
                }
                else
                {
                    self.textBlinkerState = 0;
                }
                self.textBlinkerCount = 0;
            }

            if (self.textBlinkerState == 1)
            {

                add = "|";

            }
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, name + add, textpos, Color.White, 0f, new Vector2(-8, -8), new Vector2(0.9f, 0.9f));
            Color color = Color.White;
            if (new Rectangle((int)textpos2.X, (int)textpos2.Y, (int)Main.fontDeathText.MeasureString("Use Vanilla WorldGen").X , (int)Main.fontDeathText.MeasureString("Use Vanilla WorldGen").Y).Contains((Main.MouseWorld - Main.screenPosition).ToPoint()))
            {
                color = Color.Yellow;
                if(Main.mouseLeft)
                {
                    Main.menuMode = 16;
                    usesVanillaWorldGen = true;
                    Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
                }
            }
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Use Vanilla WorldGen", textpos2, color, 0f, new Vector2(0, 0), new Vector2(0.9f, 0.9f));

        }
        public string getInputText(string text)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] oldKeys = oldState.GetPressedKeys();
            Keys[] keys = keyboardState.GetPressedKeys();
            for (int i = 0; i < keys.Length; i++)
            {
                if (!oldKeys.Contains(keys[i]) && text.Length < 10 && (int)keys[i] >= 32 && (int)keys[i] != 127 && keys[i] != Keys.LeftShift && keys[i] != Keys.RightShift)
                {

                    char c = (char)keys[i];
                    string s = c.ToString();
                    if (!keyboardState.IsKeyDown(Keys.LeftShift) && !keyboardState.IsKeyDown(Keys.RightShift))
                    { s = s.ToLower(); }
                    text += s;
                }
                else if (!oldKeys.Contains(keys[i]) && keys[i] == Keys.Back)
                {
                    if (text.Length != 0)
                    {
                        text = text.Remove(text.Length - 1);
                    }
                }

            }
            oldState = Keyboard.GetState();
            return text;
        }
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            if (Main.menuMode == 6)
            {
                Main.MenuUI.SetState(myUIWorldSelect);
                Main.menuMode = 888;
                usesVanillaWorldGen = false;
            }
            if (Main.menuMode == 16 && !usesVanillaWorldGen)
            { Main.menuMode = 1000; }
            orig(self, gameTime);
            if (Main.menuMode == 1000)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                Vector2 pos = new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                Texture2D baseText = ModContent.GetTexture("DRGN/UI/WorldGenUI/Base/Base");
                Main.spriteBatch.Draw(baseText, pos, null, Color.White, 0f, baseText.Size() * 0.5f, 1f, SpriteEffects.None, 1f);
                Texture2D searchText = ModContent.GetTexture("DRGN/UI/WorldGenUI/Base/Search");
                Main.spriteBatch.Draw(searchText, pos, null, Color.White, 0f, searchText.Size() * 0.5f, 1f, SpriteEffects.None, 1f);
                Texture2D buttonText = ModContent.GetTexture("DRGN/UI/WorldGenUI/Base/Buttons");
                Main.spriteBatch.Draw(buttonText, pos, null, Color.White, 0f, buttonText.Size() * 0.5f, 1f, SpriteEffects.None, 1f);
                HandleButtons(pos - buttonText.Size() * 0.5f, self);
                Main.spriteBatch.End();
            }
            if (Main.menuMode == 16 )
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                Vector2 vector = Main.fontDeathText.MeasureString("Use DRGN WorldGen");
                Texture2D buttonText = ModContent.GetTexture("DRGN/UI/WorldGenUI/Base/Buttons");
                Vector2 pos = new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f) - buttonText.Size() * 0.5f;
                Vector2 textpos = new Vector2(pos.X + 20, pos.Y + 374);
                Color color = Color.White;           
                if (new Rectangle((int)textpos.X, (int)textpos.Y, (int)vector.X, (int)vector.Y).Contains((Main.MouseWorld - Main.screenPosition).ToPoint()))
                {
                    color = Color.Yellow;
                    if (Main.mouseLeft)
                    {                        
                        usesVanillaWorldGen = false;
                        Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
                    }
                }
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText,"Use DRGN WorldGen", textpos, color, 0f, Vector2.Zero, new Vector2(0.9f, 0.9f));
                Main.spriteBatch.End();

            }

        }

        private void Main_DrawProj(On.Terraria.Main.orig_DrawProj orig, Main self, int i)
        {
            if (!ModContent.GetInstance<DRGNConfig>().DisableYoyoAI && Main.projectile[i].aiStyle == 99)
            {
                Projectile projectile = Main.projectile[i];
                YoyoAI.DrawString(projectile); 
                self.LoadProjectile(projectile.type);
                Texture2D text = Main.projectileTexture[projectile.type];
                if (projectile.counterweight) { text = ModContent.GetTexture("DRGN/Projectiles/CounterWeights/Projectile_" + projectile.type.ToString()); }               
                if (text != null)
                {
                    Vector2 pos = projectile.Center - Main.screenPosition;
                    Color color = Lighting.GetColor((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f));
                    Main.spriteBatch.Draw(text, pos, null, color, projectile.rotation, text.Size() * 0.5f, 1f, SpriteEffects.None, 0);
                }
            }
            else { orig(self, i); }
        }

        private void Player_Counterweight(On.Terraria.Player.orig_Counterweight orig, Player player, Vector2 hitPos, int dmg, float kb)
        {
            int counterweight = player.counterWeight;
            int ProjCap = player.GetModPlayer<DRGNPlayer>().maxYoyos;
            int playerYoyoID = -1;
            int ExistingYoyos = 0;
            int CounterWeightNum = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                {
                    if (Main.projectile[i].counterweight)
                    {
                        CounterWeightNum++;
                    }
                    else if (Main.projectile[i].aiStyle == 99)
                    {
                        ExistingYoyos++;
                        playerYoyoID = i;
                        if (ExistingYoyos > ProjCap) { Main.projectile[i].active = false; }

                    }
                }
            }
            int timeLeft = 360;
            if (ExistingYoyos < ProjCap)
            {
                if (playerYoyoID >= 0)
                {
                    Projectile projectile = Main.projectile[playerYoyoID];
                    Vector2 Vel = hitPos - player.Center;
                    Vel.Normalize();
                    Vel *= 16f;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, Vel.X, Vel.Y, projectile.type, projectile.damage, projectile.knockBack, player.whoAmI);

                }
            }
            else if (CounterWeightNum < ExistingYoyos)
            {
                Vector2 Velocity = hitPos - player.Center;
                Velocity.Normalize();
                Velocity *= 16f;
                float knockback = (kb + 6f) / 2f;
                if (CounterWeightNum > 0)
                {
                    int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y, Velocity.X, Velocity.Y, counterweight, (int)((double)dmg * 0.8), knockback, player.whoAmI);
                    Main.projectile[projid].timeLeft = timeLeft;
                }
                else
                {
                    int projid = Projectile.NewProjectile(player.Center.X, player.Center.Y, Velocity.X, Velocity.Y, counterweight, (int)((double)dmg * 0.8), knockback, player.whoAmI);
                    Main.projectile[projid].timeLeft = timeLeft;
                }
            }
        }

        private void Main_DrawInterface_Resources_Mana(On.Terraria.Main.orig_DrawInterface_Resources_Mana orig)
        {
            Terraria.Player player = Terraria.Main.LocalPlayer;
            HandleResourceBarsInit(player, out int numManaStars, out int numGoldenStars, out int numLunarStars, out Texture2D[] manaStars, out float starMana, out int sX, 1);
            if (Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 > 0)
            {
                for (int i = 1; i < numManaStars + 1; i++)
                {
                    bool scaledUp = false;
                    float scale = 1f;
                    int colorVal;
                    if (Terraria.Main.player[Terraria.Main.myPlayer].statMana >= i * starMana)
                    {
                        colorVal = 255;
                        if (Terraria.Main.player[Terraria.Main.myPlayer].statMana == i * starMana)
                        {
                            scaledUp = true;
                        }
                    }
                    else
                    {
                        float manaPercent = (float)(Terraria.Main.player[Terraria.Main.myPlayer].statMana - (i - 1) * starMana) / (float)starMana;
                        colorVal = (int)(30f + 225f * manaPercent);
                        if (colorVal < 30)
                        {
                            colorVal = 30;
                        }
                        scale = manaPercent / 4f + 0.75f;
                        if ((double)scale < 0.75)
                        {
                            scale = 0.75f;
                        }
                        if (manaPercent > 0f)
                        {
                            scaledUp = true;
                        }
                    }
                    if (scaledUp)
                    {
                        scale += Terraria.Main.cursorScale - 1f;
                    }
                    int alpha = (int)((double)((float)colorVal) * 0.9);
                    int TextureIndex = 0;
                    if (i <= numLunarStars)
                    { TextureIndex = 2; }
                    else if (i <= numGoldenStars)
                    { TextureIndex = 1; }
                    Terraria.Main.spriteBatch.Draw(manaStars[TextureIndex], new Vector2((float)(775 + sX), (float)(30 + Terraria.Main.manaTexture.Height / 2) + ((float)Terraria.Main.manaTexture.Height - (float)Terraria.Main.manaTexture.Height * scale) / 2f + (float)(28 * (i - 1))), new Rectangle?(new Rectangle(0, 0, Terraria.Main.manaTexture.Width, Terraria.Main.manaTexture.Height)), new Color(colorVal, colorVal, colorVal, alpha), 0f, new Vector2((float)(Terraria.Main.manaTexture.Width / 2), (float)(Terraria.Main.manaTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }
        }

        private void HandleHeartDraw(Terraria.Player player, out float scale, out int colorVal, float heartLife, int i, int numLifeFruits, int numHeartEmblem, out int xOff, out int yOff, out int TextureIndex)
        {
            scale = 1f;
            bool scaledUp = false;
            if ((float)player.statLife >= (float)i * heartLife)
            {
                colorVal = 255;
                if ((float)player.statLife == (float)i * heartLife)
                {
                    scaledUp = true;
                }
            }
            else
            {
                float heartPercent = ((float)player.statLife - (float)(i - 1) * heartLife) / heartLife;
                colorVal = (int)(30f + 225f * heartPercent);
                if (colorVal < 30)
                {
                    colorVal = 30;
                }
                scale = heartPercent / 4f + 0.75f;
                if ((double)scale < 0.75)
                {
                    scale = 0.75f;
                }
                if (heartPercent > 0f)
                {
                    scaledUp = true;
                }
            }
            if (scaledUp)
            {
                scale += Terraria.Main.cursorScale - 1f;
            }
            xOff = i > 10 ? -260 : 0;
            yOff = i > 10 ? 26 : 0;
            TextureIndex = 0;
            if (i <= numHeartEmblem)
            { TextureIndex = 2; }
            else if (i <= numLifeFruits)
            { TextureIndex = 1; }
        }
        private void HandleResourceBarsInit(Terraria.Player player, out int numTier1, out int numTier2, out int numTier3, out Texture2D[] resources, out float valuePerImage, out int sX, int mode)
        {
            Color color = new Color((int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor);
            sX = Terraria.Main.screenWidth - 800;
            numTier1 = 0;
            numTier2 = 0;
            numTier3 = 0;
            valuePerImage = 20f;
            resources = new Texture2D[3];
            if (mode == 0)
            {
                numTier1 = player.statLifeMax > 400 ? 20 : player.statLifeMax / 20;
                numTier2 = player.statLifeMax > 500 ? 20 : player.statLifeMax > 400 ? (player.statLifeMax - 400) / 5 : 0;
                numTier3 = player.GetModPlayer<DRGNPlayer>().heartEmblem;
                resources = new Texture2D[3] { Terraria.Main.heartTexture, Terraria.Main.heart2Texture, ModContent.GetTexture("DRGN/UI/Heart3") };
                string text = string.Concat(new object[]
                {
                "Life:",
                " ",
                player.statLifeMax2,
                "/",
                player.statLifeMax2
                });
                Vector2 vector = Terraria.Main.fontMouseText.MeasureString(text);
                Vector2 lifeVector = new Vector2((float)(500 + 13 * (numTier1 > 10 ? 10 : numTier1)) - vector.X * 0.5f + (float)sX, 6f);
                Vector2 textVector = new Vector2((float)(500 + 13 * (numTier1 > 10 ? 10 : numTier1)) + vector.X * 0.5f + (float)sX, 6f);
                if (!Terraria.Main.player[Terraria.Main.myPlayer].ghost)
                {
                    Terraria.Main.spriteBatch.DrawString(Terraria.Main.fontMouseText, "Life:", lifeVector, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    Terraria.Main.spriteBatch.DrawString(Terraria.Main.fontMouseText, player.statLife + "/" + player.statLifeMax2, textVector, color, 0f, new Vector2(Terraria.Main.fontMouseText.MeasureString(player.statLife + "/" + player.statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f);
                }
                if (player.statLifeMax > 400)
                {
                    valuePerImage = (float)Terraria.Main.player[Terraria.Main.myPlayer].statLifeMax2 / 20f;
                }
            }
            else if (mode == 1)
            {
                numTier1 = player.statManaMax > 200 ? 10 : player.statManaMax / 20;
                numTier2 = player.GetModPlayer<DRGNPlayer>().goldenStars;
                numTier3 = player.GetModPlayer<DRGNPlayer>().lunarStars;
                resources = new Texture2D[3] { Terraria.Main.manaTexture, ModContent.GetTexture("DRGN/UI/Mana2"), ModContent.GetTexture("DRGN/UI/Mana3") };
                Terraria.Main.spriteBatch.DrawString(Terraria.Main.fontMouseText, "Mana", new Vector2((float)(750 + sX), 6f), new Color((int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor, (int)Terraria.Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                valuePerImage = 20 + numTier2 / 2 + numTier3 / 2;
            }
        }
        private void Main_DrawInterface_Resources_Life(On.Terraria.Main.orig_DrawInterface_Resources_Life orig)
        {
            Terraria.Player player = Terraria.Main.LocalPlayer;
            HandleResourceBarsInit(player, out int numLifeCrystals, out int numLifeFruits, out int numHeartEmblem, out Texture2D[] hearts, out float heartLife, out int sX, 0);
            for (int i = 1; i < numLifeCrystals + 1; i++)
            {

                HandleHeartDraw(player, out float scale, out int colorVal, heartLife, i, numLifeFruits, numHeartEmblem, out int xOff, out int yOff, out int TextureIndex);

                int alpha = (int)((double)((float)colorVal) * 0.9);
                Vector2 pos = new Vector2((float)(500 + 26 * (i - 1) + xOff + sX + Terraria.Main.heartTexture.Width / 2), 32f + ((float)Terraria.Main.heartTexture.Height - (float)Terraria.Main.heartTexture.Height * scale) / 2f + (float)yOff + (float)(Terraria.Main.heartTexture.Height / 2));
                Rectangle rect = new Rectangle(0, 0, Terraria.Main.heartTexture.Width, Terraria.Main.heartTexture.Height);
                if (!Terraria.Main.player[Terraria.Main.myPlayer].ghost)
                {
                    Terraria.Main.spriteBatch.Draw(hearts[TextureIndex], pos, rect, new Color(colorVal, colorVal, colorVal, alpha), 0f, Terraria.Main.heartTexture.Size() / 2f, scale, SpriteEffects.None, 0f);
                }
            }

        }

        private bool UpdateRarity(On.Terraria.Item.orig_Prefix orig, Terraria.Item item, int pre)
        {
            orig(item, pre);
            Terraria.Item It = new Terraria.Item();
            It.SetDefaults(item.type);
            int baseRarity = It.rare;
            int baseDamage = It.damage;
            int baseUseTime = It.useTime;

            int baseMana = It.mana;
            float baseKnockback = It.knockBack;
            float baseScale = It.scale;
            float baseShootspeed = It.shootSpeed;
            int baseCrit = It.crit;
            item.rare = baseRarity;
            if (_isFixedRarity.Contains(item.rare))
            { return true; }

            float DamageInc = 1;
            if (baseDamage != 0)
            {
                DamageInc = item.damage / baseDamage;
            }
            float KnockBack = 1;
            if (baseKnockback != 0)
            {
                KnockBack = item.knockBack / baseKnockback;
            }
            float UseTimeMult = 1;
            if (baseUseTime != 0)
            {
                UseTimeMult = item.useTime / baseUseTime;
            }
            float ScaleMult = 1;
            if (baseScale != 0)
            {
                ScaleMult = item.scale / baseScale;
            }
            float ShootspeedMult = 1;
            if (baseShootspeed != 0)
            {
                ShootspeedMult = item.shootSpeed / baseShootspeed;
            }
            float ManaMult = 1;
            if (baseMana != 0)
            {
                ManaMult = item.mana / baseMana;
            }
            float CritMult = 1;
            if (baseCrit != 0)
            {
                CritMult = item.crit / baseCrit;
            };




            int i = item.prefix;
            float TotalValue = 1f * DamageInc * (2f - UseTimeMult) * (2f - ManaMult) * ScaleMult * KnockBack * ShootspeedMult * (1f + (float)CritMult * 0.02f);
            if (i == 62 || i == 69 || i == 73 || i == 77)
            {
                TotalValue *= 1.05f;
            }
            if (i == 63 || i == 70 || i == 74 || i == 78 || i == 67)
            {
                TotalValue *= 1.1f;
            }
            if (i == 64 || i == 71 || i == 75 || i == 79 || i == 66)
            {
                TotalValue *= 1.15f;
            }
            if (i == PrefixID.Warding || i == PrefixID.Menacing || i == PrefixID.Lucky || i == PrefixID.Quick || i == PrefixID.Violent)
            {
                TotalValue *= 1.2f;
            }
            if (i == ModContent.PrefixType<Shielding>() || i == ModContent.PrefixType<Wrathful>() || i == ModContent.PrefixType<Weighted>() || i == ModContent.PrefixType<Rapid>() || i == ModContent.PrefixType<Beserk>())
            {
                TotalValue *= 1.5f;
            }
            if ((double)TotalValue >= 1.5)
            {
                item.rare += 3;
            }
            else if ((double)TotalValue >= 1.2)
            {
                item.rare += 2;
            }
            else if ((double)TotalValue >= 1.05)
            {
                item.rare++;
            }
            else if ((double)TotalValue <= 0.8)
            {
                item.rare -= 2;
            }
            else if ((double)TotalValue <= 0.95)
            {
                item.rare--;
            }

            if (item.rare > MaxRarity)
            { item.rare = MaxRarity; }
            return true;
        }
        private void RarityInit()
        {
            _rarities.Clear();
            _dynamicRaritiesColor.Clear();
            _usesDiscoRGB.Clear();
            _isFixedRarity.Clear();
            _rarities.Add(-11, RarityAmber);
            _rarities.Add(-1, new Color(34, 66, 0));//grey           
            _rarities.Add(1, new Color(187, 238, 155));//snakeskin light 2
            _dynamicRaritiesColor.Add(1, new Color(238, 209, 154));//snakeskin light
            _rarities.Add(2, new Color(75, 198, 14));//toxic light
            _dynamicRaritiesColor.Add(2, new Color(14, 198, 78));//toxic light 2
            _rarities.Add(3, new Color(148, 95, 0));//ant light 
            _dynamicRaritiesColor.Add(3, new Color(148, 124, 3));//ant light 2
            _rarities.Add(4, new Color(0, 255, 136));//light turquoise
            _dynamicRaritiesColor.Add(4, new Color(0, 255, 213));//light blue
            _rarities.Add(5, new Color(255, 253, 0));//Yellow
            _dynamicRaritiesColor.Add(5, new Color(255, 0, 0));//Red
            _rarities.Add(6, new Color(31, 63, 208));//light blue 2
            _dynamicRaritiesColor.Add(6, new Color(31, 165, 208)); //light blue
            _rarities.Add(7, new Color(107, 134, 55));//yellow green
            _dynamicRaritiesColor.Add(7, new Color(46, 255, 0));//light green
            _rarities.Add(8, new Color(255, 109, 0));//light orange
            _dynamicRaritiesColor.Add(8, new Color(157, 255, 0));//light green
            _rarities.Add(9, new Color(255, 0, 136));//bright pink
            _dynamicRaritiesColor.Add(9, new Color(198, 0, 255));//bright purple
            _rarities.Add(10, new Color(255, 239, 0));//dark teal
            _dynamicRaritiesColor.Add(10, new Color(0, 255, 201));//light teal
            _rarities.Add(11, new Color(69, 239, 112));//green cyan
            _dynamicRaritiesColor.Add(11, new Color(69, 239, 197));//blue cyan
            DarkBlue db = new DarkBlue();
            db.Load();
            FieryOrange fo = new FieryOrange();
            fo.Load();
            VoidPurple vp = new VoidPurple();
            vp.Load();
            GalacticRainbow gr = new GalacticRainbow();
            gr.Load();
            Mental mt = new Mental();
            mt.Load();
            bool foundHighest = false;
            for (int i = 11; foundHighest == false; i++)
            {
                foundHighest = true;
                if (_rarities.ContainsKey(i)) { foundHighest = false; }
                if (_dynamicRaritiesColor.ContainsKey(i)) { foundHighest = false; }
                if (_usesDiscoRGB.Contains(i)) { foundHighest = false; }
                MaxRarity = i - 1;
            }
        }
        private void getTextOffset(out int X, out int Y, int hackedMouseX, int hackedMouseY, int hackedScreenWidth, int hackedScreenHeight, string cursorText)
        {
            X = Terraria.Main.mouseX + 10;
            Y = Terraria.Main.mouseY + 10;
            if (hackedMouseX != -1 && hackedMouseY != -1)
            {
                X = hackedMouseX + 10;
                Y = hackedMouseY + 10;
            }
            if (Terraria.Main.ThickMouse)
            {
                X += 6;
                Y += 6;
            }
            Vector2 vector = Terraria.Main.fontMouseText.MeasureString(cursorText);
            if (hackedScreenHeight != -1 && hackedScreenWidth != -1)
            {
                if ((float)X + vector.X + 4f > (float)hackedScreenWidth)
                {
                    X = (int)((float)hackedScreenWidth - vector.X - 4f);
                }
                if ((float)Y + vector.Y + 4f > (float)hackedScreenHeight)
                {
                    Y = (int)((float)hackedScreenHeight - vector.Y - 4f);
                }
            }
            else
            {
                if ((float)X + vector.X + 4f > (float)Terraria.Main.screenWidth)
                {
                    X = (int)((float)Terraria.Main.screenWidth - vector.X - 4f);
                }
                if ((float)Y + vector.Y + 4f > (float)Terraria.Main.screenHeight)
                {
                    Y = (int)((float)Terraria.Main.screenHeight - vector.Y - 4f);
                }
            }
        }
        private void MouseText(On.Terraria.Main.orig_MouseText orig, Terraria.Main self, string cursorText, int rare, byte diff, int hackedMouseX, int hackedMouseY, int hackedScreenWidth, int hackedScreenHeight)
        {
            orig(self, cursorText, rare, diff, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight);
            Color baseColor = Color.White;
            getTextOffset(out int X, out int Y, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight, cursorText);
            if (_usesDiscoRGB.Contains(rare))
            {
                baseColor = new Color(Terraria.Main.DiscoR, Terraria.Main.DiscoG, Terraria.Main.DiscoB);
            }
            else if (_dynamicRaritiesColor.ContainsKey(rare))
            {
                baseColor = Color.Lerp(_rarities[rare], _dynamicRaritiesColor[rare], ColorCounter);
            }
            else if (_rarities.ContainsKey(rare))
            { baseColor = _rarities[rare]; }
            if (baseColor != Color.White)
                ChatManager.DrawColorCodedStringWithShadow(Terraria.Main.spriteBatch, Terraria.Main.fontMouseText, cursorText, new Vector2(X, Y), baseColor, 0f, Vector2.Zero, Vector2.One);
        }

        private void NewText(On.Terraria.ItemText.orig_NewText orig, Terraria.Item newItem, int stack, bool noStack = false, bool longText = false)
        {
            orig(newItem, stack, noStack, longText);
            int popupTextID = -1;
            for (int i = 0; i < 20; i++)
            {
                if (Terraria.Main.itemText[i].active && Terraria.Main.itemText[i].name == newItem.AffixName())
                { popupTextID = i; break; }
            }
            if (popupTextID != -1)
            {
                if (_usesDiscoRGB.Contains(newItem.rare))
                {
                    Terraria.Main.itemText[popupTextID].color = new Color(Terraria.Main.DiscoR, Terraria.Main.DiscoG, Terraria.Main.DiscoB);
                }
                if (_dynamicRaritiesColor.ContainsKey(newItem.rare))
                {
                    Terraria.Main.itemText[popupTextID].color = Color.Lerp(_rarities[newItem.rare], _dynamicRaritiesColor[newItem.rare], ColorCounter);
                }
                else if (_rarities.ContainsKey(newItem.rare))
                { Terraria.Main.itemText[popupTextID].color = _rarities[newItem.rare]; }
                if (_itemTextRarities.ContainsKey(popupTextID))
                { _itemTextRarities.Remove(popupTextID); }
                _itemTextRarities.Add(popupTextID, newItem.rare);
            }
        }
        private void TextUpdate(On.Terraria.ItemText.orig_Update orig, Terraria.ItemText it, int whoAmI)
        {
            orig(it, whoAmI);
            if (_itemTextRarities.ContainsKey(whoAmI))
            {
                int rare = _itemTextRarities[whoAmI];
                if (_dynamicRaritiesColor.ContainsKey(rare))
                {
                    it.color = Color.Lerp(_rarities[rare], _dynamicRaritiesColor[rare], ColorCounter);
                }
                if (_usesDiscoRGB.Contains(rare))
                { it.color = new Color((byte)Terraria.Main.DiscoR, (byte)Terraria.Main.DiscoG, (byte)Terraria.Main.DiscoB, Terraria.Main.mouseTextColor); }
            }
        }
    }
}