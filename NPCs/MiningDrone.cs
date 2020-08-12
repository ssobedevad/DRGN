using DRGN.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs
{
    [AutoloadHead]
    public class MiningDrone : ModNPC
    {
        Vector2 TargetPos;
        public TilePos TargetTilePos = new TilePos(true);
        HitTile Hittile = new HitTile();
        public TilePos StationPos = new TilePos(true);
        public MiningHandler Mh;
        public Dictionary<int, int> Storage = new Dictionary<int, int>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Drone");
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.netUpdate = true;
            npc.netAlways = true;
            npc.lifeMax = 2500;
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
            npc.friendly = true;
            npc.height = 32;
            npc.width = 32;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        

        public override bool CanChat()
        {
            return false;
        }
        public override string TownNPCName()
        {
            return "Mining Drone";
        }
        public void MiningInit(int PickPower, int PickTime, float PickSpeedMult, short Style, List<int> ChosenTiles = null)
        {
            Mh = new MiningHandler(PickPower, PickSpeedMult, PickTime, Style, ChosenTiles);
            Mh.Md = npc.modNPC as MiningDrone;
        }
        public override void AI()
        {
            if (Mh != null)
            {
                npc.ai[2] += 0.1f;
                npc.ai[2] %= 3.14f;
                if (npc.ai[1] > 0) { npc.ai[1] -= 1; }
                if (npc.ai[0] == 0)
                {
                    FindNearestMineableOreTile(npc.Center);
                    npc.ai[0] = 1;

                }
                else if (npc.ai[0] == 1)
                {
                    MoveTo();
                }
                else if (npc.ai[0] == 2)
                {
                    FindNearestMineableOreTile(Main.MouseWorld);
                    npc.ai[0] = 1;
                }
                else if (npc.ai[0] == -1)
                {
                    Dock();
                }
                if (!TargetTilePos.Empty && Vector2.Distance(TargetTilePos.GetVector2Coords(), npc.Center) < 200)
                {

                    if (npc.ai[1] == 0)
                    {
                        npc.rotation = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center).ToRotation() + 1.57f;
                        npc.ai[1] = Mh.GetPickCD();
                        if (Mh.BreakTile(TargetTilePos.X, TargetTilePos.Y, Hittile) || !TargetTilePos.GetTile().active())
                        { TargetTilePos.Nullify(); npc.ai[0] = 0; }
                    }
                }
            }
            else { npc.active = false; }
        }
        public override void NPCLoot()
        {
            foreach (KeyValuePair<int, int> Data in Storage.ToArray())
            {                
                Item newItem = new Item();
                newItem.SetDefaults(Data.Key);
                newItem.stack = Data.Value;             
                Item.NewItem(npc.Center, newItem.type, newItem.stack);
            }
        }
        public void FindNearestMineableOreTile(Vector2 position)
        {
            TilePos Position = new TilePos(position);
            int x = 1;
            int y = 1;
            TilePos Requested = new TilePos(true);
            bool targetFound = false;
            while (!targetFound)
            {
                for (int i = -x; i < x + 1; i++)
                {
                    Requested = Position.Sum(i, -y);
                    if (Mh.CanBreakTile(Requested))
                    {
                        TargetTilePos = Requested;
                        TargetPos = Requested.GetVector2Coords();
                        targetFound = true;
                    }
                    Requested = Position.Sum(i, y);
                    if (Mh.CanBreakTile(Requested))
                    {
                        TargetTilePos = Requested;
                        TargetPos = Requested.GetVector2Coords();
                        targetFound = true;
                    }
                }
                for (int j = -y; j < y + 1; j++)
                {
                    Requested = Position.Sum(-x, j);
                    if (Mh.CanBreakTile(Requested))
                    {
                        TargetTilePos = Requested;
                        TargetPos = Requested.GetVector2Coords();
                        targetFound = true;
                    }
                    Requested = Position.Sum(x, j);
                    if (Mh.CanBreakTile(Requested))
                    {
                        TargetTilePos = Requested;
                        TargetPos = Requested.GetVector2Coords();
                        targetFound = true;
                    }
                }
                x += 1;
                y += 1;

            }






        }
        private void MoveTo()
        {

            Vector2 Diff = TargetPos - npc.Center;
            float Magnitude = (float)Math.Sqrt((double)(Diff.X * Diff.X + Diff.Y * Diff.Y));
            float Speed = 3f;
            if (Magnitude > 150)
            {
               
                Magnitude = Speed / Magnitude;
                Diff *= Magnitude;

                npc.velocity = (npc.velocity * 10f + Diff) / 11f;
                npc.rotation = npc.velocity.ToRotation() + 1.57f;
            }
            else { npc.velocity *= 0.9f; }

        }

        private void Dock()
        {
            Vector2 Diff = StationPos.GetVector2Coords() - npc.Center;
            float Magnitude = (float)Math.Sqrt((double)(Diff.X * Diff.X + Diff.Y * Diff.Y));
            float Speed = 8f;
            if (Magnitude > Speed * 2)
            {
                Magnitude = Speed / Magnitude;
                Diff *= Magnitude;

                npc.velocity = (npc.velocity * 2f + Diff) / 3f;
                npc.rotation = npc.velocity.ToRotation() + 1.57f;
            }
            else
            {
                DepositToStorage();
                npc.active = false;
            }
           
        }


        public void DepositToStorage()
        {
            MiningDroneStation Mds = ModContent.GetModTile(Main.tile[StationPos.X, StationPos.Y].type) as MiningDroneStation;
            Mds.SwitchStyle(StationPos.X, StationPos.Y);
            Chest chest = Main.chest[Chest.FindChest(StationPos.X, StationPos.Y)];
            Item[] items = chest.item;
            foreach (KeyValuePair<int, int> Data in Storage.ToArray())
            {
                int slot = -1;
                Item newItem = new Item();
                newItem.SetDefaults(Data.Key);
                newItem.stack = Data.Value;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].type == ItemID.None || items[i].type == newItem.type)
                    { slot = i; break; }
                }
                if (slot > -1)
                { if (chest.item[slot].type == newItem.type) { chest.item[slot].stack += newItem.stack; } else { chest.item[slot] = newItem; } }
                else { Item.NewItem(StationPos.GetVector2Coords(), newItem.type, newItem.stack); }
            }

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float rotation = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center).ToRotation();
            if (!TargetTilePos.Empty && Vector2.Distance(TargetTilePos.GetVector2Coords(), npc.Center) < 200)
            {
                Texture2D text = ModContent.GetTexture("DRGN/NPCs/MiningBeam");
                Vector2 offset = new Vector2((float)Math.Sin(npc.ai[2]) * 8f, (float)Math.Cos(npc.ai[2]) * 8f);
                Vector2 unit = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center + offset);
                float r = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center + offset).ToRotation();

                for (float i = 0; i <= Vector2.Distance(TargetTilePos.GetVector2Coords() + offset, npc.Center); i += 2)
                {
                    Vector2 origin = npc.Center + i * unit;
                    spriteBatch.Draw(text, origin - Main.screenPosition,
                    new Rectangle(0, 0, 4, 4), Color.White, r,
                    new Vector2(4 * .5f, 4 * .5f), 1f, 0, 0);
                }
                float r2 = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center - offset).ToRotation();
                Vector2 unit2 = Vector2.Normalize(TargetTilePos.GetVector2Coords() - npc.Center - offset);
                for (float i = 0; i <= Vector2.Distance(TargetTilePos.GetVector2Coords() - offset, npc.Center); i += 2)
                {
                    Vector2 origin2 = npc.Center + i * unit2;
                    spriteBatch.Draw(text, origin2 - Main.screenPosition,
                    new Rectangle(0, 0, 4, 4), Color.White, r2,
                    new Vector2(4 * .5f, 4 * .5f), 1f, 0, 0);

                }
                Dust Dust = Dust.NewDustDirect(TargetTilePos.GetVector2Coords() + offset - new Vector2(3, 3), 6, 6, DustID.SomethingRed, 0, 0, 0, Color.White, 1f);
                Dust.noGravity = true;
                Dust = Dust.NewDustDirect(TargetTilePos.GetVector2Coords() - offset - new Vector2(3, 3), 6, 6, DustID.SomethingRed, 0, 0, 0, Color.White, 1f);
                Dust.noGravity = true;

            }
            Texture2D text2 = ModContent.GetTexture(Texture);
            spriteBatch.Draw(text2, npc.Center - Main.screenPosition,
                     null, drawColor, rotation + 1.57f,
                       text2.Size() * 0.5f, 1f, 0, 0);
            return false;
        }


    }
}