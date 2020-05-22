using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class IceFish : ModNPC
    {
        private float speed;
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Fish");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 18000;
            npc.height = 100;
            npc.width = 202;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 20;
            npc.scale = 1.5f;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0;
           
           
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.125f * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.3f);
            npc.defense = (int)(npc.defense * 1.4f);
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
            
            Target();
            
            Vector2 moveTo = new Vector2(0, 0);
            float moveToX = npc.ai[1];
            float moveToY = npc.ai[2];
            float moveSpeed = 5f;
            if (npc.ai[0] == 0)
            { // set dash left 
               
               
                moveToX = player.Center.X - 1200;
                moveToY = player.Center.Y + Main.rand.Next(-40,40);
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = -1;
                
            }
            if (npc.ai[0] == 1 || npc.ai[0] == 3)
            {
                // test where reached limit 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                DespawnHandler();
                moveSpeed = (float)Main.rand.Next(15, 20);
                
                if (TestMoveTo(moveTo, moveSpeed)) { npc.ai[0] += 1f; }
                
            }
            if (npc.ai[0] == 2)
            {    // set dash right - at original player position
                moveToX = player.Center.X + 1200;
                moveToY = player.Center.Y + Main.rand.Next(-40, 40);
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = 1;
               
            }
            if (npc.ai[0] == 4)
            { // set dash left 


                moveToX = player.Center.X - 200;
                moveToY = player.Center.Y + Main.rand.Next(-10, 10);
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = -1;

            }
            if (npc.ai[0] == 6)
            {    // set dash right - at original player position
                moveToX = player.Center.X + 200;
                moveToY = player.Center.Y + Main.rand.Next(-10, 10);
                moveTo = new Vector2(moveToX, moveToY);
                SetDash(moveTo);
                npc.spriteDirection = 1;

            }
            if (npc.ai[0] == 5 || npc.ai[0] == 7)
            {
                // test where reached limit 
                moveTo = new Vector2(npc.ai[1], npc.ai[2]);
                DespawnHandler();
                moveSpeed = (float)Main.rand.Next(15, 35);
                if (TestMoveTo(moveTo, moveSpeed)) { npc.ai[0] += 1f; }

            }
            if (npc.ai[0] == 8) { npc.ai[0] = 0; }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(0, 20 + (int)(npc.life / 1000)) == 1 ) { Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X, npc.velocity.Y + 10f, mod.ProjectileType("IceCluster"), npc.damage / 2, 0); }
            }
            else { if (Main.rand.Next(0, 75 + (int)(npc.life/1000)) == 1) { Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X, npc.velocity.Y + 10f, mod.ProjectileType("IceCluster"), npc.damage / 3, 0); } }
            DespawnHandler(); // Handles if the NPC should despawn.
            moveTo = new Vector2(npc.ai[1], npc.ai[2]);
            Move(moveTo, moveSpeed); // Calls the Move Method
        }
        private void SetDash(Vector2 moveTo)
        {
            npc.ai[0] += 1f;
            npc.ai[1] = moveTo.X;
            npc.ai[2] = moveTo.Y;
            npc.velocity = new Vector2(0, 0);
        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 49;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 7.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * 144;

        }
        public override void NPCLoot()
        {
            DRGNModWorld.downedIceFish = true;
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishHead"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishTail"), 1f);
            Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/IceFishBody"), 1f);
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialShard"), 10);
                Item.NewItem(npc.getRect(), mod.ItemType("GlacialOre"), 20);
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("IceSpear")); }
            }
            else { Item.NewItem(npc.getRect(), mod.ItemType("FishBossBag")); }
            
            
        }
        private bool TestMoveTo(Vector2 moveTo, float speed)
        {

            return (Math.Abs(npc.Center.X - moveTo.X) < speed || (npc.velocity.X > 0 && npc.Center.X > moveTo.X) || (npc.velocity.X < 0 && npc.Center.X < moveTo.X));
        }
        private void Move(Vector2 moveTo, float moveSpeed)
        {
            speed = moveSpeed; // Sets the max speed of the npc.
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
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