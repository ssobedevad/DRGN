
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


	public class LunarWyrmHead : LunarWyrm
	{


		public override void SetDefaults()
		{

			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 5400;
			npc.damage = 75;
			npc.defense = 40;
			npc.value = 100000;
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

	public class LunarWyrmBody : LunarWyrm
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 5400;
			npc.damage = 65;
			npc.defense = 50;

		}
	}

	public class LunarWyrmTail : LunarWyrm
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 5400;
			npc.damage = 55;
			npc.defense = 60;

		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class LunarWyrm : Worm
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Wyrm");
			banner = npc.type;
			bannerItem = ModContent.ItemType<LunarWyrmBanner>();
		}

		public override void Init()
		{
			minLength = 18;
			maxLength = 30;
			tailType = NPCType<LunarWyrmTail>();
			bodyType = NPCType<LunarWyrmBody>();
			headType = NPCType<LunarWyrmHead>();
			speed = 7.5f;
			turnSpeed = 0.08f;
			flies = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return (NPC.downedMoonlord && spawnInfo.player.ZoneSkyHeight) ? 0.1f : 0;
		}
		public override void NPCLoot()
		{

			
				Item.NewItem(npc.getRect(), mod.ItemType("LunarFragment"),Main.rand.Next(1,20));
			

		}
	}
}