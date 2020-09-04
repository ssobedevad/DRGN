using Terraria;
using Terraria.ModLoader;

namespace DRGN.Buffs.Minion
{
    public class FireballMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fireball Minion");
            Description.SetDefault("Fireball");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int num = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("LivingFireballMinion") && Main.projectile[i].owner == player.whoAmI)
                {
                    num++;
                }
            }
            if (num > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
