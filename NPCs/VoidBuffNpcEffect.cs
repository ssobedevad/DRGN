using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace DRGN.NPCs
{
    public class VoidBuffNPCEffect : ModNPC
    {
       private NPC target ;
        private int stage;
        
        private int voidBuffBuff;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Buff");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;

            
            npc.height = 13;
            npc.width = 27;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
        }
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            voidBuffBuff = player.GetModPlayer<DRGNPlayer>().voidArmorSet ? 3 : 1;
            if (player.GetModPlayer<DRGNPlayer>().vsEquip) { voidBuffBuff *= 3; }
                target = Main.npc[(int)npc.ai[0]];
            if (target.active == false) { npc.active = false; }
            else
            {
                stage = DRGNPlayer.VoidEffect[target.whoAmI];
                if (stage >= 6) { target.StrikeNPC( ((target.lifeMax / 80) + (target.defense) + (target.damage) + 400)*voidBuffBuff , 10f, 0);   stage = 0; DRGNPlayer.VoidEffect[target.whoAmI] = 0; npc.timeLeft = 1; for (int i = 0; i < 50; i++) { int DustID = Dust.NewDust(new Vector2(target.position.X, target.position.Y + 2f), target.width + 1, target.height + 1, 98, 0, 0, 0, default(Color), 1f); } }
                if (DRGNPlayer.VoidEffect[target.whoAmI] == 0) { npc.active = false; }
                Move();
            }

        }
        public override void FindFrame(int frameHeight)
        {
            // number of frames * tick count
            int frame = stage - 1 ;  // only change frame every second tick
           
            npc.frame.Y = frame * frameHeight;

        }

        private void Move()
        {

            
            Vector2 moveTo = new Vector2(target.Center.X ,target.Bottom.Y + 30);

            npc.Center = moveTo;
            

        }
        




    }
}
