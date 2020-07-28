
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Hood");
            Tooltip.SetDefault("70% increased Magic damage" + "\n35% decreased mana cost" + "\nGreatly increased mana regeneration" +"\n + 200 max mana");

        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 560000;
            item.rare = ItemRarities.VoidPurple;
            item.defense = 14;

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


            player.magicDamage *= 1.7f;
            player.manaCost *= 0.65f;
            player.manaRegen += 10;
            player.statManaMax2 += 200;

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
