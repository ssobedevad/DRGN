using DRGN.Items.Weapons.ReaperWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
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
        public int rotatingScythes;
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
        public float SoulPickUpRangeBoost = 1f;
        public int mode;
        public int executeLifeThresholdBoost = 0;
        public int animationTime;
        public const int animationTimeMax = 30;
        public Vector2 initialPos;
        public bool isReaper
        {
            get => (player.HeldItem.modItem != null && player.HeldItem.modItem is ReaperWeapon);
        }

        public int bloodHuntExtraRange = 0;
        public int stabDashCd = 0;
        public int scytheThrowCd = 0;
        public int soulOverchargeLevel;


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
            int numReapers = 0;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead && Main.player[i].GetModPlayer<ReaperPlayer>().isReaper) { numReapers += 1; }
            }
            DRGNModWorld.ActiveReaperCount = numReapers;
            if (HuntedTarget != -1)
            {
                if (Main.npc[HuntedTarget].active == false || Vector2.Distance(Main.npc[HuntedTarget].Center, player.Center) > 1000)
                {
                    HuntedTarget = -1; mode = 0; animationTime = animationTimeMax;
                }
            }
            if (isReaper)
            {

                if (HuntedTarget == -1)
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
            if (!player.HasBuff(mod.BuffType("SoulOvercharge")) && soulOverchargeLevel > 0)
            { soulOverchargeLevel = 0; }
            if (DRGN.ReaperExecute.JustPressed && mode == 0)
            {
                if (player.GetModPlayer<ReaperPlayer>().HuntedTarget > -1 && CanTargetExecute(Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget],player))
                {                   
                    mode = 1;
                    animationTime = animationTimeMax;
                }
            }
            if (mode > 0 && player.GetModPlayer<ReaperPlayer>().HuntedTarget > -1)
            {
                player.immuneTime = animationTime;
                player.immune = true;
                NPC tpNPC = Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget];
                int Side = player.Center.X > tpNPC.Center.X ? 1 : -1;
                player.ChangeDir(Side * -1);
                if (animationTime > animationTimeMax * 0.6f)
                {
                    if (mode == 1) { initialPos = player.Center; mode = 2; }
                    Vector2 targetPos = tpNPC.Center + new Vector2(Side * tpNPC.width * 2, -100);
                    targetPos -= initialPos;
                    player.Center = initialPos + (targetPos * ((float)(animationTimeMax - animationTime) / (animationTimeMax * 0.6f)));
                }
                else if (animationTime > animationTimeMax * 0.2f)
                {
                    if (mode == 2) { initialPos = player.Center; mode = 3; }
                    Vector2 targetPos = tpNPC.Center + new Vector2(Side, -10);
                    targetPos -= initialPos;
                    player.Center = initialPos + (targetPos * ((float)(animationTimeMax * 0.6f - animationTime) / (animationTimeMax * 0.4f)));
                }
                else if (mode == 3)
                {
                    Execute(tpNPC);
                    mode = 0;
                }
                animationTime --;
            }
        }
        public void Execute(NPC npc)
        {          
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, npc.lifeMax, 0, player.direction, 1);
            }
            npc.StrikeNPC(npc.lifeMax, 0f, 1, true);
        }
        public static bool CanTargetExecute(NPC npc , Player player)
        {
            int lifeThreshold = DRGNModWorld.MentalMode? 30 : Main.expertMode ? 20 : 10;           
            lifeThreshold += player.armorPenetration + player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen + player.GetModPlayer<ReaperPlayer>().numSouls + player.GetModPlayer<ReaperPlayer>().executeLifeThresholdBoost;           
            lifeThreshold = (int)(lifeThreshold * player.GetModPlayer<ReaperPlayer>().reaperDamageMult * player.allDamageMult * player.allDamage * player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult);
            if (npc.boss) { lifeThreshold *= 4; }
            Main.NewText(lifeThreshold);
            if(npc.life <= lifeThreshold) { return true; }
            return false;
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
            executeLifeThresholdBoost = 0;
            SoulPickUpRangeBoost = 1f;
            rotatingScythes = 0;
            maxSouls2 = maxSouls;
            damageIncPerSoul = 0.005f;
            if (stabDashCd > 0) { stabDashCd -= 1; }
            else { stabDashCd = 0; }
            if (scytheThrowCd > 0) { scytheThrowCd -= 1; }
            else { scytheThrowCd = 0; }

            bloodHuntExtraRange = 0;

            reaperDamageMult = 1f;
            reaperKnockback = 0f;
            reaperCrit = 0;
            reaperCritDamageMult = 1f;
            reaperCritArmorPen = 0;


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
                    damage = (int)(damage * reaperCritDamageMult * 0.8f);
                    player.armorPenetration += reaperCritArmorPen;
                }
                if (player.armorPenetration > target.defense) { damage = (int)(damage * (1 + (player.armorPenetration - target.defense) * 0.01f)); }
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
                if (player.armorPenetration > target.defense) { damage = (int)(damage * (1 + (player.armorPenetration - target.defense) * 0.005f)); }
            }



        }


        public static int getSoulDamage()
        { return ((DRGNModWorld.downedDragon ? 75 : NPC.downedMoonlord ? 40 : NPC.downedMechBossAny ? 20 : Main.hardMode ? 10 : 5) * (DRGNModWorld.MentalMode ? 2 : 1)); }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {


            for (int i = 0; i < numSouls; i++)
            {
                Projectile.NewProjectileDirect(player.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), getSoulDamage(), 0, player.whoAmI);
            }
            numSouls = 0;
        }


    }
}
