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
    public class ExplodingFrog : ModNPC
    {


        public int whichNpc;
        private int target;
        private int targetMag = 1000;
        public static float playerMagicDamageMult;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exploding Frog");

        }
        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.height = 38;
            npc.width = 37;
            npc.aiStyle = -1;
            npc.friendly = true;
            npc.dontTakeDamage = true;
            target = -1;
            npc.damage = (int)(50 * playerMagicDamageMult);
            

        }

        public override void AI()
        {
            Target();
            
            if (target != -1)
            {
            if (npc.collideX) { npc.velocity.Y = -3; }
                if (Main.npc[target].Center.X > npc.Center.X + 30)
                {
                    npc.noTileCollide = false;
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {
                        npc.velocity.X += 5;
                        npc.velocity.Y -= 5;
                        npc.spriteDirection = -1;
                        
                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; }
                }

                else if (Main.npc[target].Center.X < npc.Center.X - 30)
                {
                    npc.noTileCollide = false;
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {
                        npc.velocity.X -= 5;
                        npc.velocity.Y -= 5;
                        npc.spriteDirection = 1;
                       

                    }
                    else if (npc.collideY == true) { npc.velocity.X = 0; npc.velocity.Y = 0; }
                }
                else if (Main.npc[target].Center.Y < npc.Center.Y - 30)
                {
                    npc.velocity.Y = -7;
                    npc.noTileCollide = true;
                }
                else if (Main.npc[target].Center.Y > npc.Center.Y + 30)
                {
                    npc.velocity.Y = 7;
                    npc.noTileCollide = true;
                }
                else

                {
                    npc.active = false; for (int i = 0; i < 25; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 2f), npc.width + 1, npc.height + 1, 273, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 0, default(Color), 3f);
                        Main.dust[DustID].noGravity = true;
                       
                    }
                    Main.npc[target].StrikeNPC(npc.damage+20, 5f, 1);
                    Main.npc[target].AddBuff(mod.BuffType("Melting"), 600);
                }
            }
        }

        private void Target()
        {


            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.npc.position.X + (float)(this.npc.width / 2) - whichNpcXpos) + Math.Abs(this.npc.position.Y + (float)(this.npc.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }



        }
    }
}