
using DRGN.Items;
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


	public class GlacialCrawlerHead : GlacialCrawler
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 1500;
			npc.damage = 45;
			npc.defense = 20;
			npc.value = 25000;
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
					Vector2 direction = (target.Center - npc.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
					int projectile = Projectile.NewProjectile(npc.Center, direction * 1, ProjectileType<IceShard>(), npc.damage/4, 0, Main.myPlayer);
					Main.projectile[projectile].timeLeft = 300;

					attackCounter = 500;
					npc.netUpdate = true;
				}
			}
		}
	}

	public class GlacialCrawlerBody : GlacialCrawler
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1500;
			npc.damage = 20;
			npc.defense = 35;

		}
	}

	public class GlacialCrawlerTail : GlacialCrawler
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1500;
			npc.damage = 14;
			npc.defense = 40;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class GlacialCrawler : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glacial Crawler");
			banner = npc.type;
			bannerItem = ModContent.ItemType<GlacialCrawlerBanner>();
		}

		public override void Init()
		{
			minLength = 12;
			maxLength = 22;
			tailType = NPCType<GlacialCrawlerTail>();
			bodyType = NPCType<GlacialCrawlerBody>();
			headType = NPCType<GlacialCrawlerHead>();
			speed = 6.5f;
			turnSpeed = 0.04f;
			flies = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return (DRGNModWorld.downedIceFish && spawnInfo.player.ZoneSnow) ? 0.1f : 0;
		}
		public override void NPCLoot()
		{


			Item.NewItem(npc.getRect(), ItemType<GlacialShard>(), Main.rand.Next(12, 20));

		}
	}
}