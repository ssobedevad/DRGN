using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class ReaperSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            
            item.rare = ItemRarityID.Red;
            item.noGrabDelay = 0;
            
            

        }
        public override bool CanPickup(Player player)
        {
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            item.SetDefaults();
        }
        public override void PostUpdate()
        {
            Player player = Main.LocalPlayer;
            if (Vector2.Distance(item.Center, player.Center) < 150 && player.GetModPlayer<ReaperPlayer>().numSouls < player.GetModPlayer<ReaperPlayer>().maxSouls2 && player.active && !player.dead)
            {
                item.velocity = Vector2.Normalize(player.Center - item.Center) * 10;
                if (Vector2.Distance(item.Center, player.Center) < 10 )
                { player.GetModPlayer<ReaperPlayer>().numSouls += 1; CombatText.NewText( new Rectangle((int)player.position.X , (int)player.position.Y - 10, player.width , 10),Color.Red , player.GetModPlayer<ReaperPlayer>().numSouls.ToString()); item.active = false;  }
            }
        }


       
        

    }
}
