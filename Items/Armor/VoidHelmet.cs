using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Helmet");
            Tooltip.SetDefault("55% increased melee damage" + "\n30% increased melee crit" + "\n55% increased melee speed"+ "\n100 max health");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 75;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VoidChestplate") && legs.type == mod.ItemType("VoidBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases the effectiveness of the void buff";
            player.GetModPlayer<DRGNPlayer>().voidArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.meleeDamage = (float)1.55 * player.meleeDamage;
            player.meleeCrit = (int)1.3 * player.meleeCrit;
            player.meleeSpeed = (float)1.55 * player.meleeSpeed;
            player.statLifeMax2 += 100;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 8);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
