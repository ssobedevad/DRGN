
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class MechaSlayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("good for those pesky metal guys");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.melee = true;
            
            item.shoot = mod.ProjectileType("MechaSlayerProj");
            item.shootSpeed = 12;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 1;
            item.knockBack = 8;
            item.value = 48000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 10;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            if (target.boss == true)
                player.AddBuff(mod.BuffType("BossSlayer"),360);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("DRGN:T3HmB",8);
            recipe.AddIngredient(ItemID.BreakerBlade);
            recipe.AddIngredient(ItemID.BeeKeeper);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
           
        }
    }
}