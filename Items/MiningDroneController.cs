using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.Tiles;

namespace DRGN.Items
{
    public class MiningDroneController : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left click to cause the mining drone to target ores near the mouse and right click to recall the mining drone");
        }
        public override void SetDefaults()
        {
            item.height = 16;
            item.width = 32;
            item.rare = ItemRarityID.Expert;
            item.value = 0;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 3;
            item.maxStack = 1;
        }
        public override bool UseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType("MiningDrone")))
            {

                int npc = NPC.FindFirstNPC(mod.NPCType("MiningDrone"));
                if (player.altFunctionUse == 2)
                { 
                    Main.npc[npc].ai[0] = -1;
                    Main.npc[npc].netUpdate = true;
                }
                else
                {
                    Main.npc[npc].ai[0] = 2;
                    Main.npc[npc].netUpdate = true;
                }
            }
            
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("IronBar", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
