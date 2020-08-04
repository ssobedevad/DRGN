
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using DRGN.Rarities;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon Cloak");
            Tooltip.SetDefault("20% increased reaper damage" + "\n25% increased reaper crit damage" + "\n+ 15 max souls" + "\n+ 8 reaper critical armor pen" + "\n+25% increased reaper knockback" + "\n+50 increased bloodhunt range" + "\n+0.25% increased damage per soul");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 150000;
            item.rare = ItemRarities.FieryOrange;
            item.defense = 32;

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

            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.2f;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 8;
            player.GetModPlayer<ReaperPlayer>().reaperKnockback *= 1.25f;
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 50;
            player.GetModPlayer<ReaperPlayer>().damageIncPerSoul += 0.0025f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.25f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 15;



        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 13);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
