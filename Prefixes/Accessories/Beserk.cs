﻿using Terraria;
using Terraria.ModLoader;
namespace DRGN.Prefixes.Accessories

{
    public class Beserk : ModPrefix
    {
        public Beserk()
        {

        }
        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }


            mod.AddPrefix("Beserk", new Beserk());


            return false;
        }





        public override bool CanRoll(Item item)
            => ((DRGNModWorld.MentalMode) ? true : false);


        public override PrefixCategory Category
            => PrefixCategory.Accessory;










        public override void ModifyValue(ref float valueMult)
        {

            valueMult *= 2f;
        }

        public override float RollChance(Item item)
            => 0.1f;

        public override void Apply(Item item)
        {


            item.rare += 1;

        }


    }
}
