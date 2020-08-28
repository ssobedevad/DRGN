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
        
        private int target;
        private bool tileCollision;
        private int projid;
        private bool stacked = false;


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
            projid = -1;
            projectile.minion = true;
            stacked = false;
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
            
            Player player = Main.player[projectile.owner];


            if (!stacked) { projectile.velocity.Y += 0.3f; projectile.velocity.X *= 0.98f; projectile.tileCollide = true; }
            else { projectile.velocity = Vector2.Zero;projectile.tileCollide = false; }
               
            
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
            projectile.frameCounter = target;
            if (target == -1 || Vector2.Distance(projectile.Center,player.Center) > 1000)
            { FollowPlayer(player);}
            else
            {

                AttackEnemy(Main.npc[target],player);

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
                        if (Main.projectile[j].type == mod.ProjectileType("ToxicFrogMinion") && Main.projectile[j].active && Main.projectile[j].ai[0] == i && Main.projectile[j].owner == projectile.owner)
                        {
                            proj = Main.projectile[j];
                        }
                    }
                    if (proj != null)
                    {
                        int dir = 0;
                        
                        if (proj.velocity.X < -0.1) { dir = 0; } else if (proj.velocity.X > 0.1) { dir = 1; } else { dir = (Main.player[projectile.owner].Center.X > proj.Center.X) ? 1 : 0; }
                        if(proj.frameCounter > -1) { dir = (Main.npc[(int)proj.frameCounter].Center.X > proj.Center.X) ? 1 : 0; }
                       
                        Rectangle rect = new Rectangle((int)(proj.position.X - Main.screenPosition.X), (int)(proj.position.Y - Main.screenPosition.Y), proj.width, proj.height);
                        spriteBatch.Draw(ModContent.GetTexture(Texture), rect, new Rectangle(0, 0, proj.width, proj.height), lightColor, proj.rotation, Vector2.Zero, (SpriteEffects)(dir), 0f);
                    }
                    
                }
            }
            return false;
        }
        private void AttackEnemy(NPC target , Player player)
        {
            ShootAtEnemy(target);
            
           
            if (projectile.ai[1] == -1)
            {
                float DesiredX = target.Center.X;
               
                if (projectile.Center.X > DesiredX + 400)
                {
                    stacked = false;
                    if (tileCollision)
                    {

                        projectile.velocity.X = -5;
                        projectile.velocity.Y = -3;


                    }
                   


                }
                else if (projectile.Center.X < DesiredX - 400)
                {
                    stacked = false;
                    if (tileCollision)
                    {


                        projectile.velocity.X = 5;
                        projectile.velocity.Y = -3;


                    }
                    


                }
                else if (tileCollision) { stacked = true; }
                
            }
            else if (!Main.projectile[(int)projectile.ai[1]].tileCollide)
            {

                Projectile proj = Main.projectile[(int)projectile.ai[1]];
                float DesiredX = proj.Center.X;
                float YOffset = 10;
                
                float speed = 8f; // Sets the max speed of the npc.
                Vector2 move = new Vector2(DesiredX, proj.Center.Y - YOffset) - projectile.Center;
                float magnitude = Magnitude(move);
                

                move *= speed / magnitude;
                if (Math.Abs(projectile.Center.X - DesiredX) < 20) { projectile.Center = new Vector2(DesiredX, proj.Center.Y - YOffset); stacked = true; ; }
                else
                {
                    stacked = false;

                    if (projectile.Center.X > DesiredX)
                    {
                        
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                       


                    }
                    else if (projectile.Center.X < DesiredX)
                    {
                        
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                        


                    }
                }





            }
            else if (!Main.projectile[(int)projectile.ai[1]].tileCollide|| !Main.projectile[(int)projectile.localAI[1]].tileCollide)
            {
                stacked = false;
                float DesiredX = Main.projectile[(int)projectile.ai[1]].Center.X + ((Main.rand.NextBool()) ? -2 : 2);
               
                if (projectile.Center.X > DesiredX)
                {
                    stacked = false;
                    if (tileCollision)
                    {

                        projectile.velocity.X = -Main.rand.Next(3, 6);
                        projectile.velocity.Y = -Main.rand.Next(2, 4);


                    }
                   


                }
                else if (projectile.Center.X < DesiredX)
                {
                    stacked = false;
                    if (tileCollision)
                    {


                        projectile.velocity.X = Main.rand.Next(3, 6);
                        projectile.velocity.Y = -Main.rand.Next(2, 4);


                    }
                    


                }
                else if (tileCollision) { projectile.velocity = Vector2.Zero; }
            }

        }
        private void FollowPlayer(Player player)
        {
            
            if (projectile.ai[1] == -1) 
            {
                float DesiredX = player.Center.X;
                if (Vector2.Distance(new Vector2(DesiredX, player.Center.Y), projectile.Center) > 800)
                {
                    projectile.Center = new Vector2(DesiredX, player.Center.Y);
                    for (int i = 0; i < 50; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 273, 0, 0, 120, default(Color), 1f);
                        Main.dust[DustID].noGravity = true;
                    }
                    stacked = false;
                }
                if (projectile.Center.X > DesiredX + 200)
                {
                    stacked = false;
                    if (tileCollision)
                    {
                        
                            projectile.velocity.X = -5;
                            projectile.velocity.Y = -3;
                        

                    }
                   


                }
                else if (projectile.Center.X < DesiredX - 200)
                {
                    stacked = false;
                    if (tileCollision)
                    {
                        
                        
                            projectile.velocity.X = 5;
                            projectile.velocity.Y = -3;
                        

                    }
                    


                }
                else if(tileCollision) { stacked = true; }
               
            }
            else if(!Main.projectile[(int)projectile.ai[1]].tileCollide)
            {

                Projectile proj = Main.projectile[(int)projectile.ai[1]];
                float DesiredX = proj.Center.X;
                float YOffset = 10;
                if (Vector2.Distance(new Vector2(DesiredX, player.Center.Y- YOffset), projectile.Center) > 800)
                {
                    projectile.Center = new Vector2(DesiredX, player.Center.Y- YOffset);
                    for (int i = 0; i < 50; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 273, 0, 0, 120, default(Color), 1f);
                        Main.dust[DustID].noGravity = true;
                    }
                    stacked = false;
                }

                float speed = 8f; // Sets the max speed of the npc.
                Vector2 move = new Vector2(DesiredX, proj.Center.Y - YOffset) - projectile.Center;
                float magnitude = Magnitude(move);

                move *= speed / magnitude;
                if (Math.Abs(projectile.Center.X - DesiredX) < 25) { projectile.Center = new Vector2(DesiredX, proj.Center.Y - YOffset); stacked = true; }
                else {
                    stacked = false;

                    if (projectile.Center.X > DesiredX)
                    {
                       
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                       

                    }
                    else if (projectile.Center.X < DesiredX)
                    {
                        
                        if (tileCollision)
                        {


                            projectile.velocity.X = move.X;
                            projectile.velocity.Y = -3;


                        }
                       


                    }
                }
               




            }
            else if (!Main.projectile[(int)projectile.ai[1]].tileCollide || !Main.projectile[(int)projectile.localAI[1]].tileCollide)
            {
                stacked = false;
                float DesiredX = Main.projectile[(int)projectile.ai[1]].Center.X +  ((Main.rand.NextBool())? -2 : 2);
                if (Vector2.Distance(new Vector2(DesiredX, player.Center.Y), projectile.Center) > 800)
                {
                    projectile.Center = new Vector2(DesiredX, player.Center.Y);
                    for (int i = 0; i < 50; i++)
                    {
                        int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 273, 0, 0, 120, default(Color), 1f);
                        Main.dust[DustID].noGravity = true;
                    }
                    stacked = false;
                }
                if (projectile.Center.X > DesiredX)
                {
                   
                    if (tileCollision)
                    {

                        projectile.velocity.X = -Main.rand.Next(3,6);
                        projectile.velocity.Y = -Main.rand.Next(2, 4);


                    }
                    


                }
                else if (projectile.Center.X < DesiredX)
                {
                    
                    if (tileCollision)
                    {


                        projectile.velocity.X = Main.rand.Next(3, 6);
                        projectile.velocity.Y = -Main.rand.Next(2, 4);


                    }
                    


                }
                else if (tileCollision) {projectile.velocity = Vector2.Zero; }
            }

        }
        private void Target()
        {
            int targetMag = 800;
            target = -1;

            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
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
        
        private void ShootAtEnemy(NPC target)
        { 
            
            Vector2 TargetDiff = target.Center - projectile.Center;
            if (projid == -1)
            {
                projid = Projectile.NewProjectile(projectile.Center, TargetDiff, ModContent.ProjectileType<FrogTongueMinion>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
            }
            else if (!Main.projectile[projid].active || Main.projectile[projid].type != ModContent.ProjectileType<FrogTongueMinion>()) { projid = -1; }
            
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}