
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip( EquipType.Shield)]

    public class GalactiteShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactite Shield");
            Tooltip.SetDefault("Allows immunity to practically every debuff"
                             + "\n+8 defense"
                             + "\nMajor increase to all stats"
                             
                             + "\nGrants immunity to fire blocks immunity to lava"
                              + "\nGrants a shield of cthulu style dash");

        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 350000;
            item.rare = ItemRarityID.Purple;
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
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Webbed>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Burning>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.BrokenWings>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.Shocked>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.GalacticCurse>()] = true;
            

            // CelestialShell
            player.meleeSpeed = (float)(player.meleeSpeed * 1.4);
            player.minionKB += 1.5f;
            player.lifeRegen += 3;
            player.rangedDamage *= 1.3f;
            player.thrownDamage *= 1.3f;
            player.magicDamage *= 1.3f;
            player.minionDamage *= 1.3f;
            player.meleeDamage *= 1.3f;
            player.GetModPlayer<Items.EngineerClass.EngineerPlayer>().engineerDamageMult *= 1.3f;
            player.rangedCrit += 6;
            player.thrownCrit += 6;
            player.magicCrit += 6;
            player.maxMinions += 6;
            player.meleeCrit += 6;
            player.GetModPlayer<Items.EngineerClass.EngineerPlayer>().engineerCrit += 6;
            player.accFlipper = true;
            player.accMerman = true;
            player.statDefense += 8;
            if (!Main.dayTime) { player.AddBuff(BuffID.Werewolf, 1); player.wereWolf = true; }
            else { player.wereWolf = false; }
            if (player.wet) { player.AddBuff(BuffID.Merfolk, 1); }
            
            player.dash = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CelestialShield"));
            recipe.AddIngredient(mod.ItemType("GalacticMedallion"));

            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}