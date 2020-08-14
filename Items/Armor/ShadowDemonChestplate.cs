
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ShadowDemonChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
           
            Tooltip.SetDefault("Grants +1 defense and + 1% reaper damage per rotating scythe alive");
        }

        public override void SetDefaults()
        {
            
            item.value = 6000;
            item.rare = ItemRarityID.Blue;
            item.defense = 3;

        }

        public override void UpdateEquip(Player player)
        {
            int numScythes = player.ownedProjectileCounts[mod.ProjectileType("ReaperScythe")];
            player.statDefense += numScythes;
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult += (0.01f * numScythes);

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
