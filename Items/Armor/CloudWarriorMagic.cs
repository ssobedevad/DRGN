
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CloudWarriorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior Hood");
            Tooltip.SetDefault("55% increased magic damage and +120 max mana.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 7;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CloudWarriorArmor") && legs.type == mod.ItemType("CloudWarriorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A sun follows you around";
            player.GetModPlayer<DRGNPlayer>().cloudArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.statManaMax2 += 120;
            player.magicDamage = (float)1.55 * player.magicDamage;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 6);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.FragmentNebula, 6);
            recipe.AddIngredient(ItemID.FragmentStardust, 6);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 15);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
