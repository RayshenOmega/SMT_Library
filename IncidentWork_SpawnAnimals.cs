using RimWorld;
using System;
using Verse;

#nullable disable
namespace SMT_Library
{
  internal class IncidentWork_SpawnAnimals : IncidentWorker
  {
    protected virtual bool CanFireNowSub(IncidentParms parms)
    {
      SpawnAnimalsIncidentDef def = (SpawnAnimalsIncidentDef) this.def;
      Map target = (Map) parms.target;
      float x = Find.WorldGrid.LongLatOf(target.Tile).x;
      return (!(((Def) def.pawn).defName == "SMT_Nahobino") || GenDate.Quadrum((long) Find.TickManager.TicksAbs, x) != 3) && (!(((Def) def.pawn).defName == "SMT_Demi-fiend") || GenDate.Quadrum((long) Find.TickManager.TicksAbs, x) != 0) && !target.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && target.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDefOf.Thrumbo) && this.TryFindEntryCell(target, out IntVec3 _);
    }

    protected virtual bool TryExecuteWorker(IncidentParms parms)
    {
      SpawnAnimalsIncidentDef def = (SpawnAnimalsIncidentDef) this.def;
      Map target = (Map) parms.target;
      IntVec3 cell;
      if (!this.TryFindEntryCell(target, out cell))
        return false;
      PawnKindDef thrumbo = PawnKindDefOf.Thrumbo;
      int num = Rand.RangeInclusive(90000, 150000);
      IntVec3 invalid = IntVec3.Invalid;
      if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, target, 10f, ref invalid))
        invalid = IntVec3.Invalid;
      Pawn pawn = (Pawn) null;
      for (int index = 0; index < def.count; ++index)
      {
        IntVec3 intVec3 = CellFinder.RandomClosewalkCellNear(cell, target, 10, (Predicate<IntVec3>) null);
        pawn = PawnGenerator.GeneratePawn(def.pawn, (Faction) null);
        GenSpawn.Spawn((Thing) pawn, intVec3, target, Rot4.Random, (WipeMode) 0, false);
        pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num;
        if (((IntVec3) ref invalid).IsValid)
          pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, target, 10, (Predicate<IntVec3>) null);
      }
      TaggedString taggedString = TranslatorFormattedStringExtensions.Translate(def.incidentLabel, NamedArgument.op_Implicit(((Def) thrumbo).label));
      this.SendStandardLetter(((TaggedString) ref taggedString).CapitalizeFirst(), TranslatorFormattedStringExtensions.Translate(def.incidentDec, NamedArgument.op_Implicit(((Def) thrumbo).label)), LetterDefOf.PositiveEvent, parms, LookTargets.op_Implicit((Thing) pawn), Array.Empty<NamedArgument>());
      return true;
    }

    private bool TryFindEntryCell(Map map, out IntVec3 cell)
    {
      return RCellFinder.TryFindRandomPawnEntryCell(ref cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, (Predicate<IntVec3>) null);
    }
  }
}
