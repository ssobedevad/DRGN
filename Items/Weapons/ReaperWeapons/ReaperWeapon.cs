using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    public abstract class ReaperWeapon : ModItem
    {
        public override bool CloneNewInstances => true;
        public int BloodHuntRange = 0;
        public int BloodHuntRangeReal = 0;
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            if (pre == -3) { return true; }
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return Main.rand.Next(DRGN.meleePrefixes);
        }
        public override bool AllowPrefix(int pre)
        {
            if (DRGN.meleePrefixes.Contains(pre)) { return true; }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (BloodHuntRange != 0)
            {
                BloodHuntRangeReal = BloodHuntRange + player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange;
            }
            if (player.GetModPlayer<ReaperPlayer>().isReaper && BloodHuntRangeReal > 0 && player.GetModPlayer<ReaperPlayer>().HuntedTarget != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
            {
                float dist = Vector2.Distance(player.Center, Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget].Center);
                int Alpha = dist < BloodHuntRangeReal ? 0 : dist < BloodHuntRangeReal * 2 ? 120 : 220;
                for (int i = 0; i < 50; i++)
                {
                    int dustid =
                        Dust.NewDust(
                        new Vector2(player.Center.X + (float)Math.Cos((Math.PI / 25f) * i) * BloodHuntRangeReal,
                        player.Center.Y + (float)Math.Sin((Math.PI / 25f) * i) * BloodHuntRangeReal),
                                              1,
                                              1,
                                              DustID.Blood,
                                              0,
                                              0,
                                              Alpha
                                              , Color.White
                                              , 1.2f
                                              );
                    Main.dust[dustid].noGravity = true;
                }
            }
        }        
        public virtual void SafeSetDefaults()
        { }
        public sealed override void SetDefaults()
        {

            SafeSetDefaults();

            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
            item.autoReuse = true;
            item.noUseGraphic = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= ReaperPlayer.ModPlayer(player).reaperDamageMult;
            mult += ReaperPlayer.ModPlayer(player).numSouls * ReaperPlayer.ModPlayer(player).damageIncPerSoul;

        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            // Adds knockback bonuses
            knockback += ReaperPlayer.ModPlayer(player).reaperKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            // Adds crit bonuses

            crit += ReaperPlayer.ModPlayer(player).reaperCrit;

        }

        // Because we want the damage tooltip to show our custom damage, we need to modify it
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.text = damageValue + " Reaper " + damageWord;
            }
        }
        public void RetractAllHooks(Player player)
        {
            Dictionary<int, int> toStrike = new Dictionary<int, int>();
            for (int i = 0; i < Main.projectile.Length; i ++)
            {
                Projectile projectile = Main.projectile[i];
                if(projectile.active && projectile.owner == player.whoAmI && projectile.modProjectile is ReaperChain)
                {
                    int target = (int)projectile.localAI[0];
                    if (target >= 0)
                    {
                        projectile.localAI[0] = -1;
                        ReaperChain Rc = projectile.modProjectile as ReaperChain;
                        int damage = Rc.baseDamage;
                        if (toStrike.ContainsKey(target))
                        { toStrike[target] += damage; }
                        else { toStrike.Add(target, damage); }
                    }
                }
            }
            if (toStrike.Count > 0)
            {
                foreach (KeyValuePair<int , int> data in toStrike)
                {
                    bool crit = Main.rand.NextBool();
                    Main.npc[data.Key].StrikeNPCNoInteraction(data.Value, 0, player.direction,crit);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {                       
                        NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, data.Key, data.Value, 0, player.direction,crit ? 1 : 0);
                    }
                }
                player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
            }
        }               
        public int getDamageIncFromArmorPen(Player player, bool crit = false, NPC npc = null)
        {
            int armorPenetration = player.armorPenetration;
            if (crit)
            {
                armorPenetration += player.GetModPlayer<DRGNPlayer>().criticalArmorPen + player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen;
            }
            if (npc != null && armorPenetration > npc.defense) { armorPenetration += npc.defense - armorPenetration; }


            return armorPenetration;

        }
        public int getProjectileDamage(float mult, Player player)
        {
            int baseDamage = (int)(item.damage * mult);
            float damageMult = player.GetModPlayer<ReaperPlayer>().reaperDamageMult * player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult * player.allDamageMult * (1 + player.GetModPlayer<ReaperPlayer>().numSouls * player.GetModPlayer<ReaperPlayer>().damageIncPerSoul);
            baseDamage = (int)(baseDamage * damageMult);



            baseDamage += getDamageIncFromArmorPen(player, true);
            baseDamage = (int)(baseDamage * player.GetModPlayer<DRGNPlayer>().criticalDamageMult);
            baseDamage = (int)(baseDamage * (1f + player.ownedProjectileCounts[mod.ProjectileType("ReaperScythe")] * 0.1f));
            return baseDamage;

        }    
        public override bool AltFunctionUse(Player player)
        {
            return player.GetModPlayer<ReaperPlayer>().isReaper;
        }
    }
}
