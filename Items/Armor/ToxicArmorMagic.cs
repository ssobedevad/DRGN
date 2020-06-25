
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Hood");
            Tooltip.SetDefault("35% increased magic damage." + "\n + 60 max mana");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 2200;
            item.rare = ItemRarityID.Green;
            item.defense = 5;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ToxicArmor") && legs.type == mod.ItemType("ToxicArmorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Produces toxic bubbles that melt enemies";
            player.GetModPlayer<DRGNPlayer>().toxicArmorSet = true;


        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.35f;
            player.statManaMax2 += 60;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 12);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
