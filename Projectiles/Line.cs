using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria; 
namespace DRGN.Projectiles
{

    public class Line
    {
        Vector2 p1, p2; //this will be the position in the center of the line
        int length, thickness; //length and thickness of the line, or width and height of rectangle
        Rectangle rect;
        float rotation; // rotation of the line, with axis at the center of the line
        Color color;


        //p1 and p2 are the two end points of the line
        public Line(Vector2 p1, Vector2 p2, int thickness, Color color)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.thickness = thickness;
            this.color = color;
        }



        ////public void Draw(SpriteBatch spriteBatch, Color color)
        ////{
        ////    Vector2 tangent = p2 - p1;
        ////    float rotation = (float)Math.Atan2(tangent.Y, tangent.X);

        ////    const float ImageThickness = 8;
        ////    float thicknessScale = thickness / ImageThickness;

        ////    Vector2 capOrigin = new Vector2(Art.HalfCircle.Width, Art.HalfCircle.Height / 2f);
        ////    Vector2 middleOrigin = new Vector2(0, Art.LightningSegment.Height / 2f);
        ////    Vector2 middleScale = new Vector2(tangent.Length(), thicknessScale);

        ////    spriteBatch.Draw(Art.LightningSegment, p1, null, color, rotation, middleOrigin, middleScale, SpriteEffects.None, 0f);
          // spriteBatch.Draw(Art.HalfCircle, p1, null, color, rotation, capOrigin, thicknessScale, SpriteEffects.None, 0f);
        ////    spriteBatch.Draw(Art.HalfCircle, p2, null, color, rotation + MathHelper.Pi, capOrigin, thicknessScale, SpriteEffects.None, 0f);
        ////}
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Color lightColor)
        {
         
            float thicknessScale = thickness / 8;
            Vector2 tangent = p2 - p1;
            length = (int)Vector2.Distance(p1, p2); //gets distance between the points
                                                    // rotation = getRotation(p1.X, p1.Y, p2.X, p2.Y); //gets angle between points(method on bottom)
                                                    //            rect = new Rectangle((int)p1.X, (int)p1.Y, length, thickness);
            float rotation = (float)Math.Atan2(tangent.Y, tangent.X);
            rect = new Rectangle((int)(p1.X - Main.screenPosition.X), (int)( p1.Y - Main.screenPosition.Y), length, thickness);   // where to draw on screen 

            spriteBatch.Draw(texture, rect, new Rectangle(10, 0, 8, 8), lightColor, rotation, new Vector2(0, 0) // point about which to rotate 
               , SpriteEffects.None, 0);

            Vector2 capOrigin = new Vector2(8, 4);   // arc width, height/2
            Vector2 middleOrigin = new Vector2(0, 4);    // main segment height / 2 
            Vector2 middleScale = new Vector2(tangent.Length(), 1/2);
           
            // draw the half cirles as the 2 end points 
            spriteBatch.Draw(texture, p1 - Main.screenPosition - new Vector2(0,0), new Rectangle(4, 0, 4, 8), lightColor, rotation, new Vector2(2,4), 0.5f,  SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, p2 - Main.screenPosition - new Vector2(-0,0), new Rectangle(20, 0, 4, 8), lightColor, rotation , new Vector2(2,4), 0.5f, SpriteEffects.None, 0f);

            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(p1,  p2
                , thickness*2, DelegateMethods.CastLight);

        }
        //  this returns the angle between two points in radians
        private float getRotation(float x, float y, float x2, float y2)
        {
            float adj = x - x2;
            float opp = y - y2;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0) { res += 360; }
            res = MathHelper.ToRadians(res);
            return res;
        }
    }
}