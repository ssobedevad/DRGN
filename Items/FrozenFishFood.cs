using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.NPCs.Boss;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items
{
    public class FrozenFishFood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen fish food");
            Tooltip.SetDefault("Causes the ancient warrior to awake");
        }
        public override void SetDefaults()
        {
            item.height = 30;
            item.width = 28;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

            bool iceBiome = player.ZoneSnow;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("IceFish"));
            return (!alreadySpawned && iceBiome) ;
        }
        public override bool UseItem(Player player)

        {
           
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("IceFish")); // Spawn the boss within a range of the player. 
            }
            else
            { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 10);
            recipe.AddIngredient(ItemID.WormFood);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.IceBlock, 10);
            recipe2.AddIngredient(ItemID.BloodySpine);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
