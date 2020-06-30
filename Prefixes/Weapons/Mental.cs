using Terraria;
using Terraria.ModLoader;

namespace DRGN.Prefixes.Weapons
{
    public class Mental : ModPrefix
    {
        public Mental()
        {

        }
        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }
           
            mod.AddPrefix("Mental", new Mental());

            return false;
        }

       

        public override bool CanRoll(Item item)
            => ((DRGNModWorld.MentalMode) ? true : false);

      
        public override PrefixCategory Category
            => PrefixCategory.AnyWeapon;





       




        public override void ModifyValue(ref float valueMult)
        {

            valueMult *= 2f;
        }

        public override float RollChance(Item item)
            => 0.001f;

        
        public override void Apply(Item item)
        {

            item.damage = (int)(item.damage * 1.25f);
            item.knockBack = (int)(item.knockBack * 1.25f);
            item.useTime = (int)(item.useTime * 0.75f);
            item.useAnimation = (int)(item.useAnimation * 0.75f);
            item.scale = (int)(item.scale * 1.25f);
            item.shootSpeed = (int)(item.shootSpeed * 1.25f);
            item.mana = (int)(item.mana * 0.75f);
            item.crit += 15;
            item.rare += 1;

        }
       

    }
}
