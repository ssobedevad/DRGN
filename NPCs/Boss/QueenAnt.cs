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
    public class QueenAnt : ModNPC
    {
        private float speed;
        private Player player;
        private Vector2 moveTo;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Ant");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 18000;
            npc.height = 150;
            npc.width = 88;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 20;
           
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
          
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

            npc.TargetClosest(true);
            player = Main.player[npc.target];
        }
        public override void AI()
        {

            Target();

            
            
            float moveSpeed = 5f;
            if (npc.ai[0] == 0)
            { // set dash left 


                moveTo = player.Center;
                
                
                RotateAround(moveTo);
                

            }
            
            DespawnHandler(); // Handles if the NPC should despawn.
            
        }
        private void RotateAround(Vector2 moveTo)
        {
            Vector2 moveTo2 = moveTo + new Vector2((float)(Math.Sin(npc.ai[1]) * 200)-50, (float)Math.Cos(npc.ai[1]) * 200);
            Vector2 move = moveTo2 - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
            npc.ai[1] += 0.025f;
        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 20;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * 150;

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