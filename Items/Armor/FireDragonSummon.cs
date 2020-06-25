
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon goggles");
            Tooltip.SetDefault("55% increased summon damage" + "\n+4 max minions ");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 150000;
            item.rare = ItemRarityID.Red;
            item.defense = 45;

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

            ;
            player.maxMinions += 4;
            player.minionDamage *= 1.55f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 8);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
