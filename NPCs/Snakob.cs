
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


	public class SnakobHead : Snakob
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 500;       
			npc.damage = 25;   
			npc.defense = 10;
			banner = npc.type;
			bannerItem = ItemType<SnakobBanner>();
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

	public class SnakobBody : Snakob
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 500;
			npc.damage = 10;
			npc.defense = 20;

		}
	}

	public class SnakobTail : Snakob
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 500;
			npc.damage = 6;
			npc.defense = 30;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class Snakob : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snakob");
			
		}

		public override void Init()
		{
			minLength = 10;
			maxLength = 20;
			tailType = NPCType<SnakobTail>();
			bodyType = NPCType<SnakobBody>();
			headType = NPCType<SnakobHead>();
			speed = 4f;
			turnSpeed = 0.01f;
			flies = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return (DRGNModWorld.downedToxicFrog && spawnInfo.player.ZoneJungle && spawnInfo.player.ZoneRockLayerHeight) ? 0.1f : 0;
		}
		public override void NPCLoot()
		{
			

				Item.NewItem(npc.getRect(), mod.ItemType("ToxicFlesh"), Main.rand.Next(12, 20));
				Item.NewItem(npc.getRect(), ItemID.Leather, Main.rand.Next(3, 10));

		}
	}
}