using Terraria;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class SnakeHunter : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 9.25f;
            OutTime = 60;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class SnakeHunterThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 7.25f;
            OutTime = 55;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
