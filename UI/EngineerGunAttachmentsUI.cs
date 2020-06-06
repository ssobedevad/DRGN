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
        private bool killItem;
        
        private Player player;
        private Item itemSlot1,itemSlot2,itemSlot3,itemSlot4,itemSlot5,itemSlot6;
        private int itemSlot1Tier, itemSlot2Tier, itemSlot3Tier, itemSlot4Tier, itemSlot5Tier, itemSlot6Tier;


        public override void OnInitialize()
        {
            
           
            
             panel = new UIPanel(); // 2
            panel.Left.Set(-1335, 1f);
            panel.Top.Set(106, 0);
            panel.Width.Set(80, 0);
            panel.Height.Set(136, 0);
            panel.OnClick += OnButtonClick; // 3
            panel.OnRightClick += OnRightMB;
            

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
            
            base.DrawSelf(spriteBatch);
            int gunStartX = 590;
            int gunStartY = 24;

            Texture2D bodyTexture = ModContent.GetTexture("DRGN/UI/GunBodies/GunBody" + itemSlot1Tier.ToString());
            Texture2D barrelTexture = ModContent.GetTexture("DRGN/UI/GunBarrels/GunBarrel"+itemSlot2Tier.ToString());
            Texture2D scopeTexture = ModContent.GetTexture("DRGN/UI/GunScopes/GunScope" + itemSlot3Tier.ToString());
            Texture2D gripTexture = ModContent.GetTexture("DRGN/UI/GunGrips/GunGrip" + itemSlot4Tier.ToString());
            Texture2D magTexture = ModContent.GetTexture("DRGN/UI/GunMags/GunMag" + itemSlot5Tier.ToString());
            Texture2D chamberTexture = ModContent.GetTexture("DRGN/UI/GunChambers/GunChamber" + itemSlot6Tier.ToString());

            Texture2D accSlot = ModContent.GetTexture("DRGN/UI/AccessorySlot");

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


            spriteBatch.Draw(accSlot, new Vector2(gunStartX , gunStartY + 95), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot1 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot1.type], new Vector2(gunStartX+(32 - Main.itemTexture[itemSlot1.type].Width)/2, gunStartY + 95+(32 - Main.itemTexture[itemSlot1.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot1.type].Width), (Main.itemTexture[itemSlot1.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(gunStartX , gunStartY + 135), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot2 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot2.type], new Vector2(gunStartX + (32 - Main.itemTexture[itemSlot2.type].Width) / 2, gunStartY + 135 + (32 - Main.itemTexture[itemSlot2.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot2.type].Width), (Main.itemTexture[itemSlot2.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(gunStartX , gunStartY + 175), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot3 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot3.type], new Vector2(gunStartX + (32 - Main.itemTexture[itemSlot3.type].Width) / 2, gunStartY + 175 + (32 - Main.itemTexture[itemSlot3.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot3.type].Width), (Main.itemTexture[itemSlot3.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(gunStartX +40, gunStartY + 95), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot4 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot4.type], new Vector2(gunStartX + 40+(32 - Main.itemTexture[itemSlot4.type].Width) / 2, gunStartY + 95 + (32 - Main.itemTexture[itemSlot4.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot4.type].Width), (Main.itemTexture[itemSlot4.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(gunStartX + 40, gunStartY + 135), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot5 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot5.type], new Vector2(gunStartX + 40+(32 - Main.itemTexture[itemSlot5.type].Width) / 2, gunStartY + 135 + (32 - Main.itemTexture[itemSlot5.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot5.type].Width), (Main.itemTexture[itemSlot5.type].Height)), Color.White);
            }
            spriteBatch.Draw(accSlot, new Vector2(gunStartX + 40, gunStartY + 175), new Rectangle(0, 0, 32, 32), Color.White);
            if (itemSlot6 != null)
            {
                spriteBatch.Draw(Main.itemTexture[itemSlot6.type], new Vector2(gunStartX + 40+(32 - Main.itemTexture[itemSlot6.type].Width) / 2, gunStartY + 175 + (32 - Main.itemTexture[itemSlot6.type].Height) / 2), new Rectangle(0, 0, (Main.itemTexture[itemSlot6.type].Width), (Main.itemTexture[itemSlot6.type].Height)), Color.White);
            }

        }


        public void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            killItem = true;
            var mouseItemClone = Main.mouseItem.Clone();
            if (mouseItemClone.modItem == null) { killItem = false; return; }
            ModItem mouseItemObject = mouseItemClone.modItem;
            
            int gunStartX = 590;
            int gunStartY = 24;
            ModItem MI = mouseItemObject as ModItem;
            EngineerAttachments EA = MI as EngineerAttachments;
            
           if (EA == null) { killItem = false; return; }
            


            if ((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 95 && Main.mouseY < gunStartY + 127) && Main.mouseItem.type != 0 && EA.isGunBody)
            {  
            if (itemSlot1 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gunBodyType); }        
                player.GetModPlayer<DRGNPlayer>().gunBodyType = mouseItemClone;         
                itemSlot1 = player.GetModPlayer<DRGNPlayer>().gunBodyType;
                itemSlot1Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().gunBodyTier = EA.AttachmentTier;


            }
            else if ((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 135 && Main.mouseY < gunStartY + 167) && Main.mouseItem.type != 0 && EA.isGunBarrel)
            {
                if (itemSlot2 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().barrelType); }
                player.GetModPlayer<DRGNPlayer>().barrelType = mouseItemClone;
                itemSlot2 = player.GetModPlayer<DRGNPlayer>().barrelType;
                itemSlot2Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().barrelTier = EA.AttachmentTier;
            }
            else if((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 175 && Main.mouseY < gunStartX + 207) && Main.mouseItem.type != 0 && EA.isGunScope)
            {
                if (itemSlot3 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().scopeType); }
                player.GetModPlayer<DRGNPlayer>().scopeType = mouseItemClone;
                itemSlot3 = player.GetModPlayer<DRGNPlayer>().scopeType;
                itemSlot3Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().scopeTier = EA.AttachmentTier;
            }
            else if((Main.mouseX > gunStartX +40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 95 && Main.mouseY < gunStartY + 127) && Main.mouseItem.type != 0 && EA.isGunGrip)
            {
                if (itemSlot4 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gripType); }
                player.GetModPlayer<DRGNPlayer>().gripType = mouseItemClone;
                itemSlot4 = player.GetModPlayer<DRGNPlayer>().gripType;
                itemSlot4Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().gripTier = EA.AttachmentTier;
            }
            else if((Main.mouseX > gunStartX + 40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 135 && Main.mouseY < gunStartY + 167) && Main.mouseItem.type != 0 && EA.isGunMag)
            {
                if (itemSlot5 != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().magType); }
                player.GetModPlayer<DRGNPlayer>().magType = mouseItemClone;
                itemSlot5 = player.GetModPlayer<DRGNPlayer>().magType;
                itemSlot5Tier = EA.AttachmentTier;
                player.GetModPlayer<DRGNPlayer>().magTier = EA.AttachmentTier;
            }
            else if((Main.mouseX > gunStartX + 40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 175 && Main.mouseY < gunStartY + 207) && Main.mouseItem.type != 0 && EA.isGunChamber)
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

            
            int gunStartX = 590;
            int gunStartY = 24;
            if (!(Main.playerInventory))
            {
                return;
            }
            if ((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 95 && Main.mouseY < gunStartY + 127))
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
            else if ((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 135 && Main.mouseY < gunStartY + 167))
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
            else if ((Main.mouseX > gunStartX && Main.mouseX < gunStartX + 32) && (Main.mouseY > gunStartY + 175 && Main.mouseY < gunStartY + 207))
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
            else if ((Main.mouseX > gunStartX + 40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 95 && Main.mouseY < gunStartY + 127))
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
            else if ((Main.mouseX > gunStartX + 40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 135 && Main.mouseY < gunStartY + 167))
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
            else if ((Main.mouseX > gunStartX + 40 && Main.mouseX < gunStartX + 72) && (Main.mouseY > gunStartY + 175 && Main.mouseY < gunStartY + 207))
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

            if (killItem && Main.mouseItem.type == 0&& player.inventory[58].type == 0) { killItem = false; }
            if (killItem) { Main.mouseItem.SetDefaults(0, true);player.inventory[58].SetDefaults(0, true); }
            // Setting the text per tick to update and show our resource values.

           
        }
    }
}