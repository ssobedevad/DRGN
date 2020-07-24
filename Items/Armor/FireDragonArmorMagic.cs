
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonArmorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon hood");
            Tooltip.SetDefault("57% increased magic damage" + "\n+150 maximum mana");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 150000;
            item.rare = ItemRarityID.Red;
            item.defense = 13;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FireDragonChestplate") && legs.type == mod.ItemType("FireDragonBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Hitting enemies has a chance to produce a fire wall that destroys nearby projectiles";
            player.GetModPlayer<DRGNPlayer>().dragonArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {

            player.magicDamage *= 1.57f;
            player.statManaMax2 += 150;
           
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 6);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
