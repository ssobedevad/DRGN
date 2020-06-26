using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    public class ToxicFrogMinion : ModProjectile
    {
        public int whichNpc;
        private int target;
        private bool tileCollision;
       
        private int targetMag;


        public override void SetStaticDefaults()
        { // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
            
        }
        public override void SetDefaults()
        {

            projectile.friendly = true;

            projectile.height = 38;
            projectile.width = 36;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.tileCollide = true;
            projectile.penetrate = -1;

            projectile.minion = true;

        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            tileCollision = true;
            if(oldVelocity.X != 0 && projectile.velocity.X == 0) { projectile.velocity.X = oldVelocity.X; }

            return false;
        }

        public override void AI()
        {
            if (projectile.velocity.X < -0.1) { projectile.direction = 1; } else if (projectile.velocity.X > 0.1) { projectile.direction = -1; }
            projectile.spriteDirection = projectile.direction;
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("ToxicFrogMinion"));
            }
            if (player.HasBuff(mod.BuffType("ToxicFrogMinion")))
            {
                projectile.timeLeft = 2;
            }
            Target();
            if (player.MinionAttackTargetNPC > 0) { target = player.MinionAttackTargetNPC; }
            if (target == -1 || (Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - Main.player[Main.myPlayer].Center.X) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - Main.player[Main.myPlayer].Center.Y)) > 1600f)
            { FollowPlayer(player); }
            else
            {

                AttackEnemy(Main.npc[target]);

            }
            
            if (projectile.velocity.Y > 16f) { projectile.velocity.Y = 16f; }
            
            tileCollision = false;

        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[1] == -1)
            {
                for (int i = Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("ToxicFrogMinion")]; i > 0; i--)
                {
                    Projectile proj = null;
                    for (int j = 0; j < Main.projectile.Length; j++)
                    {
                        if (Main.projectile[j].type == mod.ProjectileType("ToxicFrogMinion") && Main.projectile[j].active && Main.projectile[j].ai[0] == i)
                        {
                            proj = Main.projectile[j];
                        }
                    }
                    if(proj == null) { break; }
                    Rectangle rect = new Rectangle((int)(proj.position.X - Main.screenPosition.X), (int)(proj.position.Y - Main.screenPosition.Y), proj.width, proj.height);
                    spriteBatch.Draw(ModContent.GetTexture(Texture),rect , new Rectangle(0, 0, proj.width, proj.height), Color.White, proj.rotation, Vector2.Zero, (proj.direction == 1)?SpriteEffects.FlipHorizontally: SpriteEffects.None, 0f);
                }
            }
            return false;
        }
        private void AttackEnemy(NPC target)
        {
            

        }
        private void FollowPlayer(Player player)
        {
            
            if (projectile.ai[1] == -1) 
            {
                float DesiredX = player.Center.X;

                if (projectile.Center.X > DesiredX + 100)
                {
                    projectile.localAI[0] = 0;
                    if (tileCollision)
                    {
                        
                            projectile.velocity.X = -5;
                            projectile.velocity.Y = -3;
                        

                    }
                    else { projectile.velocity.Y += 0.3f;projectile.velocity.X *= 0.98f; }


                }
                else if (projectile.Center.X < DesiredX - 100)
                {
                    projectile.localAI[0] = 0;
                    if (tileCollision)
                    {
                        
                        
                            projectile.velocity.X = 5;
                            projectile.velocity.Y = -3;
                        

                    }
                    else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.98f; }


                }
                else if(tileCollision){ projectile.localAI[0] = -1;projectile.velocity = Vector2.Zero; }
                else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.98f; }
            }
            else if(Main.projectile[(int)projectile.ai[1]].localAI[0] == -1)
            {
                Projectile proj = Main.projectile[(int)projectile.ai[1]];
                float DesiredX = proj.Center.X;
                float YOffset = 10;

                float speed = 8f; // Sets the max speed of the npc.
                Vector2 move = new Vector2(DesiredX, proj.Center.Y - YOffset) - projectile.Center;
                float magnitude = Magnitude(move);

                move *= speed / magnitude;
                if (Math.Abs(projectile.Center.X - DesiredX) < 50) { projectile.Center = new Vector2(DesiredX, proj.Center.Y - YOffset); projectile.localAI[0] = -1;projectile.velocity = Vector2.Zero; }
                else {


                    if (projectile.Center.X > DesiredX)
                    {
                        projectile.localAI[0] = 0;
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                        else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.98f; }


                    }
                    else if (projectile.Center.X < DesiredX)
                    {
                        projectile.localAI[0] = 0;
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                        else { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.98f; }


                    }
                }
               




            }

        }
        private void Target()
        {
            targetMag = 800;
            target = -1;

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {


                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {


                    float DistanceProjtoNpc = Vector2.Distance(projectile.Center, Main.npc[whichNpc].Center);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }


        }
        private void Move(Vector2 MoveTo)
        {
            // Sets the max speed of the npc.
           
            Vector2 move = MoveTo - projectile.Center;
            float magnitude = Magnitude(move);

            if (magnitude > 20)
            {
                move *= 20 / magnitude;
            }


            projectile.velocity = (projectile.velocity * 30f + move) / 31f;

        }
        private Vector2 JumpAtEnemy(NPC target)
        {
            float speed = 10f; // Sets the max speed of the npc.
            Vector2 move = target.Center - projectile.Bottom;
            float magnitude = Magnitude(move);

            move *= speed / magnitude;


            return move;

        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}