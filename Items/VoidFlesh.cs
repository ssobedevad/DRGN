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

            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("VoidSnakeHead")); // Spawn the boss within a range of the player. 
            }
            else { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("VoidSnakeHead")); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            int x = (int)(player.Center.X) / 16 - 70;
            int y = (int)(player.Center.Y) / 16 - 60;




            for (int i = 0; i <= 140; i++)//top line
            {
                if (!Main.tile[x + i, y].active())
                {
                    Main.tile[x + i, y].active(true);
                    Main.tile[x + i, y].type = (ushort)mod.TileType("VoidBrickTileArena");
                }


            }

            for (int j = 0; j < 120; j++)// loop for each row i is column j is row
            {


                if (!Main.tile[x, y + j].active())
                {

                    Main.tile[x, y + j].active(true);
                    Main.tile[x, y + j].type = (ushort)mod.TileType("VoidBrickTileArena");
                }
                if (!Main.tile[x + 140, y + j].active())
                {
                    Main.tile[x + 140, y + j].active(true);
                    Main.tile[x + 140, y + j].type = (ushort)mod.TileType("VoidBrickTileArena");
                }




            }
           
            for (int i = 0; i <= 140; i++)// bottom row
            {
                if (!Main.tile[x + i, y + 120].active())
                {
                    Main.tile[x + i, y + 120].active(true);
                    Main.tile[x + i, y + 120].type = (ushort)mod.TileType("VoidBrickTileArena");
                }
            }

        
        
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
