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
    public class CloudSummon : ModNPC
    {
        public int whichNpc;
        private int target ;
        private Vector2 targetPos;
        private int randMode;
        private int targetMag ;
        private int proj1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloud");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.friendly = true;
            npc.dontTakeDamage = true;
            npc.height = 45;
            npc.width = 62;
            npc.aiStyle = -1;   
            npc.noGravity = true;
            npc.noTileCollide = true;
            randMode = -1;
            proj1 = -1;
            npc.damage = 150;
            
        }
        
        public override void AI()
        {
           if (!Main.player[(int)npc.ai[0]].buffType.Contains(mod.BuffType("CloudSummon"))|| Main.player[(int)npc.ai[0]].dead ) { npc.active = false;  }
            Target();
            if (target == -1 || (Math.Abs(this.npc.position.X + (float)(this.npc.width / 2) - Main.player[(int)npc.ai[0]].Center.X) + Math.Abs(this.npc.position.Y + (float)(this.npc.height / 2) - Main.player[(int)npc.ai[0]].Center.Y)) > 1600f)
            { targetPos = Main.player[(int)npc.ai[0]].Center + new Vector2(0, -100); randMode = -1; npc.frame.Y = 0; if (proj1 >= 0) { Main.projectile[proj1].ai[0] = -1;proj1 = -1; } }
           else { targetPos = Main.npc[target].Center + new Vector2(0, -100); 
           if (randMode == -1)
           { randMode = Main.rand.Next(1,4); }
                npc.frame.Y = randMode * 45;
                if (randMode == 1 && proj1 == -1)
                { proj1 = Projectile.NewProjectile(npc.Center, new Vector2(0, 14), mod.ProjectileType("SunRayMini"), (int)(npc.damage * Main.player[(int)npc.ai[0]].minionDamage), 0f, 0, (float)npc.whoAmI); }
                else if (randMode == 2)
                {
                    if (Main.rand.Next(0, 12) == 1)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Bottom.Y, 0, 5, mod.ProjectileType("RainMini"), (int)(npc.damage * Main.player[(int)npc.ai[0]].minionDamage), 0, 0);
                    }
                }
                else if (randMode == 3)
                {
                    if (Main.rand.Next(0, 18) == 1)
                    { int projid = Projectile.NewProjectile(npc.Center, new Vector2((float)Main.rand.Next(-20, 20), 500f), mod.ProjectileType("SingleLighteningMini"), (int)(npc.damage * Main.player[(int)npc.ai[0]].minionDamage), 1f, 0, (float)npc.whoAmI, 1); }
                }
            


           }
           
            Move();
        }

        private void Target()
        {
            targetMag = 1000;
            target = -1;

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
        private void Move()
        {
            // Sets the max speed of the npc.
            Vector2 moveTo = targetPos + new Vector2((float)Math.Sin(npc.ai[1] / 2) * 50, (float)Math.Cos(npc.ai[1]) * 50);
            Vector2 move = moveTo - npc.Bottom;
            float magnitude = Magnitude(move);
            if (magnitude > 20)
            {
                move *= 20 / magnitude;
            }

            npc.velocity = move;
            npc.ai[1] += 0.045f;
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}