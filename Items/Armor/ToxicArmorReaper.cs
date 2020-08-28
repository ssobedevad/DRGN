
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorReaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Cloak");
            Tooltip.SetDefault("12% increased reaper damage" + "\n14% increased reaper crit damage" + "\n+ 8 max souls" + "\n+ 3 reaper critical armor pen");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 2200;
            item.rare = ItemRarityID.Green;
            item.defense = 5;
            
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
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.12f;
            player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen += 3;
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.14f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 8;

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
