using DRGN.Items;
using DRGN.Items.Equipables;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class Crystil : ModNPC
    {
        private const int CrystilDamage = 50;
        private Vector2 MoveTo;        
        private Vector2[] oldPos = new Vector2[9] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystil");
        }
        public override void SetDefaults()
        {
            npc.lifeMax = DRGNModWorld.MentalMode ? 225000 : Main.expertMode ? 185000 : 175000;
            npc.damage = DRGNModWorld.MentalMode ? 78 : Main.expertMode ? 65 : 54;
            npc.defense = DRGNModWorld.MentalMode ? 80 : Main.expertMode ? 70 : 60;
            npc.height = 104;
            npc.width = 112;
            npc.aiStyle = -1;
            npc.value = 300000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            bossBag = mod.ItemType("CrystilBossBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            return;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)MoveTo.X);
            writer.Write((int)MoveTo.Y);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            MoveTo.X = reader.ReadInt32();
            MoveTo.Y = reader.ReadInt32();
        }
        private void CrystilCircle(int num = 10, float startRotaion = 0f, int type = -1)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                float speed = DRGNModWorld.MentalMode ? 12f : Main.expertMode ? 10f : 8f;
                float inc = MathHelper.ToRadians(360 / num);
                Vector2 vel = DavesUtils.Rotate(new Vector2(0, -speed), startRotaion);
                for (int i = 0; i < num; i++)
                {
                    int projid = Projectile.NewProjectile(npc.Center, DavesUtils.Rotate(vel, inc * i), type == -1? mod.ProjectileType("GiantCrystil") : type, CrystilDamage, 0f, Main.myPlayer);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
            }
        }
        private void CrystilLasersFromNPC(Player player,int num = 3)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {               
                Vector2 vel = Vector2.Normalize(player.Center - npc.Center) * 16;                
                for (int i = 0; i < num; i++)
                {
                    int projid = Projectile.NewProjectile(npc.Center, DavesUtils.Rotate(vel, MathHelper.ToRadians(10 * (i - num/2))), mod.ProjectileType("CrystilWarning"), CrystilDamage, 0f, Main.myPlayer,npc.whoAmI);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
            }
        }
        private void CrystilLasersSide(Player player, int num = 7, Vector2 CenterPos = new Vector2(), float decrementY = 100, bool left = true)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = new Vector2(left? 16 : -16, 0);
                for (int i = 0; i < num; i++)
                {
                    int projid = Projectile.NewProjectile(CenterPos + new Vector2(1500 * (left? -1 : 1 ),decrementY * i - (num * decrementY/2)),vel, mod.ProjectileType("CrystilWarning"), CrystilDamage, 0f, Main.myPlayer, -1);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
            }
        }
        private void CrystilLasersTop(Player player, int num = 7, Vector2 CenterPos = new Vector2(), float decrementX = 100, bool top = true)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = new Vector2( 0, top ? 16 : -16);
                for (int i = 0; i < num; i++)
                {
                    int projid = Projectile.NewProjectile(CenterPos + new Vector2(decrementX * i - (num * decrementX / 2) , 1200 * (top ? -1 : 1)), vel, mod.ProjectileType("CrystilWarning"), CrystilDamage, 0f, Main.myPlayer, -1);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
            }
        }     
        public override void AI()
        {
            for (int i = 8; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = npc.Center; }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }
                if (oldPos[i] == Vector2.Zero) { oldPos[i] = npc.Center; }
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.ai[2] == 0)
            {
                float speed = 12f;
                if (npc.ai[0] == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(); CrystilLasersFromNPC(player, 5); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] >= 60) { npc.ai[1] = 0; npc.ai[0] = 1; }
                    }
                }
                else if (npc.ai[0] == 1)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 20 == 0) { CrystilCircle(10, MathHelper.ToRadians(npc.ai[1] / 2)); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 2; }
                    }
                }
                else if (npc.ai[0] == 2)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 10 == 0) { CrystilCircle(4, MathHelper.ToRadians(npc.ai[1])); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 0; npc.ai[2] = npc.life > npc.lifeMax * 0.75 ? 0 : 1; CrystilLasersSide(player, 7, MoveTo, 200, Main.rand.NextBool()); CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); }
                    }
                }
            }
            else if (npc.ai[2] == 1)
            {
                float speed = 14f;
                if (npc.ai[0] == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilLasersTop(player, 7, MoveTo, 200, Main.rand.NextBool()); CrystilLasersSide(player, 7, MoveTo, 200, Main.rand.NextBool()); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] >= 120) { npc.ai[1] = 0; npc.ai[0] = 1; CrystilLasersFromNPC(player, 10); }
                    }
                }
                else if (npc.ai[0] == 1)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 20 == 0) { CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); CrystilLasersFromNPC(player, 1); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 2; }
                    }
                }
                else if (npc.ai[0] == 2)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; } }
                    else
                    {
                        npc.ai[1]++;                     
                        if (npc.ai[1] >= 60) { npc.ai[1] = 0; npc.ai[0] = 0; npc.ai[2] = npc.life > npc.lifeMax * 0.5 ? 1 : 2; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); }
                    }
                }
            }
            else if (npc.ai[2] == 2)
            {
                float speed = 16f;
                if (npc.ai[0] == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] >= 120) { npc.ai[1] = 0; npc.ai[0] = 1; CrystilLasersFromNPC(player, 10); }
                    }
                }
                else if (npc.ai[0] == 1)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 20 == 0) { CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); CrystilLasersFromNPC(player, 1); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 2; CrystilLasersFromNPC(player, 10); }
                    }
                }
                else if (npc.ai[0] == 2)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 10 == 0) { CrystilLasersFromNPC(player, 3); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 3; }
                    }
                }
                else if (npc.ai[0] == 3)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1]), mod.ProjectileType("CrystilBouncer")); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] >= 60) { npc.ai[1] = 0; npc.ai[0] = 0; npc.ai[2] = npc.life > npc.lifeMax * 0.25 ? 2 : 3; CrystilLasersTop(player, 12, MoveTo, 150, Main.rand.NextBool()); CrystilLasersSide(player, 12, MoveTo, 150, Main.rand.NextBool()); }
                    }
                }
            }
            else if (npc.ai[2] == 3)
            {
                float speed = 16f;
                if (npc.ai[0] == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); }  }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 20 == 0) { CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); CrystilLasersFromNPC(player, 2); }
                        if (npc.ai[1] >= 120) { npc.ai[1] = 0; npc.ai[0] = 1; CrystilLasersFromNPC(player, 10); }
                    }
                }
                else if (npc.ai[0] == 1)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); }  }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 20 == 0) { CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); CrystilLasersFromNPC(player, 2); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 2; CrystilLasersFromNPC(player, 10); }
                    }
                }
                else if (npc.ai[0] == 2)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); } }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] % 10 == 0) { CrystilLasersFromNPC(player, 3); }
                        if (npc.ai[1] >= 180) { npc.ai[1] = 0; npc.ai[0] = 3; }
                    }
                }
                else if (npc.ai[0] == 3)
                {
                    if (npc.ai[1] == 0)
                    {
                        MoveTo = player.Center;
                        npc.ai[1] = 1;
                    }
                    else if (npc.ai[1] == 1) { if (Move(speed)) { npc.ai[1] = 2; npc.velocity = Vector2.Zero; CrystilCircle(6, MathHelper.ToRadians(npc.ai[1])); }  }
                    else
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] >= 30) { npc.ai[1] = 0; npc.ai[0] = 0; npc.ai[2] = 3; CrystilLasersTop(player, 12, MoveTo, 150, Main.rand.NextBool()); CrystilLasersSide(player, 12, MoveTo, 150, Main.rand.NextBool()); }
                    }
                }
            }
            DespawnHandler();
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedTheVirus = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)), mod.GetGoreSlot("Gores/CrystilLeft"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)), mod.GetGoreSlot("Gores/CrystilRight"), 1f);            
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("CrystilOre"), Main.rand.Next(20, 40));

                int rand = Main.rand.Next(1, 11);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilBow")); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilShuriken")); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilSword")); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilDagger")); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilFlyingKnife")); }
                else if (rand == 6)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilHooks")); }
                else if (rand == 7)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilKnives")); }
                else if (rand == 8)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrystilScythe")); }
                else if (rand == 9)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrsytilStaff")); }
                else if (rand == 10)
                { Item.NewItem(npc.getRect(), mod.ItemType("CrsytilTome")); }

            }
            else { npc.DropBossBags(); }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("MegaHealingPotion");
        }
        private bool Move(float moveSpeed)
        {
            Vector2 moveTo2 = MoveTo - npc.Center;
            float magnitude = moveTo2.Length();
            if (magnitude > moveSpeed * 2)
            {
                moveTo2 *= moveSpeed / magnitude;
            }
            else { return true; }
            npc.velocity = (npc.velocity * 10f + moveTo2) / 11f;
            return false;
        }       
        private Vector2 ShootAtPlayer(float shootSpeed)
        {
            Vector2 moveTo2 = Main.player[npc.target].Center - npc.Center;
            float magnitude = moveTo2.Length();
            moveTo2 *= shootSpeed / magnitude;
            return moveTo2;
        }
        private void DespawnHandler()
        {
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 2)
                    {
                        npc.timeLeft = 2;
                    }
                    return;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int i = 8; i >= 0; i -= 2)
            {
                Vector2 oldV = oldPos[i];
                Vector2 vect = new Vector2(oldV.X - Main.screenPosition.X, oldV.Y - Main.screenPosition.Y);
                Rectangle rect = npc.frame;

                Color alpha9 = lightColor;
                alpha9.R = (byte)(alpha9.R * (30 - (3 * i)) / 30);
                alpha9.G = (byte)(alpha9.G * (30 - (3 * i)) / 30);
                alpha9.B = (byte)(alpha9.B * (30 - (3 * i)) / 30);
                alpha9.A = (byte)(alpha9.A * (30 - (3 * i)) / 30);
                spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect, rect, alpha9, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);
            }
            Vector2 vect2 = new Vector2(npc.position.X + npc.width / 2 - Main.screenPosition.X, npc.position.Y + npc.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = npc.frame;
            spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect2, rect2, lightColor, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }
    }
}