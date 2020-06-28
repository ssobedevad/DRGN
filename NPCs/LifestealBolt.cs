using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace DRGN.NPCs
{
    public class LifestealBolt : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LifestealBolt");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;

            npc.scale = 0.5f;
            npc.height = 20;
            npc.width = 20;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
        }
        public override void AI()
        {
            Target();


            
                int Dustid = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.HealingPlus, 0, 0, 120, default(Color), 1.5f);
                Main.dust[Dustid].noGravity = true;
            
            Move();

        }


        private void Move()
        {
            speed = 20f;
            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            else
            {
                player.statLife += 1;
                player.HealEffect(1);
                npc.active = false;
                npc.timeLeft = 0;
            }
            npc.velocity = move;

        }
        private void Target()
        {
            player = Main.player[0];
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }



    }
}
