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
            int playerID = -1;
            float dist = -1;
            for (int i = 0; i < Main.ActivePlayersCount; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead && Main.player[i].GetModPlayer<ReaperPlayer>().isReaper)
                {
                    if (Vector2.Distance(Main.player[i].Center, item.Center) < dist || dist == -1)
                    { playerID = i; dist = Vector2.Distance(Main.player[i].Center, item.Center); }
                }
            }
            if (playerID != -1)
            {
                Player player = Main.player[playerID];
                if (Vector2.Distance(item.Center, player.Center) < 150 && player.GetModPlayer<ReaperPlayer>().numSouls < player.GetModPlayer<ReaperPlayer>().maxSouls2 && player.active && !player.dead )
                {
                    item.velocity = Vector2.Normalize(player.Center - item.Center) * 10;
                    if (Vector2.Distance(item.Center, player.Center) < 15)
                    {   item.active = false; player.GetModPlayer<ReaperPlayer>().numSouls += item.stack; NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI); }
                }
                else
                {
                    CombineWithNearbyItems(item.whoAmI);
                }
            }
            
        }
		private void CombineWithNearbyItems(int myItemIndex)
		{
			if (item.stack >= item.maxStack)
			{
				return;
			}
			for (int i = myItemIndex + 1; i < 400; i++)
			{
				Item MergeItem = Main.item[i];
				if (!MergeItem.active || MergeItem.type != item.type || MergeItem.stack <= 0 )
				{
					continue;
				}
				float dist = Vector2.Distance(item.Center,MergeItem.Center);
				float mergeDist = 250f;				
				if (dist < mergeDist)
				{
                    item.position = (item.position + MergeItem.position) / 2f;
                    item.velocity = (item.velocity + MergeItem.velocity) / 2f;
					int stack = MergeItem.stack;
					if (stack > item.maxStack - item.stack)
					{
						stack = item.maxStack - item.stack;
					}
					MergeItem.stack -= stack;
                    item.stack += stack;
					if (MergeItem.stack <= 0)
					{
						MergeItem.SetDefaults();
						MergeItem.active = false;
					}
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, myItemIndex);
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i);
					}
				}
			}
		}






	}
}
