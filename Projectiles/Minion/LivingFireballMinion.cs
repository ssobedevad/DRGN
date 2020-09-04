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

namespace DRGN.Projectiles.Minion
{
    public class LivingFireballMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        { 
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.height = 28;
            projectile.width = 28;
            projectile.aiStyle = -1;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.minion = true;
        }
        public override void AI()
        {

            Player player = Main.player[projectile.owner];
            int Dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * -0.4f, projectile.velocity.Y * -0.4f, 0, Color.White, 1f);
            Main.dust[Dustid].noGravity = true;
            if (player.dead || !player.active)
            {
                player.ClearBuff(mod.BuffType("FireballMinion"));
            }
            if (player.HasBuff(mod.BuffType("FireballMinion")))
            {
                projectile.timeLeft = 2;
            }
            int target = DavesUtils.FindNearestTargettableNPC(projectile);
            if(player.MinionAttackTargetNPC > -1 && Main.npc[player.MinionAttackTargetNPC].CanBeChasedBy(this)) { OrbitTarget(player.MinionAttackTargetNPC); projectile.ai[1]++; Shoot(player.MinionAttackTargetNPC); return; }  
            else if(target > -1) { Shoot(target); }
            OrbitTarget();
        }        
        private void OrbitTarget(int npc = -1) 
        {
            Player player = Main.player[projectile.owner];
            Vector2 origin = player.Center;
            if (npc > -1)
            { origin = Main.npc[npc].Center; }        
            
            float pos = 1;
            float max = 1;
            for(int i = 0; i < Main.projectile.Length;i++)
            {
                if (i != projectile.whoAmI)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("LivingFireballMinion") && Main.projectile[i].owner == projectile.owner)
                    {
                        max++;
                        if (i < projectile.whoAmI)
                        { pos++; }

                    }
                }
            }
            Vector2 offset = new Vector2(0, -25 - max * 5);
            float rotation = pos/max;
            offset = DavesUtils.Rotate(offset, MathHelper.ToRadians(rotation * 360) + Main.GameUpdateCount/25f);
            projectile.Center = origin + offset;

        }
        private void Shoot(int npc = -1)
        {
            if(npc < 0) { return; }
            projectile.ai[1]++;
            if (projectile.ai[1] > 60)
            {
                projectile.ai[1] = 0;
                projectile.netUpdate = true;
                Projectile.NewProjectile(projectile.Center, Vector2.Normalize(Main.npc[npc].Center - projectile.Center) * 12, mod.ProjectileType("FireBlastMinion"),projectile.damage,projectile.knockBack,projectile.owner,npc);               
            }
        }
    }
}