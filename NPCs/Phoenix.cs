using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.NPCs
{
    public class Phoenix : ModNPC
    {

        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 20;
            npc.damage = 25;
            npc.defense = 12;
            npc.value = 3000;
            npc.lifeMax = 600;
            npc.aiStyle = -1;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            int Dustid = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X * -0.4f, npc.velocity.Y * -0.4f, 0, Color.White, 1f);
            Main.dust[Dustid].noGravity = true;
            if (npc.ai[0] == 0)
            {   if(npc.ai[1] == 0) { npc.ai[2] = Main.player[npc.target].Center.X; npc.ai[3] = Main.player[npc.target].Center.Y; npc.ai[1] = 1; }
                npc.noTileCollide = true;
                if (moveToPlayerOffset(Main.player[npc.target])) { npc.ai[0] = 1; npc.ai[1] = 0; }              
            }
            else if (npc.ai[0] == 1)
            {
                npc.noTileCollide = true;
                npc.ai[1] += 1;             
                if (npc.ai[1] > 30)
                {                   
                    if (dashToPlayer(Main.player[npc.target])) { npc.ai[0] = 2; npc.ai[1] = 0; }
                }
                else { npc.velocity *= 0.95f; npc.ai[2] = Main.player[npc.target].Center.X; npc.ai[3] = Main.player[npc.target].Center.Y; }
            }
            else if (npc.ai[0] == 2)
            {
                npc.noTileCollide = true;
                npc.ai[1] += 1;
                if (npc.ai[1] > 30)
                {
                    npc.ai[0] = 0; npc.ai[1] = 0;
                }
                else
                {
                    npc.velocity.X *= 0.99f;
                    npc.velocity.Y -= 0.5f;
                }
               
            }
            else if (npc.ai[0] == 3)
            {
                npc.noTileCollide = false;
                if (!npc.collideY && npc.velocity.Y < 16f)
                {
                    npc.velocity.Y += 0.5f;
                }
                else
                {
                    npc.velocity.X *= 0.8f;
                }
                npc.ai[1] += 1;
                if (npc.ai[1] > 360)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        int Dustid2 = Dust.NewDust(npc.position + new Vector2(-2,-2), npc.width+2, npc.height+2, DustID.Fire, 0,0, 0, Color.White, 3f);
                        Main.dust[Dustid2].velocity.Y = Main.rand.NextFloat(-10f, -2f);
                        Main.dust[Dustid2].velocity.X = Main.rand.NextFloat(-5f, 5f);
                        Main.dust[Dustid2].noGravity = true;
                    }
                    npc.ai[0] = 0; npc.ai[1] = 0;
                }
            }
        }
        private bool moveToPlayerOffset(Player player)
        {
            Vector2 playerCenter = new Vector2(npc.ai[2], npc.ai[3]);
            bool left = playerCenter.X > npc.Center.X;
            Vector2 Diff = playerCenter - npc.Center - new Vector2(left ? 300 : -300, 200);
            float Magnitude = Diff.Length();
            float Speed = npc.velocity.Length() > 12f ? 12f : npc.velocity.Length() + 0.2f;
            if (Magnitude > Speed)
            {
                Magnitude = Speed / Magnitude;
                Diff *= Magnitude;
                npc.velocity = Diff;
              
            }
            else { npc.velocity *= 0.6f; return true; }
            return false;
        }
        private bool dashToPlayer(Player player)
        {
            Vector2 playerCenter = new Vector2(npc.ai[2], npc.ai[3]);           
            Vector2 Diff = playerCenter - npc.Center;
            float Magnitude = Diff.Length();
            float Speed = npc.velocity.Length() > 16f ? 16f : npc.velocity.Length() + 0.5f;
            if (Magnitude > Speed)
            {
                Magnitude = Speed / Magnitude;
                Diff *= Magnitude;
                npc.velocity.X = Diff.X;
                npc.velocity.Y = (playerCenter.Y - npc.Center.Y > 25) ? Diff.Y * 2 : Diff.Y;
                if(npc.velocity.Y > 14f) { npc.velocity.Y = 14f; }
            }
            else { return true; }
            return false;
        }
        public override bool CheckDead()
        {
            if (npc.ai[0] < 3) { npc.ai[1] = 0; npc.ai[0] = 3; npc.life = npc.lifeMax; return false; }
            return true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.GetModPlayer<DRGNPlayer>().DragonBiome) ? 0.5f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("FlareCrystal"), Main.rand.Next(6, 14));
            Item.NewItem(npc.getRect(), mod.ItemType("AshenWood"), Main.rand.Next(6, 14));
        }
        public override void FindFrame(int frameHeight)
        {
            int frame = npc.frame.Y / frameHeight;
            if (npc.ai[0] < 3)
            {
                npc.frameCounter++;
                if(npc.frameCounter % 10 == 0)
                {
                    frame++;
                    npc.frameCounter = 0;
                }
                if(frame > 3) { frame = 0; }
            }
            else { frame = 4; }
            npc.frame.Y = frame * frameHeight;
        }

    }
}