
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.NPCs.Boss
{
	[AutoloadBossHead]

	public class VoidSnakeHead : VoidSnake
	{

		
		public override void SetDefaults()
		{
			
			npc.CloneDefaults(NPCID.DiggerHead);
			npc.aiStyle = -1;
			npc.lifeMax = 2000000;        
			npc.damage = 120;    
			npc.defense = 75;
			npc.width = 96; 
			npc.height = 96;
			npc.value = 1000000;
			npc.boss = true;
			bossBag = mod.ItemType("VoidBossBag");
		}

		public override void Init()
		{
			base.Init();
			head = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("OmegaHealingPotion");
		}

		public override void NPCLoot()
		{
			
			DRGNModWorld.downedVoidSnake = true;
			Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/VoidSnakeHead"), 1f);
			if (!Main.expertMode)
			{
				int i = Main.rand.Next(6);
				Item.NewItem(npc.getRect(), mod.ItemType("VoidOre"), 35);
				Item.NewItem(npc.getRect(), mod.ItemType("VoidSoul"), 20);
				if (i == 0) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSpear")); }
				else if (i == 1) { Item.NewItem(npc.getRect(), mod.ItemType("VoidScythe")); }
				else if (i == 2) { Item.NewItem(npc.getRect(), mod.ItemType("VoidBar"), 15); }
				else if (i == 3) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSoul"), 20); }
				
				else if (i == 4) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSnakeWhip")); }
				else if (i == 5) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSnakeStaff")); }
			}
			else { npc.DropBossBags(); }
		}
		

	}

	public class VoidSnakeBody : VoidSnake
	{


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 2500000;

			npc.width = 48;
			npc.height = 48;
			npc.damage = 40;
			npc.defense = 150;
			npc.value = 0;

		}
		public override void NPCLoot()
		{

			
			Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/VoidSnakeBody"), 1f);
		}
	}

	public class VoidSnakeTail : VoidSnake
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 2500000;
			npc.width = 48;
			npc.height = 48;
			npc.damage = 20;
			npc.defense = 120;
			npc.value = 0;
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
		public override void NPCLoot()
		{

			
			Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/VoidSnakeTail"), 1f);
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class VoidSnake : VoidSnakeAI
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Snake");
			
		}

		public override void Init()
		{
			minLength = 35 ;
			maxLength = 45 ;
			tailType = NPCType<VoidSnakeTail>();
			bodyType = NPCType<VoidSnakeBody>();
			headType = NPCType<VoidSnakeHead>();
			speed = (float)((DRGNModWorld.MentalMode ? 14 : Main.expertMode ? 11 : 8));
			turnSpeed = (DRGNModWorld.MentalMode ? 0.06f : Main.expertMode ? 0.04f : 0.02f); 
			turnSpeed2 = (DRGNModWorld.MentalMode ? 0.2f : Main.expertMode ? 0.12f : 0.08f);
			flies = false;
			reqPlayerDist = ((DRGNModWorld.MentalMode ? 900 : Main.expertMode ? 1000 : 1300)); ;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
			npc.lifeMax = (int)(npc.lifeMax  * 2.5);
			npc.damage = (int)(npc.damage * 1.3);
			npc.defense = (int)(npc.defense * 1.4);
		}

    }
	
}