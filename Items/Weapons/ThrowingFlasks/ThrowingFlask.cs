
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public abstract class ThrowingFlask : ModItem
    {
        public virtual void SafeSetDefaults()
        { }
        public override void SetDefaults()
        {
            SafeSetDefaults();
            item.useStyle = 1;
            item.thrown = true;
            item.noMelee = true; 
            item.noUseGraphic = true; 
            item.autoReuse = true;
            item.consumable = true;
            item.UseSound = SoundID.Item106;
            item.maxStack = 999;
        }
        public override bool ConsumeItem(Player player)
        {
            return Main.rand.NextBool();
        }
    }
}