using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DRGN.Items.Weapons;
using DRGN.Items;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.SummonStaves;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class FireDragon : ModNPC
    {
        private Player player;

        private const int fireBallDamage = 125;
        private const int mouthFireDamage = 150;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Dragon");
            Main.npcFrameCount[npc.type] = 11;
            
        }
        
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 580000;
            npc.damage = 75;
            npc.defense = 80;
            npc.knockBackResist = 0f;
            npc.width = 240;
            npc.height = 60;
            npc.value = 1000000;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.drippingSlime = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - set charge R to L, 1 - move, 2 - set charge L to R, 3 - move loop, 4 - set move center above player, 5 - move , 6 - drop  fireballs . repeat to half health  
           
            bossBag = mod.ItemType("DragonBossBag");
            music = MusicID.Boss3;

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("OmegaHealingPotion");
        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = DRGNModWorld.MentalMode ? 2140200 : 1044000;
            npc.damage = DRGNModWorld.MentalMode ? 122 : 90;
            npc.defense = DRGNModWorld.MentalMode ? 140 : 104;
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedDragon = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonTail"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/DragonBody"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DragonScale"), Main.rand.Next(15, 30));
                Item.NewItem(npc.getRect(), mod.ItemType("SolariumOre"), Main.rand.Next(15, 30));
                int rand = Main.rand.Next(1, 6);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonSpear>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonPick>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonWhip>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<DragonStaff>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<MagmaticHuntingRifle>()); }

            }
            else { npc.DropBossBags();  }
        }
        private void Target()
        {
            npc.TargetClosest(false);
            player = Main.player[npc.target];
           
        }
        public override void AI()
        {

            
            Target();
          

            
            
            float  moveSpeed = 5f;
          
            if (npc.ai[0] == 0 )
            { // set dash left 
                Main.npc[npc.whoAmI].modNPC.drawOffsetY = 170f;
                npc.width = 240;
                npc.height = 60;
                
                Vector2 moveTo = new Vector2(player.Center.X - 1200 ,  player.Top.Y - 200);
                
                npc.spriteDirection = -1;
                if (Move(moveTo, moveSpeed)) { npc.ai[0] = 1; }
            }
           

            if (npc.ai[0] == 1)
            {    // set dash right - at original player position
                
                
                Vector2 moveTo = new Vector2(player.Center.X + 1200, player.Top.Y);

                npc.spriteDirection = 1;
                if (Move(moveTo, moveSpeed)) { npc.ai[0] = 2; }
            }
            if (npc.ai[0] == 2)
            { // slow to middle
                Vector2 moveTo = new Vector2(player.Center.X, player.Top.Y);

                npc.spriteDirection = -1;
                if (Move(moveTo, moveSpeed)) { npc.ai[0] = 3; }
            }
            if (npc.ai[0] == 3)
            {
                npc.velocity = Vector2.Zero;
                Main.npc[npc.whoAmI].modNPC.drawOffsetY = 0f;
                npc.width = 70;
                npc.height = 250;
                
                moveSpeed = (DRGNModWorld.MentalMode ? 5 : Main.expertMode ? 4 : 3);

                npc.ai[1] = 0; npc.ai[0] = 4; int npcid = NPC.NewNPC((int)npc.Center.X,(int)npc.Center.Y - 500, mod.NPCType("MegaMagmaticCrawlerHead")); 
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
                }
                
            }
            if (NPC.AnyNPCs(mod.NPCType("MegaMagmaticCrawlerHead"))) { npc.dontTakeDamage = true; } else { npc.dontTakeDamage = false;npc.ai[1]++; }
            if (npc.ai[0] == 4)
            {
                // in middle , drop fireballs and exit after 300 ticks of no MMC
                int fireballNumber = DRGNModWorld.MentalMode ? 36 : Main.expertMode ? 46 : 56;
                if(npc.life < npc.lifeMax * 0.8f) { fireballNumber = DRGNModWorld.MentalMode ? 32 : Main.expertMode ? 42 : 52; }
                else if (npc.life < npc.lifeMax * 0.6f) { fireballNumber = DRGNModWorld.MentalMode ? 28 : Main.expertMode ? 36 : 46; }
                if (npc.life < npc.lifeMax * 0.4f) { fireballNumber = DRGNModWorld.MentalMode ? 24 : Main.expertMode ? 33 : 43; }
                if (npc.life < npc.lifeMax * 0.2f) { fireballNumber = DRGNModWorld.MentalMode ? 22 : Main.expertMode ? 31 : 39; }
                if (npc.life < npc.lifeMax * 0.1f) { fireballNumber = DRGNModWorld.MentalMode ? 20 : Main.expertMode ? 29 : 35; }
                if (npc.life < npc.lifeMax * 0.05f) { fireballNumber = DRGNModWorld.MentalMode ? 18 : Main.expertMode ? 26 : 32; }
                if (npc.life < npc.lifeMax * 0.025f) { fireballNumber = DRGNModWorld.MentalMode ? 16 : Main.expertMode ? 22 : 28; }

                if (Main.rand.Next(fireballNumber) == 1)
                {
                    int projid;
                    DespawnHandler();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int fireBall = Main.rand.Next(0, 4);

                    if (fireBall == 0)
                    { projid = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000, 0, (DRGNModWorld.MentalMode ? 14 : Main.expertMode ? 10 : 6), mod.ProjectileType("DragonFireballProjHostile"), fireBallDamage, 0); }
                    else if (fireBall == 1)
                    { projid  = Projectile.NewProjectile(player.Center.X - Main.rand.Next(-1000, 1000), npc.Center.Y + 1000, 0, (DRGNModWorld.MentalMode ? -14 : Main.expertMode ? -10 : -6), mod.ProjectileType("DragonFireballProjHostile"), fireBallDamage, 0); }
                    else if (fireBall == 2)
                    { projid = Projectile.NewProjectile(player.Center.X - 1000, npc.Center.Y + Main.rand.Next(-1000, 1000), (DRGNModWorld.MentalMode ? 14 : Main.expertMode ? 10 : 6), 0, mod.ProjectileType("DragonFireballProjHostile"), fireBallDamage, 0); }
                    else
                    { projid = Projectile.NewProjectile(player.Center.X + 1000, npc.Center.Y + Main.rand.Next(-1000, 1000), (DRGNModWorld.MentalMode ? -14 : Main.expertMode ? -10 : -6), 0, mod.ProjectileType("DragonFireballProjHostile"), fireBallDamage, 0); }
                    
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        npc.netUpdate = true;
                    }



                }
                if (npc.ai[1] >= 600 && !NPC.AnyNPCs(mod.NPCType("MegaMagmaticCrawlerHead"))) { npc.ai[0] = 5; }
                    
                
                
            }

            if (npc.ai[0] > 4)
            {
                npc.ai[0] += 1;
            }
            if (npc.ai[0] > 100)
            {
                npc.ai[0] = 0;
                
            }

            

            DespawnHandler(); // Handles if the NPC should despawn.

            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }

            // sprite animation 
            if (npc.frameCounter  == 20 && npc.ai[0] < 2 ) {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projid;
                if (npc.spriteDirection == 1)
                {
                    projid = Projectile.NewProjectile(npc.Right.X+12, npc.Bottom.Y + 25, npc.velocity.X, 5, mod.ProjectileType("DragonFireballProjHostile"),mouthFireDamage, 0);
                }
                else
                {
                    projid = Projectile.NewProjectile(npc.Left.X-12, npc.Bottom.Y + 25, npc.velocity.X, 5, mod.ProjectileType("DragonFireballProjHostile"), mouthFireDamage, 0);
                }
                
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }

            }
      
        }

       

        

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] < 2)
            {
                npc.frameCounter += 1;
                npc.frameCounter %= 40;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 8.0);  // only change frame every second tick
                npc.frame.Y = frame * 314;
            }
            else {
                npc.frameCounter += 1;
                npc.frameCounter %= 48;  // number of frames * tick count
                int frame = (int)(npc.frameCounter / 8.0) + 5 ;  // only change frame every second tick
                npc.frame.Y = frame * 314;
            }

    }
        private bool Move(Vector2 moveTo , float moveSpeed)
        {
            float speed = moveSpeed; // Sets the max speed of the npc.
            if (npc.life < npc.lifeMax / 2)
            {  // when dragon gets to half health increase speed 
                speed *= 1.8f;

            }
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > 300)
            {
                move *= speed / magnitude;
            }
            else { return true; }

            npc.velocity = move;
            return false;
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead || !player.ZoneUnderworldHeight )
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || !player.ZoneUnderworldHeight)
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