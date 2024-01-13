using RimWorld;
using Verse;

#nullable disable
namespace SMT_Library
{
  public class CompFood : ThingComp
  {
    public virtual void CompTick()
    {
      base.CompTick();
      Need food = (Need) ((Pawn) this.parent).needs.food;
      food.CurLevel = food.MaxLevel * 0.1f;
    }
  }
}
