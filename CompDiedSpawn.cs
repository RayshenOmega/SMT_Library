using Verse;

#nullable disable
namespace SMT_Library
{
  public class CompDiedSpawn : ThingComp
  {
    public CompPropertiesDiedSpawn Props => (CompPropertiesDiedSpawn) this.props;

    public virtual void Notify_KilledPawn(Pawn pawn)
    {
      base.Notify_KilledPawn(pawn);
      Corpse corpse = pawn.Corpse;
      if (corpse == null || !((Thing) corpse).Spawned || ((Thing) corpse).Destroyed)
        return;
      if (((Def) pawn.kindDef).defName != "SMT_Demi-fiend")
      {
        Thing thing = GenSpawn.Spawn(this.Props.thing, ((Thing) corpse).Position, ((Thing) corpse).Map, (WipeMode) 0);
        thing.stackCount = this.Props.count > thing.def.stackLimit ? thing.def.stackLimit : this.Props.count;
      }
      else
      {
        for (int index = 0; index < 133; ++index)
          GenSpawn.Spawn(this.Props.thing, ((Thing) corpse).Position, ((Thing) corpse).Map, (WipeMode) 0).stackCount = 500;
        GenSpawn.Spawn(this.Props.thing, ((Thing) corpse).Position, ((Thing) corpse).Map, (WipeMode) 0).stackCount = 332;
      }
      ((Thing) corpse).Destroy((DestroyMode) 0);
    }
  }
}
