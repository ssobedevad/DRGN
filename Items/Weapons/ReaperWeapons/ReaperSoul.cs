using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
        public int FindClosestReaper(Item item , float maxRange = 1000)
        {
            int target = -1;
            for (int i = 0; i < 255; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    float dist = Vector2.Distance(item.Center, player.Center);
                    if (player.GetModPlayer<ReaperPlayer>().isReaper && dist < maxRange)
                    {
                        target = i;
                        maxRange = dist;
                    }
                }

            }
            return target;
        }
        public override void PostUpdate()
        {            
            int owner = FindClosestReaper(item);            
            if(owner == -1) { return; }
            Player player = Main.player[owner];
            float range = 300 * player.GetModPlayer<ReaperPlayer>().SoulPickUpRangeBoost;
            if (Vector2.Distance(player.Center, item.Center) <= range)
            {                
                if (Vector2.Distance(player.Center, item.Center) <= range / 2)
                {                    
                    item.Center += Vector2.Normalize(player.Center - item.Center) * 8;
                    if (Vector2.Distance(item.Center, player.Center) < 20)
                    {
                        if (player.GetModPlayer<ReaperPlayer>().numSouls + item.stack < player.GetModPlayer<ReaperPlayer>().maxSouls2)
                        {
                            player.GetModPlayer<ReaperPlayer>().numSouls += item.stack;
                            item.SetDefaults();
                            item.active = false;
                        }
                        else
                        {
                            int inc = item.stack - (player.GetModPlayer<ReaperPlayer>().numSouls - player.GetModPlayer<ReaperPlayer>().maxSouls2);

                            player.GetModPlayer<ReaperPlayer>().soulOverchargeLevel += inc;
                            if (player.GetModPlayer<ReaperPlayer>().soulOverchargeLevel > player.GetModPlayer<ReaperPlayer>().maxSouls2) { player.GetModPlayer<ReaperPlayer>().soulOverchargeLevel = player.GetModPlayer<ReaperPlayer>().maxSouls2; }
                            player.GetModPlayer<ReaperPlayer>().numSouls = player.GetModPlayer<ReaperPlayer>().maxSouls2;
                            player.AddBuff(mod.BuffType("SoulOvercharge"), 180 * inc);
                            item.SetDefaults();
                            item.active = false;
                        }
                    }
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.whoAmI);
                    }
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
            for (int i = myItemIndex + 1; i < Main.item.Length; i++)
            {
                Item MergeItem = Main.item[i];
                if (!MergeItem.active || MergeItem.type != item.type || MergeItem.stack <= 0)
                {
                    continue;
                }
                float dist = Vector2.Distance(item.Center, MergeItem.Center);
                float mergeDist = 100f;
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
