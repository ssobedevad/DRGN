using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs
{
    public class GalacticBarrier : ModNPC
    {
        private const int laserDamage = 250;
        Vector2 PlayerPos;
        public override void SetDefaults()
        {
            npc.lifeMax = 350;
            npc.height = 64;
            npc.width = 64;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.damage = 1;
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
            int off = DRGNModWorld.MentalMode ? 750 : Main.expertMode ? 850 : 950;
            Vector2 Offset = new Vector2(-off, -off);
            if (npc.ai[1] == 1) { Offset.X *= -1; }
            else if (npc.ai[1] == 2) { Offset.X *= -1; Offset.Y *= -1; }
            else if (npc.ai[1] == 3) { Offset.Y *= -1; }
            PlayerPos = Main.player[npc.target].Center;
            if (!NPC.AnyNPCs(mod.NPCType("GalacticGuardian"))) { npc.active = false; }
            else { npc.timeLeft = 100; }
            if (npc.ai[3] == 0)
            {
                npc.Center = PlayerPos + Offset;
                if (npc.ai[2] == 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npci = Main.npc[i];
                        if (npci.active && npci.type == mod.NPCType("GalacticBarrier") && (npci.ai[1] == npc.ai[1] + 1 || (npci.ai[1] == 3 && npc.ai[1] == 0)))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                npc.ai[2] = Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("GalacticBeam"), laserDamage, 0f , 255 , npc.whoAmI , i);
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, (int)npc.ai[2]);
                            }
                           
                        }
                    }                   
                }
                else { Main.projectile[(int)npc.ai[2]].active = false; npc.ai[2] = 0; }
                npc.ai[3] = 1;
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead)
                {
                    Player playeri = Main.player[i];
                    npc.ai[0] = 0;
                    if (Offset.X > 0 && playeri.Center.X > npc.Center.X) { TryDontTakeDamage(); }
                    else if (Offset.X < 0 && playeri.Center.X < npc.Center.X) { TryDontTakeDamage(); }
                    else if (Offset.Y > 0 && playeri.Center.Y > npc.Center.Y) { TryDontTakeDamage(); }
                    else if (Offset.Y < 0 && playeri.Center.Y < npc.Center.Y) { TryDontTakeDamage(); }
                }
            }
        }
        private void TryDontTakeDamage()
        { npc.ai[0] = 1; npc.netUpdate = true; }
    }
}