﻿using System.Collections.Generic;
using System.Linq;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DRGN.Items.EngineerClass.Weapons
{
    // This class handles everything for our custom damage class
    // Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
    public abstract class EngineerWeapon : ModItem
    {
        public override bool CloneNewInstances => true;
        public int numBullets = 1;
        public float attackSpeed;
        public float baseAttackSpeed;
        public int baseDamage;
        public int damage;
        public float baseSpread;
        public float spread;
        public float damageMult = 1f;
        public float damageAdd = 0f;
        public float damageFlat = 0f;
        public int Crit = 0;
        public int Pierce = 1;
        public int ammoConsumeChance;
        public bool didConsumeAmmo;
        public int projType;
        public override void UpdateInventory(Player player) {
            ammoConsumeChance = 20;
            numBullets = 1;
            attackSpeed = baseAttackSpeed;
            damage = baseDamage;
            spread = baseSpread;
            damageMult = 1f;
            damageAdd = 0f;
            damageFlat = 0f;
            Crit = 0;
            projType = 0;
            Pierce = 1;
            if (player.GetModPlayer<DRGNPlayer>().barrelTier == 0)
            {
               
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 1)
            {
                numBullets += 1;
                damageMult *= 0.75f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 2)
            {
                numBullets += 3;
                damageMult *= 0.3f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 3)
            {
                numBullets += 7;
                spread += 4f;
                attackSpeed = attackSpeed * 0.85f;
                damageMult *= 0.2f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 4)
            {
                
                spread -= 6f;
                attackSpeed = attackSpeed * 0.75f;
                damageMult *= 2f;
                Pierce += 5;
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 5)
            {

                spread += 4f;
                attackSpeed = attackSpeed / 0.15f;
                damageMult *= 0.3f;
                ammoConsumeChance += 55;
                
            }
            else if (player.GetModPlayer<DRGNPlayer>().barrelTier == 6)
            {

                spread -= 1f;
                attackSpeed = attackSpeed * 0.75f;
                damageMult *= 0.8f;
                projType = 1;

            }

            if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 0)
            {
                
            }
            else if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 1)
            {
                spread -= 1f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 2)
            {
                spread += 2f;
                attackSpeed = attackSpeed / 0.85f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 3)
            {
                spread -= 2f;
                attackSpeed = attackSpeed * 0.85f;
                damageFlat += 10;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 4)
            {
                spread -= 3f;
                
                damageFlat += 20;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gunBodyTier == 5)
            {
                spread -= 3.5f;
                attackSpeed = attackSpeed / 0.85f;
                damageFlat += 35;
            }
            if (player.GetModPlayer<DRGNPlayer>().scopeTier == 0)
            {
                
                
            }
            else if (player.GetModPlayer<DRGNPlayer>().scopeTier == 1)
            {
                spread -= 4f;
                attackSpeed = attackSpeed * 0.75f;
                Crit += 15;
            }
            else if (player.GetModPlayer<DRGNPlayer>().scopeTier == 2)
            {
                spread -= 2f;
                
                Crit += 5;
            }
            else if (player.GetModPlayer<DRGNPlayer>().scopeTier == 3)
            {
                

                Crit += 30;
            }
            else if (player.GetModPlayer<DRGNPlayer>().scopeTier == 4)
            {
                damageFlat += 10;
                spread -= 4f;
                Crit += 25;
            }
            else if (player.GetModPlayer<DRGNPlayer>().scopeTier == 5)
            {
                damageFlat += 25;
                attackSpeed = attackSpeed / 0.9f;
                spread -= 5f;
                Crit += 35;
            }
            if (player.GetModPlayer<DRGNPlayer>().gripTier == 0)
            {
                
                
            }
            else if (player.GetModPlayer<DRGNPlayer>().gripTier == 1)
            {
                spread -= 1f;
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 4;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gripTier == 2)
            {
                spread += 2f;
                attackSpeed = attackSpeed / 0.80f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gripTier == 3)
            {
                spread -= 1f;
                attackSpeed = attackSpeed / 0.90f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gripTier == 4)
            {
                spread += 3f;
                attackSpeed = attackSpeed / 0.75f;
            }
            else if (player.GetModPlayer<DRGNPlayer>().gripTier == 5)
            {
                spread -= 3f;
                damageFlat += 15;
            }
            if (player.GetModPlayer<DRGNPlayer>().magTier == 0)
            {
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 2;
            }
            else if (player.GetModPlayer<DRGNPlayer>().magTier == 1)
            {
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 10;
            }
            else if (player.GetModPlayer<DRGNPlayer>().magTier == 2)
            {
                player.GetModPlayer<EngineerPlayer>().ReloadCounter2 -= (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2 * 0.5f);
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 4;
            }
            else if (player.GetModPlayer<DRGNPlayer>().magTier == 3)
            {
                player.GetModPlayer<EngineerPlayer>().ReloadCounter2 -= (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2 * 0.8f);
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 15;
            }
            else if (player.GetModPlayer<DRGNPlayer>().magTier == 4)
            {
                player.GetModPlayer<EngineerPlayer>().ReloadCounter2 -= (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2 * 0.6f);
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 20;
            }
            else if (player.GetModPlayer<DRGNPlayer>().magTier == 5)
            {
                player.GetModPlayer<EngineerPlayer>().ReloadCounter2 -= (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2 * 0.45f);
                player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 35;
            }
            if (player.GetModPlayer<DRGNPlayer>().chamberTier == 0)
            {
                
            }
            else if (player.GetModPlayer<DRGNPlayer>().chamberTier == 1)
            {
                attackSpeed = attackSpeed / 0.9f;
                damageFlat += 10;
            }
            else if (player.GetModPlayer<DRGNPlayer>().chamberTier == 2)
            {
                attackSpeed = attackSpeed / 0.8f;
                damageFlat += 20;
            }
            else if (player.GetModPlayer<DRGNPlayer>().chamberTier == 3)
            {
                attackSpeed = attackSpeed / 0.7f;
                damageFlat += 30;
            }
            else if (player.GetModPlayer<DRGNPlayer>().chamberTier == 4)
            {
                attackSpeed = attackSpeed / 0.6f;
                damageFlat += 40;
            }
            else if (player.GetModPlayer<DRGNPlayer>().chamberTier == 5)
            {
                attackSpeed = attackSpeed / 0.5f;
                damageFlat += 50;
            }
            if (spread < 0) { spread = 0; }
           

            if (attackSpeed < 2) { numBullets += (int)(Math.Abs(0 - attackSpeed)/2); attackSpeed = 2;  }


        }
        
        public override float UseTimeMultiplier(Player player)
        {

            return attackSpeed / baseAttackSpeed;

        }
        public override float MeleeSpeedMultiplier(Player player)
        {

            return attackSpeed / baseAttackSpeed;

        }


        // Custom items should override this to set their defaults
        public virtual void SafeSetDefaults()
        {
            
        }

        // By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
        // We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            // all vanilla damage types must be false for custom damage types to work
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        // As a modder, you could also opt to make these overrides also sealed. Up to the modder
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += EngineerPlayer.ModPlayer(player).engineerDamageAdd;
            mult *= EngineerPlayer.ModPlayer(player).engineerDamageMult;
            add += damageAdd;
            mult *= damageMult;
            flat += damageFlat;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            // Adds knockback bonuses
            knockback += EngineerPlayer.ModPlayer(player).engineerKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            // Adds crit bonuses
            crit += 4;
            crit += EngineerPlayer.ModPlayer(player).engineerCrit;
            crit += Crit;
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
                tt.text = damageValue + " engineer " + damageWord;
            }

           
        }
        public override bool ConsumeAmmo(Player player)
        {
            didConsumeAmmo = true;
            if (Main.rand.Next(0, 100) <= ammoConsumeChance)
            { didConsumeAmmo = false; return false; }
            return true;
        
        
        }

        // Make sure you can't use the item if you don't have enough resource and then use 10 resource otherwise.
        public override bool CanUseItem(Player player)
        {
            var EngineerPlayer = player.GetModPlayer<EngineerPlayer>();
            if (player.altFunctionUse == 2) { EngineerPlayer.Reload = true; return false; }
            if (DRGNPlayer.EngineerWeapon == false) { return false; }
                if (EngineerPlayer.BulletsCurrent >= 1 && EngineerPlayer.Reload == false)
            {
                if (didConsumeAmmo)
                {
                    EngineerPlayer.BulletsCurrent -= 1;
                }
                return true;
                
            }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 35f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int RealCrit = 4 + player.GetModPlayer<EngineerPlayer>().engineerCrit + Crit;
            for (int i = 0; i < numBullets; i++)
            {
                if (projType == 0)
                {
                    Projectile.NewProjectile(position, new Vector2(speedX + Main.rand.Next(-(int)(spread / 2), (int)(spread / 2)), speedY + Main.rand.Next(-(int)(spread), (int)(spread))), mod.ProjectileType("EngineerBullet"), damage, knockBack, player.whoAmI, Pierce, RealCrit);
                }
                else if (projType == 1)
                {
                    Projectile.NewProjectile(position, new Vector2(speedX + Main.rand.Next(-(int)(spread / 2), (int)(spread / 2)), speedY + Main.rand.Next(-(int)(spread), (int)(spread))), mod.ProjectileType("MegaEngineerElectroBall"), damage, knockBack, player.whoAmI, Pierce, RealCrit);
                }



            }
           




            return false;
        }
        public override bool AltFunctionUse(Player player)
        { return true; }





    }
}