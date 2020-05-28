using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Items.EngineerClass.Attachments
{
    // This class handles everything for our custom damage class
    // Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
   
    public abstract class EngineerAttachments : ModItem
    {
        public override bool CloneNewInstances => true;
        public bool isGunBody;
        public bool isGunBarrel;
        public bool isGunMag;
        public bool isGunScope;
        public bool isGunGrip;
        public bool isGunChamber;
        public int AttachmentTier;
        

    }
}