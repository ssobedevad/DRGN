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
    public class DragonFlyMini : ModNPC
    {
        private int shootCD;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon fly");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 10000;
            npc.height = 25;
            npc.width = 108;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 25;
            npc.noGravity = true;
            npc.value = 1000;
            npc.knockBackResist = 0.8f;


        }
        public override void AI() { npc.spriteDirection = npc.direction; npc.TargetClosest(true); move(); if (shootCD == 0) { Projectile.NewProjectile(npc.Center, new Vector2(0, 5), mod.ProjectileType("BlueFireball"), npc.damage / 10, 0f);shootCD = 100; }if (shootCD > 0){ shootCD -= 1; } }
       


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 20;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * frameHeight;

        }
        public override void NPCLoot()
        {
            if (DRGNModWorld.downedDragonFly)
            {
                if (Main.rand.Next(2) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyDust"),Main.rand.Next(1,5)); }
                if (Main.rand.Next(2) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("DragonFlyWing"), Main.rand.Next(1, 5)); }
            }
            else if (Main.rand.Next(5) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("DragonFliesCall")); }


        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == mod.TileType("AntsNest") && (DRGNModWorld.SwarmKilledPostMoonlord) ? 2f : 0f;




        }
        private void move()
        {

            float speed = 6f;
            Vector2 moveTo = Main.player[npc.target].Center;
            Vector2 moveVel = (moveTo - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;



            }
            npc.velocity = moveVel;

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}