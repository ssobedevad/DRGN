
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.EngineerClass.Weapons
{
    public class EngineerRifleTier6 : EngineerWeapon
    {
        // This is a staff that uses the example damage class stuff you've set up before, but uses exampleResource instead of mana.
        // This is a very simple way of doing it, and if you plan on multiple items using exampleResource then I'd suggest making a new abstract ModItem class that inherits ExampleDamageItem,
        // and doing the CanUseItem and UseItem in a more generalized way there, so you can just define the resource usage in SetDefaults and it'll do it automatically for you.
       

        public override void SafeSetDefaults()
        {
            item.damage = 100;
            baseDamage = 100;
            item.noMelee = true;
            
            item.autoReuse = true;
            item.rare = ItemRarityID.Red;
            item.width = 58;
            item.height = 26;
            item.useTime = 18;
            baseAttackSpeed = 18;
            baseSpread = 7f;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 18f;
            item.useAnimation = 18;
            item.shoot = mod.ProjectileType("EngineerBullet");
            item.useAmmo = AmmoID.Bullet;
            item.value = 180000;
        }
        
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InsectiumScrew"), 42);
            recipe.AddIngredient(mod.ItemType("InsectiumPlate"), 45);
            recipe.AddIngredient(mod.ItemType("InsectiumCog"), 38);
            recipe.AddIngredient(mod.ItemType("InsectiumPipe"), 38);
            recipe.AddIngredient(mod.ItemType("EngineerRifleTier5"));
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}