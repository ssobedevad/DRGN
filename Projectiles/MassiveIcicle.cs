using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles
{
    public class MassiveIcicle : ModProjectile
    {
        private int[,] angles = new int[8, 2] { { 8, -2 },{ 7, -4 },{ 6, -6 },{ 5, -8 },{ -5, -8 },{ -6, -6 },{ -7, -4 },{ -8, -2 } };
        public override void SetDefaults()
        {

            projectile.height = 80;
            projectile.width = 80;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            
            projectile.penetrate = -1;


            projectile.tileCollide = true;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 8; i++)
            { Projectile.NewProjectile(projectile.Center, new Vector2(angles[i, 0], angles[i, 1]), (DRGNModWorld.MentalMode ? ModContent.ProjectileType<IceCluster>() : ModContent.ProjectileType<IceShard>()), projectile.damage, 0f); }
            return true;
        
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frozen, 200);

        }




    }
}


