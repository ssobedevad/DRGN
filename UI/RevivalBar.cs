using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.UI.Elements;
using static DRGN.DRGNPlayer;
using System;
using System.Collections.Generic;
namespace DRGN.UI
{
     class RevivalBar : UIState
    {
        
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;
        

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 500, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);
            
            

            gradientA = new Color(5, 5, 138); // A dark purple
            gradientB = new Color(41, 41, 201); // A light purple

            
           
            Append(area);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleDamageItem
            if (!(DRGNPlayer.secondlife))
                return;
            DrawSelf(spriteBatch);
            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            
            // Calculate quotient
            float quotient = (float)((DRGNPlayer.lifeCounter)/100f)/ ((DRGNPlayer.lifeCounterMax) / 100f);
            // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = new Rectangle(1450, 30, 138, 34);
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / steps; // Alternate Gradient Approach
                                                  //float percent = (float)i / (right - left);
                if (quotient == 1) { spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), gradientA); }
                else
                {
                    spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
                }
            }
            Texture2D texture = ModContent.GetTexture("DRGN/RevivalBar");
            barFrame = new UIImage(texture);

            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            // Calculate quotient
            spriteBatch.Draw(texture, new Vector2(1450, 30), new Rectangle(0, 0, 138, 34), Color.White);
        }
        public override void Update(GameTime gameTime)
        {
            if (!(DRGNPlayer.secondlife))
                return;

            
            // Setting the text per tick to update and show our resource values.
           
            base.Update(gameTime);
        }
    }
}