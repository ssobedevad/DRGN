

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
namespace DRGN.Items.Weapons.ReaperWeapons.Daggers
{
    public abstract class ThrowingDagger : ReaperWeapon
    {
        public int shoot2;
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
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
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), item.shoot, damage,knockBack,player.whoAmI, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit , player.GetModPlayer<ReaperPlayer>().HuntedTarget);
            }
            else 
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), shoot2, damage, knockBack, player.whoAmI, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
           if(player.altFunctionUse == 2)
           {
                int tpNPC = player.GetModPlayer<ReaperPlayer>().HuntedTarget;

                if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
                {
                    if (Vector2.Distance(Main.MouseWorld, Main.npc[tpNPC].Center) > Main.npc[tpNPC].width * 5f || Vector2.Distance(player.Center, Main.npc[tpNPC].Center) > BloodHuntRangeReal)
                    {
                        tpNPC = -1;
                    }
                    if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
                    {
                        player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
                        player.GetModPlayer<ReaperPlayer>().stabDashCd = item.useTime * 10;
                        return true;
                    }
                }
                return false;
           }
           else { return true; }
        }


    }
}