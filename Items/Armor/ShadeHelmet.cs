
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ShadeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Cloak");
            Tooltip.SetDefault("8% increased reaper damage" + "\n9% increased reaper crit damage" + "\n+ 4 max souls");
        }

        public override void SetDefaults()
        {

            item.value = 1200;
            item.rare = ItemRarityID.Orange;
            item.defense = 3;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ShadeChestplate") && legs.type == mod.ItemType("ShadeBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants 25 increased critical armor penetration and 0.25% lifesteal when below 75% health";
            if (player.statLife < player.statLifeMax2 * 0.75f)
            {
                player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 25;
                player.GetModPlayer<DRGNPlayer>().lifeSteal += 0.25f;
            }


        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.08f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.09f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 4;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowDemonHelmet"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
