using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN
{

    class ReaperGlobalNPC : GlobalNPC
    {

        public override bool InstancePerEntity => true;
        public bool CanExecute(NPC npc, Player player)
            => ReaperPlayer.CanTargetExecute(npc, player);


        public int soulReward = 1;
        public override void PostAI(NPC npc)
        {
            Player player = Main.LocalPlayer;
            ReaperPlayer reaperPlayer = player.GetModPlayer<ReaperPlayer>();
            if (reaperPlayer.HuntedTarget == npc.whoAmI)
            {
                if (CanExecute(npc, player))
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldFlame, 0, 0, 0, Color.White, 1.2f);
                }
                else
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0, 0, 0, Color.White, 1.2f);
                }
            }
        }

        public static int FindClosestReaper(Vector2 pos, float maxRange = 1000)
        {
            int target = -1;
            for (int i = 0; i < 255; i++)
            {

                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    float dist = Vector2.Distance(pos, player.Center);
                    if (player.GetModPlayer<ReaperPlayer>().isReaper && dist < maxRange)
                    {
                        target = i;
                        maxRange = dist;
                    }
                }

            }
            return target;
        }
        public int FindClosestReaper(NPC npc, float maxRange = 1000)
        {
            int target = -1;
            for (int i = 0; i < 255; i++)
            {

                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    float dist = Vector2.Distance(npc.Center, player.Center);
                    if (player.GetModPlayer<ReaperPlayer>().isReaper && dist < maxRange)
                    {
                        target = i;
                        maxRange = dist;
                    }
                }

            }
            return target;
        }
        public void AddSoulReward(NPC npc , int reward , Player player)
        {
            if(npc.boss || soulReward > 5)
            {
                if(soulReward > 5) { reward = Main.rand.NextBool()? 1 : 0; }
                for(int i = 0; i < reward; i++)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), ReaperPlayer.getSoulDamage(), 0, player.whoAmI);
                }

            }
            else { soulReward += reward; }

        }
        public override void NPCLoot(NPC npc)
        {
            int reaperPlayer = FindClosestReaper(npc);
            if (reaperPlayer != -1)
            {
                if ((soulReward > 1 || Main.rand.NextBool()) || Main.player[reaperPlayer].GetModPlayer<ReaperPlayer>().HuntedTarget == npc.whoAmI)
                {
                    if (Main.player[reaperPlayer].GetModPlayer<ReaperPlayer>().HuntedTarget == npc.whoAmI)
                    {
                        soulReward = (int)(soulReward * 1.5f);

                    }
                    if (soulReward > 0 && !npc.boss)
                    {
                        for (int i = 0; i < soulReward; i++)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), ReaperPlayer.getSoulDamage(), 0, reaperPlayer);
                        }



                    }
                }
            }

        }




    }
}