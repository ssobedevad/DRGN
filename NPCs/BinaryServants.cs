using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.NPCs
{
    public class BinaryServants : ModNPC
    {





        private const int BinaryShotDamage = 50;

        public override void SetDefaults()
        {

            

            npc.height = 16;
            npc.width = 16;
            npc.aiStyle = -1;

            npc.damage = 35;
            npc.defense = 40;
            npc.lifeMax = 2000;
            npc.noTileCollide = true;
            npc.noGravity = true;

            Main.npcFrameCount[npc.type] = 2;
        }


        public override void AI()
        {



            





            npc.TargetClosest(true);
            


            MoveTo((int)npc.ai[0]);
            if(Main.netMode != 1)
            { npc.netUpdate = true; }

        }
        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;
            frame = (int)npc.ai[0];
            npc.frame.Y = frame * frameHeight;
        }

        
        private void MoveTo(int style)
        {
            if (style == 0|| Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 250)
            {
                Vector2 targetPos = Main.player[npc.target].Center;

                Vector2 ThisCenter = npc.Center;
                float Xdiff = targetPos.X - ThisCenter.X + Main.rand.Next(-100, 100);
                float YDiff = targetPos.Y - ThisCenter.Y - Main.rand.Next(-100, 100);
                float Magnitude = (float)Math.Sqrt((double)(Xdiff * Xdiff + YDiff * YDiff));
                float Speed = 16f;
                Magnitude = Speed / Magnitude;
                Xdiff *= Magnitude;
                YDiff *= Magnitude;
                npc.velocity = (npc.velocity * 40f + new Vector2(Xdiff,YDiff)) / 41f;
                
                npc.rotation += 0.3f;
            }
            else
            {
                npc.rotation = 0f;
                npc.velocity *= 0.9f;
                npc.localAI[0] += 1;
                if (npc.localAI[0] > 60)
                { Projectile.NewProjectile(npc.Center, shootVel(), mod.ProjectileType("BinaryBlast"), BinaryShotDamage, 0f); npc.localAI[0] = 0; }

            }
        }
        private Vector2 shootVel()
        {
            Vector2 targetPos = Main.player[npc.target].Center;
            targetPos = targetPos - npc.Center;
            targetPos = Vector2.Normalize(targetPos);
            targetPos *= 16f;
            npc.velocity += -targetPos / 2;
            return targetPos;


        }

    }
}