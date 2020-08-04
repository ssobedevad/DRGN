
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.SummonStaves;
using DRGN.Items.Weapons.Whips;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class DragonFly : ModNPC
    {
        
        private Player player;
        private float speed;
        private Vector2 target;
        
        private int shootCD;
        private const int blueFireDamage = 110;
        private const int homingMissileDamage = 65;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fly");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 230000;
            npc.height = 80;
            npc.width = 400;
            npc.aiStyle = -1;
            npc.damage = 56;
            npc.defense = 63;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            drawOffsetY = 90f;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
            shootCD = 100;
            bossBag = mod.ItemType("DragonFlyBossBag");

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("MegaHealingPotion");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = DRGNModWorld.MentalMode ? 782000 : 391000;
            npc.damage = DRGNModWorld.MentalMode ? 90 : 68;
            npc.defense = DRGNModWorld.MentalMode ? 105 : 82;
        }
        private void Target()
        {

            npc.TargetClosest(true);
            player = Main.player[npc.target];
            
        }
        public override void AI()
        {
            
            if (npc.ai[0] >= 4)
            {
                

                    if (Main.rand.Next(0, (DRGNModWorld.MentalMode ? 60 : Main.expertMode ? 90 : 120)) == 0)

                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.Next(-500, 500), -1000), new Vector2(Main.rand.Next(-5, 5), (DRGNModWorld.MentalMode ? 8 : Main.expertMode ? 6 : 4)), mod.ProjectileType("BlueFireball"), blueFireDamage, 0f);
                        int projid2 = Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("BlueFireMeteor"), homingMissileDamage, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid,projid2);
                    }
                }
            }
            Target();
            
            if (npc.ai[0] == 0)
            { target = player.Center + new Vector2(1000, -500); move();npc.ai[0] = 1;npc.spriteDirection = npc.direction; }
            else if (npc.ai[0] == 1)
            { if (move()) { npc.ai[0] = 2; }
                if (shootCD > 0) { shootCD -= 1; }
                if (shootCD == 0)
                {
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center, new Vector2(0, 5), mod.ProjectileType("BlueFireball"), blueFireDamage, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                    shootCD = (DRGNModWorld.MentalMode ? 26 : Main.expertMode ? 32 : 40); ;


                }
            }
            else if (npc.ai[0] == 2)
            { target = player.Center + new Vector2(-1000, -500); move(); npc.ai[0] = 3; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 3)
            { 
                if (shootCD > 0) { shootCD -= 1; }
                if (shootCD == 0) 
                { 
               
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center, new Vector2(0, 5), mod.ProjectileType("BlueFireball"), blueFireDamage, 0f);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                    shootCD = (DRGNModWorld.MentalMode ? 26 : Main.expertMode ? 32 : 40);
                       
                    
                } 
                if (move()) { npc.ai[0] = 4; }
             }
            else if(npc.ai[0] == 4)
            { target = player.Center + new Vector2(-1000, Main.rand.Next(-5, 5)); move(); npc.ai[0] = 5; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 5)
            { if (move()) { npc.ai[0] = 6; } }
            else if(npc.ai[0] == 6)
            { target = player.Center + new Vector2(1000, Main.rand.Next(-5, 5)); DashTo(); npc.ai[0] = 7; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 7)
            {
                
                if (DashTo()) { npc.ai[0] = 8; }
            }
            else if(npc.ai[0] == 8)
            { target = player.Center + new Vector2(-1000, Main.rand.Next(-5, 5)); DashTo(); npc.ai[0] = 9; npc.spriteDirection = npc.direction; }
            else if(npc.ai[0] == 9)
            {
                
                if (DashTo()) { npc.ai[0] = 0 ; }
                
            }


            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }
            DespawnHandler(); // Handles if the NPC should despawn.

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("BrokenWings"), (DRGNModWorld.MentalMode ? 30 : 60));

        }
        private bool move()
        {

            speed = (DRGNModWorld.MentalMode ? 12 : Main.expertMode ? 10 : 8);
           
            Vector2 moveVel = (target - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
               
              
            }
            else { return true; }
            npc.velocity = moveVel;
            return false;

        }
        private bool DashTo()
        {
            speed = (DRGNModWorld.MentalMode ? 45 : Main.expertMode ? 35 : 25);
            Vector2 moveVel = (target - npc.Center);
            float magnitude = Magnitude(moveVel);
            if(magnitude > speed)
            {
                moveVel *= speed / magnitude;


            }
            else { return true; }



            npc.velocity = moveVel;
            return false;

        }

        public override void FindFrame(int frameHeight)
        {
           
                npc.frameCounter += 1;
                npc.frameCounter %= 20;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
                if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
                npc.frame.Y = frame * 272;
            
            
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedDragonFly = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyBody"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonFlyTail"), 1f);
            if (!Main.expertMode)
            {
                
                Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyDust"), 20);
                Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyWing"), 20);
                 Item.NewItem(npc.getRect(), mod.ItemType("GalacticEssence"),Main.rand.Next(1,4));
                int rand = Main.rand.Next(1, 5);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<TheDragonFly>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonFlySlicer>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonFlyWhip>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonFlyStaff>()); }
            }
            else { npc.DropBossBags(); }


        }


        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        private void DespawnHandler()
        {
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
    }
}