
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Cloak");
            Tooltip.SetDefault("40% increased reaper damage" + "\n45% increased reaper crit damage" + "\n+ 30 max souls" + "\n+ 18 reaper critical armor pen" + "\n+75% increased reaper knockback" + "\n+250 increased bloodhunt range" + "\n+1% increased damage per soul");

        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 600000;
            item.rare = ItemRarities.GalacticRainbow;
            item.defense = 34;

        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.4f;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 18;
            player.GetModPlayer<ReaperPlayer>().reaperKnockback *= 1.75f;
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 250;
            player.GetModPlayer<ReaperPlayer>().damageIncPerSoul += 0.01f;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.45f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 30;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("GalactiteArmor") && legs.type == mod.ItemType("GalactiteBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A star will protect you";
            player.GetModPlayer<DRGNPlayer>().galactiteArmorSet = true;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidReaper")); 
            recipe.AddIngredient(mod.ItemType("CloudWarriorReaper"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorReaper"));
            recipe.AddIngredient(mod.ItemType("FireDragonReaper"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorReaper"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorReaper"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
