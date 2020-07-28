using DRGN.Buffs;
using DRGN.Items.EngineerClass;
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DRGN
{
    public class DRGNPlayer : ModPlayer
    {
        public bool DragonBiome;
        public bool AntBiome;
        public bool VoidBiome;

        public bool secondlife;
        public int lifeQuality;
        public bool NinjaSuit;
        public bool brawlerGlove;
        public bool beeVeil;
        public bool protectorsVeil;
        public int dodgeCounter;
        public int dodgeCounterMax;
        public int lifeCounter;
        public int lifeCounterMax;

        public bool snakeArmorSet;
        public bool toxicArmorSet;
        public bool glacialArmorSet;
        public bool cloudArmorSet;
        public bool dragonArmorSet;
        public bool voidArmorSet;
        public bool galactiteArmorSet;
        public bool technoArmorSet;
        public bool rockArmorSet;

        public bool dsEquip;
        public bool ksEquip;
        public bool eocEquip;
        public bool eowEquip;
        public bool bocEquip;
        public bool tfEquip;
        public bool qbEquip;
        public bool skEquip;
        public bool qaEquip;
        public bool wofEquip;
        public bool ifEquip;
        public bool ttEquip;
        public bool tdEquip;
        public bool spEquip;
        public bool ptEquip;
        public bool gmEquip;
        public bool clEquip;
        public bool lcEquip;
        public bool mlEquip;
        public bool dfEquip;
        public bool fdEquip;
        public bool vsEquip;
        public bool frEquip;
        
        public int critCountResetable;
        public int freezecounter;
        public int freezeCounterMax;
        public int timeWarpCounter;
        public int timeWarpCounterMax;
        public Vector4[] oldPos = new Vector4[60];
        public bool SuperYoyoBag;
        public int maxYoyos;
        public int maxFlails;

        
        public bool melting;
        public bool burning;
        public bool shocked;
        public bool galacticCurse;

        public static int[] VoidEffect = new int[255];
       
        public int defenseLevel = 0;
        public bool tvEquip;
        public bool ggEquip;
        public int heartEmblem;
        public const int heartEmblemMax = 10;

        public bool lunarBlessing;
        public bool voidDebuffReduced;
        public float lifeSteal;

        public bool EngineerWeapon;
        public Item gunBodyType;
        public Item barrelType;
        public Item scopeType;
        public Item gripType;
        public Item magType;
        public Item chamberType;
        public int gunBodyTier, barrelTier, scopeTier, gripTier, magTier, chamberTier;

        public float YoyoDamageInc;
        public int YoyoBonusCrit;

        public float FlailDamageInc;
        public int FlailBonusCrit;


        public int summonTagDamage;
        public int summonTagCrit;




        public override void ResetEffects()
        {
            YoyoDamageInc = 0;
            YoyoBonusCrit = 0;
            FlailDamageInc = 0;
            FlailBonusCrit = 0;
            snakeArmorSet = false;
            toxicArmorSet = false;
            glacialArmorSet = false;
            cloudArmorSet = false;
            technoArmorSet = false;
            lifeSteal = 0f;
            dragonArmorSet = false;
            dragonArmorSet = false;
            voidArmorSet = false;
            galactiteArmorSet = false;
            rockArmorSet = false;
            voidDebuffReduced = false;
            dsEquip = false;
            ksEquip = false;
            eocEquip = false;
            eowEquip = false;
            bocEquip = false;
            tfEquip = false;
            qbEquip = false;
            skEquip = false;
            qaEquip = false;
            wofEquip = false;
            ifEquip = false;
            ttEquip = false;
            tdEquip = false;
            spEquip = false;
            ptEquip = false;
            gmEquip = false;
            clEquip = false;
            mlEquip = false;
            dfEquip = false;
            fdEquip = false;
            vsEquip = false;
            ggEquip = false;
            SuperYoyoBag = false;
            maxYoyos = 1;
            maxFlails = 1;
            if(!player.HasBuff(mod.BuffType("Shielded"))) { defenseLevel = 0; }
          
            

            NinjaSuit = false;
            secondlife = false;
            brawlerGlove = false;
            beeVeil = false;
            protectorsVeil = false;
            EngineerWeapon = false;
            if (lunarBlessing) { player.extraAccessorySlots += 1; }
            player.statLifeMax2 += 5 * heartEmblem;
            for (int i = 0; i < 59; i++)
            { if (player.inventory[i].type == mod.ItemType("EngineerRifle") || player.inventory[i].type == mod.ItemType("EngineerRifleTier1") || player.inventory[i].type == mod.ItemType("EngineerRifleTier2") || player.inventory[i].type == mod.ItemType("EngineerRifleTier3") || player.inventory[i].type == mod.ItemType("EngineerRifleTier4") || player.inventory[i].type == mod.ItemType("EngineerRifleTier5") || player.inventory[i].type == mod.ItemType("EngineerRifleTier6") || player.inventory[i].type == mod.ItemType("EngineerRifleTier7") || player.inventory[i].type == mod.ItemType("EngineerRifleTier8")) { EngineerWeapon = true; } }
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];


                if (item.type == mod.ItemType("NinjaSuit"))
                {
                    NinjaSuit = true;
                    dodgeCounterMax = 1200;
                    if (dodgeCounter < dodgeCounterMax)
                    {
                        dodgeCounter += 1;

                    }
                    if (dodgeCounter == dodgeCounterMax) { player.AddBuff(mod.BuffType("FromTheShadows"), 2); }
                }
                else if (item.type == mod.ItemType("EssenceofExpert") || item.type == mod.ItemType("CrystalofCharisma") || item.type == mod.ItemType("PowderofCourage"))
                {
                    secondlife = true;
                    if (item.type == mod.ItemType("EssenceofExpert"))
                    {
                        lifeQuality = 10; lifeCounterMax = 10000;
                    }
                    else if (item.type == mod.ItemType("CrystalofCharisma"))
                    { lifeQuality = 5; lifeCounterMax = 11000; }
                    else if (item.type == mod.ItemType("PowderofCourage"))
                    { lifeQuality = 2; lifeCounterMax = 12000; }

                    if (lifeCounter < lifeCounterMax) { lifeCounter += 1; }
                    if (lifeCounter > lifeCounterMax) { lifeCounter = lifeCounterMax; }
                    else { player.AddBuff(mod.BuffType("Revival"), 2); }
                }
                else if (item.type == mod.ItemType("GalactiteBrawlerGloves"))
                { brawlerGlove = true; }
                else if (item.type == mod.ItemType("ProtectorsVeil"))
                { protectorsVeil = true; }
                else if (item.type == mod.ItemType("BeeVeil"))
                { beeVeil = true; }

            }
            if (player.FindBuffIndex(mod.BuffType("Melting")) == -1)
            { melting = false; }
            if (player.FindBuffIndex(mod.BuffType("Burning")) == -1)
            { burning = false; }
            if (player.FindBuffIndex(mod.BuffType("GalacticCurse")) == -1)
            { galacticCurse = false; }
            if (player.FindBuffIndex(mod.BuffType("Shocked")) == -1)
            { shocked = false; }



        }


        public override TagCompound Save()
        {

            return new TagCompound
            {
                {"Ks", ksEquip },
                {"Virus", tvEquip },
                {"HEmblem", heartEmblem },
                { "LBlessing", lunarBlessing },
                { "GBody", gunBodyType },
                { "GBarrel", barrelType },
                { "GChamber", chamberType },
                { "GMag", magType },
                { "GGrip", gripType },
                { "GScope", scopeType },
                { "GBodyTier", gunBodyTier },
                { "GBarrelTier", barrelTier },
                { "GChamberTier", chamberTier },
                { "GMagTier", magTier },
                { "GGripTier", gripTier },
                { "GScopeTier", scopeTier },
               
            };

        }
        public override void Load(TagCompound tag)
        {
            tvEquip = tag.GetBool("Ks");
            tvEquip = tag.GetBool("Virus");
            heartEmblem = tag.GetInt("HEmblem");
            lunarBlessing = tag.GetBool("LBlessing");
            gunBodyType = tag.Get<Item>("GBody");
            barrelType = tag.Get<Item>("GBarrel");
            chamberType = tag.Get<Item>("GChamber");
            magType = tag.Get<Item>("GMag");
            gripType = tag.Get<Item>("GGrip");
            scopeType = tag.Get<Item>("GScope");
            gunBodyTier = tag.GetInt("GBodyTier");
            barrelTier = tag.GetInt("GBarrelTier");
            chamberTier = tag.GetInt("GChamberTier");
            magTier = tag.GetInt("GMagTier");
            gripTier = tag.GetInt("GGripTier");
            scopeTier = tag.GetInt("GScopeTier");
            

        }

        public override void UpdateBadLifeRegen()
        {
            if (melting)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= DRGNModWorld.MentalMode ? 20 : 10;
                
            }
            if (burning)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= DRGNModWorld.MentalMode ? 48 : 24; ;
            }
            if (galacticCurse)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= DRGNModWorld.MentalMode ? 200 : 100;
            }
            if (shocked)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= DRGNModWorld.MentalMode ? 36 : 18;
            }

        }
        public override void PostUpdateEquips()
        {
            if (ksEquip) {  player.magicCrit += 5; player.thrownCrit += 5; player.meleeCrit += 5; player.rangedCrit += 5; player.GetModPlayer<EngineerPlayer>().engineerCrit += 5; }
            if (player.yoyoGlove) { maxYoyos += 1; }
            if (rockArmorSet) { maxYoyos += 1; maxFlails += 1; }
            if (SuperYoyoBag) { maxYoyos += 3; }
            if (toxicArmorSet && Main.rand.Next(0, 60) == 1) { Projectile.NewProjectile(player.Center.X + Main.rand.Next(-300, 300), player.Center.Y + Main.rand.Next(-200, 200), 0, 0, mod.ProjectileType("ToxicBubble"), 25, 1f, player.whoAmI); }
            else if (snakeArmorSet)
            {
                player.buffImmune[BuffID.Poisoned] = true; ;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;

                if (player.ZoneJungle) { player.statDefense += 10; }
            }
            else if (cloudArmorSet && (player.ownedProjectileCounts[mod.ProjectileType("Sun")] == 0)) {player.AddBuff(mod.BuffType("Sun"), 2); Projectile.NewProjectile(player.Center.X, player.Center.Y - 10, 0, 0, mod.ProjectileType("Sun"), 100, 1f, player.whoAmI); }

            else if (galactiteArmorSet && (player.ownedProjectileCounts[mod.ProjectileType("GalactiteStar")] == 0)) { player.AddBuff(mod.BuffType("GalactiteStar"), 2); Projectile.NewProjectile(player.Center.X, player.Center.Y - 10, 0, 0, mod.ProjectileType("GalactiteStar"), 1000, 1f, player.whoAmI); }
            
            if (eocEquip) { player.nightVision = true; Lighting.AddLight((int)((player.Center.X + (float)(player.width / 2)) / 16f), (int)((player.Center.Y + (float)(player.height / 2)) / 16f), 2f, 2f, 2f); player.magicCrit += 10; player.thrownCrit += 10; player.meleeCrit += 10; player.rangedCrit += 10;player.GetModPlayer<EngineerPlayer>().engineerCrit += 10; }
            if (eowEquip) { player.allDamageMult *= (1f + (critCountResetable / 400f)); }
            if (eowEquip || bocEquip || fdEquip) { player.AddBuff(ModContent.BuffType<CritCounter>(), 2); }
            
            if (bocEquip) { lifeSteal += (0.01f * critCountResetable); }
            if (tfEquip)
            {
                player.buffImmune[BuffID.Poisoned] = true; ;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
                if (player.ZoneJungle) { player.statDefense += 25; }
            }
            if (qbEquip) { player.honey = true; player.strongBees = true; player.bee = true; player.beeDamage(35); player.beeKB(3); }
            if (skEquip) { player.lavaImmune = true; player.gills = true; player.accFlipper = true; if (player.lavaWet) { player.ignoreWater = true;player.moveSpeed *= 3; player.statDefense += 25; player.allDamageMult *= 1.35f; } }
            if (ifEquip) { freezeCounterMax = 2800; if (freezecounter < freezeCounterMax) { freezecounter += 1; } else { int dustid = Dust.NewDust(player.position, player.width, player.height, DustID.Ice); Main.dust[dustid].noGravity = true; } }
           
           
            if (ptEquip && Main.rand.Next(0, 20) == 1) { Projectile.NewProjectile(player.Center.X + Main.rand.Next(-400, 400), player.Center.Y + Main.rand.Next(-400, 400), 0, 0, mod.ProjectileType("Bulb"), 0, 1f, player.whoAmI); }
            if (gmEquip) { player.armorPenetration += 15; player.lifeSteal += 0.05f; player.shinyStone = true; }
            if (clEquip) { if (Main.dayTime) { player.allDamageMult *= 1.25f; } else { player.statDefense += 30; player.statLifeMax2 += 75; } }
            if (lcEquip) { if (NPC.LunarApocalypseIsUp) { lifeSteal += 1f; player.longInvince = true; player.shadowDodge = true; } }
            if (mlEquip) { player.maxRunSpeed *= 2; player.moveSpeed *= 2; player.maxMinions += 2; player.allDamageMult *= 1.2f; player.jumpSpeedBoost *= 2; player.statDefense += 20; }
            if (dfEquip) { player.wingTime = 1; player.magicQuiver = true; player.frostArmor = true; }
            if (fdEquip) { player.blackBelt = true; player.allDamageMult *= (1f + (critCountResetable * 0.04f)); lifeSteal += (0.015f * critCountResetable); player.statDefense += critCountResetable; player.statLifeMax2 += critCountResetable; player.statManaMax2 += critCountResetable; }
            if (spEquip && player.HasBuff(mod.BuffType("Shielded")))
            {
                player.statDefense += (int)((float)player.statDefense * (0.5f + (0.1f * (float)defenseLevel)));
            }
        }
        public override void PostUpdate()
        {
            if(technoArmorSet)
            {
                if (Main.time % 30 == 0)
                {
                    int numShots = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        int dustid = Dust.NewDust(new Vector2(player.Center.X + (float)Math.Cos((0.0314f * i)) * 300, player.Center.Y + (float)Math.Sin((0.0314f * i)) * 300), 1, 1, 107);
                        Main.dust[dustid].noGravity = true;


                        if (Main.npc[i].CanBeChasedBy(this, false) && Vector2.Distance(Main.npc[i].Center, player.Center) < 300f)
                        {
                            Main.npc[i].AddBuff(mod.BuffType("Bugged"), 60);
                            if (numShots < 5)
                            {
                                Vector2 vel = Main.npc[i].Center - player.Center;
                                vel = Vector2.Normalize(vel) * 12;
                                Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("BinaryShot"), 25, 0f, player.whoAmI);
                                numShots++;
                            }
                            
                        }
                    }
                }


                   
            
            
            }
            if (frEquip)
            {
                timeWarpCounterMax = 240;
                if (timeWarpCounter < timeWarpCounterMax)
                {
                    timeWarpCounter += 1;
                }
                else
                {
                    bool exists = false; for (int i = 0; i < Main.projectile.Length; i++) { if (Main.projectile[i].type == mod.ProjectileType("PlayerGhost") && Main.projectile[i].owner == player.whoAmI) { exists = true; break; } }
                    if (!exists) { Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("PlayerGhost"), 0, 0f, player.whoAmI); }
                }
            }
            for (int i = oldPos.Length - 1; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = new Vector4(player.Center.X, player.Center.Y - 6, player.statLife, player.direction); }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }



                if (oldPos[i] == Vector4.Zero) { oldPos[i] = new Vector4(player.Center.X, player.Center.Y, player.statLife, player.direction); }

            }

        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (ttEquip) { int[] possibleProjectiles = new int[3] { ProjectileID.CursedFlameFriendly, ProjectileID.DeathLaser, ModContent.ProjectileType<IchorFlame>() }; int projid = Projectile.NewProjectile(position, new Vector2(speedX, speedY), Main.rand.Next(possibleProjectiles), damage, knockBack, Main.myPlayer); Main.projectile[projid].hostile = false; Main.projectile[projid].friendly = true; }
            return true;
        }
        public override void UpdateBiomes()
        {
            DragonBiome = (DRGNModWorld.DragonDen > 20);
            VoidBiome = (DRGNModWorld.isVoidBiome > 20);
            AntBiome = (DRGNModWorld.isAntBiome > 20);
           
        }
        public override bool CustomBiomesMatch(Player other)
        {
            DRGNPlayer modOther = other.GetModPlayer<DRGNPlayer>();
            bool allMatch = (DragonBiome == modOther.DragonBiome)?((VoidBiome == modOther.VoidBiome) ? ((AntBiome == modOther.AntBiome) ? true : false) : false) : false;

            return allMatch;

        }

        public override void CopyCustomBiomesTo(Player other)
        {
            DRGNPlayer modOther = other.GetModPlayer<DRGNPlayer>();
            modOther.DragonBiome = DragonBiome;
            modOther.VoidBiome = VoidBiome;
            modOther.AntBiome = AntBiome;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = DragonBiome;
            flags[1] = VoidBiome;
            flags[2] = AntBiome;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            DragonBiome = flags[0];
            VoidBiome = flags[1];
            AntBiome = flags[2];
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (ggEquip && !npc.boss) { damage = -1; crit = false; }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            
            if (NinjaSuit == true && dodgeCounter == dodgeCounterMax)
            {


                player.statLife += damage;
                player.HealEffect(damage);
                player.immune = true;
                player.immuneTime = 100;
                dodgeCounter = 0;

                for (int i = 0; i < 55; i++)
                {
                    int DustID = Dust.NewDust(player.Center, 0, 0, 182, 0.0f, 0.0f, 10, default(Color), 2.5f);
                    Main.dust[DustID].noGravity = true;
                }
                return false;

            }
            else if (ifEquip && freezecounter >= freezeCounterMax && (damage >= 180 || damage > player.statLife))
            { player.AddBuff(BuffID.Frozen, 120); player.immuneTime =120; player.immune = true; freezecounter = 0; player.statLife += 100; player.HealEffect(100); return false; }
            else
            {

                return true;
            }


        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            critCountResetable = 0;
            if (spEquip) { if (!player.HasBuff(mod.BuffType("Shielded"))) { player.AddBuff(mod.BuffType("Shielded"), 180); } else if(defenseLevel < 20) { defenseLevel += 1; }}
        }


        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.HasItem(mod.ItemType("OmegaHealingPotion")) && !player.HasBuff(BuffID.PotionSickness)) 
            { 
                player.ConsumeItem(mod.ItemType("OmegaHealingPotion")); player.immune = true;
                player.AddBuff(BuffID.PotionSickness, 7200);
                player.immuneTime = 180;

                if (player.statLife + 450 < player.statLifeMax2)
                {
                    player.statLife += 450;
                    player.HealEffect(450);
                }
                else
                {
                    player.HealEffect(player.statLifeMax2-player.statLife);
                    player.statLife = player.statLifeMax2;
                }
                return false; 
            }

            if (secondlife == true && lifeCounter == lifeCounterMax)
            {

                player.statLife = ((int)player.statLifeMax2 / 10) * lifeQuality;
                player.HealEffect((int)player.statLifeMax2 / 10 * lifeQuality);
                player.immune = true;
                player.immuneTime = 200;
                lifeCounter = 0;
                return false;
            }
            else { return true; }

        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (ggEquip && !target.boss) { crit = true; damage = target.lifeMax; }
            if (crit)
            {
                if (dsEquip  ) { player.armorPenetration += 10; target.AddBuff(ModContent.BuffType<Melting>(), 180); }
                if (qaEquip  )
                {
                    int[] buffchoice = new int[3] { ModContent.BuffType<Shocked>(), ModContent.BuffType<Burning>(), ModContent.BuffType<Melting>() }; target.AddBuff(Main.rand.Next(buffchoice), 220);
                    for (int i = 0; i < 2; i++)
                    { Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), 40, 1f, player.whoAmI); }
                }
                if (  wofEquip) { damage = (int)(damage * 1.25); player.armorPenetration += 10; }
                if (  tvEquip) { damage = (int)(damage * 1.1); }
                if (ksEquip) { player.armorPenetration += 8; }
            }
            
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ggEquip && !target.boss) { crit = true; damage = target.lifeMax; }
            if (crit)
            {
                if (dsEquip ) { player.armorPenetration += 10; target.AddBuff(ModContent.BuffType<Melting>(), 180); }
                if (qaEquip )
                {
                    int[] buffchoice = new int[3] { ModContent.BuffType<Shocked>(), ModContent.BuffType<Burning>(), ModContent.BuffType<Melting>() }; target.AddBuff(Main.rand.Next(buffchoice), 220);
                    for (int i = 0; i < 2; i++)
                    { Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), 40, 1f, player.whoAmI); }
                }
                if ( wofEquip) { damage = (int)(damage * 1.25); player.armorPenetration += 10; }
                if ( tvEquip) { damage = (int)(damage * 1.1); }
                if (ksEquip) { player.armorPenetration += 8; }
            }


            if ((proj.minion || ProjectileID.Sets.MinionShot[proj.type]) && target.whoAmI == player.MinionAttackTargetNPC) { damage += summonTagDamage; if (summonTagCrit > 0) { if (Main.rand.Next(1, 101) < summonTagCrit) { crit = true; } } }
        
    }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (brawlerGlove) { target.AddBuff(mod.BuffType("GalacticCurse"), 280); }
            if (dragonArmorSet && Main.rand.Next(60) == 0 && target.CanBeChasedBy(this) && target.boss)
            {
                bool exists = false; for (int i = 0; i < Main.projectile.Length; i++) { if (Main.projectile[i].type == mod.ProjectileType("FireWall")) { exists = true; break; } }
                if (!exists) { Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("FireWall"), 0, 0f, player.whoAmI); }
            }
            if (glacialArmorSet && Main.rand.Next(20) == 0 && target.CanBeChasedBy(this)) { Projectile.NewProjectile(target.Center.X, target.Center.Y - 500, (float)(Main.rand.Next(-100, 100)) / 100f, 5, mod.ProjectileType("Icicle"), 50, 1f, player.whoAmI); }
            if (crit && target.CanBeChasedBy(this) && target.boss)
            {
               
                if (critCountResetable < 100) { critCountResetable += 1; }
                if (tdEquip && Main.rand.Next(0,2) == 0) { Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<ProbeFriendly>(), damage, 1f, Main.myPlayer); }
                if (vsEquip) { target.AddBuff(ModContent.BuffType<VoidBuff>(), 120); }
            }
            if (lifeSteal > 0f && target.CanBeChasedBy(this) && crit) 
            { 
                int healing = (int)(damage * lifeSteal / 500f); 
                if (healing < 1) 
                { 
                    healing = 1; 
                }
                player.statLife = (player.statLife + healing < player.statLifeMax2) ? player.statLife + healing : player.statLifeMax2; player.HealEffect(healing);
            }


        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {

            if (brawlerGlove ) { target.AddBuff(mod.BuffType("GalacticCurse"), 280); }
            if (dragonArmorSet && Main.rand.Next(120) == 0 && target.CanBeChasedBy(this))
            {
                bool exists = false; for (int i = 0; i < Main.projectile.Length; i++) { if (Main.projectile[i].type == mod.ProjectileType("FireWall")) { exists = true; break; } }
                if (!exists) { Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("FireWall"), 0, 0f, player.whoAmI); }
            }
            if (glacialArmorSet && Main.rand.Next(20) == 0) { Projectile.NewProjectile(target.Center.X, target.Center.Y - 500, (float)(Main.rand.Next(-100, 100)) / 100f, 5, mod.ProjectileType("Icicle"), 50, 1f, player.whoAmI); }
            if (crit && target.CanBeChasedBy(this))
            {
                
                if (critCountResetable < 100) { critCountResetable += 1; }
                if (tdEquip) { Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<ProbeFriendly>(), damage, 1f, Main.myPlayer); }
                if (vsEquip) { target.AddBuff(ModContent.BuffType<VoidBuff>(), 120); }
            }
            if (lifeSteal > 0f && target.CanBeChasedBy(this) && crit)
            {
                int healing = (int)(damage * lifeSteal / 1000f); 
                if (healing < 1) 
                { 
                    healing = 1;  
                }
                player.statLife = (player.statLife + healing < player.statLifeMax2) ? player.statLife + healing : player.statLifeMax2; player.HealEffect(healing);
            }
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {

            if (protectorsVeil)
            {

                Projectile.NewProjectile(player.Center.X + Main.rand.Next(-5, 5), player.Center.Y + Main.rand.Next(-5, 5), 0, 0, mod.ProjectileType("OmegaStarBee"), 280, 1f, player.whoAmI, 0);
                player.immune = true;
                player.immuneTime = 140;

            }
            else if (beeVeil)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-5, 5), player.Center.Y + Main.rand.Next(-5, 5), 0, 0, mod.ProjectileType("StarBee"), 80, 1f, player.whoAmI, 0);

                }
                player.immune = true;
                player.immuneTime = 75;
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("MossyBowWood"));
            item.stack = 1;
            items.Add(item);
            item = new Item();
            item.SetDefaults(mod.ItemType("SnappedHandle"));
            item.stack = 1;
            items.Add(item);
            item = new Item();
            item.SetDefaults(mod.ItemType("TornBook"));
            item.stack = 1;
            items.Add(item);
            
            item = new Item();
            item.SetDefaults(mod.ItemType("CursedHeart"));
            item.stack = 1;
            items.Add(item);
            if (Main.expertMode)
            {
                item = new Item();
                item.SetDefaults(mod.ItemType("PowderofCourage"));
                item.stack = 1;
                items.Add(item);
            }

        }






    }
}
