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
    public class Melting : ModBuff
    {




        public override void SetDefaults()
        {
            DisplayName.SetDefault("Melting");
            Main.debuff[Type] = true;
           longerExpertDebuff = true;



        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.Next(12) == 1)
            {
                npc.HitEffect(1, 1);
                npc.life -= 1;
            }

            int DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width + 1, npc.height + 1, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;




        }
        public override void Update(Player player, ref int buffIndex)
        {
               DRGNPlayer.melting = true;
            

            int DustID = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 2f), player.width + 1, player.height + 1, 273, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 120, default(Color),2f);
            Main.dust[DustID].noGravity = true;




        }





    }
}
