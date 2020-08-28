using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN
{

    public class YoyoAI : GlobalProjectile
    {        
        public override bool PreAI(Projectile projectile)
        {                      
            if (projectile.aiStyle == 99 && !ModContent.GetInstance<DRGNConfig>().DisableYoyoAI)
            {               
                if (projectile.owner == Main.myPlayer)
                {                   
                    int leftButton = (Mouse.GetState().LeftButton == ButtonState.Pressed) ? 1 : -1;
                    int rightButton = (Mouse.GetState().RightButton == ButtonState.Pressed) ? 1 : -1;
                    if (leftButton != projectile.ai[0] || rightButton != projectile.ai[1])
                    {
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.whoAmI);
                        projectile.netUpdate = true;
                        projectile.ai[0] = leftButton * Main.MouseWorld.X;
                        projectile.ai[1] = rightButton * Main.MouseWorld.Y;
                    }
                }
                projectile.tileCollide = false;
                Player player = Main.player[projectile.owner];
                float thisNum = 1f;
                float totalProjs = 1f;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle ==  99)
                    {
                        if (i < projectile.whoAmI)
                        {
                            thisNum += 1f;
                            projectile.localAI[0] = Main.projectile[i].localAI[0];                            
                        }
                        totalProjs += 1f;
                    }
                }
                float increment = (thisNum / totalProjs) * 6.28f;
                player.itemAnimation = 1;
                player.heldProj = projectile.whoAmI;
                int Timeleft = (int)(ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] * 80);
                float range = ProjectileID.Sets.YoyosMaximumRange[projectile.type];
                if (player.yoyoString) { range = range * 1.25f + 30f; }
                if (player.GetModPlayer<DRGNPlayer>().SuperYoyoBag && projectile.counterweight) { range = range * 1.75f + 40f; }
                float topSpeed = ProjectileID.Sets.YoyosTopSpeed[projectile.type];
                if (player.GetModPlayer<DRGNPlayer>().SuperYoyoBag) { topSpeed *= 1.25f; }

                if (Timeleft < -1 || player.GetModPlayer<DRGNPlayer>().SuperYoyoBag) { projectile.timeLeft = 6; }
                else
                {
                    if (projectile.timeLeft > Timeleft) { projectile.timeLeft = Timeleft; }
                    else if (projectile.timeLeft <= 5) { Retract(projectile, player, range); projectile.timeLeft = 4; }
                }
                if ((projectile.ai[0] <= 0 && projectile.ai[1] <= 0) || player.dead || (Vector2.Distance(player.Center, projectile.Center) > range * 1.5f)) { Retract(projectile, player, range); }
                player.bodyFrame.Y = player.bodyFrame.Height * 3;

                if (projectile.ai[0] > 0 && projectile.ai[1] > 0)
                { Oval(projectile, topSpeed, range, increment); }

                else if (projectile.ai[0] <= 0 && projectile.ai[1] > 0)
                { Spin(projectile, topSpeed, range, increment); }
                else
                { Idle(projectile, player, topSpeed, range); }
                if (projectile.ai[1] <= 0)
                {
                    projectile.localAI[0] = 0;
                }
                if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
                {
                    player.ChangeDir(1);
                    projectile.direction = 1;
                }
                else
                {
                    player.ChangeDir(-1);
                    projectile.direction = -1;
                }
                if (projectile.type == 603 && projectile.owner == Main.myPlayer)
                {
                    projectile.localAI[1] += 1f;
                    if (projectile.localAI[1] >= 6f)
                    {
                        float num3 = 400f;
                        Vector2 velocity = projectile.velocity;
                        Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(10, 41) * 0.1f;
                        if (Main.rand.Next(3) == 0)
                        {
                            vector *= 2f;
                        }
                        velocity *= 0.25f;
                        velocity += vector;
                        for (int j = 0; j < 200; j++)
                        {
                            if (Main.npc[j].CanBeChasedBy(this))
                            {
                                float num4 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
                                float num5 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
                                float num6 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num4) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num5);
                                if (num6 < num3 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                                {
                                    num3 = num6;
                                    velocity.X = num4;
                                    velocity.Y = num5;
                                    velocity -= projectile.Center;
                                    velocity.Normalize();
                                    velocity *= 8f;
                                }
                            }
                        }
                        velocity *= 0.8f;
                        Projectile.NewProjectile(projectile.Center.X - velocity.X, projectile.Center.Y - velocity.Y, velocity.X, velocity.Y, ProjectileID.TerrarianBeam, projectile.damage, projectile.knockBack, projectile.owner);
                        projectile.localAI[1] = 0f;
                        projectile.netUpdate = true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;

        }
        private float GetVelMult(float dist, float rotSpeed, bool Flipped = false)
        {
            if (dist < 100f) { return rotSpeed; }
            rotSpeed *= (100f + (dist / 4)) / dist;
            if (Flipped) { rotSpeed *= -1f; }



            return rotSpeed;
        }
       
        public void Retract(Projectile proj, Player player, float range)
        {
            Vector2 restingPoint = player.Center;
            proj.rotation += 0.45f;
            Vector2 diff = restingPoint - proj.Center;
            float Mag = Vector2.Distance(restingPoint, proj.Center);
            if (Mag < 20)
            {
                proj.Kill(); player.itemAnimation = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                    {
                        if (Main.projectile[i].counterweight)
                        {
                            Main.projectile[i].Kill();
                        }
                    }
                }
            }
            if (Vector2.Distance(player.Center, proj.Center) > range * 1.5f)
            {
                if (Mag > 150) { diff *= 150 / Mag; }
            }
            else
            {
                if (Mag > 50) { diff *= 50 / Mag; }
            }
            player.itemAnimation = 1;

            proj.velocity = (proj.velocity * 10f + diff) / 11f;
        }
        public void Idle(Projectile proj, Player player, float topSpeed, float maxRange)
        {
            if (proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
            float YDiff = Math.Abs(proj.ai[1]) - player.Center.Y;
            if (YDiff < 5) { YDiff = 5; }
            if (YDiff > maxRange) { YDiff = maxRange; }
            float ProjYDiff = proj.Center.Y - player.Center.Y;
            if (ProjYDiff > maxRange / 1.8f) { proj.velocity.Y *= 0.9f; }
            Vector2 restingPoint = player.Center + new Vector2(10 * player.direction + Main.rand.Next(-3, 4), YDiff + Main.rand.Next(-3, 4));
            proj.rotation += 0.45f;
            Vector2 diff = restingPoint - proj.Center;
            float Mag = Vector2.Distance(restingPoint, proj.Center);
            diff *= topSpeed / Mag;
            proj.velocity = (proj.velocity * 40f + diff) / 41f;
        }
        public void Spin(Projectile proj, float topSpeed, float maxRange, float inc)
        {
            Player player = Main.player[proj.owner];
            if (proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
            float extensionLength = Vector2.Distance(new Vector2(Math.Abs(proj.ai[0]),Math.Abs(proj.ai[1])), player.Center);
            if (extensionLength > maxRange) { extensionLength = maxRange; }
            float rotationSpeed = 0.25f;
            proj.localAI[0] += GetVelMult(extensionLength, rotationSpeed);
            Vector2 restingPoint = player.Center + new Vector2((float)Math.Sin(proj.localAI[0] + inc) * extensionLength + (10 * player.direction), (float)Math.Cos(proj.localAI[0] + inc) * extensionLength);
            proj.rotation += 0.2f;
            proj.Center = restingPoint;
        }
        public void Oval(Projectile proj, float topSpeed, float maxRange, float inc)
        {
            Player player = Main.player[proj.owner];
            int MW = Math.Abs(proj.ai[0]) > player.Center.X ? -1 : 1;
            if (proj.velocity.Length() > topSpeed) { proj.velocity = Vector2.Normalize(proj.velocity) * topSpeed; }
            float extensionLength = Vector2.Distance(new Vector2(Math.Abs(proj.ai[0]), Math.Abs(proj.ai[1])), player.Center) / 2;
            if (extensionLength > maxRange * 0.55f) { extensionLength = maxRange * 0.55f; }
            inc *= MW;
            float rotationSpeed = 0.4f;
            proj.localAI[0] += GetVelMult(extensionLength, rotationSpeed, MW == -1);
            Vector2 SpinnyPlace = new Vector2((float)Math.Sin(proj.localAI[0] + inc) * extensionLength + (extensionLength), (float)Math.Cos(proj.localAI[0] + inc) * (extensionLength / 3));
            float rotatyBoi = (new Vector2(Math.Abs(proj.ai[0]), Math.Abs(proj.ai[1])) - player.Center).ToRotation();
            SpinnyPlace = DavesUtils.Rotate(SpinnyPlace, rotatyBoi);
            SpinnyPlace += player.Center;
            proj.rotation += 0.2f;
            proj.Center = SpinnyPlace;
        }
        public static Color TryApplyingPlayerStringColor(int playerStringColor, Microsoft.Xna.Framework.Color stringColor)
        {
            if (playerStringColor > 0)
            {
                stringColor = WorldGen.paintColor(playerStringColor);
                if (stringColor.R < 75)
                {
                    stringColor.R = 75;
                }
                if (stringColor.G < 75)
                {
                    stringColor.G = 75;
                }
                if (stringColor.B < 75)
                {
                    stringColor.B = 75;
                }
                switch (playerStringColor)
                {
                    case 13:
                        stringColor = new Color(20, 20, 20);
                        break;
                    case 0:
                    case 14:
                        stringColor = new Color(200, 200, 200);
                        break;
                    case 28:
                        stringColor = new Color(163, 116, 91);
                        break;
                    case 27:
                        stringColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                        break;
                }
                stringColor.A = (byte)((float)(int)stringColor.A * 0.4f);
            }
            return stringColor;
        }      
        public static void DrawString(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.MountedCenter + new Vector2(player.direction * 10, -10);
            vector.Y += player.gfxOffY;
            float num2 = projectile.Center.X - vector.X;
            float num3 = projectile.Center.Y - vector.Y;
            if (!projectile.counterweight)
            {
                int num5 = -1;
                if (projectile.Center.X < player.Center.X)
                {
                    num5 = 1;
                }
                num5 *= -1;
                player.itemRotation = (float)Math.Atan2(num3 * (float)num5, num2 * (float)num5);
            }
            bool flag = true;
            if (num2 == 0f && num3 == 0f)
            {
                flag = false;
            }
            else
            {
                float num6 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                num6 = 12f / num6;
                num2 *= num6;
                num3 *= num6;
                vector.X -= num2 * 0.1f;
                vector.Y -= num3 * 0.1f;
                num2 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
                num3 = projectile.position.Y + (float)projectile.height * 0.5f - vector.Y;
            }
            while (flag)
            {
                float speed = 12f;
                float mag = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                float mag2 = mag;
                if (float.IsNaN(mag) || float.IsNaN(mag2))
                {
                    flag = false;
                    continue;
                }
                if (mag < 20f)
                {
                    speed = mag - 8f;
                    flag = false;
                }
                mag = 12f / mag;
                num2 *= mag;
                num3 *= mag;
                vector.X += num2;
                vector.Y += num3;
                num2 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
                num3 = projectile.position.Y + (float)projectile.height * 0.1f - vector.Y;
                if (mag2 > 12f)
                {
                    float num10 = 0.3f;
                    float num11 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                    if (num11 > 16f)
                    {
                        num11 = 16f;
                    }
                    num11 = 1f - num11 / 16f;
                    num10 *= num11;
                    num11 = mag2 / 80f;
                    if (num11 > 1f)
                    {
                        num11 = 1f;
                    }
                    num10 *= num11;
                    if (num10 < 0f)
                    {
                        num10 = 0f;
                    }
                    num10 *= num11;
                    num10 *= 0.5f;
                    if (num3 > 0f)
                    {
                        num3 *= 1f + num10;
                        num2 *= 1f - num10;
                    }
                    else
                    {
                        num11 = Math.Abs(projectile.velocity.X) / 3f;
                        if (num11 > 1f)
                        {
                            num11 = 1f;
                        }
                        num11 -= 0.5f;
                        num10 *= num11;
                        if (num10 > 0f)
                        {
                            num10 *= 2f;
                        }
                        num3 *= 1f + num10;
                        num2 *= 1f - num10;
                    }
                }
                float Rotation = (float)Math.Atan2(num3, num2) - 1.57f;
                Color white = Color.White;
                white.A = (byte)((float)(int)white.A * 0.4f);
                white = TryApplyingPlayerStringColor(player.stringColor, white);
                float num12 = 0.8f * (Vector2.Distance(player.Center, vector) / (Vector2.Distance(player.Center, projectile.Center) * 1.4f));
                white = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), white);
                Texture2D text = Main.fishingLineTexture;
                Color color = new Color((byte)((float)(int)white.R * num12), (byte)((float)(int)white.G * num12), (byte)((float)(int)white.B * num12), (byte)((float)(int)white.A * num12));
                Vector2 pos = new Vector2(vector.X - Main.screenPosition.X + (float)text.Width * 0.5f, vector.Y - Main.screenPosition.Y + (float)text.Height * 0.5f) - new Vector2(6f, 0f);
                Main.spriteBatch.Draw(text, pos, new Rectangle(0, 0, text.Width, (int)speed), color, Rotation, new Vector2((float)text.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0);
            }
        }

    }

}