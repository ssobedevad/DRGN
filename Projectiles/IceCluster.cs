using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class IceCluster : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 29;
            projectile.width = 28;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            
            projectile.penetrate = -1;
            
           
            projectile.tileCollide = true;

        }
     
        public override bool OnTileCollide(Vector2 Vc)
        {
            for (int i = 0; i < 10; i ++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("IceShard"), projectile.damage,projectile.knockBack); }
           


            return true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frozen, 200);

        }




    }
}


