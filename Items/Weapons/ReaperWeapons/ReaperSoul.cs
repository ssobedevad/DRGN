using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class ReaperSoul : ModItem
    {
        public bool noKill = false;
        public override void SetStaticDefaults()
        {
            
            
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            
            item.rare = ItemRarityID.Red;
            item.noGrabDelay = 0;
            item.maxStack = 999;
            

        }
        public override bool CanPickup(Player player)
        {
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            item.SetDefaults();
        }
        public override void PostUpdate()
        {
            int ReaperPlayer = ReaperGlobalNPC.FindClosestReaper(item.Center);
            if(ReaperPlayer == -1 && !noKill) { item.active = false; }
            Player player = Main.LocalPlayer;
            if (Vector2.Distance(item.Center, player.Center) < 150 && player.GetModPlayer<ReaperPlayer>().numSouls < player.GetModPlayer<ReaperPlayer>().maxSouls2 && player.active && !player.dead && player.GetModPlayer<ReaperPlayer>().isReaper)
            {
                item.velocity = Vector2.Normalize(player.Center - item.Center) * 10;
                if (Vector2.Distance(item.Center, player.Center) < 10 )
                { player.GetModPlayer<ReaperPlayer>().numSouls += item.stack; item.active = false;  }
            }
            CombineWithNearbyItems(item.whoAmI);
            
        }
		private void CombineWithNearbyItems(int myItemIndex)
		{
			if (item.stack >= item.maxStack)
			{
				return;
			}
			for (int num = myItemIndex + 1; num < 400; num++)
			{
				Item num2 = Main.item[num];
				if (!num2.active || num2.type != item.type || num2.stack <= 0 )
				{
					continue;
				}
				float num4 = Math.Abs(item.position.X + (float)(item.width / 2) - (num2.position.X + (float)(num2.width / 2))) + Math.Abs(item.position.Y + (float)(item.height / 2) - (num2.position.Y + (float)(num2.height / 2)));
				int num5 = 120;				
				if (num4 < (float)num5)
				{
                    item.position = (item.position + num2.position) / 2f;
                    item.velocity = (item.velocity + num2.velocity) / 2f;
					int num3 = num2.stack;
					if (num3 > item.maxStack - item.stack)
					{
						num3 = item.maxStack - item.stack;
					}
					num2.stack -= num3;
                    item.stack += num3;
					if (num2.stack <= 0)
					{
						num2.SetDefaults();
						num2.active = false;
					}
					if (Main.netMode != NetmodeID.SinglePlayer &&  Main.myPlayer == item.owner)
					{
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, myItemIndex);
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num);
					}
				}
			}
		}






	}
}
