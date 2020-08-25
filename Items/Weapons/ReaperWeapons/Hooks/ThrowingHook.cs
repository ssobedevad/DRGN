

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public abstract class ThrowingHook : ReaperWeapon
    {
        public int outTime;
        private bool toShoot;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hitting enemies leaves a hook that can be retracted with right click");
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
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useTurn = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (toShoot)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
            { RetractAllHooks(player); toShoot = false; }
            else if (player.altFunctionUse != 2) { toShoot = true; }
        }
    }
}