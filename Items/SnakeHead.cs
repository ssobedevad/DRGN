using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
    

namespace DRGN.Items
{
    public class SnakeHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snake Head");
            Tooltip.SetDefault("Dont know how it got there");
        }
        public override void SetDefaults()
        {
            item.height = 26;
            item.width = 28;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {
            bool day = Main.dayTime;
            bool desert = player.ZoneDesert;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("DesertSerpent"));
            return (!alreadySpawned && desert && day);
        }
        public override bool UseItem(Player player)

        {
            
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DesertSerpent")); // Spawn the boss within a range of the player. 
                Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;
                
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddIngredient(ItemID.SandBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
