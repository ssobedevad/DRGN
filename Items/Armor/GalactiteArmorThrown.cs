
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Mask");
            Tooltip.SetDefault("110% increased throwing velocity" + "\n85% increased throwing damage");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 600000;
            item.rare = ItemRarityID.Expert;
            item.defense = 55;

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


            player.thrownVelocity *= 2.1f;
            player.thrownDamage *= 1.85f;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidMask"));
            recipe.AddIngredient(mod.ItemType("CloudWarriorThrown"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorHelmet"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorThrown"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorThrown"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorThrown"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
