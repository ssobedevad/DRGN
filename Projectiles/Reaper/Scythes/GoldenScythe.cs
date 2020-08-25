using Terraria;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class GoldenScythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 8.5f;
            OutTime = 55;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class GoldenScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 6.5f;
            OutTime = 50;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
