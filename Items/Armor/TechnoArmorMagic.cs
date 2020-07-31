

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TechnoArmorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Hood");
            Tooltip.SetDefault("36% increased magic damage" + "\n40% reduced mana cost" + "\n+75 maximum mana");
        }

        public override void SetDefaults()
        {

            item.value = 2600;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 6;

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
            player.magicDamage *= 1.33f;
            player.manaCost *= 0.6f;
            player.statManaMax2 += 75;
            
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
