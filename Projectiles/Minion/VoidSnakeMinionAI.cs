using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public abstract class VoidSnakeMinionAI : ModProjectile
    {


        public bool head;

        public int headID;
        private int PreviousPart;


        private Vector2 MoveTo;
        public int baseDamage = -1;
        public float speed;
        public float turnSpeed;
        public float turnSpeed2 = -1;

       



        

        public override void AI()
        {
            if(baseDamage == -1) { baseDamage = projectile.damage; }
            
            if (projectile.ai[0] == -2)
            {
                head = true;

                
                int BodyID = Projectile.NewProjectile(projectile.position, Vector2.Zero, mod.ProjectileType("VoidSnakeMinionBody"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                int TailID = Projectile.NewProjectile(projectile.position, Vector2.Zero, mod.ProjectileType("VoidSnakeMinionTail"), projectile.damage, projectile.knockBack, projectile.owner, BodyID);
                projectile.ai[0] = -1;
            }
            if (PreviousPart > -1)
            {
                PreviousPart = (int)projectile.ai[0];
            }
            Player player = Main.player[projectile.owner];
            projectile.damage = baseDamage * player.ownedProjectileCounts[mod.ProjectileType("VoidSnakeMinionBody")];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("VoidSnakeMinion"));
            }
            if (player.HasBuff(mod.BuffType("VoidSnakeMinion")))
            {
                projectile.timeLeft = 2;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == mod.ProjectileType("VoidSnakeMinionHead") && Main.projectile[i].owner == projectile.owner && Main.projectile[i].active)
                {
                    headID = i;
                    if (i == projectile.whoAmI) { PreviousPart = -1; }
                    break;
                }

            }
            int target = Target();
            if (target != -1)
            { MoveTo = Main.npc[target].Center; }
            else { MoveTo = Main.player[projectile.owner].Center; }
            if (head)
            {
                if (target == -1)

                { projectile.localAI[0] += 0.03f; MoveTo = new Vector2(player.Center.X + (float)Math.Cos(projectile.localAI[0]) * 200, player.Center.Y + (float)Math.Sin(projectile.localAI[0]) * 200); }
                MoveTo = MoveTo - projectile.Center;
                float Mag = (float)Math.Sqrt((MoveTo.X * MoveTo.X + MoveTo.Y * MoveTo.Y));
                MoveTo *= (30 / Mag);
                if(target != -1)
                {
                    
                    if (Mag > 800 || Mag < 300 ) { MoveTo *= 2f; }
                    if (Mag > 1100 || Mag < 80) { MoveTo *= 2f; }

                }
                projectile.velocity.X = (projectile.velocity.X * 65f + MoveTo.X) / 66f;
                projectile.velocity.Y = (projectile.velocity.Y * 65f + MoveTo.Y) / 66f;


                projectile.rotation = projectile.velocity.ToRotation() + 1.57f;

            }




            if (PreviousPart != -1)
            {


                Vector2 ProjCenter = projectile.Center;
                float targetPosX = Main.projectile[PreviousPart].Center.X - ProjCenter.X;
                float targetPosY = Main.projectile[PreviousPart].Center.Y - ProjCenter.Y;


                projectile.rotation = (float)System.Math.Atan2((double)targetPosY, (double)targetPosX) + 1.57f;
                float Mag = (float)System.Math.Sqrt((double)(targetPosX * targetPosX + targetPosY * targetPosY));
                int width = projectile.width;
                Mag = (Mag - (float)width) / Mag;
                targetPosX *= Mag;
                targetPosY *= Mag;
                projectile.velocity = Vector2.Zero;
                projectile.position.X = projectile.position.X + targetPosX;
                projectile.position.Y = projectile.position.Y + targetPosY;

                if (targetPosX < 0f)
                {
                    projectile.spriteDirection = 1;
                }
                if (targetPosX > 0f)
                {
                    projectile.spriteDirection = -1;
                }
            }

        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            string texture = "";
            if(projectile.type == mod.ProjectileType("VoidSnakeMinionBody")) { texture = "Projectiles/Minion/VoidSnakeMinionBody"; }
            else if (projectile.type == mod.ProjectileType("VoidSnakeMinionHead")) { texture = "Projectiles/Minion/VoidSnakeMinionHead"; }
            else if (projectile.type == mod.ProjectileType("VoidSnakeMinionTail")) { texture = "Projectiles/Minion/VoidSnakeMinionTail"; }
            Texture2D SnakeTexture = mod.GetTexture(texture);
            Vector2 vect2 = new Vector2(projectile.Center.X  - Main.screenPosition.X, projectile.position.Y + projectile.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0,0,SnakeTexture.Width, SnakeTexture.Height);
            spriteBatch.Draw(
                   SnakeTexture ,
                     vect2, rect2, Color.White, projectile.rotation, new Vector2(SnakeTexture.Width / 2, SnakeTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }
        private int Target()
        {
            int targetMag = 1200;
            int target = -1;
            if (Main.player[projectile.owner].MinionAttackTargetNPC > 0)
            { target = Main.player[projectile.owner].MinionAttackTargetNPC; return target; }
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


    }
}