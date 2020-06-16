using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    
    public class DRGNGlobalItem : GlobalItem
    {
        public override bool UseItem(Item item, Player player)
        {

			 if (item.type == ItemID.MechanicalEye && DRGNModWorld.MentalMode)
			{
				if (!Main.dayTime)
				{
					
					Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.SpawnOnPlayer(player.whoAmI, 125);
						NPC.SpawnOnPlayer(player.whoAmI, 126);
						NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Triplet"));
					}
					else
					{
						NetMessage.SendData(61, -1, -1, null, player.whoAmI, 125f);
						NetMessage.SendData(61, -1, -1, null, player.whoAmI, 126f);
					}
					return true;
				}
                
			}
			 return false;
		}
    }
}
