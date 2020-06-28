
using DRGN.Items.Banners;
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.NPCs
{


	public class VoidCrawlerHead : VoidCrawlerWorm
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 10000;
			npc.damage = 100;
			npc.defense = 5;
		}

		public override void Init()
		{
			base.Init();
			head = true;
		}

		private int attackCounter;
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attackCounter = reader.ReadInt32();
		}

		public override void CustomBehavior()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (attackCounter > 0)
				{
					attackCounter--;
				}

				Player target = Main.player[npc.target];
				if (attackCounter <= 0 && Vector2.Distance(npc.Center, target.Center) < 200 && Collision.CanHit(npc.Center, 1, 1, target.Center, 1, 1))
				{
					//Vector2 direction = (target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
					//direction = direction.RotatedByRandom(MathHelper.ToRadians(10));


					attackCounter = 500;
					npc.netUpdate = true;
				}
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedMoonlord == true)
			{
				return spawnInfo.player.GetModPlayer<DRGNPlayer>().VoidBiome ? 0.05f : 0f;
			}
			else { return 0f; }
		}
	}

	public class VoidCrawlerBody : VoidCrawlerWorm
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 10000;
			npc.damage = 80;
			npc.defense = 55;

		}
	}

	public class VoidCrawlerTail : VoidCrawlerWorm
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 10000;
			npc.damage = 60;
			npc.defense = 95;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class VoidCrawlerWorm : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Crawler");
			banner = npc.type;
			bannerItem = ModContent.ItemType<VoidCrawlerWormBanner>();
		}

		public override void Init()
		{
			minLength = 13;
			maxLength = 16;
			tailType = NPCType<VoidCrawlerTail>();
			bodyType = NPCType<VoidCrawlerBody>();
			headType = NPCType<VoidCrawlerHead>();
			speed = 22f;
			turnSpeed = 0.5f;
			turnSpeed2 = 0.2f;
			flies = false;
		}
		
		public override void NPCLoot()
		{
			

				Item.NewItem(npc.getRect(), mod.ItemType("VoidSilk"), Main.rand.Next(8, 15));
			
		}
	}
}