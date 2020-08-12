using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using DRGN.Rarities;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Cloak");
            Tooltip.SetDefault("32% increased reaper damage" + "\n35% increased reaper crit damage" + "\n+ 20 max souls" + "\n+ 12 reaper critical armor pen" + "\n+50% increased reaper knockback" + "\n+150 increased bloodhunt range" + "\n+0.5% increased damage per soul");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 560000;
            item.rare = ItemRarities.VoidPurple;
            item.defense = 16;

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


            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.32f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.35f;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 12;
            player.GetModPlayer<ReaperPlayer>().reaperKnockback *= 1.5f;
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 150;
            player.GetModPlayer<ReaperPlayer>().damageIncPerSoul += 0.005f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 20;

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
