
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Shield)]
    public class CelestialShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Shield");
            Tooltip.SetDefault("Allows immunity to nearly all debuffs"
                             +"\n+5 defense"
                             + "\nMinor increase to all stats"
                             + "\nTurn into merfolk in water and wearwolf at night"
                             + "\nGrants a shield of cthulu style dash"
                            );
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 17);
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // ankh shield
            player.noKnockback = true;
            player.buffImmune[BuffID.Poisoned] = true; ;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[ BuffID.Rabies] = true;

            // CelestialShell
            player.meleeSpeed = (float)(player.meleeSpeed * 1.2);
            player.minionKB += 1f;
            player.lifeRegen += 2;
            player.rangedDamage *= 1.15f;
            player.thrownDamage *= 1.15f;
            player.magicDamage *= 1.15f;
            player.minionDamage *= 1.15f;
            player.meleeDamage *= 1.15f;
            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.15f;
            player.dash = 1;
            player.rangedCrit += 3;
            player.thrownCrit += 3;
            player.magicCrit += 3;
            player.maxMinions += 3;
            player.meleeCrit += 3;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 3;
            player.accFlipper = true;
            player.accMerman = true;
            player.statDefense += 5;
            if (!Main.dayTime) { player.AddBuff(BuffID.Werewolf,1); player.wereWolf = true; }
            else { player.wereWolf = false; }
            if (player.wet){ player.AddBuff(BuffID.Merfolk,1);  }
            
            player.dash =2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}