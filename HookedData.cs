
using DRGN.Projectiles.Reaper;
using System.Collections.Generic;

public class HookedData
{
	public List<int> crit = new List<int>();
	public int stack = 0;
	public List<ReaperChain> ownerProjs = new List<ReaperChain>();
	public int npc;
	public HookedData(ReaperChain Rc, int Npc , bool Crit = false)
	{
        if (Crit) { crit.Add(Rc.projectile.whoAmI); }
		stack = 1;
		ownerProjs.Add(Rc);
		npc = Npc;
	}
	public void AddProjAndValues(ReaperChain Rc ,bool Crit)
    {
		stack += 1;
		if (Crit) { crit.Add(Rc.projectile.whoAmI); }
		ownerProjs.Add(Rc);
	}
	public void RemoveThisProj(ReaperChain Rc)
	{
		if (ownerProjs.Remove(Rc))
		{
			stack -= 1;
			crit.Remove(Rc.projectile.whoAmI);
		}

	}
}
