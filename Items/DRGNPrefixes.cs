

using DRGN.Items.EngineerClass;
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








		public bool needsRarityCheck = true;

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
				player.GetModPlayer<EngineerPlayer>().engineerCrit += 5;
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
        public override void PostReforge(Item item)
        {
			
			 updateRarity(item);
		}
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (needsRarityCheck) { updateRarity(item); needsRarityCheck = false; }
        }
        public override void UpdateInventory(Item item, Player player)
        {
			if (needsRarityCheck) { updateRarity(item); needsRarityCheck = false; }
		}
        public override void PostUpdate(Item item)
        {
			if (needsRarityCheck) { updateRarity(item); needsRarityCheck = false; }
		}
		
        private void updateRarity(Item item)
		{
			Item It = new Item();
			It.SetDefaults(item.type);
			int baseRarity = It.rare;
			int baseDamage = It.damage;
			int baseUseTime = It.useTime;
			
			int baseMana = It.mana;
			float baseKnockback = It.knockBack;
			float baseScale = It.scale;
			float baseShootspeed = It.shootSpeed;
			int baseCrit = It.crit;
			item.rare = baseRarity;

			float DamageInc = 1;
			if (baseDamage != 0)
			{
				 DamageInc = item.damage / baseDamage;
			}
			float KnockBack = 1;
			if (baseKnockback != 0)
			{
				KnockBack = item.knockBack / baseKnockback;
			}
			float UseTimeMult = 1;
			if (baseUseTime != 0)
			{
				UseTimeMult = item.useTime / baseUseTime;
			}
			float ScaleMult = 1;
			if (baseScale != 0)
			{
				ScaleMult = item.scale / baseScale;
			}
			float ShootspeedMult = 1;
			if (baseShootspeed != 0)
			{
				ShootspeedMult = item.shootSpeed / baseShootspeed;
			}
			float ManaMult = 1;
			if (baseMana != 0)
			{
				ManaMult = item.mana / baseMana;
			}
			float CritMult = 1;
			if (baseCrit != 0)
			{
				CritMult = item.crit / baseCrit;
			};
			
			
			
			
			int i = item.prefix;
			float TotalValue = 1f * DamageInc * (2f - UseTimeMult) * (2f - ManaMult) * ScaleMult * KnockBack * ShootspeedMult * (1f + (float)CritMult * 0.02f);
			if (i == 62 || i == 69 || i == 73 || i == 77)
			{
				TotalValue *= 1.05f;
			}
			if (i == 63 || i == 70 || i == 74 || i == 78 || i == 67)
			{
				TotalValue *= 1.1f;
			}
			if (i == 64 || i == 71 || i == 75 || i == 79 || i == 66)
			{
				TotalValue *= 1.15f;
			}
			if (i == PrefixID.Warding || i == PrefixID.Menacing || i == PrefixID.Lucky || i == PrefixID.Quick || i == PrefixID.Violent)
			{
				TotalValue *= 1.2f;
			}
			if (i == PrefixType<Shielding>() || i == PrefixType<Wrathful>() || i == PrefixType<Weighted>() || i == PrefixType<Rapid>() || i == PrefixType<Beserk>())
			{
				TotalValue *= 1.5f;
			}
			if ((double)TotalValue >= 1.5)
			{
				item.rare += 3;
			}
			else if ((double)TotalValue >= 1.2)
			{
				item.rare += 2;
			}
			else if ((double)TotalValue >= 1.05)
			{
				item.rare++;
			}
			else if ((double)TotalValue <= 0.8)
			{
				item.rare -= 2;
			}
			else if ((double)TotalValue <= 0.95)
			{
				item.rare--;
			}
			
			if(item.rare > DRGN.MaxRarity)
			{ item.rare = DRGN.MaxRarity; }
			if(item.rare > -10 && item.rare <= -7)
			{ item.rare = -10; }
		}

    }
}
