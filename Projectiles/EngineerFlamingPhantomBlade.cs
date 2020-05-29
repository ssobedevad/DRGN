using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class EngineerFlamingPhantomBlade : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 18;
            projectile.width = 18;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = false;
            projectile.ranged = false;
            projectile.magic = false;
            projectile.thrown = false;
            projectile.minion = false;
            projectile.ai[0] = 1;
            projectile.penetrate = (int)projectile.ai[0];
            projectile.ai[1] = 0;



        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)

        {

            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-20,20) -(projectile.velocity.X * 25), projectile.Center.Y+ Main.rand.Next(-30,10) - (projectile.velocity.Y * 25), (float)(projectile.velocity.X * 1.7) + Main.rand.Next(-2, 2), (float)(projectile.velocity.Y * 1.7)+ Main.rand.Next(-2,2), mod.ProjectileType("EngineerFlamingPhantomBladePhantom"), projectile.damage, projectile.knockBack, Main.myPlayer, (int)projectile.ai[0], projectile.ai[1]);
            }
           

        }
        public override void AI()
        {
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 70, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 200, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.Next(0, 100) <= (int)(projectile.ai[1]))
            {
                crit = true;


            }


        }


    }
}
