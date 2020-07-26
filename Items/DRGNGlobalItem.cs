using DRGN.Items.EngineerClass.Attachments;
using DRGN.Items.Equipables.MentalModeDrops;
using DRGN.Items.Weapons;
using DRGN.Items.Weapons.Whips;
using DRGN.Items.Weapons.Yoyos;
using DRGN.NPCs.Boss;

using DRGN.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{

    public class DRGNGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Marshmallow)
            {
                item.ammo = ItemID.Marshmallow;
                item.consumable = true;
            }
            if(item.type == ItemID.BallOHurt)
            { item.damage = 30; }
            if (item.type == ItemID.TheMeatball)
            { item.damage = 34; }
            if (item.type == ItemID.BlueMoon)
            { item.damage = 54; }
            if (item.type == ItemID.Sunfury)
            { item.damage = 70; }
            if (item.type == ItemID.DaoofPow)
            { item.damage = 100; }
            if (item.type == ItemID.FlowerPow)
            { item.damage = 130; }

        }
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (ItemID.Sets.Yoyo[item.type])
            { return true; }
            return false;
        }

        public override bool CanRightClick(Item item)
        {
            if(ModContent.GetInstance<DRGN>()._DisUI.CurrentState != null && item.damage > 0 && item.maxStack == 1) { return true; }
            var itemClone = item.Clone();
            if (itemClone.modItem == null) { return false; }
            ModItem mouseItemObject = itemClone.modItem;


            ModItem MI = mouseItemObject as ModItem;
            EngineerAttachments EA = MI as EngineerAttachments;

            if (EA == null || !Main.LocalPlayer.GetModPlayer<DRGNPlayer>().EngineerWeapon) { return false; }
           
            return true;
        }
       
        public override void RightClick(Item item, Player player)
        {
            Item itemClone = item.Clone();
            if (ModContent.GetInstance<DRGN>()._DisUI.CurrentState != null && item.damage > 0 && item.maxStack == 1 && !item.favorited) 
            { 
                for (int i = 0; i < 24; i++) 
                {
                   
                    if (ModContent.GetInstance<DRGN>().disassembleUI.itemslots[i] == null || ModContent.GetInstance<DRGN>().disassembleUI.itemslots[i].type == ItemID.None)
                    { ModContent.GetInstance<DRGN>().disassembleUI.itemslots[i] = itemClone; return; }
                }
                Item.NewItem(player.position, new Vector2(player.height, player.width), itemClone.type, 1, false, itemClone.prefix, false, false);

            }
           
            if (itemClone.modItem != null)
            {
                ModItem MI = itemClone.modItem;


               
                EngineerAttachments EA = MI as EngineerAttachments;

                if (EA != null)
                {
                    if (EA.isGunBody) { if (player.GetModPlayer<DRGNPlayer>().gunBodyType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gunBodyType); } player.GetModPlayer<DRGNPlayer>().gunBodyType = itemClone; player.GetModPlayer<DRGNPlayer>().gunBodyTier = EA.AttachmentTier; }
                    else if (EA.isGunBarrel) { if (player.GetModPlayer<DRGNPlayer>().barrelType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().barrelType); } player.GetModPlayer<DRGNPlayer>().barrelType = itemClone; player.GetModPlayer<DRGNPlayer>().barrelTier = EA.AttachmentTier; }
                    else if (EA.isGunChamber) { if (player.GetModPlayer<DRGNPlayer>().chamberType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().chamberType); } player.GetModPlayer<DRGNPlayer>().chamberType = itemClone; player.GetModPlayer<DRGNPlayer>().chamberTier = EA.AttachmentTier; }
                    else if (EA.isGunGrip) { if (player.GetModPlayer<DRGNPlayer>().gripType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().gripType); } player.GetModPlayer<DRGNPlayer>().gripType = itemClone; player.GetModPlayer<DRGNPlayer>().gripTier = EA.AttachmentTier; }
                    
                    else if (EA.isGunMag) { if (player.GetModPlayer<DRGNPlayer>().magType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().magType); } player.GetModPlayer<DRGNPlayer>().magType = itemClone; player.GetModPlayer<DRGNPlayer>().magTier = EA.AttachmentTier; }
                    else if (EA.isGunScope) { if (player.GetModPlayer<DRGNPlayer>().scopeType != null) { player.QuickSpawnItem(player.GetModPlayer<DRGNPlayer>().scopeType); } player.GetModPlayer<DRGNPlayer>().scopeType = itemClone; player.GetModPlayer<DRGNPlayer>().scopeTier = EA.AttachmentTier; }
                }
            }
        }
        public override bool UseItem(Item item, Player player)
        {

            if (item.type == ItemID.MechanicalEye && DRGNModWorld.MentalMode)
            {
                if (!Main.dayTime)
                {

                    Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, 125);
                        NPC.SpawnOnPlayer(player.whoAmI, 126);
                        NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Triplet"));
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 125f);
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 126f);
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, (float)ModContent.NPCType<Triplet>());
                    }
                    return true;
                }

            }
            return false;
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {


            if (context == "bossBag" && DRGNModWorld.MentalMode)
            {

                if (arg == ModContent.ItemType<SerpentBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Desertserpent>()); }
                else if (arg == ItemID.KingSlimeBossBag) { player.QuickSpawnItem(ModContent.ItemType<KingSlime>()); }
                else if (arg == ItemID.EyeOfCthulhuBossBag) { player.QuickSpawnItem(ModContent.ItemType<EyeOfCthulu>()); }
                else if (arg == ItemID.EaterOfWorldsBossBag) { player.QuickSpawnItem(ModContent.ItemType<EaterOfWorlds>()); }
                else if (arg == ItemID.BrainOfCthulhuBossBag) { player.QuickSpawnItem(ModContent.ItemType<BrainOfCthulu>()); }
                else if (arg == ModContent.ItemType<FrogBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.ToxicFrog>()); }
                else if (arg == ItemID.QueenBeeBossBag) { player.QuickSpawnItem(ModContent.ItemType<QueenBee>()); }
                else if (arg == ItemID.SkeletronBossBag) { player.QuickSpawnItem(ModContent.ItemType<Skeletron>()); }
                else if (arg == ModContent.ItemType<AntsBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.QueenAnt>()); }
                else if (arg == ItemID.WallOfFleshBossBag) { player.QuickSpawnItem(ModContent.ItemType<WallOfFlesh>()); }
                else if (arg == ModContent.ItemType<FishBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.IceFish>()); }
                else if (arg == ItemID.TwinsBossBag) { player.QuickSpawnItem(ModContent.ItemType<Twins>()); }
                else if (arg == ItemID.SkeletronPrimeBossBag) { player.QuickSpawnItem(ModContent.ItemType<SkeletronPrime>()); }
                else if (arg == ItemID.DestroyerBossBag) { player.QuickSpawnItem(ModContent.ItemType<Destroyer>()); }
                else if (arg == ItemID.PlanteraBossBag) { player.QuickSpawnItem(ModContent.ItemType<Plantera>()); }
                else if (arg == ItemID.GolemBossBag) { player.QuickSpawnItem(ModContent.ItemType<Golem>());}
                else if (arg == ItemID.FishronBossBag) { player.QuickSpawnItem(ModContent.ItemType<Fishron>()); }
                else if (arg == ModContent.ItemType<CloudBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.Cloud>()); }
                else if (arg == ItemID.CultistBossBag) { player.QuickSpawnItem(ModContent.ItemType<Cultists>()); }
                else if (arg == ItemID.MoonLordBossBag) { player.QuickSpawnItem(ModContent.ItemType<MoonLord>()); }
                else if (arg == ModContent.ItemType<DragonFlyBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.DragonFly>()); }
                else if (arg == ModContent.ItemType<DragonBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.FireDragon>()); }
                else if (arg == ModContent.ItemType<VoidBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.VoidSnake>()); }
                else if (arg == ModContent.ItemType<TechnoBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.TheVirus>()); }
                else if (arg == ModContent.ItemType<GalactiteBossBag>()) { player.QuickSpawnItem(ModContent.ItemType<Equipables.MentalModeDrops.GalacticGuardian>()); }






            }
            if (context == "bossBag")
            {

                int rand = Main.rand.Next(1, 6);
                if (arg == ItemID.GolemBossBag)
                {
                    if (rand == 1) { player.QuickSpawnItem(ModContent.ItemType<RockSpear>()); }
                    else if (rand == 2) { player.QuickSpawnItem(ModContent.ItemType<RockWhip>()); }
                    else if (rand == 3) { player.QuickSpawnItem(ModContent.ItemType<RockSprayer>()); }
                    else if (rand == 4) { player.QuickSpawnItem(ModContent.ItemType<CelestialSundial>()); }
                    else if (rand == 5) { player.QuickSpawnItem(ModContent.ItemType<RockYoyo>()); }


                }
            
            
            
            }
        }
    }
}
