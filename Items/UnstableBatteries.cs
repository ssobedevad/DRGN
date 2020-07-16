using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.NPCs.Boss;
using Terraria.Localization;


namespace DRGN.Items
{
    public class UnstableBatteries : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Corrupts your hard drive");
        }
        public override void SetDefaults()
        {
            
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.rare = ItemRarityID.LightPurple;
            item.value = 2000;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.maxStack = 999;
        }
        public override bool CanUseItem(Player player)
        {

           
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("TheVirus"));
            return (!alreadySpawned);
        }
        public override bool UseItem(Player player)

        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TheVirus")); // Spawn the boss within a range of the player. 
            }
            else
            { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
