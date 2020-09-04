using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 38;
            item.magic = true;
            item.useTime = 4;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 250000;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("Crystil");
            item.mana = 10;
            item.shootSpeed = 14f;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {           
            Vector2 startPos = new Vector2(position.X, position.Y - 800) + new Vector2(Main.rand.NextFloat(-200, 200), Main.rand.NextFloat(-200, 200));
            Projectile.NewProjectile(startPos, Vector2.Normalize(Main.MouseWorld - startPos) * item.shootSpeed, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 14);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}


