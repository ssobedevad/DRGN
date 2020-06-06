using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items
{
    public class FireDragonEgg : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire dragon egg");
            Tooltip.SetDefault("Summons an angry beast");
        }
        public override void SetDefaults()
        {
            item.height = 23;
            item.width = 25;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {
            bool dragonBiome = player.GetModPlayer<DRGNPlayer>().DragonBiome;
            bool moonLord = NPC.downedMoonlord;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("FireDragon"));
            return (!alreadySpawned && moonLord && dragonBiome) ;
        }
        public override bool UseItem(Player player)
       
        {
            
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("FireDragon")); // Spawn the boss within a range of the player. 
                Main.PlaySound(SoundID.Roar, player.Right, 0);
                return true;
            
            
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.CelestialSigil);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
