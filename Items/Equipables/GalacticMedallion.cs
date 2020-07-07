using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class GalacticMedallion : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Medallion");
            Tooltip.SetDefault("Void web debuff lasts half a second"+"\nGrants immunity to melting, shocked, burning, galactic curse and broken wings debuffs");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 150000;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DRGNPlayer>().voidDebuffReduced = true;
            player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Shocked>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Burning>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.BrokenWings>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.GalacticCurse>()] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidMedallion"));
            recipe.AddIngredient(mod.ItemType("FlareMedallion"));
            recipe.AddIngredient(mod.ItemType("ToxicMedallion"));
            recipe.AddIngredient(mod.ItemType("ElectricMedallion"));
            recipe.AddIngredient(mod.ItemType("GalacticEssence"),5);



            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}