
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Shoes,EquipType.Shield)]

    public class DefenderGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Defender Greaves");
            Tooltip.SetDefault("Allows immunity to practically every debuff"
                             + "\n6 defense"
                             + "\nminor increase to all stats"
                             + "\nturn into merfolk in water and wearwolf at night"
                             +"\nAllows flight, super fast running, and extra mobility on ice"
                             + "\n10 % increased movement speed"
                             + "\nProvides the ability to walk on water and lava"
                             + "\nGrants immunity to fire blocks immunity to lava");
                            
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
            player.buffImmune[BuffID.Rabies] = true;

            // CelestialShell
            player.meleeSpeed = (float)(player.meleeSpeed * 1.3);
            player.minionKB += 1f;
            player.lifeRegen += 2;
            player.rangedDamage = (float)(player.rangedDamage * 1.2);
            player.thrownDamage = (float)(player.thrownDamage * 1.2);
            player.magicDamage = (float)(player.magicDamage * 1.2);
            player.minionDamage = (float)(player.minionDamage * 1.2);
            player.meleeDamage = (float)(player.meleeDamage * 1.2);

            player.rangedCrit += 4;
            player.thrownCrit += 4;
            player.magicCrit += 4;
            player.maxMinions += 4;
            player.meleeCrit += 4;
            player.accFlipper = true;
            player.accMerman = true;
            player.statDefense += 6;
            if (!Main.dayTime) { player.AddBuff(BuffID.Werewolf, 1); player.wereWolf = true; }
            else { player.wereWolf = false; }
            if (player.wet) { player.AddBuff(BuffID.Merfolk, 1); }
            // Lava Waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            // Frostspark Boots
            player.accRunSpeed = 9.75f;
            player.rocketBoots = 5;
            player.moveSpeed += 0.1f;
            player.iceSkate = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CelestialShield"));
            recipe.AddIngredient(mod.ItemType("LavaSparkBoots"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}