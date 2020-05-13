using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class ExCaliburr : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("kinda holy ya know");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.shoot = mod.ProjectileType("ExCaliburrProj");
            item.shootSpeed = 25;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.knockBack =9;
            item.value = 150000;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 11;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            if (target.boss == true)
                    player.AddBuff(mod.BuffType("BossSlayer"), 360);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Excalibur);
            recipe.AddIngredient(mod.ItemType("TheDebuffGiver"));
            recipe.AddIngredient(mod.ItemType("MechaSlayer"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
           
        }
    }
}