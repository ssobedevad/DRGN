
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


	public class MagmaticCrawlerHead : MagmaticCrawler
	{
		

		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 1250;
			npc.damage = 80;
			npc.defense = 10;
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
	}

	public class MagmaticCrawlerBody : MagmaticCrawler
	{
		

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1250;
			npc.damage = 40;
			npc.defense = 40;

		}
	}

	public class MagmaticCrawlerTail : MagmaticCrawler
	{
		
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1250;
			npc.damage = 20;
			npc.defense = 80;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class MagmaticCrawler : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magmatic Crawler");
			banner = npc.type;
			bannerItem = ModContent.ItemType<MagmaticCrawlerBanner>();
		}

		public override void Init()
		{
			minLength = 8;
			maxLength = 14;
			tailType = NPCType<MagmaticCrawlerTail>();
			bodyType = NPCType<MagmaticCrawlerBody>();
			headType = NPCType<MagmaticCrawlerHead>();
			speed = 10f;
			turnSpeed = 0.05f;
			flies = true;
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (DRGNModWorld.downedDragon && spawnInfo.player.GetModPlayer<DRGNPlayer>().DragonBiome)? 1f : 0;
        }
		public override void NPCLoot()
		{
			if (DRGNModWorld.downedDragon)
			{
				
				 Item.NewItem(npc.getRect(), mod.ItemType("SolariumOre"), Main.rand.Next(12,20)); 
			}
		}
	}
}