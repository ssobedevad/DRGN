using DRGN.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

public class MiningHandler
{
    int pickPower;
    float pickSpeedMult;
    int pickTime;
    List<int> chosenTiles = new List<int>();
    int tileStyle = 0;
    public const short None = 0;
    public const short Custom = -1;
    public const short Ores = 1;
    public MiningDrone Md;
    public MiningHandler(int PickPower, float PickSpeedMult, int PickTime, short TileStyle, List<int> Tiles = null)
    {
        pickPower = PickPower;
        pickSpeedMult = PickSpeedMult;
        pickTime = PickTime;
        tileStyle = TileStyle;
        if (TileStyle == -1)
        {
            chosenTiles = Tiles;
        }
    }
    public bool CanBreakTile(TilePos TilePosition)
    {

        if (TilePosition.Empty || TilePosition.Invalid() || pickPower == 0)
        {
            return false;
        }
        Tile tile = TilePosition.GetTile();
        if (tile == null || !tile.active())
        {

            return false;
        }
        
        if (!WorldGen.CanKillTile(TilePosition.X, TilePosition.Y) || GetPickaxeDamage(TilePosition.X, TilePosition.Y, tile.type, tile) == 0)
        {

            return false;
        }
        if (ModContent.GetModTile(tile.type) != null)
        {
            if (pickPower < ModContent.GetModTile(tile.type).minPick)
            { return false; }
        }
        if (tileStyle == 1) { if (TileID.Sets.Ore.Length < tile.type || !TileID.Sets.Ore[tile.type]) { return false; } }
        else if (tileStyle == -1) { if (!chosenTiles.Contains(tile.type)) { return false; } }

        return true;
    }
    public int GetPickCD()
    {
        int pickCD = pickTime * 2;
        pickCD = (int)(pickCD / pickSpeedMult);
        return pickCD;
    }
    public bool BreakTile(int x, int y, HitTile Hittile)
    {

        if (pickPower == 0)
        {
            return false;
        }
        Tile tile = Main.tile[x, y];
        int type = tile.type;
        int pickaxeDamage = GetPickaxeDamage(x, y, type, tile);

        if (pickaxeDamage == 0 || !CanBreakTile(new TilePos(false, x, y)))
        {
            return false;
        }


        if (Hittile.AddDamage(Hittile.HitObject(x, y, 1), pickaxeDamage) >= 100)
        {
            AchievementsHelper.CurrentlyMining = true;

            if (Main.netMode == NetmodeID.MultiplayerClient && Main.tileContainer[Main.tile[x, y].type])
            {
                if (Main.tile[x, y].type == 470 || Main.tile[x, y].type == 475)
                {
                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 20, x, y);
                }
                else
                {
                    WorldGen.KillTile(x, y, fail: true);
                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y, 1f);
                }
                if (Main.tile[x, y].type == 21)
                {
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y);
                }
                if (Main.tile[x, y].type == 467)
                {
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 5, x, y);
                }
                if (Main.tile[x, y].type == 88)
                {
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 3, x, y);
                }
            }
            else
            {
                bool num2 = Main.tile[x, y].active();
                WorldGen.KillTile(x, y, false, false, true);
                
                int item = GetOreDrops(type);
               
                if (Md.Storage.ContainsKey(item)) { Md.Storage[item] += 1; }
                else { Md.Storage.Add(item, 1); }
                if (num2 && !Main.tile[x, y].active())
                {
                    AchievementsHelper.HandleMining();
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
                }
            }
            return true;
        }
        else
        {
            WorldGen.KillTile(x, y, fail: true);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y, 1f);
                NetMessage.SendData(125, -1, -1, null, Main.myPlayer, x, y, pickaxeDamage);
            }
        }

        return false;

    }
    public static Item GetBestPickaxe(Player player)
    {
        Item item = null;
        for (int i = 0; i < 50; i++)
        {
            if (player.inventory[i].stack > 0 && player.inventory[i].pick > 0 && (item == null || player.inventory[i].pick > item.pick))
            {
                item = player.inventory[i];
            }
        }
        return item;
    }
    public int GetPickaxeDamage(int x, int y, int tileId, Tile tileTarget)
    {
        int e = 0;
        if (Main.tileNoFail[tileTarget.type])
        {
            e = 100;
        }
        e = ((!Main.tileDungeon[tileTarget.type] && tileTarget.type != 25 && tileTarget.type != 58 && tileTarget.type != 117 && tileTarget.type != 203) ? ((tileTarget.type == 85) ? (e + pickPower / 3) : ((tileTarget.type != 48 && tileTarget.type != 232) ? ((tileTarget.type == 226) ? (e + pickPower / 4) : ((tileTarget.type != 107 && tileTarget.type != 221) ? ((tileTarget.type != 108 && tileTarget.type != 222) ? ((tileTarget.type == 111 || tileTarget.type == 223) ? (e + pickPower / 4) : ((tileTarget.type != 211) ? (e + pickPower) : (e + pickPower / 5))) : (e + pickPower / 3)) : (e + pickPower / 2))) : (e + pickPower * 2))) : (e + pickPower / 2));
        if (tileTarget.type == 211 && pickPower < 200)
        {
            e = 0;
        }
        if ((tileTarget.type == 25 || tileTarget.type == 203) && pickPower < 65)
        {
            e = 0;
        }
        else if (tileTarget.type == 117 && pickPower < 65)
        {
            e = 0;
        }
        else if (tileTarget.type == 37 && pickPower < 50)
        {
            e = 0;
        }
        else if ((tileTarget.type == 22 || tileTarget.type == 204) && (double)y > Main.worldSurface && pickPower < 55)
        {
            e = 0;
        }
        else if (tileTarget.type == 56 && pickPower < 65)
        {
            e = 0;
        }
        else if (tileTarget.type == 77 && pickPower < 65 && y >= Main.maxTilesY - 200)
        {
            e = 0;
        }
        else if (tileTarget.type == 58 && pickPower < 65)
        {
            e = 0;
        }
        else if ((tileTarget.type == 226 || tileTarget.type == 237) && pickPower < 210)
        {
            e = 0;
        }
        else if (tileTarget.type == 137 && pickPower < 210)
        {
            int num = tileTarget.frameY / 18;
            if ((uint)(num - 1) <= 3u)
            {
                e = 0;
            }
        }
        else if (Main.tileDungeon[tileTarget.type] && pickPower < 100 && (double)y > Main.worldSurface)
        {
            if ((double)x < (double)Main.maxTilesX * 0.35 || (double)x > (double)Main.maxTilesX * 0.65)
            {
                e = 0;
            }
        }
        else if (tileTarget.type == 107 && pickPower < 100)
        {
            e = 0;
        }
        else if (tileTarget.type == 108 && pickPower < 110)
        {
            e = 0;
        }
        else if (tileTarget.type == 111 && pickPower < 150)
        {
            e = 0;
        }
        else if (tileTarget.type == 221 && pickPower < 100)
        {
            e = 0;
        }
        else if (tileTarget.type == 222 && pickPower < 110)
        {
            e = 0;
        }
        else if (tileTarget.type == 223 && pickPower < 150)
        {
            e = 0;
        }
        if (tileTarget.type == 147 || tileTarget.type == 0 || tileTarget.type == 40 || tileTarget.type == 53 || tileTarget.type == 57 || tileTarget.type == 59 || tileTarget.type == 123 || tileTarget.type == 224 || tileTarget.type == 397)
        {
            e += pickPower;
        }
        if (tileTarget.type == 404)
        {
            e += 5;
        }
        if (tileTarget.type == 165 || Main.tileRope[tileTarget.type] || tileTarget.type == 199)
        {
            e = 100;
        }
        if (tileTarget.type == 128 || tileTarget.type == 269)
        {
            if (tileTarget.frameX == 18 || tileTarget.frameX == 54)
            {
                x--;
                tileTarget = Main.tile[x, y];

            }
            if (tileTarget.frameX >= 100)
            {
                e = 0;
                Main.blockMouse = true;
            }
        }
        if (tileTarget.type == 334)
        {
            if (tileTarget.frameY == 0)
            {
                y++;
                tileTarget = Main.tile[x, y];

            }
            if (tileTarget.frameY == 36)
            {
                y--;
                tileTarget = Main.tile[x, y];

            }
            int frameX = tileTarget.frameX;
            bool flag = frameX >= 5000;
            bool flag2 = false;
            if (!flag)
            {
                int num2 = frameX / 18;
                num2 %= 3;
                x -= num2;
                tileTarget = Main.tile[x, y];
                if (tileTarget.frameX >= 5000)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                frameX = tileTarget.frameX;
                int num3 = 0;
                while (frameX >= 5000)
                {
                    frameX -= 5000;
                    num3++;
                }
                if (num3 != 0)
                {
                    flag2 = true;
                }
            }
            if (flag2)
            {
                e = 0;
                Main.blockMouse = true;
            }
        }
        return e;
    }
    public int GetOreDrops(int type)
    {
        int item = 0;
        switch (type)
        {
            case TileID.Copper:
                item = ItemID.CopperOre;
                break;
            case TileID.Tin:
                item = ItemID.TinOre;
                break;
            case TileID.Iron:
                item = ItemID.IronOre;
                break;
            case TileID.Lead:
                item = ItemID.LeadOre;
                break;
            case TileID.Silver:
                item = ItemID.SilverOre;
                break;
            case TileID.Tungsten:
                item = ItemID.TungstenOre;
                break;
            case TileID.Gold:
                item = ItemID.GoldOre;
                break;
            case TileID.Platinum:
                item = ItemID.PlatinumOre;
                break;
            case TileID.Meteorite:
                item = ItemID.Meteorite;
                break;
            case TileID.Demonite:
                item = ItemID.DemoniteOre;
                break;
            case TileID.Crimtane:
                item = ItemID.CrimtaneOre;
                break;
            case TileID.Obsidian:
                item = ItemID.Obsidian;
                break;
            case TileID.Hellstone:
                item = ItemID.Hellstone;
                break;
            case TileID.Cobalt:
                item = ItemID.CobaltOre;
                break;
            case TileID.Palladium:
                item = ItemID.PalladiumOre;
                break;
            case TileID.Mythril:
                item = ItemID.MythrilOre;
                break;
            case TileID.Orichalcum:
                item = ItemID.OrichalcumOre;
                break;
            case TileID.Adamantite:
                item = ItemID.AdamantiteOre;
                break;
            case TileID.Titanium:
                item = ItemID.TitaniumOre;
                break;
            case TileID.Chlorophyte:
                item = ItemID.ChlorophyteOre;
                break;
            case TileID.LunarOre:
                item = ItemID.LunarOre;
                break;
        }
        if (ModContent.GetModTile(type) != null)
        { item = ModContent.GetModTile(type).drop; }
        return item;

    }
}
