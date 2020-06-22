﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.NPCs.Boss;
using Terraria.Localization;
//using static DRGN.DRGNPlayer;

namespace DRGN.Items
{
    public class AntsCall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ants Call");
            Tooltip.SetDefault("Awakes the queen of all the ants");
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

            bool AntBiome = player.ZoneOverworldHeight;
            bool alreadySpawned = NPC.AnyNPCs(mod.NPCType("QueenAnt"));
            return (!alreadySpawned && AntBiome);
        }
        public override bool UseItem(Player player)

        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("QueenAnt")); // Spawn the boss within a range of the player. 
            }
            else
            { NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI); }
            Main.PlaySound(SoundID.Roar, player.Right, 0);
            return true;


        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 5);
            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
        }
    }
}
