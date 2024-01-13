using Verse;

#nullable disable
namespace SMT_Library
{
  public class CompPropertiesDiedSpawn : CompProperties
  {
    public ThingDef thing;
    public int count;

    public CompPropertiesDiedSpawn() => this.compClass = typeof (CompDiedSpawn);
  }
}
