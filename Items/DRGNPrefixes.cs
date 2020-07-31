


using DRGN.Prefixes.Accessories;
using DRGN.Prefixes.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
	public class DRGNPrefixes : GlobalItem
	{








		

        public override bool CloneNewInstances => true;
        public override bool InstancePerEntity => true;

     
        public override void UpdateEquip(Item item, Player player)
        {
			if (item.prefix == PrefixType<Wrathful>())
			{
				player.allDamage += 0.05f;
			}
			else if (item.prefix == PrefixType<Shielding>())
			{
				player.statDefense += 5;
			}
			else if (item.prefix == PrefixType<Weighted>())
			{
				player.magicCrit += 5;
				player.meleeCrit += 5;
				player.rangedCrit += 5;
				player.thrownCrit += 5;
				player.GetModPlayer<ReaperPlayer>().reaperCrit += 5;
			}
			else if (item.prefix == PrefixType<Rapid>())
			{
				player.maxRunSpeed *= 1.05f;
				player.moveSpeed *= 1.05f;

			}
			else if (item.prefix == PrefixType<Beserk>())
			{
				player.meleeSpeed *= 1.05f;
				

			}
			
		}
		
        public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if (item.damage > 0 && item.maxStack == 1)
			{
				if (rand.NextBool(30) && DRGNModWorld.MentalMode)
				{
					
					return mod.PrefixType("Mental");
				}
			}
			else if (item.accessory && item.maxStack == 1)
			{

				if (rand.NextBool(30))
				{

					return mod.PrefixType("Wrathful");
				}
				if (rand.NextBool(30))
				{

					return mod.PrefixType("Shielding");
				}
				if (rand.NextBool(30))
				{

					return mod.PrefixType("Weighted");
				}
				if (rand.NextBool(30))
				{

					return mod.PrefixType("Rapid");
				}
				if (rand.NextBool(30))
				{

					return mod.PrefixType("Beserk");
				}


			}
			return -1;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (!item.social && item.prefix > 0)
			{
				if (item.prefix == PrefixType<Wrathful>())
				{
					TooltipLine line = new TooltipLine(mod, "Wrathful", "+5% damage")
					{
						isModifier = true

					};
					tooltips.Add(line);
				}
				else if (item.prefix == PrefixType<Shielding>())
				{
					TooltipLine line = new TooltipLine(mod, "Shielding", "+5 defense")
					{
						isModifier = true

					};
					tooltips.Add(line);
				}
				else if (item.prefix == PrefixType<Weighted>())
				{
					TooltipLine line = new TooltipLine(mod, "Weighted", "+5% critical strike chance")
					{
						isModifier = true

					};
					tooltips.Add(line);
				}
				else if (item.prefix == PrefixType<Rapid>())
				{
					TooltipLine line = new TooltipLine(mod, "Rapid", "+5% movement speed")
					{
						isModifier = true

					};
					tooltips.Add(line);
				}
				else if (item.prefix == PrefixType<Beserk>())
				{
					TooltipLine line = new TooltipLine(mod, "Beserk", "+5% melee speed")
					{
						isModifier = true

					};
					tooltips.Add(line);
				
				}

			}






		}
        
		
        

    }
}
