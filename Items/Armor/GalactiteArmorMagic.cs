
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Hood");
            Tooltip.SetDefault("90% increased Magic damage" + "\n45% decreased mana cost" + "\nReally Greatly increased mana regeneration" + "\n + 250 max mana");

        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 1000;
            item.rare = 12;
            item.defense = 35;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("GalactiteArmor") && legs.type == mod.ItemType("GalactiteBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A star will protect you";
            player.GetModPlayer<DRGNPlayer>().galactiteArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.magicDamage = (float)1.9 * player.magicDamage;
            player.manaCost = (float)0.55 * player.manaCost;
            player.manaRegen += 20;
            player.statManaMax2 += 250;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidHood"));
            recipe.AddIngredient(mod.ItemType("CloudWarriorMagic"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorMagic"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorMagic"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorMagic"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinHelm"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
