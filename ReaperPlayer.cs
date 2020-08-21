using DRGN.Items.Weapons.ReaperWeapons;
using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DRGN
{

    public class ReaperPlayer : ModPlayer
    {
        public static ReaperPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<ReaperPlayer>();
        }

        public Dictionary<int, HookedData> hookedTargets = new Dictionary<int, HookedData>();
        public float reaperDamageMult = 1f;
        public float reaperCritDamageMult = 1f;
        public int reaperCritArmorPen;
        public float reaperKnockback;
        public int reaperCrit;
        public int numSouls;
        public float damageIncPerSoul = 0.005f;
        public const int maxSouls = 20;
        public int maxSouls2 = 20;
        public int HuntedTarget = -1;
        public bool isReaper;
        public int bloodHuntExtraRange = 0;
        public int stabDashCd = 0;
        public int scytheThrowCd = 0;


        public override TagCompound Save()
        {
            return new TagCompound
            {
                 { "Souls", numSouls },



            };
        }
        public override void Load(TagCompound tag)
        {
            numSouls = tag.GetInt("Souls");

        }



        public override void PostUpdate()
        {

            



            if (HuntedTarget != -1)
            {
                if (Main.npc[HuntedTarget].active == false || Vector2.Distance(Main.npc[HuntedTarget].Center, player.Center) > 1000)
                {
                    HuntedTarget = -1;

                }
            }
            else if (isReaper)
            {
                int hunt = -1;
                float dist = 1000;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(this) && Vector2.Distance(Main.npc[i].Center, player.Center) < dist)
                    {
                        hunt = i;
                        dist = Vector2.Distance(Main.npc[i].Center, player.Center);


                    }

                }
                HuntedTarget = hunt;



            }
        }
        public override void ResetEffects()
        {

            ResetVariables();
            if (numSouls < 0) { numSouls = 0; }
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }
        public override void PostUpdateEquips()
        {
            if (numSouls > maxSouls2)
            {
                for (int i = 0; i < numSouls - maxSouls2; i++)
                {
                    Projectile.NewProjectile(player.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), getSoulDamage(), 0, player.whoAmI);


                }
                numSouls = maxSouls2;
            }
        }
        private void ResetVariables()
        {

            maxSouls2 = maxSouls;
            damageIncPerSoul = 0.005f;
            if (stabDashCd > 0) { stabDashCd -= 1; }
            else { stabDashCd = 0; }
            if (scytheThrowCd > 0) { scytheThrowCd -= 1; }
            else { scytheThrowCd = 0; }

            bloodHuntExtraRange = 0;
            isReaper = false;
            reaperDamageMult = 1f;
            reaperKnockback = 0f;
            reaperCrit = 0;
            reaperCritDamageMult = 1f;
            reaperCritArmorPen = 0;

            ModItem mi = player.HeldItem.modItem;
            if (mi != null)
            {
                ReaperWeapon rw = mi as ReaperWeapon;
                if (rw != null) { isReaper = true; }
            }

        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (isReaper)
            {
                if (target.whoAmI == HuntedTarget)
                {
                    crit = true;



                }
                if (crit)
                {
                    damage = (int)(damage * reaperCritDamageMult);
                    player.armorPenetration += reaperCritArmorPen;
                }
                if (player.armorPenetration > target.defense) { damage = (int)(damage * (1 + (player.armorPenetration - target.defense) * 0.04)); }
            }



        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {


            if (isReaper)
            {
                if (target.whoAmI == HuntedTarget)
                {
                    crit = true;



                }
                if (crit)
                {
                    damage = (int)(damage * reaperCritDamageMult);
                    player.armorPenetration += reaperCritArmorPen;
                }
                if (player.armorPenetration > target.defense) { damage = (int)(damage * (1 + (player.armorPenetration - target.defense) * 0.01)); }
            }



        }


        public static int getSoulDamage()
        { return ((DRGNModWorld.downedDragon ? 75 : NPC.downedMoonlord ? 40 : NPC.downedMechBossAny ? 20 : Main.hardMode ? 10 : 5) * (DRGNModWorld.MentalMode ? 2 : 1)); }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {


            for (int i = 0; i < numSouls; i++)
            {
                Projectile proj =Projectile.NewProjectileDirect(player.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), getSoulDamage(), 0, player.whoAmI);               
            }
            numSouls = 0;
        }


    }
}
