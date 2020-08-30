using Microsoft.Xna.Framework;
using System;
using Terraria;

public class DavesUtils
{
    public static Vector2 Rotate(Vector2 v, float radians)
    {
        double ca = Math.Cos(radians);
        double sa = Math.Sin(radians);
        return new Vector2((float)(ca * v.X - sa * v.Y), (float)(sa * v.X + ca * v.Y));
    }
    public static int FindNearestTargettableNPC(Entity e, float MaxDist = 1000)
    {
        int target = -1;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            if (Main.npc[i].CanBeChasedBy(e, false))
            {
                float dist = Vector2.Distance(e.Center, Main.npc[i].Center);
                if (dist < MaxDist)
                {
                    MaxDist = dist;
                    target = i;

                }
            }
        }
        return target;
    }
    public static bool IsTileTypeFitForTree(ushort type)
    {
        if (type == 2 || type == 23 || type == 60 || type == 70 || type == 109 || type == 147 || type == 199 || type == 477 || type == 492 || Terraria.ModLoader.TileLoader.CanGrowModTree(type))
        {
            return true;
        }

         return false;
        
    }
    public static bool IsWallTypeFitForTree(ushort type)
    {
        if (type == 0 || type == 106 || type == 107 || (type >= 138 && type <= 141) || type == 145 || type == 150 || type == 152 || type == 80 || DRGN.DRGN.wallsForTreeGrow.Contains(type))
        {
            return true;
        }
        return false;

    }
    public static bool GrowTree(int i, int y)
    {
        int j;
        for (j = y; Terraria.ModLoader.TileLoader.IsSapling(Main.tile[i, j].type); j++)
        {
        }

        if (Main.tile[i, j].nactive() && !Main.tile[i, j].halfBrick() && Main.tile[i, j].slope() == 0 && IsTileTypeFitForTree(Main.tile[i, j].type) && IsWallTypeFitForTree(Main.tile[i, j-1].wall) && ((Main.tile[i - 1, j].active() && IsTileTypeFitForTree(Main.tile[i - 1, j].type)) || (Main.tile[i + 1, j].active() && IsTileTypeFitForTree(Main.tile[i + 1, j].type))))
        {
            byte color = Main.tile[i, j].color();
            int width = 2;
            int height = WorldGen.genRand.Next(5, 17);
            int extraHeight = height + 4;
            if (Main.tile[i, j].type == 60)
            {
                extraHeight += 5;
            }
            bool CanGrow = false;
            if (Main.tile[i, j].type == 70 && WorldGen.EmptyTileCheck(i - width, i + width, j - extraHeight, j - 3, 20) && WorldGen.EmptyTileCheck(i - 1, i + 1, j - 2, j - 1, 20))
            {
                CanGrow = true;
            }
            if (WorldGen.EmptyTileCheck(i - width, i + width, j - extraHeight, j - 1, 20))
            {
                CanGrow = true;
            }
            if (CanGrow)
            {
                bool flag2 = false;
                bool flag3 = false;
                int style;
                for (int k = j - height; k < j; k++)
                {
                    Main.tile[i, k].frameNumber((byte)WorldGen.genRand.Next(3));
                    Main.tile[i, k].active(active: true);
                    Main.tile[i, k].type = 5;
                    Main.tile[i, k].color(color);
                    style = WorldGen.genRand.Next(3);
                    int style2 = WorldGen.genRand.Next(10);
                    if (k == j - 1 || k == j - height)
                    {
                        style2 = 0;
                    }
                    while (((style2 == 5 || style2 == 7) && flag2) || ((style2 == 6 || style2 == 7) && flag3))
                    {
                        style2 = WorldGen.genRand.Next(10);
                    }
                    flag2 = false;
                    flag3 = false;
                    if (style2 == 5 || style2 == 7)
                    {
                        flag2 = true;
                    }
                    if (style2 == 6 || style2 == 7)
                    {
                        flag3 = true;
                    }
                    switch (style2)
                    {
                        case 1:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 66;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 88;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 110;
                            }
                            break;
                        case 2:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 0;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 22;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 44;
                            }
                            break;
                        case 3:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 44;
                                Main.tile[i, k].frameY = 66;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 44;
                                Main.tile[i, k].frameY = 88;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 44;
                                Main.tile[i, k].frameY = 110;
                            }
                            break;
                        case 4:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 66;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 88;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 22;
                                Main.tile[i, k].frameY = 110;
                            }
                            break;
                        case 5:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 88;
                                Main.tile[i, k].frameY = 0;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 88;
                                Main.tile[i, k].frameY = 22;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 88;
                                Main.tile[i, k].frameY = 44;
                            }
                            break;
                        case 6:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 66;
                                Main.tile[i, k].frameY = 66;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 66;
                                Main.tile[i, k].frameY = 88;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 66;
                                Main.tile[i, k].frameY = 110;
                            }
                            break;
                        case 7:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 110;
                                Main.tile[i, k].frameY = 66;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 110;
                                Main.tile[i, k].frameY = 88;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 110;
                                Main.tile[i, k].frameY = 110;
                            }
                            break;
                        default:
                            if (style == 0)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 0;
                            }
                            if (style == 1)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 22;
                            }
                            if (style == 2)
                            {
                                Main.tile[i, k].frameX = 0;
                                Main.tile[i, k].frameY = 44;
                            }
                            break;
                    }
                    if (style2 == 5 || style2 == 7)
                    {
                        Main.tile[i - 1, k].active(active: true);
                        Main.tile[i - 1, k].type = 5;
                        Main.tile[i - 1, k].color(color);
                        style = WorldGen.genRand.Next(3);
                        if (WorldGen.genRand.Next(3) < 2)
                        {
                            if (style == 0)
                            {
                                Main.tile[i - 1, k].frameX = 44;
                                Main.tile[i - 1, k].frameY = 198;
                            }
                            if (style == 1)
                            {
                                Main.tile[i - 1, k].frameX = 44;
                                Main.tile[i - 1, k].frameY = 220;
                            }
                            if (style == 2)
                            {
                                Main.tile[i - 1, k].frameX = 44;
                                Main.tile[i - 1, k].frameY = 242;
                            }
                        }
                        else
                        {
                            if (style == 0)
                            {
                                Main.tile[i - 1, k].frameX = 66;
                                Main.tile[i - 1, k].frameY = 0;
                            }
                            if (style == 1)
                            {
                                Main.tile[i - 1, k].frameX = 66;
                                Main.tile[i - 1, k].frameY = 22;
                            }
                            if (style == 2)
                            {
                                Main.tile[i - 1, k].frameX = 66;
                                Main.tile[i - 1, k].frameY = 44;
                            }
                        }
                    }
                    if (style2 != 6 && style2 != 7)
                    {
                        continue;
                    }
                    Main.tile[i + 1, k].active(active: true);
                    Main.tile[i + 1, k].type = 5;
                    Main.tile[i + 1, k].color(color);
                    style = WorldGen.genRand.Next(3);
                    if (WorldGen.genRand.Next(3) < 2)
                    {
                        if (style == 0)
                        {
                            Main.tile[i + 1, k].frameX = 66;
                            Main.tile[i + 1, k].frameY = 198;
                        }
                        if (style == 1)
                        {
                            Main.tile[i + 1, k].frameX = 66;
                            Main.tile[i + 1, k].frameY = 220;
                        }
                        if (style == 2)
                        {
                            Main.tile[i + 1, k].frameX = 66;
                            Main.tile[i + 1, k].frameY = 242;
                        }
                    }
                    else
                    {
                        if (style == 0)
                        {
                            Main.tile[i + 1, k].frameX = 88;
                            Main.tile[i + 1, k].frameY = 66;
                        }
                        if (style == 1)
                        {
                            Main.tile[i + 1, k].frameX = 88;
                            Main.tile[i + 1, k].frameY = 88;
                        }
                        if (style == 2)
                        {
                            Main.tile[i + 1, k].frameX = 88;
                            Main.tile[i + 1, k].frameY = 110;
                        }
                    }
                }
                int num6 = WorldGen.genRand.Next(3);
                bool flag4 = false;
                bool flag5 = false;
                if (Main.tile[i - 1, j].nactive() && !Main.tile[i - 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && IsTileTypeFitForTree(Main.tile[i - 1, j].type))
                {
                    flag4 = true;
                }
                if (Main.tile[i + 1, j].nactive() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i + 1, j].slope() == 0 && IsTileTypeFitForTree(Main.tile[i + 1, j].type))
                {
                    flag5 = true;
                }
                if (!flag4)
                {
                    if (num6 == 0)
                    {
                        num6 = 2;
                    }
                    if (num6 == 1)
                    {
                        num6 = 3;
                    }
                }
                if (!flag5)
                {
                    if (num6 == 0)
                    {
                        num6 = 1;
                    }
                    if (num6 == 2)
                    {
                        num6 = 3;
                    }
                }
                if (flag4 && !flag5)
                {
                    num6 = 2;
                }
                if (flag5 && !flag4)
                {
                    num6 = 1;
                }
                if (num6 == 0 || num6 == 1)
                {
                    Main.tile[i + 1, j - 1].active(active: true);
                    Main.tile[i + 1, j - 1].type = 5;
                    Main.tile[i + 1, j - 1].color(color);
                    style = WorldGen.genRand.Next(3);
                    if (style == 0)
                    {
                        Main.tile[i + 1, j - 1].frameX = 22;
                        Main.tile[i + 1, j - 1].frameY = 132;
                    }
                    if (style == 1)
                    {
                        Main.tile[i + 1, j - 1].frameX = 22;
                        Main.tile[i + 1, j - 1].frameY = 154;
                    }
                    if (style == 2)
                    {
                        Main.tile[i + 1, j - 1].frameX = 22;
                        Main.tile[i + 1, j - 1].frameY = 176;
                    }
                }
                if (num6 == 0 || num6 == 2)
                {
                    Main.tile[i - 1, j - 1].active(active: true);
                    Main.tile[i - 1, j - 1].type = 5;
                    Main.tile[i - 1, j - 1].color(color);
                    style = WorldGen.genRand.Next(3);
                    if (style == 0)
                    {
                        Main.tile[i - 1, j - 1].frameX = 44;
                        Main.tile[i - 1, j - 1].frameY = 132;
                    }
                    if (style == 1)
                    {
                        Main.tile[i - 1, j - 1].frameX = 44;
                        Main.tile[i - 1, j - 1].frameY = 154;
                    }
                    if (style == 2)
                    {
                        Main.tile[i - 1, j - 1].frameX = 44;
                        Main.tile[i - 1, j - 1].frameY = 176;
                    }
                }
                style = WorldGen.genRand.Next(3);
                switch (num6)
                {
                    case 0:
                        if (style == 0)
                        {
                            Main.tile[i, j - 1].frameX = 88;
                            Main.tile[i, j - 1].frameY = 132;
                        }
                        if (style == 1)
                        {
                            Main.tile[i, j - 1].frameX = 88;
                            Main.tile[i, j - 1].frameY = 154;
                        }
                        if (style == 2)
                        {
                            Main.tile[i, j - 1].frameX = 88;
                            Main.tile[i, j - 1].frameY = 176;
                        }
                        break;
                    case 1:
                        if (style == 0)
                        {
                            Main.tile[i, j - 1].frameX = 0;
                            Main.tile[i, j - 1].frameY = 132;
                        }
                        if (style == 1)
                        {
                            Main.tile[i, j - 1].frameX = 0;
                            Main.tile[i, j - 1].frameY = 154;
                        }
                        if (style == 2)
                        {
                            Main.tile[i, j - 1].frameX = 0;
                            Main.tile[i, j - 1].frameY = 176;
                        }
                        break;
                    case 2:
                        if (style == 0)
                        {
                            Main.tile[i, j - 1].frameX = 66;
                            Main.tile[i, j - 1].frameY = 132;
                        }
                        if (style == 1)
                        {
                            Main.tile[i, j - 1].frameX = 66;
                            Main.tile[i, j - 1].frameY = 154;
                        }
                        if (style == 2)
                        {
                            Main.tile[i, j - 1].frameX = 66;
                            Main.tile[i, j - 1].frameY = 176;
                        }
                        break;
                }
                if (WorldGen.genRand.Next(13) != 0)
                {
                    style = WorldGen.genRand.Next(3);
                    if (style == 0)
                    {
                        Main.tile[i, j - height].frameX = 22;
                        Main.tile[i, j - height].frameY = 198;
                    }
                    if (style == 1)
                    {
                        Main.tile[i, j - height].frameX = 22;
                        Main.tile[i, j - height].frameY = 220;
                    }
                    if (style == 2)
                    {
                        Main.tile[i, j - height].frameX = 22;
                        Main.tile[i, j - height].frameY = 242;
                    }
                }
                else
                {
                    style = WorldGen.genRand.Next(3);
                    if (style == 0)
                    {
                        Main.tile[i, j - height].frameX = 0;
                        Main.tile[i, j - height].frameY = 198;
                    }
                    if (style == 1)
                    {
                        Main.tile[i, j - height].frameX = 0;
                        Main.tile[i, j - height].frameY = 220;
                    }
                    if (style == 2)
                    {
                        Main.tile[i, j - height].frameX = 0;
                        Main.tile[i, j - height].frameY = 242;
                    }
                }
                WorldGen.RangeFrame(i - 2, j - height - 1, i + 2, j + 1);
                if (Main.netMode == 2)
                {
                    NetMessage.SendTileSquare(-1, i, (int)((double)j - (double)height * 0.5), height + 1);
                }
                return true;
            }
        }
        return false;
    }
}