
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ShadowDemonHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Demon Cloak");
            Tooltip.SetDefault("5% increased reaper damage" + "\n6% increased reaper crit damage" + "\n+ 3 max souls");
        }

        public override void SetDefaults()
        {
      
            item.value = 800;
            item.rare = ItemRarityID.Blue;
            item.defense = 1;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ShadowDemonChestplate") && legs.type == mod.ItemType("ShadowDemonBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants 15 increased critical armor penetration and 0.1% lifesteal when below 65% health";
            if (player.statLife < player.statLifeMax2 * 0.65f)
            {
                player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 15;
                player.GetModPlayer<DRGNPlayer>().lifeSteal += 0.1f;
            }


        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.05f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.06f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 3;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
