
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
			npc.lifeMax = 1500000;        
			npc.damage = 550;    
			npc.defense = 100;
			npc.width = 96; 
			npc.height = 96;
			npc.value = 0;
			npc.boss = true;
			bossBag = mod.ItemType("VoidBossBag");
		}

		public override void Init()
		{
			base.Init();
			head = true;
		}
		
		
		
		public override void NPCLoot()
		{
			Main.NewText("Is it the end?", 150, 10, 150);
			DRGNModWorld.downedVoidSnake = true;
			Gore.NewGore(npc.Center, npc.velocity + new Vector2(Main.rand.Next(-1, 1), Main.rand.Next(-1, 1)), mod.GetGoreSlot("Gores/VoidSnakeHead"), 1f);
			if (!Main.expertMode)
			{
				int i = Main.rand.Next(5);
				Item.NewItem(npc.getRect(), mod.ItemType("VoidOre"), 35);
				Item.NewItem(npc.getRect(), mod.ItemType("VoidSoul"), 20);
				if (i == 0) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSpear")); }
				else if (i == 1) { Item.NewItem(npc.getRect(), mod.ItemType("VoidScythe")); }
				else if (i == 2) { Item.NewItem(npc.getRect(), mod.ItemType("VoidBar"), 15); }
				else if (i == 3) { Item.NewItem(npc.getRect(), mod.ItemType("VoidSoul"), 20); }
				else if (i == 4) { Item.NewItem(npc.getRect(), mod.ItemType("VoidBlade")); }
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
			npc.lifeMax = 1500000;       
			
			npc.width = 48;              
			npc.height = 48;              
			npc.damage = 150;
			npc.defense = 300;
			npc.value = 0;

		}
	}

	public class VoidSnakeTail : VoidSnake
	{

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DiggerBody);
			npc.aiStyle = -1;
			npc.lifeMax = 1500000;      
			npc.width = 68;     
			npc.height = 68;      
			npc.damage = 80;
			npc.defense = 80;
			npc.value = 0;
		}

		public override void Init()
		{
			base.Init();
			tail = true;
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
		

	}
}