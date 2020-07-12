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
using DRGN.Items.Banners;

namespace DRGN.NPCs
{
    public class FlyingAnt : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying ant");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 2500;
            npc.height = 56;
            npc.width = 100;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 5;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 1000;
            npc.knockBackResist = 0.8f;
            npc.noTileCollide = true;
            banner = npc.type;
            bannerItem = ModContent.ItemType<FlyingAntBanner>();


        }
        public override void AI() {
            npc.spriteDirection = npc.direction; npc.TargetClosest(true);move(); 
            
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == mod.TileType("AntsNest"))&&(DRGNModWorld.SwarmKilledPostMechBoss) ? 2f : 0f;

        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 12;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 2.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * frameHeight;

        }
        public override void NPCLoot()
        {

            if (Main.rand.Next(2) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("AntWing")); }
        }
        private void move()
        {

            float speed = 5f;
            Vector2 moveTo = Main.player[npc.target].Center;
            Vector2 moveVel = (moveTo - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;



            }
            npc.velocity = (npc.velocity * 40f + moveVel) / 41f;
            

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}