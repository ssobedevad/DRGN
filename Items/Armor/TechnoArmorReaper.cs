

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TechnoArmorReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Cloak");
            Tooltip.SetDefault("22% increased reaper damage" + "\n5% increased reaper crit chance" + "\n17% increased reaper crit damage" + "\n+ 11 max souls" + "\n+ 5 reaper critical armor pen" + "\n+5% increased reaper knockback");
        }

        public override void SetDefaults()
        {

            item.value = 2200;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 18;

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
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.22f;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 5;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 5;
            player.GetModPlayer<ReaperPlayer>().reaperKnockback *= 1.05f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.17f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 11;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
