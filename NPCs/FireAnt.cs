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
    public class FireAnt : ModNPC
    {
        private Vector2 projVel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Ant");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 2000;
            npc.height = 34;
            npc.width = 66;
            npc.aiStyle = 3;
            npc.damage = 35;
            npc.defense = 5;

            npc.value = 1000;
            npc.knockBackResist = 0.8f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<FireAntBanner>();


        }
        public override void AI() { npc.spriteDirection = npc.direction; npc.TargetClosest(true); if (Main.rand.Next(0, 120) == 0) { move(); Projectile.NewProjectile(npc.Center + new Vector2 (0,-10), projVel, mod.ProjectileType("FireBallBouncy"), npc.damage / 3, 0f); } }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == mod.TileType("AntsNest")) && (DRGNModWorld.SwarmKilledPostQA) ? 2f : 0f;

        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 30;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 10.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * frameHeight;

        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("FireAntJaw")); }
        }
        private void move()
        {

            float speed = 12f;
            Vector2 moveTo = Main.player[npc.target].Center;
            Vector2 moveVel = (moveTo - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;



            }
            projVel = moveVel;

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}