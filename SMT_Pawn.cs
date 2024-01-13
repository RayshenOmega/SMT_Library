using Verse;

#nullable disable
namespace SMT_Library
{
  public class SMT_Pawn : Pawn
  {
    public virtual void Kill(DamageInfo? dinfo, Hediff exactCulprit = null)
    {
      base.Kill(dinfo, exactCulprit);
      ((ThingComp) ThingCompUtility.TryGetComp<CompDiedSpawn>((Thing) this)).Notify_KilledPawn((Pawn) this);
    }
  }
}
