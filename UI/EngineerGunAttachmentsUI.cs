using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.UI.Elements;
using static DRGN.DRGNPlayer;
using System;
using System.Collections.Generic;
using DRGN.Items.EngineerClass.Attachments;


namespace DRGN.UI
{
   
    class EngineerGun : UIState
    {
        
        private UIPanel panel;
        private UIPanel slot1,slot2,slot3,slot4,slot5,slot6;
        private bool killItem;
        
        private Player player;
        private Item itemSlot1,itemSlot2,itemSlot3,itemSlot4,itemSlot5,itemSlot6;
        private int itemSlot1Tier, itemSlot2Tier, itemSlot3Tier, itemSlot4Tier, itemSlot5Tier, itemSlot6Tier;


        public override void OnInitialize()
        {
            
           
            
            panel = new UIPanel(); // 2
            panel.Left.Pixels = 585;
            panel.Top.Set(26, 0);
            panel.Width.Set(280, 0);
            panel.Height.Set(240, 0);
            
            slot1 = new UIPanel(); // 2

            slot1.Left.Set(30, 0);
            slot1.Top.Set(118, 0);
            slot1.Width.Set(38, 0);
            slot1.Height.Set(38, 0);
            slot1.OnClick += OnButtonClick; // 3
            slot1.OnRightClick += OnRightMB;
            panel.Append(slot1);

            slot2 = new UIPanel(); // 2
            slot2.Left.Set(220, 0);
            slot2.Top.Set(124, 0);
            slot2.Width.Set(38, 0);
            slot2.Height.Set(38, 0);
            slot2.OnClick += OnButtonClick; // 3
            slot2.OnRightClick += OnRightMB;
            panel.Append(slot2);

            slot3 = new UIPanel(); // 2
            slot3.Left.Set(110, 0);
            slot3.Top.Set(22, 0);
            slot3.Width.Set(38, 0);
            slot3.Height.Set(38, 0);
            slot3.OnClick += OnButtonClick; // 3
            slot3.OnRightClick += OnRightMB;
            panel.Append(slot3);

            slot4 = new UIPanel(); // 2
            slot4.Left.Set(74, 0);
            slot4.Top.Set(168, 0);
            slot4.Width.Set(38, 0);
            slot4.Height.Set(38, 0);
            slot4.OnClick += OnButtonClick; // 3
            slot4.OnRightClick += OnRightMB;
            panel.Append(slot4);

            slot5 = new UIPanel();
            slot5.Left.Set(118, 0);
            slot5.Top.Set(168, 0);
            slot5.Width.Set(38, 0);
            slot5.Height.Set(38, 0);
            slot5.OnClick += OnButtonClick; // 3
            slot5.OnRightClick += OnRightMB;
            panel.Append(slot5);

            slot6 = new UIPanel();
            slot6.Left.Set(10, 0);
            slot6.Top.Set(66, 0);
            slot6.Width.Set(38, 0);
            slot6.Height.Set(38, 0);
            slot6.OnClick += OnButtonClick; // 3
            slot6.OnRightClick += OnRightMB;
            panel.Append(slot6);


            Append(panel); // 4

        }




        //
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleDamageItem
            if (!(Main.playerInventory && player.GetModPlayer<DRGNPlayer>().EngineerWeapon))
            {
                return;
            }
            DrawSelf(spriteBatch);
            //base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
            
            int gunStartX = (int)(panel.Left.Pixels + 79);
            int gunStartY = (int)(panel.Top.Pixels + 84);

            Texture2D bodyTexture = ModContent.GetTexture("DRGN/UI/GunBodies/GunBody" + itemSlot1Tier.ToString());
            Texture2D barrelTexture = ModContent.GetTexture("DRGN/UI/GunBarrels/GunBarrel"+itemSlot2Tier.ToString());
            Texture2D scopeTexture = ModContent.GetTexture("DRGN/UI/GunScopes/GunScope" + itemSlot3Tier.ToString());
            Texture2D gripTexture = ModContent.GetTexture("DRGN/UI/GunGrips/GunGrip" + itemSlot4Tier.ToString());
            Texture2D magTexture = ModContent.GetTexture("DRGN/UI/GunMags/GunMag" + itemSlot5Tier.ToString());
            Texture2D chamberTexture = ModContent.GetTexture("DRGN/UI/GunChambers/GunChamber" + itemSlot6Tier.ToString());

            Texture2D accSlot = ModContent.GetTexture("DRGN/UI/AccessorySlot");
            Texture2D panelSlot = ModContent.GetTexture("DRGN/UI/PanelSlot");
            spriteBatch.Draw(panelSlot, new Vector2(panel.Left.Pixels, panel.Top.Pixels), new Rectangle(0, 0, 280, 240), Color.White);

            spriteBatch.Draw(bodyTexture, new Vector2(gunStartX, gunStartY + 20), new Rectangle(0, 0, bodyTexture.Width, bodyTexture.Height), Color.White);
            spriteBatch.Draw(barrelTexture, new Vector2(gunStartX + 74, gunStartY + 24), new Rectangle(0, 0, barrelTexture.Width, barrelTexture.Height), Color.White);
            if (itemSlot3Tier <= 4)
            {

                spriteBatch.Draw(scopeTexture, new Vector2(gunStartX + 2, gunStartY), new Rectangle(0, 0, scopeTexture.Width, scopeTexture.Height), Color.White);
            } 
            else{ spriteBatch.Draw(scopeTexture, new Vector2(gunStartX + 2, gunStartY-6), new Rectangle(0, 0, scopeTexture.Width, scopeTexture.Height), Color.White);
             }
            spriteBatch.Draw(gripTexture, new Vector2(gunStartX + 8, gunStartY + 52), new Rectangle(0, 0, gripTexture.Width, gripTexture.Height), Color.White);
            spriteBatch.Draw(magTexture, new Vector2(gunStartX + 40, gunStartY + 52), new Rectangle(0, 0, magTexture.Width, magTexture.Height), Color.White);
            spriteBatch.Draw(chamberTexture, new Vector2(gunStartX + 14, gunStartY + 28), new Rectangle(0, 0, chamberTexture.Width, chamberTexture.Height), Color.White);

            

            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot1.Left.Pixels, panel.Top.Pixels+slot1.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot1 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot1.type], new Vector2(panel.Left.Pixels + slot1.Left.Pixels + (38 - Main.itemTexture[itemSlot1.type].Width) / 2, panel.Top.Pixels + slot1.Top.Pixels + (38 - Main.itemTexture[itemSlot1.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot1.type].Width), (Main.itemTexture[itemSlot1.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot2.Left.Pixels, panel.Top.Pixels + slot2.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot2 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot2.type], new Vector2(panel.Left.Pixels + slot2.Left.Pixels + (38 - Main.itemTexture[itemSlot2.type].Width) / 2, panel.Top.Pixels + slot2.Top.Pixels + (38 - Main.itemTexture[itemSlot2.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot2.type].Width), (Main.itemTexture[itemSlot2.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot3.Left.Pixels, panel.Top.Pixels + slot3.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot3 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot3.type], new Vector2(panel.Left.Pixels + slot3.Left.Pixels + (38 - Main.itemTexture[itemSlot3.type].Width) / 2, panel.Top.Pixels + slot3.Top.Pixels + (38 - Main.itemTexture[itemSlot3.type].Height) / 2),  new Rectangle(0, 0, (Main.itemTexture[itemSlot3.type].Width), (Main.itemTexture[itemSlot3.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot4.Left.Pixels, panel.Top.Pixels + slot4.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot4 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot4.type], new Vector2(panel.Left.Pixels + slot4.Left.Pixels + (38 - Main.itemTexture[itemSlot4.type].Width) / 2, panel.Top.Pixels + slot4.Top.Pixels + (38 - Main.itemTexture[itemSlot4.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot4.type].Width), (Main.itemTexture[itemSlot4.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot5.Left.Pixels, panel.Top.Pixels + slot5.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot5 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot5.type], new Vector2(panel.Left.Pixels + slot5.Left.Pixels + (38 - Main.itemTexture[itemSlot5.type].Width) / 2, panel.Top.Pixels + slot5.Top.Pixels + (38 - Main.itemTexture[itemSlot5.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot5.type].Width), (Main.itemTexture[itemSlot5.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(panel.Left.Pixels + slot6.Left.Pixels, panel.Top.Pixels + slot6.Top.Pixels), new Rectangle(0, 0, 38, 38), Color.White);
            if (itemSlot6 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot6.type], new Vector2(panel.Left.Pixels + slot6.Left.Pixels + (38 - Main.itemTexture[itemSlot6.type].Width) / 2, panel.Top.Pixels + slot6.Top.Pixels + (38 - Main.itemTexture[itemSlot6.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot6.type].Width), (Main.itemTexture[itemSlot6.type].Height)), Color.White);
            }

        }


        public void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            killItem = true;
            var mouseItemClone = Main.mouseItem.Clone();
            if (mouseItemClone.modItem == null) { killItem = false; return; }
            ModItem mouseItemObject = mouseItemClone.modItem;
            
           
            ModItem MI = mouseItemObject as ModItem;
            EngineerAttachments EA = MI as EngineerAttachments;
            
           if (EA == null) { killItem = false; return; }
            


            if (listeningElement == slot1 && Main.mouseItem.type != ItemID.None && EA.isGunBody)
            {  
            if (itemSlot1 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gunBodyType); }        
                player.GetModPlayer<DRGNPlayer>().gunBodyType = mouseItemClone;         
                itemSlot1 = player.GetModPlayer<DRGNPlayer>().gunBodyType;
                itemSlot1Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().gunBodyTier = EA.AttachmentTier;


            }
            else if (listeningElement == slot2 && Main.mouseItem.type != ItemID.None && EA.isGunBarrel)
            {
                if (itemSlot2 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().barrelType); }
                player.GetModPlayer<DRGNPlayer>().barrelType = mouseItemClone;
                itemSlot2 = player.GetModPlayer<DRGNPlayer>().barrelType;
                itemSlot2Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().barrelTier = EA.AttachmentTier;
            }
            else if(listeningElement == slot3 && Main.mouseItem.type != ItemID.None && EA.isGunScope)
            {
                if (itemSlot3 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().scopeType); }
                player.GetModPlayer<DRGNPlayer>().scopeType = mouseItemClone;
                itemSlot3 = player.GetModPlayer<DRGNPlayer>().scopeType;
                itemSlot3Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().scopeTier = EA.AttachmentTier;
            }
            else if(listeningElement == slot4 && Main.mouseItem.type != ItemID.None && EA.isGunGrip)
            {
                if (itemSlot4 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gripType); }
                player.GetModPlayer<DRGNPlayer>().gripType = mouseItemClone;
                itemSlot4 = player.GetModPlayer<DRGNPlayer>().gripType;
                itemSlot4Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().gripTier = EA.AttachmentTier;
            }
            else if(listeningElement == slot5 && Main.mouseItem.type != ItemID.None && EA.isGunMag)
            {
                if (itemSlot5 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().magType); }
                player.GetModPlayer<DRGNPlayer>().magType = mouseItemClone;
                itemSlot5 = player.GetModPlayer<DRGNPlayer>().magType;
                itemSlot5Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().magTier = EA.AttachmentTier;
            }
            else if(listeningElement == slot6 && Main.mouseItem.type != ItemID.None && EA.isGunChamber)
            {
                if (itemSlot6 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().chamberType); }
                player.GetModPlayer<DRGNPlayer>().chamberType = mouseItemClone;
                itemSlot6 = player.GetModPlayer<DRGNPlayer>().chamberType;
                itemSlot6Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().chamberTier = EA.AttachmentTier;
            }
            else { killItem = false; }
            
        }
        
        private void OnRightMB(UIMouseEvent evt, UIElement listeningElement)
        {

            
            
            if (!(Main.playerInventory))
            {
                return;
            }
            if (listeningElement == slot1)
            {
                itemSlot1 = player.GetModPlayer<DRGNPlayer>().gunBodyType;
                if (itemSlot1 != null)
                {
                   
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gunBodyType);
                    player.GetModPlayer<DRGNPlayer>().gunBodyType = null;
                    itemSlot1 = player.GetModPlayer<DRGNPlayer>().gunBodyType;
                    itemSlot1Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().gunBodyTier = 0;
                }
            }
            else if (listeningElement == slot2)
            {
                itemSlot2 = player.GetModPlayer<DRGNPlayer>().barrelType;
                if (itemSlot2 != null)
                {
                   
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().barrelType);
                    player.GetModPlayer<DRGNPlayer>().barrelType = null;
                    itemSlot2 = player.GetModPlayer<DRGNPlayer>().barrelType;
                    itemSlot2Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().barrelTier = 0;
                }
            }
            else if (listeningElement == slot3)
            {
                itemSlot3 = player.GetModPlayer<DRGNPlayer>().scopeType;
                if (itemSlot3 != null)
                {
                   
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().scopeType);
                    player.GetModPlayer<DRGNPlayer>().scopeType = null;
                    itemSlot3 = player.GetModPlayer<DRGNPlayer>().scopeType;
                    itemSlot3Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().scopeTier = 0;
                }
            }
            else if (listeningElement == slot4)
            {
                itemSlot4 = player.GetModPlayer<DRGNPlayer>().gripType;
                if (itemSlot4 != null)
                {
                    
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gripType);
                    player.GetModPlayer<DRGNPlayer>().gripType = null;
                    itemSlot4 = player.GetModPlayer<DRGNPlayer>().gripType;
                    itemSlot4Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().gripTier = 0;
                }
            }
            else if (listeningElement == slot5)
            {
                itemSlot5 = player.GetModPlayer<DRGNPlayer>().magType;
                if (itemSlot5 != null)
                {
                    
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().magType);
                    player.GetModPlayer<DRGNPlayer>().magType = null;
                    itemSlot5 = player.GetModPlayer<DRGNPlayer>().magType;
                    itemSlot5Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().magTier = 0;
                }
            }
            else if (listeningElement == slot6)
            {
                itemSlot6 = player.GetModPlayer<DRGNPlayer>().chamberType;
                if (itemSlot6 != null)
                {
                   
                    player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().chamberType);
                    player.GetModPlayer<DRGNPlayer>().chamberType = null;
                    itemSlot6 = player.GetModPlayer<DRGNPlayer>().chamberType;
                    itemSlot6Tier = 0;
                    player.GetModPlayer<DRGNPlayer>().chamberTier = 0;
                }
            }

        }
        public override void Update(GameTime gameTime)
        {
            player = Main.LocalPlayer;
            base.Update(gameTime);
            itemSlot1 = player.GetModPlayer<DRGNPlayer>().gunBodyType;
            itemSlot2 = player.GetModPlayer<DRGNPlayer>().barrelType;
            itemSlot3 = player.GetModPlayer<DRGNPlayer>().scopeType;
            itemSlot4 = player.GetModPlayer<DRGNPlayer>().gripType;
            itemSlot5 = player.GetModPlayer<DRGNPlayer>().magType;
            itemSlot6 = player.GetModPlayer<DRGNPlayer>().chamberType;

            itemSlot1Tier = player.GetModPlayer<DRGNPlayer>().gunBodyTier;
            itemSlot2Tier = player.GetModPlayer<DRGNPlayer>().barrelTier;
            itemSlot3Tier = player.GetModPlayer<DRGNPlayer>().scopeTier;
            itemSlot4Tier = player.GetModPlayer<DRGNPlayer>().gripTier;
            itemSlot5Tier = player.GetModPlayer<DRGNPlayer>().magTier;
            itemSlot6Tier = player.GetModPlayer<DRGNPlayer>().chamberTier;

            if (killItem && Main.mouseItem.type == ItemID.None&& player.inventory[58].type == ItemID.None) { killItem = false; }
            if (killItem) { Main.mouseItem.SetDefaults(0, true);player.inventory[58].SetDefaults(0, true); }
            // Setting the text per tick to update and show our resource values.

           
        }
    }
}