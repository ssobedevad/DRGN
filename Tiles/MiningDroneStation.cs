using DRGN.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DRGN.Tiles
{
    public class MiningDroneStation : ModTile
    {
        public MiningDrone Md;
        public List<int> ChosenTiles;
        
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileContainer[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileValue[Type] = 500;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);


            TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;

            TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Containers };
            chest = "Storage";
            On.Terraria.Chest.UpdateChestFrames += Chest_UpdateChestFrames;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return !NPC.AnyNPCs(mod.NPCType("MiningDrone"));
        }
        private void Chest_UpdateChestFrames(On.Terraria.Chest.orig_UpdateChestFrames orig)
        {
            List<int>_chestInUse = new List<int>();
            for (int k = 0; k < 255; k++)
            {
                if (Main.player[k].active && Main.player[k].chest >= 0 && Main.player[k].chest < Main.chest.Length)
                {
                    _chestInUse.Add(Main.player[k].chest);
                }
            }
            for (int l = 0; l < Main.chest.Length; l++)
            {
                Chest chest = Main.chest[l];
                if (chest != null && _chestInUse.Contains(l))
                {
                    if (Main.tile[chest.x, chest.y].type != mod.TileType("MiningDroneStation"))
                    {

                        orig();
                    }
                    else
                    {
                        
                        for (int x = 0; x < 2; x++)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                Tile newTile = Main.tile[chest.x + x, chest.y + y];
                                newTile.frameY = (short)(y * 18);
                            }
                        }
                    }
                }
            }
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            DRGNModWorld.MiningDroneStation = true;
        }

        public override bool HasSmartInteract() => true;
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        { 
            Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("MiningDroneStation"));
            DRGNModWorld.MiningDroneStation = false;
        
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY > 18)
            {

                if (player.sign >= 0)
                {
                    Main.PlaySound(SoundID.MenuClose);
                    player.sign = -1;
                    Main.editSign = false;
                    Main.npcChatText = "";
                }
                if (Main.editChest)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    Main.editChest = false;
                    Main.npcChatText = "";
                }
                if (player.editedChestName)
                {
                    NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
                    player.editedChestName = false;
                }

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    if (left == player.chestX && top == player.chestY && player.chest >= 0)
                    {
                        player.chest = -1;
                        Recipe.FindRecipes();
                        Main.PlaySound(SoundID.MenuClose);
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
                        Main.stackSplit = 600;
                    }
                }
                else
                {
                    top -= 2;

                    int chest = Chest.FindChest(left, top);

                    if (chest >= 0)
                    {
                        Main.stackSplit = 600;
                        if (chest == player.chest)
                        {
                            player.chest = -1;
                            Main.PlaySound(SoundID.MenuClose);
                        }
                        else
                        {
                            player.chest = chest;
                            Main.playerInventory = true;
                            Main.recBigList = false;
                            player.chestX = left;
                            player.chestY = top;
                            Main.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
                        }
                        Recipe.FindRecipes();

                    }

                }
                return true;
            }
            else if (tile.frameY != 0)
            {
                top--;
            }

            tile = Main.tile[left, top];
            if (tile.frameX != 36)
            {
                
                DroneCreate(player, left, top);
                
            }
            else if(Md != null) { Md.npc.ai[0] = -1; Md.npc.netUpdate = true; }
            return true;
        }
       
        public void SwitchStyle(int i, int j)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Tile newTile = Main.tile[i + x, j + y];
                    newTile.frameX += (short)(newTile.frameX >= 36 ? -36 : 36);
                }
            }
        }
        public void DroneCreate(Player player, int i, int j)
        {
            Item Bestpick = MiningHandler.GetBestPickaxe(player);
            if(Bestpick == null)
            { Main.NewText("Must have a pickaxe in your inventory"); return; }
            int npcid = NPC.NewNPC(16 + i * 16, 16 + j * 16, mod.NPCType("MiningDrone"));
            Md = Main.npc[npcid].modNPC as MiningDrone;
            Md.MiningInit(Bestpick.pick, Bestpick.useTime, player.pickSpeed, 1, null);
            Md.StationPos.Set(i,j);
            SwitchStyle(i, j);
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY > 18)
            {
                top -= (short)(tile.frameY / 18);
            }
            else
            {

                
                    player.showItemIcon2 = mod.ItemType("MiningDroneController");
                    
                    player.showItemIconText = "";
                
                player.noThrow = 2;
                player.showItemIcon = true;
                return;
            }
            int chest = Chest.FindChest(left, top);
            player.showItemIcon2 = -1;
            if (chest < 0)
            {
                player.showItemIconText = Language.GetTextValue("LegacyChestType.0");
            }
            else
            {
                player.showItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Container";
                if (player.showItemIconText == "Container")
                {
                    player.showItemIcon2 = mod.ItemType("MiningDroneStation");
                    
                    player.showItemIconText = "";
                }
            }
            player.noThrow = 2;
            player.showItemIcon = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.showItemIconText == "")
            {
                player.showItemIcon = false;
                player.showItemIcon2 = 0;
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if(!NPC.AnyNPCs(mod.NPCType("MiningDrone")) && Main.tile[i,j].frameX >= 36)
            { SwitchStyle(i,j); }
        }
        
    }
}

