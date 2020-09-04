
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


	public class FireWormHead : FireWorm
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 1200;
			npc.damage = 45;
			npc.defense = 12;
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

	public class FireWormBody : FireWorm
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1200;
			npc.damage = 18;
			npc.defense = 20;

		}
	}

	public class FireWormTail : FireWorm
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1200;
			npc.damage = 15;
			npc.defense = 30;
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class FireWorm : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FireWorm");

		}

		public override void Init()
		{
			minLength = 10;
			maxLength = 20;
			tailType = NPCType<FireWormTail>();
			bodyType = NPCType<FireWormBody>();
			headType = NPCType<FireWormHead>();
			speed = 12f;
			turnSpeed = 0.5f;
			flies = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return (spawnInfo.player.GetModPlayer<DRGNPlayer>().DragonBiome) ? 1f : 0f;
		}
		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), mod.ItemType("FlareCrystal"), Main.rand.Next(4, 8));
			Item.NewItem(npc.getRect(), mod.ItemType("AshenWood"), Main.rand.Next(4, 8));
		}
	}
}