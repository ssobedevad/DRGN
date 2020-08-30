using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs
{
    public class GalacticGuardianDockingStation : ModNPC
    {

        Vector2 PlayerPos;
        private const int laserDamage = 140;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Guardian");

        }
        public override void SetDefaults()
        {
            npc.lifeMax = 350;
            npc.height = 64;
            npc.width = 68;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.damage = 150;
            npc.defense = 1;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.dontTakeDamage = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
        public override void AI()
        {
            if (!NPC.AnyNPCs(mod.NPCType("GalacticGuardian"))) { npc.active = false; return; }
            else
            {
                NPC GuardianNPC = Main.npc[(int)npc.ai[0]];
                if (GuardianNPC.life < GuardianNPC.lifeMax / 2 && npc.ai[1] == 0)
                {
                    npc.ai[1] = 1;
                    
                }
                if (npc.ai[1] == 0)
                {
                    if (GuardianNPC.ai[0] >= 5 && GuardianNPC.ai[0] < 13)
                    {
                        if (Vector2.Distance(npc.Center, PlayerPos) > 20) { Move(8f, PlayerPos); }
                        if (npc.ai[2] == 0)
                        {
                            npc.ai[3] = 0;
                            for (int j = 0; j < Main.npc.Length && npc.ai[3] < 4; j++)
                            {
                                
                                NPC npcj = Main.npc[j];
                                if (npcj.active && npcj.type == mod.NPCType("GalacticBarrier") && npcj.ai[1] == npc.ai[3])
                                {                                    
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {                                       
                                        npc.ai[2] = Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("GalacticBeam"), laserDamage, 0f , Main.myPlayer, npc.whoAmI , j);
                                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, (int)npc.ai[2]);
                                        npc.ai[3]++;
                                        npc.netUpdate = true;
                                    }
                                   
                                }
                            }

                        }
                    }
                    else
                    {
                        npc.ai[2] = 0;
                        Vector2 moveTo = GuardianNPC.Center + new Vector2(0, 106);
                        Move(25f, moveTo);
                        PlayerPos = Main.player[GuardianNPC.target].Center;

                    }
                }



                else
                {

                    npc.ai[2] = 0;
                    Vector2 moveTo = GuardianNPC.Center + new Vector2(0, 106);
                    Move(40f, moveTo);
                    

                }
            }
            
        }
        private void Move(float moveSpeed , Vector2 moveTo)
        {

            Vector2 moveTo2 = moveTo - npc.Center;
            float magnitude = Magnitude(moveTo2);
            if (magnitude > moveSpeed * 2)
            {
                moveTo2 *= moveSpeed / magnitude;
                npc.velocity = (npc.velocity * 10f + moveTo2) / 11f;
            }
            else { npc.Center = moveTo; npc.velocity = Vector2.Zero; }


            


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }





    }
}