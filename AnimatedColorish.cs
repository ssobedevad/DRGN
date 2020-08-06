
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN
{
    public class AnimatedColorish
    {
        private static float ColorCounter = 0f;
        private static bool directionOfChange = false;             
        public static void UpdateColorChange()
        {
            ColorCounter += directionOfChange ? 0.02f : -0.02f;
            ColorCounter = MathHelper.Clamp(ColorCounter, 0, 1);
            if (ColorCounter >= 1) directionOfChange = false;
            if (ColorCounter <= 0) directionOfChange = true;            
        }      
        public AnimatedColorish(Color c1, Color c2 , out Color c)
        {           
            c = Color.Lerp(c1, c2, ColorCounter) ;
        }
    }
}