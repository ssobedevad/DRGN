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

namespace DRGN.NPCs
{
    public class Ant : ModNPC
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.height = 26;
            npc.width = 52;
            npc.aiStyle = 3;
            npc.damage = 20;
            npc.defense = 5;
           
            npc.value = 1000;
            npc.knockBackResist = 0.8f;


        }
        public override void AI() { npc.spriteDirection = npc.direction; }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            
                return Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == mod.TileType("AntsNest") ? 2f : 0f;
           
        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 15;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 10.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * frameHeight;

        }
        public override void NPCLoot()
        {
           
            if (Main.rand.Next(2) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("AntJaw")); }
        }
        
    }
}