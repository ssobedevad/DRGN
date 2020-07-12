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
    public class ElectricAnt : ModNPC
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Ant");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.height = 34;
            npc.width = 66;
            npc.aiStyle = 3;
            npc.damage = 25;
            npc.defense = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 1000;
            npc.knockBackResist = 0.8f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<ElectricAntBanner>();


        }
        public override void AI() { npc.spriteDirection = npc.direction; npc.TargetClosest(true); if (Main.rand.Next(0, 80)==0) { Projectile.NewProjectile(npc.Center + new Vector2(0, -1000f), new Vector2((float)Main.rand.Next(-200, 200), 500f), mod.ProjectileType("LightningAnt"), npc.damage, 1f, 0, (float)npc.whoAmI, 1); } }
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
            { Item.NewItem(npc.getRect(), mod.ItemType("ElectricAntJaw")); }
        }
        
   
    }
}
