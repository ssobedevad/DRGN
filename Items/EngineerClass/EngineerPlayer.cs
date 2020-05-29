using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.Localization;

namespace DRGN.Items.EngineerClass
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
    public class EngineerPlayer : ModPlayer
    {
        public static EngineerPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<EngineerPlayer>();
        }
        
        // Vanilla only really has damage multipliers in code
        // And crit and knockback is usually just added to
        // As a modder, you could make separate variables for multipliers and simple addition bonuses
        public float engineerDamageAdd;
        public float engineerDamageMult = 1f;
        public float engineerKnockback;
        public int engineerCrit;
        public bool Flareproj;
        public bool Voidproj;
        // Here we include a custom resource, similar to mana or health.
        // Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
        public int BulletsCurrent;
        public const int DefaultMaxBullets = 6;
        public int MaxBullets;
        public int MaxBullets2;
        public const int DefaultReloadTimer = 120;
        public int ReloadCounter;
        public int ReloadCounter2 = 120;
        public bool Reload;
        

        /*
		In order to make the Example Resource example straightforward, several things have been left out that would be needed for a fully functional resource similar to mana and health. 
		Here are additional things you might need to implement if you intend to make a custom resource:
		- Multiplayer Syncing: The current example doesn't require MP code, but pretty much any additional functionality will require this. ModPlayer.SendClientChanges and clientClone will be necessary, as well as SyncPlayer if you allow the user to increase exampleResourceMax.
		- Save/Load and increased max resource: You'll need to implement Save/Load to remember increases to your exampleResourceMax cap.
		- Resouce replenishment item: Use GlobalNPC.NPCLoot to drop the item. ModItem.OnPickup and ModItem.ItemSpace will allow it to behave like Mana Star or Heart. Use code similar to Player.HealEffect to spawn (and sync) a colored number suitable to your resource.
		*/

        public override void Initialize()
        {
            MaxBullets = DefaultMaxBullets;
           
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            engineerDamageAdd = 0f;
            engineerDamageMult = 1f;
            engineerKnockback = 0f;
            engineerCrit = 0;
            ReloadCounter2 = DefaultReloadTimer;
            MaxBullets2 = MaxBullets;
        }

        public override void PostUpdateMiscEffects()
        {
            UpdateResource();
        }

        // Lets do all our logic for the custom resource here, such as limiting it, increasing it and so on.
        private void UpdateResource()
        {
            // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
             //Increase it by 60 per second, or 1 per tick.

            // A simple timer that goes up to 3 seconds, increases the exampleResourceCurrent by 1 and then resets back to 0.
            if (BulletsCurrent <= 0)
            {
                Reload = true;
                
            }
            if (ReloadCounter >= ReloadCounter2) { Reload = false; BulletsCurrent = MaxBullets2; ReloadCounter = 0; }
            if (Reload) { ReloadCounter += 1; }

            // Limit exampleResourceCurrent from going over the limit imposed by exampleResourceMax.
            BulletsCurrent = Utils.Clamp(BulletsCurrent, 0, MaxBullets2);
        }
        
        public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Flareproj && Main.rand.Next(0,20)==0) { Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner); }
            if (Voidproj) { target.AddBuff(mod.BuffType("VoidBuff"), 120); }
        
    }
    }
}
