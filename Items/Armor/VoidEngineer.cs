using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Hardhat");
            Tooltip.SetDefault("55% increased engineer damage" + "\n+ 60 max bullets");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 560000;
            item.rare = ItemRarityID.Purple;
            item.defense = 30;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VoidChestplate") && legs.type == mod.ItemType("VoidBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases the effectiveness of the void buff";
            player.GetModPlayer<DRGNPlayer>().voidArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.55f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 60;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 10);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
