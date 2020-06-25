
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Hardhat");
            Tooltip.SetDefault("30% increased engineer damage 12 additional max bullets.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 2200;
            item.rare = ItemRarityID.Green;
            item.defense = 7;
            
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
            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.3f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 12;

        }


            public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 14);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }


    
}
