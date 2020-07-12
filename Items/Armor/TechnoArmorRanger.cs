
using DRGN.Items.EngineerClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TechnoArmorRanger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Visor");
            Tooltip.SetDefault("34% increased ranged and thrown damage" + "\n33% chance no to consume thrown item" + "\n20% chance no to consume ammo" + "\n50% increased thrown velocity" + "\n+2% increased crit chance");
        }

        public override void SetDefaults()
        {

            item.value = 2400;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 12;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechnoArmor") && legs.type == mod.ItemType("TechnoBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Nearby enemies become glitched";
            player.GetModPlayer<DRGNPlayer>().technoArmorSet = true;


        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.34f;
            player.thrownDamage *= 1.34f;
            player.thrownVelocity *= 1.5f;
            player.thrownCost33 = true;
            player.ammoCost80 = true;
            player.magicCrit += 2;
            player.meleeCrit += 2;
            player.thrownCrit += 2;
            player.rangedCrit += 2;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 2;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 22);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
