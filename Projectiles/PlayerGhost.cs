﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class PlayerGhost : ModProjectile
    {
        private Vector2[] oldPos = new Vector2[6] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero};
        public override void SetDefaults()
        {

            projectile.height = 24;
            projectile.width = 42;
            projectile.aiStyle = -1;
            
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 2;
            
            projectile.tileCollide = false;
            
            
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if(projectile.frameCounter %40 == 0 && projectile.frame == 0)
            { projectile.frame = 1;projectile.frameCounter = 0; }
            else if (projectile.frameCounter % 10 == 0 && projectile.frame == 1)
            { projectile.frame = 0; projectile.frameCounter = 0; }
            for (int i = 5; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = projectile.Center; }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }



                if (oldPos[i] == Vector2.Zero) { oldPos[i] = projectile.Center; }

            }
            Player player = Main.player[projectile.owner];
            projectile.Center = player.GetModPlayer<DRGNPlayer>().oldPos[59].XY();
            projectile.spriteDirection = player.direction ;
            if (DRGN.TimeWarpHotkey.JustPressed && player.GetModPlayer<DRGNPlayer>().timeWarpCounter >= player.GetModPlayer<DRGNPlayer>().timeWarpCounterMax) 
            { 
                player.Center = projectile.Center;
                if (player.statLife + 50 < player.statLifeMax2)
                {
                    player.statLife += 50;
                   
                }
                else { player.statLife = player.statLifeMax2; }
                player.HealEffect(50);
                projectile.active = false;
                for (int i = 0; i < 20; i ++)
                { Dust.NewDust(player.position, player.width, player.height, DustID.BubbleBlock, 0, 0, 0, default, 2f); }
                player.GetModPlayer<DRGNPlayer>().timeWarpCounter = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int i = 5; i >= 0; i--)
            {
                Vector2 oldV = oldPos[i];
                Vector2 vect = new Vector2(oldV.X - Main.screenPosition.X, oldV.Y - Main.screenPosition.Y);
                Rectangle rect = new Rectangle(0, 0, 24, 42);

                Color alpha9 = projectile.GetAlpha(Color.White);
                alpha9.R = (byte)(alpha9.R * (10 - (2 * i)) / 20);
                alpha9.G = (byte)(alpha9.G * (10 - (2 * i)) / 20);
                alpha9.B = (byte)(alpha9.B * (10 - (2 * i)) / 20);
                alpha9.A = (byte)(alpha9.A * (10 - (2 * i)) / 20);
                spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect, rect, alpha9, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);




            }
            Vector2 vect2 = new Vector2(projectile.position.X + projectile.width / 2 - Main.screenPosition.X, projectile.position.Y + projectile.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0, 0, 24, 42);
            spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect2, rect2, Color.White, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }




    }
}


