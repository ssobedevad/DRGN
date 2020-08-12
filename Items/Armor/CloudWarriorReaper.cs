
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CloudWarriorReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior Cloak");
            Tooltip.SetDefault("22% increased reaper damage" + "\n25% increased reaper crit damage" + "\n+ 12 max souls" + "\n+ 6 reaper critical armor pen" + "\n+10% increased reaper knockback" );
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 50000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 12;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CloudWarriorArmor") && legs.type == mod.ItemType("CloudWarriorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A sun follows you around";
            player.GetModPlayer<DRGNPlayer>().cloudArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.22f;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 6;
            player.GetModPlayer<ReaperPlayer>().reaperKnockback *= 1.1f;
            
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.25f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 12;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 6);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.FragmentNebula, 6);
            recipe.AddIngredient(ItemID.FragmentStardust, 6);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 15);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
