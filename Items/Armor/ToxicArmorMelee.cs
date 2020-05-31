
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Helmet");
            Tooltip.SetDefault("25% increased melee damage."+ "\n + 35 max hp");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 1000;
            item.rare = 2;
            item.defense = 8;
            
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
            player.meleeDamage = (float)(1.25 * player.meleeDamage);
            player.statLifeMax2 += 35;
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
