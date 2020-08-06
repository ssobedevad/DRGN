
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class RockHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Rock Helmet");
            Tooltip.SetDefault("35% increased flail and yoyo damage.");
        }

        public override void SetDefaults()
        {
            
            item.value = 30000;
            item.rare = ItemRarityID.Lime;
            item.defense = 14;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RockArmor") && legs.type == mod.ItemType("RockBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants the player one additional yoyo and allows dual wield for flails";
            player.GetModPlayer<DRGNPlayer>().rockArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<DRGNPlayer>().YoyoDamageInc += 0.35f;
            player.GetModPlayer<DRGNPlayer>().FlailDamageInc += 0.35f;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 15);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
