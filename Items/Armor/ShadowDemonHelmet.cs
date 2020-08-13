
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
            Tooltip.SetDefault("5% increased reaper damage" + "\n10% increased reaper crit damage" + "\n+ 3 max souls");
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
            player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult *= 1.1f;
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 3;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 6);
            recipe.AddIngredient(ItemID.Deathweed);
            recipe.AddIngredient(ItemID.Ebonwood, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.Vertebrae, 6);
            recipe.AddIngredient(ItemID.Deathweed);
            recipe2.AddIngredient(ItemID.Shadewood, 10);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }



}
