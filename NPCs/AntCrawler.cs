
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


	public class AntCrawlerHead : AntCrawler
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 3800;
			npc.damage = 65;
			npc.defense = 30;
			npc.value = 50000;
			banner = npc.type;
			bannerItem = ItemType<AntCrawlerBanner>();

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
					//int projectile = Projectile.NewProjectile(npc.Center, direction * 1, ProjectileType<IceShard>(), npc.damage / 4, 0, Main.myPlayer);
					//Main.projectile[projectile].timeLeft = 300;

					attackCounter = 500;
					npc.netUpdate = true;
				}
			}
		}
	}

	public class AntCrawlerBody : AntCrawler
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 3800;
			npc.damage = 55;
			npc.defense = 40;

		}
	}

	public class AntCrawlerTail : AntCrawler
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 3800;
			npc.damage = 45;
			npc.defense = 50;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class AntCrawler : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ant Crawler");
			
		}

		public override void Init()
		{
			minLength = 12;
			maxLength = 22;
			tailType = NPCType<AntCrawlerTail>();
			bodyType = NPCType<AntCrawlerBody>();
			headType = NPCType<AntCrawlerHead>();
			speed = 6.5f;
			turnSpeed = 0.06f;
			flies = true;
			
		}
		
		public override void NPCLoot()
		{

			if (Main.rand.NextBool())
			{
				Item.NewItem(npc.getRect(), mod.ItemType("AntCrawlerScale"));
			}

		}
	}
}