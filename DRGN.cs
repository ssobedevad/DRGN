using DRGN.Items;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using DRGN.Rarities;
using DRGN.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
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


                bossChecklist.Call("AddBoss", 2.5f, NPCType("DesertSerpent"), this, "Desert Serpent", (Func<bool>)(() => DRGNModWorld.downedSerpent), ItemType("SnakeHead"), new List<int> { }, new List<int> { ModContent.ItemType<SnakeScale>(), ModContent.ItemType<ToxicFang>(), ModContent.ItemType<SnakeHeadThrown>(), ModContent.ItemType<SnakeSlayer>(), ModContent.ItemType<SnakeStaff>(), ModContent.ItemType<SnakeWhip>(), ItemID.Cactus }, "Use a [i:" + ItemType("SnakeHead") + "] in the Day.");
                bossChecklist.Call("AddEvent", 1f, NPCType("Ant"), this, "The Swarm Pre QA", (Func<bool>)(() => DRGNModWorld.SwarmKilled), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");

                bossChecklist.Call("AddBoss", 4.5f, NPCType("ToxicFrog"), this, "Toxic Frog", (Func<bool>)(() => DRGNModWorld.downedToxicFrog), ItemType("FrogClaw"), new List<int> { }, new List<int> { ModContent.ItemType<ToxicFlesh>(), ModContent.ItemType<Lobber>(), ModContent.ItemType<ToxicRifle>(), ModContent.ItemType<ThrowingTongue>(), ModContent.ItemType<ThePlague>(), ModContent.ItemType<TongueSword>(), ModContent.ItemType<TongueWhip>(), ModContent.ItemType<FrogStaff>(), ModContent.ItemType<EarthenOre>() }, "Use a [i:" + ItemType("FrogClaw") + "] in the Day on the surface Jungle.");
                bossChecklist.Call("AddBoss", 6f, NPCType("QueenAnt"), this, "Queen Ant", (Func<bool>)(() => DRGNModWorld.downedQueenAnt), ItemType("AntsCall"), new List<int> { }, new List<int> { ModContent.ItemType<AntEssence>(), ModContent.ItemType<AntJaw>(), ModContent.ItemType<AntBiter>(), ModContent.ItemType<AntJaws>(), ModContent.ItemType<AntSlicer>(), ModContent.ItemType<ElementalAntWhip>(), ModContent.ItemType<AntStaff>() }, "Use a [i:" + ItemType("AntsCall") + "] in the ant nest.");
                bossChecklist.Call("AddEvent", 6.1f, NPCType("ElectricAnt"), this, "The Swarm Post Queen Ant", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostQA), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 6.5f, NPCType("IceFish"), this, "Ice Fish", (Func<bool>)(() => DRGNModWorld.downedIceFish), ItemType("FrozenFishFood"), new List<int> { }, new List<int> { ModContent.ItemType<GlacialShard>(), ModContent.ItemType<IceChains>(), ModContent.ItemType<IceChainWhip>(), ModContent.ItemType<IceSpear>(), ModContent.ItemType<IcicleBlaster>(), ModContent.ItemType<IcicleSlicer>(), ModContent.ItemType<ArcticHuntingRifle>(), ModContent.ItemType<FishStaff>(), ModContent.ItemType<GlacialOre>() }, "Use a [i:" + ItemType("FrozenFishFood") + "] in the Ice Biome.");
                bossChecklist.Call("AddBoss", 9.5f, NPCType("TheVirus"), this, "The Virus", (Func<bool>)(() => DRGNModWorld.downedTheVirus), ItemType("UnstableBatteries"), new List<int> { }, new List<int> { ModContent.ItemType<TechnoOre>(), ModContent.ItemType<TechnoWhip>(), ModContent.ItemType<TechnoSpear>(), ModContent.ItemType<TechnoSlicer>(), ModContent.ItemType<TechnoShuriken>(), ModContent.ItemType<SourceCode>(), ModContent.ItemType<SourceThrow>(), ModContent.ItemType<SecurityBreach>(), ModContent.ItemType<BinaryStaff>() }, "Use a [i:" + ItemType("UnstableBatteries") + "] anywhere and anytime.");
                bossChecklist.Call("AddEvent", 8f, NPCType("FlyingAnt"), this, "The Swarm Post Mechanical Boss", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMechBoss), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 11.5f, NPCType("Cloud"), this, "Cloud", (Func<bool>)(() => DRGNModWorld.downedCloud), ItemType("CelestialSundial"), new List<int> { }, new List<int> { ModContent.ItemType<CloudStaff>(), ModContent.ItemType<ElectroStaff>(), ModContent.ItemType<SunBook>(), ModContent.ItemType<CosmoOre>(), ModContent.ItemType<CosmoBlade>(), ModContent.ItemType<CosmoSpear>(), ModContent.ItemType<CosmoWhip>() }, "Use a [i:" + ItemType("CelestialSundial") + "] in Space.");
                bossChecklist.Call("AddEvent", 14.4f, NPCType("DragonFlyMini"), this, "The Swarm Post Moonlord", (Func<bool>)(() => DRGNModWorld.SwarmKilledPostMoonlord), ItemType("TheSwarm"), new List<int> { }, new List<int> { ItemType("AntCrawlerScale"), ItemType("DragonFlyWing"), ItemType("DragonFliesCall"), ItemType("ElectricAntJaw"), ItemType("FireAntJaw"), ItemType("AntJaw"), ItemType("AntWing"), ItemType("DragonFliesCall") }, "Use a [i:" + ItemType("TheSwarm") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 14.5f, NPCType("DragonFly"), this, "DragonFly", (Func<bool>)(() => DRGNModWorld.downedDragonFly), ItemType("DragonFliesCall"), new List<int> { }, new List<int> { ModContent.ItemType<DragonFlyDust>(), ModContent.ItemType<DragonFlyWing>(), ModContent.ItemType<GalacticScale>(), ModContent.ItemType<GalacticEssence>(), ModContent.ItemType<TheDragonFly>(), ModContent.ItemType<DragonFlySlicer>(), ModContent.ItemType<DragonFlyStaff>(), ModContent.ItemType<DragonFlyWhip>() }, "Use a [i:" + ItemType("DragonFliesCall") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 15f, NPCType("FireDragon"), this, "Fire Dragon", (Func<bool>)(() => DRGNModWorld.downedDragon), ItemType("FireDragonEgg"), new List<int> { }, new List<int> { ModContent.ItemType<SolariumOre>(), ModContent.ItemType<DragonScale>(), ModContent.ItemType<DragonPick>(), ModContent.ItemType<DragonStaff>(), ModContent.ItemType<DragonSpear>(), ModContent.ItemType<MagmaticHuntingRifle>(), ModContent.ItemType<DragonWhip>() }, "Use a [i:" + ItemType("FireDragonEgg") + "] in the Dragon's Lair.");
                bossChecklist.Call("AddBoss", 16f, NPCType("VoidSnakeHead"), this, "Void Snake", (Func<bool>)(() => DRGNModWorld.downedVoidSnake), ItemType("VoidFlesh"), new List<int> { }, new List<int> { ModContent.ItemType<VoidOre>(), ModContent.ItemType<VoidBar>(), ModContent.ItemType<VoidSpear>(), ModContent.ItemType<VoidScythe>(), ModContent.ItemType<VoidSoul>(), ModContent.ItemType<VoidSnakeStaff>(), ModContent.ItemType<VoidSnakeWhip>() }, "Use a [i:" + ItemType("VoidFlesh") + "] anywhere and anytime.");
                bossChecklist.Call("AddBoss", 17f, NPCType("GalacticGuardian"), this, "Galactic Guardian", (Func<bool>)(() => DRGNModWorld.downedGalacticGuardian), ItemType("GalacticTotem"), new List<int> { }, new List<int> { ModContent.ItemType<GalacticaOre>(), ModContent.ItemType<GalacticaBar>(), ModContent.ItemType<GalacticOrbs>(), ModContent.ItemType<GalacticShower>(), ModContent.ItemType<GalacticStaff>(), ModContent.ItemType<GalacticWhip>(), ModContent.ItemType<GalacticYoyo>(), ModContent.ItemType<GalactiteChain>(), ModContent.ItemType<GalactiteRifle>(), ModContent.ItemType<GalactiteThrowingAxe>(), ModContent.ItemType<GalaxySlicer>(), ModContent.ItemType<GalactiteBow>() }, "Use a [i:" + ItemType("GalacticTotem") + "] anywhere and anytime.");
            }
        }
        public override void PostUpdateEverything()
        {
            AnimatedColor.Update();
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
            LoadItemRare();
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




        private Color darkBlue = new Color(30, 43, 82);
        private Color fieryOrange = new Color(177, 38, 3);
        private Color voidPurple = new Color(96, 34, 186);

        private void LoadItemRare()
        {
            On.Terraria.GameContent.UI.ItemRarity.Initialize += ItemRarity_Initialize;
            On.Terraria.GameContent.UI.ItemRarity.GetColor += ItemRarity_GetColor;
            On.Terraria.Main.MouseText += Main_MouseText;
            On.Terraria.ItemText.NewText += ItemText_NewText;
            On.Terraria.ItemText.Update += ItemText_Update;

            ItemRarity.Initialize();
        }


        public static Dictionary<int, Color> _rarities = new Dictionary<int, Color>();
        private void ItemRarity_Initialize(On.Terraria.GameContent.UI.ItemRarity.orig_Initialize orig)
        {
            _rarities.Clear();

            _rarities.Add(-11, RarityAmber);
            _rarities.Add(-1, RarityTrash);
            _rarities.Add(1, RarityBlue);
            _rarities.Add(2, RarityGreen);
            _rarities.Add(3, RarityOrange);
            _rarities.Add(4, RarityRed);
            _rarities.Add(5, RarityPink);
            _rarities.Add(6, RarityPurple);
            _rarities.Add(7, RarityLime);
            _rarities.Add(8, RarityYellow);
            _rarities.Add(9, RarityCyan);
            _rarities.Add(10, new Color(255, 40, 100));
            _rarities.Add(11, new Color(180, 40, 255));
            _rarities.Add(ItemRarities.DarkBlue, darkBlue);
            _rarities.Add(ItemRarities.FieryOrange, fieryOrange);
            _rarities.Add(ItemRarities.VoidPurple, voidPurple);


            Logger.Info("Rarities dictionary: " + _rarities);
        }

        private Color ItemRarity_GetColor(On.Terraria.GameContent.UI.ItemRarity.orig_GetColor orig, int rarity)
        {

            if (rarity >= ItemRarities.Mental && rarity < -13)
            { return new AnimatedColor(Color.Blue, Color.Yellow).GetColor(); }
            if (rarity >= ItemRarities.GalacticRainbow)
            { return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB); }
            Color result = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            if (_rarities.ContainsKey(rarity))
            {
                return _rarities[rarity];
            }
            return result;
        }

        private void Main_MouseText(On.Terraria.Main.orig_MouseText orig, Main self, string cursorText, int rare, byte diff, int hackedMouseX, int hackedMouseY, int hackedScreenWidth, int hackedScreenHeight)
        {
            orig(self, cursorText, rare, diff, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight);

            Color baseColor = Color.White;
           
            int X = Main.mouseX + 10;
            int Y = Main.mouseY + 10;

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

            float num = (float)(int)Main.mouseTextColor / 255f;


            if (rare >= ItemRarities.GalacticRainbow && rare < -13)
            {
                baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);

            }
            else if (rare >= ItemRarities.Mental && rare < ItemRarities.DarkBlue)
            {
                baseColor = new AnimatedColor(Color.Blue, Color.Yellow).GetColor() * num;
            }
            
            else if (_rarities.ContainsKey(rare))
            { baseColor = _rarities[rare]; }

            if (baseColor != Color.White)
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, cursorText, new Vector2(X, Y), baseColor, 0f, Vector2.Zero, Vector2.One);
        }

        private void ItemText_NewText(On.Terraria.ItemText.orig_NewText orig, Item newItem, int stack, bool noStack, bool longText)
        {
            bool flag = newItem.type >= 71 && newItem.type <= 74;
            if (!Main.showItemText || newItem.Name == null || !newItem.active || Main.netMode == 2)
            {
                return;
            }
            for (int k = 0; k < 20; k++)
            {
                if ((!Main.itemText[k].active || (!(Main.itemText[k].name == newItem.AffixName()) && (!flag || !Main.itemText[k].coinText)) || Main.itemText[k].NoStack) | noStack)
                {
                    continue;
                }
                string text3 = newItem.Name + " (" + (Main.itemText[k].stack + stack).ToString() + ")";
                string text2 = newItem.Name;
                if (Main.itemText[k].stack > 1)
                {
                    text2 = text2 + " (" + Main.itemText[k].stack.ToString() + ")";
                }
                Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
                vector2 = Main.fontMouseText.MeasureString(text3);
                if (Main.itemText[k].lifeTime < 0)
                {
                    Main.itemText[k].scale = 1f;
                }
                if (Main.itemText[k].lifeTime < 60)
                {
                    Main.itemText[k].lifeTime = 60;
                }
                if (flag && Main.itemText[k].coinText)
                {
                    orig(newItem, stack, noStack, longText);
                }
                Main.itemText[k].stack += stack;
                Main.itemText[k].scale = 0f;
                Main.itemText[k].rotation = 0f;
                Main.itemText[k].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
                Main.itemText[k].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
                Main.itemText[k].velocity.Y = -7f;
                if (Main.itemText[k].coinText)
                {
                    Main.itemText[k].stack = 1;
                }
                return;
            }
            int num4 = -1;
            for (int j = 0; j < 20; j++)
            {
                if (!Main.itemText[j].active)
                {
                    num4 = j;
                    break;
                }
            }
            if (num4 == -1)
            {
                double num3 = Main.bottomWorld;
                for (int i = 0; i < 20; i++)
                {
                    if (num3 > (double)Main.itemText[i].position.Y)
                    {
                        num4 = i;
                        num3 = Main.itemText[i].position.Y;
                    }
                }
            }
            if (num4 < 0)
            {
                return;
            }
            string text4 = newItem.AffixName();
            if (stack > 1)
            {
                text4 = text4 + " (" + stack.ToString() + ")";
            }
            Vector2 vector3 = Main.fontMouseText.MeasureString(text4);
            Main.itemText[num4].alpha = 1f;
            Main.itemText[num4].alphaDir = -1;
            Main.itemText[num4].active = true;
            Main.itemText[num4].scale = 0f;
            Main.itemText[num4].NoStack = noStack;
            Main.itemText[num4].rotation = 0f;
            Main.itemText[num4].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector3.X * 0.5f;
            Main.itemText[num4].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector3.Y * 0.5f;
            Main.itemText[num4].color = Color.White;
           
            if (newItem.rare >= ItemRarities.GalacticRainbow && newItem.rare < -13)
            {
                Main.itemText[num4].color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                Main.itemText[num4].expert = true;
            }
            else if (newItem.rare == ItemRarities.Mental && newItem.rare < ItemRarities.DarkBlue)
            {
                Main.itemText[num4].color = new AnimatedColor(Color.Blue, Color.Yellow).GetColor();
            }
            else if (_rarities.ContainsKey(newItem.rare))
            { Main.itemText[num4].color = _rarities[newItem.rare]; }
            
            
            

            Main.itemText[num4].expert = newItem.expert;
            Main.itemText[num4].name = newItem.AffixName();
            Main.itemText[num4].stack = stack;
            Main.itemText[num4].velocity.Y = -7f;
            Main.itemText[num4].lifeTime = 60;
            if (longText)
            {
                Main.itemText[num4].lifeTime *= 5;
            }
            Main.itemText[num4].coinValue = 0;
            Main.itemText[num4].coinText = (newItem.type >= ItemID.CopperCoin && newItem.type <= ItemID.PlatinumCoin);
            if (!Main.itemText[num4].coinText)
            {
                return;
            }
            if (newItem.type >= ItemID.CopperCoin && newItem.type <= ItemID.PlatinumCoin)
            {
                orig(newItem, stack, noStack, longText);
            }
        }

        private void ItemText_Update(On.Terraria.ItemText.orig_Update orig, ItemText self, int whoAmI)
        {
            if (!self.active)
            {
                return;
            }
            float targetScale = ItemText.TargetScale;
            self.alpha += (float)self.alphaDir * 0.01f;
            if ((double)self.alpha <= 0.7)
            {
                self.alpha = 0.7f;
                self.alphaDir = 1;
            }
            if (self.alpha >= 1f)
            {
                self.alpha = 1f;
                self.alphaDir = -1;
            }
            if (self.expert && self.expert)
            {
                self.color = new Color((byte)Main.DiscoR, (byte)Main.DiscoG, (byte)Main.DiscoB, Main.mouseTextColor);
            }
            

            bool flag = false;
            string text3 = self.name;
            if (self.stack > 1)
            {
                text3 = text3 + " (" + self.stack.ToString() + ")";
            }
            Vector2 vector = Main.fontMouseText.MeasureString(text3);
            vector *= self.scale;
            vector.Y *= 0.8f;
            Rectangle rectangle = new Rectangle((int)(self.position.X - vector.X / 2f), (int)(self.position.Y - vector.Y / 2f), (int)vector.X, (int)vector.Y);
            for (int i = 0; i < 20; i++)
            {
                if (!Main.itemText[i].active || i == whoAmI)
                {
                    continue;
                }
                string text2 = Main.itemText[i].name;
                if (Main.itemText[i].stack > 1)
                {
                    text2 = text2 + " (" + Main.itemText[i].stack.ToString() + ")";
                }
                Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
                vector2 *= Main.itemText[i].scale;
                vector2.Y *= 0.8f;
                Rectangle value = new Rectangle((int)(Main.itemText[i].position.X - vector2.X / 2f), (int)(Main.itemText[i].position.Y - vector2.Y / 2f), (int)vector2.X, (int)vector2.Y);
                if (rectangle.Intersects(value) && (self.position.Y < Main.itemText[i].position.Y || (self.position.Y == Main.itemText[i].position.Y && whoAmI < i)))
                {
                    flag = true;
                    int num = ItemText.numActive;
                    if (num > 3)
                    {
                        num = 3;
                    }
                    Main.itemText[i].lifeTime = ItemText.activeTime + 15 * num;
                    self.lifeTime = ItemText.activeTime + 15 * num;
                }
            }
            if (!flag)
            {
                self.velocity.Y *= 0.86f;
                if (self.scale == targetScale)
                {
                    self.velocity.Y *= 0.4f;
                }
            }
            else if (self.velocity.Y > -6f)
            {
                self.velocity.Y -= 0.2f;
            }
            else
            {
                self.velocity.Y *= 0.86f;
            }
            self.velocity.X *= 0.93f;
            self.position += self.velocity;
            self.lifeTime--;
            if (self.lifeTime <= 0)
            {
                self.scale -= 0.03f * targetScale;
                if ((double)self.scale < 0.1 * (double)targetScale)
                {
                    self.active = false;
                }
                self.lifeTime = 0;
                return;
            }
            if (self.scale < targetScale)
            {
                self.scale += 0.1f * targetScale;
            }
            if (self.scale > targetScale)
            {
                self.scale = targetScale;
            }
        }

    }


}