using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles.Minion
{
    public class DragonMinion : ModProjectile
    {


       



        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;

        }
        public string TextureFolder = "DRGN/Projectiles/Minion/";
        public override string Texture => (projectile.ai[1] >= 10) ? TextureFolder + "OmegaDragonForm" : (projectile.ai[1] >= 5) ? TextureFolder + "AdultDragonForm" : TextureFolder + "BabyDragonForm";
        
        public override void SetDefaults()
        {

            projectile.friendly = true;


            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.height = 38;
            projectile.width = 62;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ai[1] = 1;
            projectile.minion = true;
            Main.projFrames[projectile.type] = 4;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {



            Player player = Main.player[projectile.owner];
            projectile.spriteDirection = -projectile.direction;

            
            if (++projectile.frameCounter % 5 == 0)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame == 4)
            { projectile.frame = 0; }
            
            if (projectile.ai[1] > player.maxMinions )
            {
                projectile.ai[1] = player.maxMinions ;
            }
            projectile.minionSlots = projectile.ai[1]-1;
            projectile.damage = (int)projectile.ai[0] * (int)projectile.ai[1];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("DragonMinion"));
            }
            if (player.HasBuff(mod.BuffType("DragonMinion")))
            {
                projectile.timeLeft = 2;
            }

            if (Vector2.Distance(projectile.Center, player.Center) > 1600f)
            { projectile.Center = player.Center + new Vector2(Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1), Main.rand.Next(-projectile.minionPos - 1, projectile.minionPos + 1)); }

            MoveTo(Target(), player);

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 vect2 = new Vector2(projectile.Center.X- Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0, projectile.frame * 40, 62, 40);
            spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect2, rect2, Color.White, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, (SpriteEffects)(projectile.direction == 1? 1: 0), 0f);
            return false;
        }

        private int Target()
        {
            int targetMag = 1000;
            int target = -1;
            if (Main.player[projectile.owner].MinionAttackTargetNPC > 0)
            { return Main.player[projectile.owner].MinionAttackTargetNPC; }
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {


                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag )
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }
            return target;


        }
        private void MoveTo(int target, Player player)
        {
            Vector2 moveTo = (target == -1) ? player.Center : Main.npc[target].Center;
            
            
            
            Vector2 Diff = moveTo - projectile.Center + new Vector2 (Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));
            
            float Magnitude = (float)Math.Sqrt((double)(Diff.X * Diff.X + Diff.Y * Diff.Y));
            float Speed = 10f;
            Speed *= (0.6f + (projectile.ai[1] / 4f));
            float mult = 45f -(projectile.ai[1] * 2);
            Magnitude = Speed / Magnitude;
            Diff *= Magnitude;
            
            projectile.velocity = (projectile.velocity * mult + Diff) / (mult+1f);
           
        }

    }
}