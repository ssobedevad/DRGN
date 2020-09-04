using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Buffs;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilSword : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 110;
            item.melee = true;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.value = 80000;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 2;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Vector2 velInit = Vector2.Normalize(target.Center - player.Center) * 16f;
            for (int i = -1; i < 3; i++)
            {
                Vector2 vel = DavesUtils.Rotate(velInit, MathHelper.ToRadians(2) * i);
                Projectile.NewProjectile(target.Center, vel, mod.ProjectileType("CrystilShard"), damage, knockBack, player.whoAmI);
            }
        }

    }
}