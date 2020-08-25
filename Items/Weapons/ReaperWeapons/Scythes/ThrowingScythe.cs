

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public abstract class ThrowingScythe : ReaperWeapon
    {
        public int shoot2;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }
        public virtual void SSD()
        {
        }
        public override void SafeSetDefaults()
        {
            SSD();
            item.useAnimation = item.useTime;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
                player.velocity += new Vector2(-speedX, -speedY);
            }
            else
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), shoot2, damage, knockBack, player.whoAmI, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.GetModPlayer<ReaperPlayer>().scytheThrowCd == 0 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
                {
                    player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
                    player.GetModPlayer<ReaperPlayer>().scytheThrowCd = item.useTime * 10;
                    return true;
                }
                return false;
            }
            else { return true; }
        }
    }
}


