using DRGN.Buffs;
using DRGN.Items;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using Microsoft.Xna.Framework;
using Steamworks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN
{

    class ReaperGlobalNPC : GlobalNPC
    {
       
        public override bool InstancePerEntity => true;

         
        

        
        public override void PostAI(NPC npc)
        {
            Player player = Main.LocalPlayer;
            ReaperPlayer reaperPlayer = player.GetModPlayer<ReaperPlayer>();
            if (reaperPlayer.HuntedTarget == npc.whoAmI)
            {
                Dust.NewDust(
                          npc.position,
                          
                                                npc.width,
                                                npc.height,
                                                DustID.Blood
                                                
                                                );
            }
        }



        public override void NPCLoot(NPC npc)
        {
            Player player = Main.LocalPlayer;
            ReaperPlayer reaperPlayer = player.GetModPlayer<ReaperPlayer>();
            if (reaperPlayer.HuntedTarget == npc.whoAmI)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("ReaperSoul"));
            }

        }

       

        
    }
}