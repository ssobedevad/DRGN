using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ThePlague : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("ribbit");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.noMelee = true;
            item.magic = true;
            item.useAnimation = 20;
            item.mana = 12;
            item.rare = 5;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.reuseDelay = 30;
            item.useStyle = 5;
            
            item.useAnimation = 20;
            
            item.value = 2000;
        }
        public override bool UseItem(Player player)
        {
            NPCs.ExplodingFrog.playerMagicDamageMult = player.magicDamage;
            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("ExplodingFrog"));
           
            return true; 
            }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 12);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
