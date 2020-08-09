
using DRGN.Projectiles.Reaper;
using System.Collections.Generic;

public class HookedData
{
	public int crit = 0;
	public int stack = 0;
	public List<ReaperChain> ownerProjs = new List<ReaperChain>();
	public int npc;
	public HookedData(ReaperChain Rc, int Npc , bool Crit = false)
	{
		crit = Crit? 1 : 0;
		stack = 1;
		ownerProjs.Add(Rc);
		npc = Npc;
	}
	public void AddProjAndValues(ReaperChain Rc ,bool Crit)
    {
		stack += 1;
		crit += Crit ? 1 : 0;
		ownerProjs.Add(Rc);
	}
}
