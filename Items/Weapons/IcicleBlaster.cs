using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class IcicleBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots an ice shard that falls after a while");
        }
        private int projID;
        public override void SetDefaults()
        {
            item.damage = 55;
            item.magic = true;
            item.mana = 15;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; 
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AntBiterJaws");
            
            item.shootSpeed = 14;
            
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            
                projID = Projectile.NewProjectile(position.X, position.Y, speedX, speedY,mod.ProjectileType("IcicleBlast"), damage, knockBack, player.whoAmI);
                 
            
           
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 15);

            recipe.AddIngredient(mod.ItemType("GlacialBar"), 15);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}