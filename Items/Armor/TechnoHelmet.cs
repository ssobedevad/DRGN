
using DRGN.Items.EngineerClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TechnoHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
           
            Tooltip.SetDefault("28% increased damage"+"\n5% increased crit chance"+"\n+25 increased max health" + "\n+35 increased max bullets");
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
            player.allDamage *= 1.28f;
            player.statLifeMax2 += 25;
            
            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.thrownCrit += 5;
            player.rangedCrit += 5;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 5;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 35;
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
