
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon mask");
            Tooltip.SetDefault("65% increased thown damage and velocity");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 35;

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

            player.thrownVelocity = (float)(1.65 * player.thrownVelocity);
           
            player.thrownDamage = (int)(1.65 * player.meleeDamage);

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
