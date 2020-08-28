using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class DeathShowerProj : ModProjectile
    {                                          
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.tileCollide = false;                   
        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            int Dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SomethingRed, 0, 0, 120, default(Color), 2f);
            Main.dust[Dustid].noGravity = true;
            Move();            
        }
        private void Move()
        {
            int target = DavesUtils.FindNearestTargettableNPC(projectile);
            if (target != -1)
            {
                float speed = 15f;
                Vector2 MoveTo = Main.npc[target].Center - projectile.Center;
                float magnitude = MoveTo.Length();
                if (magnitude > speed)
                {
                    MoveTo *= speed / magnitude;
                }
                projectile.velocity = (projectile.velocity * 20f + MoveTo) / 21f ;
            }          
        }       
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(0, 5) == 1)
                NPC.NewNPC((int)target.position.X, (int)target.position.Y, mod.NPCType("LifestealBolt"));
        }
    }
}