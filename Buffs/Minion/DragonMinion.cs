using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Buffs.Minion
{
    public class DragonMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Dragon minion");
            Description.SetDefault("The dragon will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("DragonMinion")] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.player[Main.myPlayer];
            int numSummons = 0;
            if (player.ownedProjectileCounts[mod.ProjectileType("DragonMinion")] > 0)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                { if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("DragonMinion") && Main.projectile[i].ai[1] < player.maxMinions && Main.projectile[i].owner == player.whoAmI) { numSummons = (int)Main.projectile[i].ai[1] + 1; } }
            }
            
            string tooltip = $"You have {numSummons} levels of dragon.";
            tip = tooltip;

           

        }







    }
}
