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
}