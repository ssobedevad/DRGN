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
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ID.Colors;

namespace DRGN
{
    public class DRGN : Mod
    {
        public static ModHotKey TimeWarpHotkey;

        internal RevivalBar RevivalBar;
        public static float ColorCounter = 0f;
        private static bool directionOfChange = false;

        internal DodgeBar DodgeBar;
        internal ReaperSoulBar ReaperSoulBar;

        private UserInterface _reaperSoulBar;
        private UserInterface _revivalCooldownBar;

        private UserInterface _dodgeCooldownBar;

        public static List<int> meleePrefixes = new List<int>();

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
                        _revivalCooldownBar.Draw(Main.spriteBatch, new GameTime());
                        _dodgeCooldownBar.Draw(Main.spriteBatch, new GameTime());
                        _reaperSoulBar.Draw(Main.spriteBatch, new GameTime());

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }


        public void Init()
        {
            On.Terraria.Main.MouseText += MouseText;
            On.Terraria.ItemText.NewText += NewText;
            On.Terraria.ItemText.Update += TextUpdate;
            On.Terraria.Item.Prefix += UpdateRarity;
            On.Terraria.Main.DrawInterface_Resources_Life += Main_DrawInterface_Resources_Life;
            RarityInit();
        }
        private void HandleHeartDraw(Player player , out float scale , out int colorVal , float heartLife , int i ,int numLifeFruits , int numHeartEmblem, out int xOff , out int yOff , out int TextureIndex)
        {

            scale = 1f;
            bool flag = false;           
            if ((float)player.statLife >= (float)i * heartLife)
            {
                colorVal = 255;
                if ((float)player.statLife == (float)i * heartLife)
                {
                    flag = true;
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
                    flag = true;
                }
            }
            if (flag)
            {
                scale += Main.cursorScale - 1f;
            }
            xOff = i > 10 ? -260 : 0;
            yOff = i > 10 ? 26 : 0;
            TextureIndex = 0;
            if (i <= numHeartEmblem * 2)
            { TextureIndex = 2; }
            else if (i <= numLifeFruits)
            { TextureIndex = 1; }      
        }
        private void HandleLifeBarsInit(Player player , out int numLifeCrystals , out int numLifeFruits, out int numHeartEmblem , out Texture2D[] hearts , out float heartLife , out int sX)
        {
            numLifeCrystals = player.statLifeMax > 400 ? 20 : player.statLifeMax / 20;
            numLifeFruits = player.statLifeMax > 500 ? 20 : player.statLifeMax > 400 ? (player.statLifeMax - 400) / 5 : 0;
            numHeartEmblem = player.GetModPlayer<DRGNPlayer>().heartEmblem;
            hearts = new Texture2D[3] { Main.heartTexture, Main.heart2Texture, ModContent.GetTexture("DRGN/UI/Heart3") };
            sX = Main.screenWidth - 800;
            string text = string.Concat(new object[]
            {
                "Life:",
                " ",
                player.statLifeMax2,
                "/",
                player.statLifeMax2
            });
            Vector2 vector = Main.fontMouseText.MeasureString(text);
            Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            Vector2 lifeVector = new Vector2((float)(500 + 13 * (numLifeCrystals > 10 ? 10 : numLifeCrystals)) - vector.X * 0.5f + (float)sX, 6f);
            Vector2 textVector = new Vector2((float)(500 + 13 * (numLifeCrystals > 10 ? 10 : numLifeCrystals)) + vector.X * 0.5f + (float)sX, 6f);
            if (!Main.player[Main.myPlayer].ghost)
            {
                Main.spriteBatch.DrawString(Main.fontMouseText, "Life:", lifeVector, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(Main.fontMouseText, player.statLife + "/" + player.statLifeMax2, textVector, color, 0f, new Vector2(Main.fontMouseText.MeasureString(player.statLife + "/" + player.statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f);
            }
            heartLife = 20f;
            if (Main.player[Main.myPlayer].statLifeMax > 400)
            {
                heartLife = (float)Main.player[Main.myPlayer].statLifeMax2 / 20f;
            }



        }
        private void Main_DrawInterface_Resources_Life(On.Terraria.Main.orig_DrawInterface_Resources_Life orig)
        {
            Player player = Main.LocalPlayer;
            HandleLifeBarsInit(player, out int numLifeCrystals, out int numLifeFruits, out int numHeartEmblem, out Texture2D[] hearts, out float heartLife , out int sX);
            for (int i = 1; i < numLifeCrystals + 1; i ++)
            {
                
                HandleHeartDraw(player, out float scale, out int colorVal, heartLife , i , numLifeFruits , numHeartEmblem,out int xOff , out int yOff , out int TextureIndex);
                
                int alpha = (int)((double)((float)colorVal) * 0.9);
                Vector2 pos = new Vector2((float)(500 + 26 * (i - 1) + xOff + sX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yOff + (float)(Main.heartTexture.Height / 2));
                Rectangle rect = new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height);
                if (!Main.player[Main.myPlayer].ghost)
                {
                    Main.spriteBatch.Draw(hearts[TextureIndex], pos, rect, new Color(colorVal, colorVal, colorVal, alpha), 0f, Main.heartTexture.Size() / 2f, scale, SpriteEffects.None, 0f);
                }                
            }

        }

        private bool UpdateRarity(On.Terraria.Item.orig_Prefix orig, Item item, int pre)
        {
            orig(item, pre);
            Item It = new Item();
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
            X = Main.mouseX + 10;
            Y = Main.mouseY + 10;
            if (hackedMouseX != -1 && hackedMouseY != -1)
            {
                X = hackedMouseX + 10;
                Y = hackedMouseY + 10;
            }
            if (Main.ThickMouse)
            {
                X += 6;
                Y += 6;
            }
            Vector2 vector = Main.fontMouseText.MeasureString(cursorText);
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
                if ((float)X + vector.X + 4f > (float)Main.screenWidth)
                {
                    X = (int)((float)Main.screenWidth - vector.X - 4f);
                }
                if ((float)Y + vector.Y + 4f > (float)Main.screenHeight)
                {
                    Y = (int)((float)Main.screenHeight - vector.Y - 4f);
                }
            }
        }
        private void MouseText(On.Terraria.Main.orig_MouseText orig, Main self, string cursorText, int rare, byte diff, int hackedMouseX, int hackedMouseY, int hackedScreenWidth, int hackedScreenHeight)
        {
            orig(self, cursorText, rare, diff, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight);
            Color baseColor = Color.White;
            getTextOffset(out int X, out int Y, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight, cursorText);
            if (_usesDiscoRGB.Contains(rare))
            {
                baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
            else if (_dynamicRaritiesColor.ContainsKey(rare))
            {
                baseColor = Color.Lerp(_rarities[rare], _dynamicRaritiesColor[rare], ColorCounter);
            }
            else if (_rarities.ContainsKey(rare))
            { baseColor = _rarities[rare]; }
            if (baseColor != Color.White)
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, cursorText, new Vector2(X, Y), baseColor, 0f, Vector2.Zero, Vector2.One);
        }

        private void NewText(On.Terraria.ItemText.orig_NewText orig, Item newItem, int stack, bool noStack = false, bool longText = false)
        {
            orig(newItem, stack, noStack, longText);
            int popupTextID = -1;
            for (int i = 0; i < 20; i++)
            {
                if (Main.itemText[i].active && Main.itemText[i].name == newItem.AffixName())
                { popupTextID = i; break; }
            }
            if (popupTextID != -1)
            {
                if (_usesDiscoRGB.Contains(newItem.rare))
                {
                    Main.itemText[popupTextID].color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                }
                if (_dynamicRaritiesColor.ContainsKey(newItem.rare))
                {
                    Main.itemText[popupTextID].color = Color.Lerp(_rarities[newItem.rare], _dynamicRaritiesColor[newItem.rare], ColorCounter);
                }
                else if (_rarities.ContainsKey(newItem.rare))
                { Main.itemText[popupTextID].color = _rarities[newItem.rare]; }
                if (_itemTextRarities.ContainsKey(popupTextID))
                { _itemTextRarities.Remove(popupTextID); }
                _itemTextRarities.Add(popupTextID, newItem.rare);
            }
        }
        private void TextUpdate(On.Terraria.ItemText.orig_Update orig, ItemText it, int whoAmI)
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
                { it.color = new Color((byte)Main.DiscoR, (byte)Main.DiscoG, (byte)Main.DiscoB, Main.mouseTextColor); }
            }
        }
    }
}