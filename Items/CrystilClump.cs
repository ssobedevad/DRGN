using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.NPCs.Boss;

namespace DRGN.Items
{
    public class CrystilClump : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Crystil");
        }
        public override void SetDefaults()
        {
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.rare = ItemRarityID.Purple;
            item.value = 10000;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("Crystil"));
            return (!alreadySpawned);
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Crystil")); 
            }
            else
            { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 10);
            recipe.AddIngredient(ItemID.CrystalShard,10);
            recipe.AddIngredient(mod.ItemType("GlacialFragment"), 10);
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();            
        }
    }
}
