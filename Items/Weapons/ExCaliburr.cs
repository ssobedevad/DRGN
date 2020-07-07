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
            item.damage = 68;
            item.melee = true;
            item.shoot = mod.ProjectileType("ExCaliburrProj");
            item.shootSpeed = 25;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack =9;
            item.value = 80000;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 11;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            
            if (target.boss == true)
                    player.AddBuff(mod.BuffType("BossSlayer"), 120);
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