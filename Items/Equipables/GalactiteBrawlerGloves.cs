using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.HandsOff, EquipType.HandsOn)]
    public class GalactiteBrawlerGloves : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactite Brawler Gloves");
            Tooltip.SetDefault("Hitting enemies inflicts galactic curse"+"\nGrants 10% increased damage ,melee speed, thrown velocity"+"\n10% decreased mana usage"+"\n5% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 100000;
            item.rare = 13;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage = (float)(player.rangedDamage * 1.1);
            player.thrownDamage = (float)(player.thrownDamage * 1.1);
            player.magicDamage = (float)(player.magicDamage * 1.1);
            player.minionDamage = (float)(player.minionDamage * 1.1);
            player.meleeDamage = (float)(player.meleeDamage * 1.1);
            player.rangedCrit += 5;
            player.thrownCrit += 5;
            player.magicCrit += 5;
            player.maxMinions += 5;
            player.meleeCrit += 5;
            player.meleeSpeed = (float)(player.meleeSpeed * 1.1);
            
            player.thrownVelocity = (float)(player.thrownVelocity * 1.1);
            player.manaCost = (float)(player.manaCost * 0.9);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FireGauntlet);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
       
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}