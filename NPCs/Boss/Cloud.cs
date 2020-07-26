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
    public class Cloud : ModNPC
    {
        private Player player;
        private const int laserDamage = 45;
        private const int rainDamage = 65;
        private const int lightningDamage = 35;


        private int[] proj = new int[4] {-1,-1,-1,-1 };

       

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big angry nimbus");
            Main.npcFrameCount[npc.type] = 4;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 52000;
            npc.damage = 48;
            npc.defense = 30;
            npc.knockBackResist = 0f;
            npc.width = 422;
            npc.height = 196;
            npc.value = 100000;
            npc.npcSlots = 5f;
            npc.boss = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.ai[0] = 0;  // phase  0 - asleep , 1 - happy, 2 sad ,   3 angry. repeat   1,2,3 not 0.
            npc.ai[1] = 0;
            npc.ai[2] = 0;
           
            music = MusicID.Boss1;
            bossBag = mod.ItemType("CloudBossBag");

        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.6f);
            npc.damage = (int)(npc.damage * 1.2f);
            npc.defense = (int)(npc.defense * 1.2f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedCloud = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudEyeRight"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudEyeLeft"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/CloudMouth"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("CosmoOre"), Main.rand.Next(15, 25));

                int rand = Main.rand.Next(1, 6);
                if (rand == 1)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<CloudStaff>()); }
                else if (rand == 2)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<CosmoBlade>()); }
                else if (rand == 3)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<CosmoSpear>()); }
                else if (rand == 4)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<CosmoWhip>()); }
                else if (rand == 5)
                { Item.NewItem(npc.getRect(), ModContent.ItemType<ElectroStaff>()); }


            }

            else { npc.DropBossBags(); }
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        private bool moveOn(int delayCounter, int maxDelayCounter)
        { 
        if(delayCounter >= maxDelayCounter)
            { return true; }
         return false; 
        
        }
        public override void AI()
        {

            

            Target();

            
           



            if (npc.ai[0] == 0)
            {
                int delay = DRGNModWorld.MentalMode ? 60 : Main.expertMode ? 120 : 180;
                npc.ai[2] += 1;
                if(moveOn((int)npc.ai[2] , delay)) { npc.ai[0] = 1;npc.ai[2] = 0; }
                Move(0f);

            }
            if (npc.ai[0] == 1)
            {
                if (proj[0] == -1)
                {


                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        proj[0] = Projectile.NewProjectile(npc.Center, new Vector2(0, 14), mod.ProjectileType("SunRayHostile"), laserDamage, 0f, 0, (float)npc.whoAmI);
                        proj[1] = Projectile.NewProjectile(npc.Center, new Vector2(14, 0), mod.ProjectileType("SunRayHostile"), laserDamage, 0, 0, (float)npc.whoAmI);
                        proj[2] = Projectile.NewProjectile(npc.Center, new Vector2(0, -14), mod.ProjectileType("SunRayHostile"), laserDamage, 0, 0, (float)npc.whoAmI);
                        proj[3] = Projectile.NewProjectile(npc.Center, new Vector2(-14, 0), mod.ProjectileType("SunRayHostile"), laserDamage, 0, 0, (float)npc.whoAmI);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, proj[0], proj[1], proj[2], proj[3]);
                    }

                }
               npc.ai[2] += 1;
                if (npc.ai[2] >= 350) { npc.ai[0] = 2;npc.ai[2] = 0;Main.projectile[proj[0]].ai[0] = -1; Main.projectile[proj[1]].ai[0] = -1; Main.projectile[proj[2]].ai[0] = -1; Main.projectile[proj[3]].ai[0] = -1; proj[0] = -1; }
                float speed = DRGNModWorld.MentalMode ? 7f : Main.expertMode ? 5f : 2f;
                Move(speed);
            }

            if (npc.ai[0] == 2 )
            {
                float speed = DRGNModWorld.MentalMode ? 30f : Main.expertMode ? 20f : 10f;
                Move(speed);
                if (Main.rand.Next(0, DRGNModWorld.MentalMode ? 4 : Main.expertMode ? 5 : 6) == 1)
                {
                   
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Bottom.Y, 0, (DRGNModWorld.MentalMode ? 8 : Main.expertMode ? 6 : 4), mod.ProjectileType("Rain"), rainDamage, 0);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                }
               npc.ai[2] += 1;
                if (npc.ai[2] >= 250) { npc.ai[0] = 3;npc.ai[2] = 0; }
            }

            if (npc.ai[0] == 3)
            {
                float speed = DRGNModWorld.MentalMode ? 15f : Main.expertMode ? 10f : 5f;
                Move(speed);
               npc.ai[2] += 1;
                if (npc.ai[2] % (DRGNModWorld.MentalMode ? 35 : Main.expertMode ? 45 : 55) == 1) 
                { 
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Bottom - (npc.velocity)*16 , new Vector2(0, 500f), mod.ProjectileType("Lightning"), lightningDamage, 1f, 255, (float)npc.whoAmI, 2);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                }
                if (npc.ai[2] % (DRGNModWorld.MentalMode ? 5 : Main.expertMode ? 10 : 15) == 1)
                {
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projid = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Bottom.Y, 0, (DRGNModWorld.MentalMode ? 8 : Main.expertMode ? 6 : 4), mod.ProjectileType("Rain"), rainDamage, 0);
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                    }
                    
                }

                if (npc.ai[2] == 250) { npc.ai[0] = 1;npc.ai[2] = 0; }
            }
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }
            DespawnHandler(); // Handles if the NPC should despawn.

        }





        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0)
            {
              
               
                npc.frame.Y = 0;
            }
            else if (npc.ai[0] == 1)
            {

                
                npc.frame.Y = 196;
            }
            
            else if (npc.ai[0] == 2 )
            {
              
                npc.frame.Y = 2 * 196;
            }
            
            else if (npc.ai[0] == 3 )
            {
                npc.frameCounter += 1;

               
                npc.frame.Y = 3 * 196;
            }

        }
        private void Move( float speed)
        {
            // Sets the max speed of the npc.
            Vector2 moveTo = Main.player[npc.target].Center + new Vector2(0 , -400);
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
            
        }


        private float Magnitude(Vector2 mag)
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


                }
            }
        }
    }
}