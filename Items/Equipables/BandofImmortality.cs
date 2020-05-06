using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BandofImmortality : ModItem
    {
        private static int immuneTime = 120;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Band of immortality");
            Tooltip.SetDefault("While equipped you can have 2 seconds of invincibility"
                                +"\nHowever you have no immunity frames");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 17);
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlDown && immuneTime >= 1) { player.immune = true; immuneTime -= 1; }
            else { player.immune = false;
                if (Main.rand.Next(0, 3 ) == 1 && immuneTime < 120) { immuneTime += 1; } }
            if (immuneTime == 119 && !player.controlDown) { for (int i = 0; i < 10; i++) { int DustID = Dust.NewDust(player.Center, 0, 0, 226, 0.0f, 0.0f, 10, default(Color), 2f); } }

}
       



            
        
            
            

     
    }
}