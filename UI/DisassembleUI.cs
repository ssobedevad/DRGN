using DRGN.Items.EngineerMaterials;
using DRGN.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;


namespace DRGN.UI
{

    class DisassembleUI : UIState
    {

        private UIPanel panel;
        private UIPanel button;
        private UIPanel[] slots = new UIPanel[24];
        private int[,] slotPos = new int[24, 2]
        {
            {8,26}, {48, 26}, {88,26 }, {128,26}, {168,26}, {208, 26},
            {8,66}, {48, 66}, {88,66 }, {128,66}, {168,66}, {208, 66},
            {8,106}, {48, 106}, {88,106 }, {128,106}, {168,106}, {208, 106},
            {8,146}, {48, 146}, {88,146 }, {128,146}, {168,146}, {208, 146},


        };


        private Player player;
        public Item[] itemslots = new Item[24];



        public override void OnInitialize()
        {



            panel = new UIPanel(); // 2
            panel.Left.Pixels = 20;
            panel.Top.Set(280, 0);
            panel.Width.Set(254, 0);
            panel.Height.Set(240, 0);

            button = new UIPanel(); // 2
            button.Left.Set(12, 0);
            button.Top.Set(186, 0);
            button.Width.Set(230, 0);
            button.Height.Set(46, 0);
            button.OnClick += OnButtonClick; // 3
            panel.Append(button);

            for (int i = 0; i < 24; i++)
            {
                slots[i] = new UIPanel(); // 2

                slots[i].Left.Set(slotPos[i, 0], 0);
                slots[i].Top.Set(slotPos[i, 1], 0);
                slots[i].Width.Set(38, 0);
                slots[i].Height.Set(38, 0);
                slots[i].OnClick += OnButtonClick; // 3
                slots[i].OnRightClick += OnRightMB;
                panel.Append(slots[i]);
            }



            Append(panel); // 4

        }




        //
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleDamageItem
            //if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<Engineer>())
            //{
            //    player.GetModPlayer<DRGNPlayer>().disassembleUI = false;



            //}
            //if (player.GetModPlayer<DRGNPlayer>().disassembleUI == true)
            //{
            //    DrawSelf(spriteBatch);
            //}
            //base.Draw(spriteBatch);
            DrawSelf(spriteBatch);

        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            


            //Main.HidePlayerCraftingMenu = true;

            Texture2D accSlot = ModContent.GetTexture("DRGN/UI/AccessorySlot");
            Texture2D disButton = ModContent.GetTexture("DRGN/UI/DisassembleButton");
            Texture2D disUI = ModContent.GetTexture("DRGN/UI/DisassembleUI");
            spriteBatch.Draw(disUI, new Vector2(panel.Left.Pixels, panel.Top.Pixels), new Rectangle(0, 0, 254, 240), Color.White);
            spriteBatch.Draw(disButton, new Vector2(panel.Left.Pixels + button.Left.Pixels, panel.Top.Pixels + button.Top.Pixels), new Rectangle(0, 0, 230, 46), Color.White);




            for (int i = 0; i < 24; i++)
            {
                spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slots[i].Left.Pixels, panel.Top.Pixels + slots[i].Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
                if (itemslots[i] != null)
                {
                    spriteBatch.Draw(Main.itemTexture[itemslots[i].type], new Vector2(panel.Left.Pixels + slots[i].Left.Pixels + (38 - Main.itemTexture[itemslots[i].type].Width) / 2, panel.Top.Pixels + slots[i].Top.Pixels + (38 - Main.itemTexture[itemslots[i].type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemslots[i].type].Width), (Main.itemTexture[itemslots[i].type].Height)), Color.White);
                }
            }


        }
        public int[] ItemQualityCheck(Item item)
        {
            int[] cogTypes = new int[9] { ModContent.ItemType<WoodenCog>(), ModContent.ItemType<Cog>(), ModContent.ItemType<GoldenCog>(), ModContent.ItemType<IcyCog>(), ModContent.ItemType<PlantenCog>(), ModContent.ItemType<LunarCog>(), ModContent.ItemType<InsectiumCog>(), ModContent.ItemType<FlariumCog>(), ModContent.ItemType<VoidCog>() };
            int[] pipeTypes = new int[9] { ModContent.ItemType<WoodenDowel>(), ModContent.ItemType<MetalPipe>(), ModContent.ItemType<GoldenPipe>(), ModContent.ItemType<IcyPipe>(), ModContent.ItemType<PlantenPipe>(), ModContent.ItemType<LunarPipe>(), ModContent.ItemType<InsectiumPipe>(), ModContent.ItemType<FlariumPipe>(), ModContent.ItemType<VoidPipe>() };
            int[] plateTypes = new int[9] { ModContent.ItemType<WoodenPlate>(), ModContent.ItemType<MetalPlate>(), ModContent.ItemType<GoldenPlate>(), ModContent.ItemType<IcyPlate>(), ModContent.ItemType<PlantenPlate>(), ModContent.ItemType<LunarPlate>(), ModContent.ItemType<InsectiumPlate>(), ModContent.ItemType<FlariumPlate>(), ModContent.ItemType<VoidPlate>() };
            int[] screwTypes = new int[9] { ModContent.ItemType<WoodenFixing>(), ModContent.ItemType<Screw>(), ModContent.ItemType<GoldenScrew>(), ModContent.ItemType<IcyScrew>(), ModContent.ItemType<PlantenScrew>(), ModContent.ItemType<LunarScrew>(), ModContent.ItemType<InsectiumScrew>(), ModContent.ItemType<FlariumScrew>(), ModContent.ItemType<VoidScrew>() };
            int type = 4;
            if (item.summon || item.thrown) { type = 1; }
            else if (item.melee) { type = 2; }
            else if (item.ranged) { type = 3; }
            int stackNum = 5;
            if (item.value > 1000000) { stackNum += Main.rand.Next(30, 40); }
            else if (item.value > 500000) { stackNum += Main.rand.Next(23, 34); }
            else if (item.value > 250000) { stackNum += Main.rand.Next(20, 28); }
            else if (item.value > 100000) { stackNum += Main.rand.Next(17, 24); }
            else if (item.value > 50000) { stackNum += Main.rand.Next(15, 20); }
            else if (item.value > 25000) { stackNum += Main.rand.Next(12, 16); }
            else if (item.value > 10000) { stackNum += Main.rand.Next(10, 14); }
            else if (item.value > 5000) { stackNum += Main.rand.Next(8, 12); }
            else if (item.value > 2500) { stackNum += Main.rand.Next(6, 10); }
            else if (item.value > 1000) { stackNum += Main.rand.Next(4, 8); }
            else if (item.value > 500) { stackNum += Main.rand.Next(2, 6); }
            int rarityNum = 0;
            if (item.rare == ItemRarityID.Purple) {stackNum += 30; }
            else if (item.rare >= ItemRarityID.Cyan && NPC.downedMoonlord) { rarityNum = 5; stackNum -= 8; }
            else if (item.rare >= ItemRarityID.Lime && NPC.downedPlantBoss) { rarityNum = 4; stackNum -= 6; }
            else if (item.rare >= ItemRarityID.LightRed && DRGNModWorld.downedIceFish) { rarityNum = 3; stackNum -= 4; }
            else if (item.rare >= ItemRarityID.Orange && NPC.downedBoss3) { rarityNum = 2; stackNum -= 2; }
            else if (item.rare >= ItemRarityID.Blue && NPC.downedBoss1) { rarityNum = 1; }
            if (DRGNModWorld.downedDragonFly) { rarityNum += 1; }
            if (DRGNModWorld.downedDragon) { rarityNum += 1; }
            if (DRGNModWorld.downedVoidSnake) { rarityNum += 1; }
            int itemType = (type == 4) ? cogTypes[rarityNum] : (type == 3) ? pipeTypes[rarityNum] : (type == 2) ? plateTypes[rarityNum] : screwTypes[rarityNum];
            return new int[3] { itemType, stackNum, rarityNum };
        }

        public void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            
                var mouseItem = Main.mouseItem.Clone();
               

                if (listeningElement != button && mouseItem != null && mouseItem.damage > 0 && mouseItem.maxStack == 1)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        if (listeningElement == slots[i])
                        {
                            if (itemslots[i] != null) { player.QuickSpawnItem(itemslots[i]); }

                            itemslots[i] = mouseItem;
                            Main.mouseItem.SetDefaults();
                            player.inventory[58].SetDefaults();
                        }
                    }
                }
                else if (listeningElement == button)
                {
                    
                    int highestRarity = 1;
                    bool isFull = true;
                    string[] possibleParts = new string[6] { "GunScope", "GunBarrel", "GunMag", "GunBody", "GunChamber", "GunGrip" };
                    for (int i = 0; i < 24; i++)
                    {
                        if (itemslots[i] != null)
                        {
                            int[] itemType = ItemQualityCheck(itemslots[i]);
                            player.QuickSpawnItem(itemType[0], itemType[1]);
                            itemslots[i].SetDefaults(0);
                            if (itemType[2] > highestRarity) { highestRarity = itemType[2]; }
                        }
                        else { isFull = false; }
                    }
                    if (isFull) { string rand = Main.rand.Next(possibleParts); int randTier = Main.rand.Next(1, highestRarity); player.QuickSpawnItem(ModContent.GetInstance<DRGN>().ItemType(rand + randTier)); }
                }


            

        }
        public void OnRightMB(UIMouseEvent evt, UIElement listeningElement)
        {
            
                Item mouseItem = Main.mouseItem.Clone();

                for (int i = 0; i < 24; i++)
                {
                    if (listeningElement == slots[i])
                    {
                        if (itemslots[i] != null) { player.QuickSpawnItem(itemslots[i]); }
                        if (mouseItem != null)
                        {
                            itemslots[i] = mouseItem;
                            Main.mouseItem.SetDefaults();
                            player.inventory[58].SetDefaults();
                        }
                    }
                }
            


        }
        public override void OnDeactivate()
        {
            for (int i = 0; i < 24; i++)
            {

                if (itemslots[i] != null) { player.QuickSpawnItem(itemslots[i]); itemslots[i] = null; }


            }
        }

        public override void Update(GameTime gameTime)
        {
            player = Main.LocalPlayer;
            base.Update(gameTime);
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[player.talkNPC].type != ModContent.NPCType<Engineer>())
            {
                // When that happens, we can set the state of our UserInterface to null, thereby closing this UIState. This will trigger OnDeactivate above.
               ModContent.GetInstance<DRGN>()._DisUI.SetState(null);
                
            }


        }
    }
}