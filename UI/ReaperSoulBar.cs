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
using DRGN.Items.Weapons;
using System.Linq;

namespace DRGN.UI
{
    class ReaperSoulBar : UIState
    {                    
        public override void Draw(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            if (!player.GetModPlayer<ReaperPlayer>().isReaper)
            { return; }
                      
            DrawSelf(spriteBatch);         
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            char[] numSouls = player.GetModPlayer<ReaperPlayer>().numSouls.ToString().ToCharArray();
            char[] maxSouls = player.GetModPlayer<ReaperPlayer>().maxSouls2.ToString().ToCharArray();            
            int numParts = 1 + numSouls.Length + maxSouls.Length;
            Vector2 drawStart = new Vector2(player.Center.X  - (6 * numParts), player.Center.Y - 49) - Main.screenPosition;                    
            Texture2D fontText = ModContent.GetTexture("DRGN/UI/ReaperFont");           
            for (int i = 0; i < numParts; i ++)
            { 
                if (i < numSouls.Length) 
                {
                    spriteBatch.Draw(fontText, drawStart + new Vector2((i * 12), 0), new Rectangle(0,  int.Parse(numSouls[i].ToString()) * 14, 10, 14), Color.White);

                }
                else if (i == numSouls.Length)
                {
                    spriteBatch.Draw(fontText, drawStart + new Vector2((i * 12), 0), new Rectangle(0, 140, 10, 14), Color.White);

                }
                else if (i >= numSouls.Length)
                {
                    spriteBatch.Draw(fontText, drawStart + new Vector2((i * 12), 0), new Rectangle(0, int.Parse(maxSouls[i - numSouls.Length - 1].ToString()) * 14, 10, 14), Color.White);
                }
            }                  
        }
        public override void Update(GameTime gameTime)
        {                            
            base.Update(gameTime);
        }
    }
}