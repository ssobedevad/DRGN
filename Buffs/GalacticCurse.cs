﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Buffs
{
    public class GalacticCurse : ModBuff
    {




        public override void SetDefaults()
        {
            DisplayName.SetDefault("Galactic curse");
            Description.SetDefault("Causes big damage");
            Main.debuff[Type] = true;
            longerExpertDebuff = true;



        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.Next(12) == 1)
            {
                npc.StrikeNPC(250, 0, 0, false);
            }

            int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width + 1, npc.height + 1, 48, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;




        }
        public override void Update(Player player, ref int buffIndex)
        {

            DRGNPlayer.galacticCurse = true;


            int DustID = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 2f), player.width + 1, player.height + 1, 48, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;




        }





    }
}
