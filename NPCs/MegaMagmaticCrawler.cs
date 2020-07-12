
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.NPCs
{


	public class MegaMagmaticCrawlerHead : MegaMagmaticCrawler
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 100000;
			npc.damage = 85;
			npc.defense = 50;
			npc.height = 30;
			npc.width = 30;
			npc.value = 0;
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

	public class MegaMagmaticCrawlerBody : MegaMagmaticCrawler
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 100000;
			npc.damage = 75;
			npc.defense = 60;
			npc.height = 30;
			npc.width = 30;
			npc.value = 0;

		}
	}

	public class MegaMagmaticCrawlerTail : MegaMagmaticCrawler
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 100000;
			npc.damage = 65;
			npc.defense = 70;
			npc.height = 30;
			npc.width = 30;
			npc.value = 0;
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class MegaMagmaticCrawler : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mega Magmatic Crawler");
		}

		public override void Init()
		{
			minLength = 18;
			maxLength = 30;
			tailType = NPCType<MegaMagmaticCrawlerTail>();
			bodyType = NPCType<MegaMagmaticCrawlerBody>();
			headType = NPCType<MegaMagmaticCrawlerHead>();
			speed = 10f;
			turnSpeed = 0.1f;
			flies = false;
		}
		
	}
}