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
    public class VoidFlesh : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Flesh");
            Tooltip.SetDefault("Such a bad smell it angers a beast from a different dimension");
        }
        public override void SetDefaults()
        {
            item.height =16;
            item.width = 16;
            item.scale = 2f;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

            
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("VoidSnakeHead"));
            return (!alreadySpawned);
        }
        public override bool UseItem(Player player)

        {

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("VoidSnakeHead")); // Spawn the boss within a range of the player. 
            }
            else { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("VoidSnakeHead")); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
           
        
        
                return true;
            


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"),12);
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
