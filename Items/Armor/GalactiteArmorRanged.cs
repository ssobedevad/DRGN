
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Visor");
            Tooltip.SetDefault("80% increased ranged damage" + "\n45% increased ranged crit" + "\n20% chance not to consume ammo");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 600000;
            item.rare = ItemRarityID.Expert;
            item.defense = 45;

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


            player.rangedDamage *= 1.8f;
            player.rangedCrit += 45;
            player.ammoCost80 = true;
            player.archery = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidVisor"));
            recipe.AddIngredient(mod.ItemType("CloudWarriorRanged"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorRanged"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorRanged"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorRanged"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorRanged"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
