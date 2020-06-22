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
    public class CelestialSundial : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial sundial");
            Tooltip.SetDefault("Use high in the sky to catch someones attention");
        }
        public override void SetDefaults()
        {
            item.height = 32;
            item.width = 32;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

            bool sky = player.ZoneSkyHeight;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("Cloud"));
            return (!alreadySpawned && sky) ;
        }
        public override bool UseItem(Player player)

        {
            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Cloud")); // Spawn the boss within a range of the player. 
            }
            else { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("Cloud")); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarTablet);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
