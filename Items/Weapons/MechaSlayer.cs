
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
            item.value = 100000;
            item.rare = 4;
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
            recipe.AddIngredient(ItemID.AdamantiteBar , 10);
            recipe.AddIngredient(mod.ItemType("TheDebuffGiver"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TitaniumBar, 10);
            recipe2.AddIngredient(mod.ItemType("TheDebuffGiver"));
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}