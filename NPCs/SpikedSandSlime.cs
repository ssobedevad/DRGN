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
    public class SpikedSandSlime : ModNPC
    {
		private int shootCD;
        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 80;
            npc.height = 30;
            npc.width = 36;
            npc.aiStyle = 1;
            npc.damage = 20;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.defense = 2;
            animationType = NPCID.SandSlime;
            npc.value = 350;
            npc.knockBackResist = 0.6f;
           

        }
        public override void PostAI()
        {
			if (shootCD > 0) { shootCD -= 1; }
			if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
			{
				Vector2 vector3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float num11 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector3.X;
				float num12 = Main.player[npc.target].position.Y - vector3.Y;
				float num13 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
				if (num13 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
				{
					
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.X *= 0.9f;
					}
					if (Main.netMode != 1 && shootCD == 0)
					{
						for (int j = 0; j < 5; j++)
						{
							Vector2 vector4 = new Vector2(j - 2, -4f);
							vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Normalize();
							vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
							
							Projectile.NewProjectile(vector3.X, vector3.Y, vector4.X, vector4.Y, ProjectileID.JungleSpike, 11, 0f, Main.myPlayer);
							shootCD = 30;
						}
					}
				}
				else if (num13 < 200f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
				{
					
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.X *= 0.9f;
					}
					if (Main.netMode != NetmodeID.MultiplayerClient && shootCD == 0)
					{
						num12 = Main.player[npc.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
						num13 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
						num13 = 4.5f / num13;
						num11 *= num13;
						num12 *= num13;
						shootCD = 50;
						Projectile.NewProjectile(vector3.X, vector3.Y, num11, num12, ProjectileID.JungleSpike, 9, 0f, Main.myPlayer);
					}
				}
			}
		
		}



        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneDesert ? 0.05f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(3, 8));

        }

    }
}