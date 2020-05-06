using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Projectiles
{
    public class TrueDragonBladeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 40;
            projectile.width = 40;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
           

        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            if (target.active == true)
            {
                target.AddBuff(BuffID.Daybreak, 600); }
            if (target.boss == true )
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("BossSlayer"), 360);
   
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
            projectile.spriteDirection = projectile.direction;
        }
    }

}

