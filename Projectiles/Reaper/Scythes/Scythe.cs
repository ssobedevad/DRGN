using Terraria;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class Scythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 8f;
            OutTime = 50;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class ScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 6f ;
            OutTime = 45;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
