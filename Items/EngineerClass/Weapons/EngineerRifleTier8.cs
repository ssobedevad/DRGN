
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.EngineerClass.Weapons
{
    public class EngineerRifleTier8 : EngineerWeapon
    {
        // This is a staff that uses the example damage class stuff you've set up before, but uses exampleResource instead of mana.
        // This is a very simple way of doing it, and if you plan on multiple items using exampleResource then I'd suggest making a new abstract ModItem class that inherits ExampleDamageItem,
        // and doing the CanUseItem and UseItem in a more generalized way there, so you can just define the resource usage in SetDefaults and it'll do it automatically for you.
       

        public override void SafeSetDefaults()
        {
            item.damage = 195;
            baseDamage = 195;
            item.noMelee = true;
            
            item.autoReuse = true;
            item.rare = ItemRarityID.Purple;
            item.width = 58;
            item.height = 26;
            item.useTime = 14;
            baseAttackSpeed = 14;
            baseSpread = 6f;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 19f;
            item.useAnimation = 14;
            item.shoot = mod.ProjectileType("EngineerBullet");
            item.useAmmo = AmmoID.Bullet;
            item.value = 550000;
        }
        
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScrew"), 42);
            recipe.AddIngredient(mod.ItemType("VoidPlate"), 45);
            recipe.AddIngredient(mod.ItemType("VoidCog"), 38);
            recipe.AddIngredient(mod.ItemType("VoidPipe"), 38);
            recipe.AddIngredient(mod.ItemType("EngineerRifleTier7"));
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}