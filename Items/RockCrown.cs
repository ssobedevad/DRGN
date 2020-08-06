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
    public class RockCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Summons the Rock Monarch");
        }
        public override void SetDefaults()
        {
            item.height = 22;
            item.width = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.value = 10000;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

           
            bool ug = player.ZoneRockLayerHeight;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("RockSlimeKing"));
            return (!alreadySpawned && ug );
        }
        public override bool UseItem(Player player)

        {

            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("RockSlimeKing")); // Spawn the boss within a range of the player. 
            }
            else { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("RockSlimeKing")); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(ItemID.StoneBlock,25);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddIngredient(ItemID.Topaz, 3);
            recipe.AddIngredient(ItemID.Amethyst, 3);
            recipe.AddIngredient(ItemID.Amber, 3);
            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
        }
    }
}
