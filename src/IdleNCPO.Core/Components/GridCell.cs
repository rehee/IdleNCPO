using IdleNCPO.Abstractions.Components;
using IdleNCPO.Abstractions.Interfaces;

namespace IdleNCPO.Core.Components;

/// <summary>
/// Represents a grid cell in the 2D map
/// </summary>
public class GridCell
{
  public int X { get; }
  public int Y { get; }
  public List<IMapActor> Actors { get; } = new();

  public GridCell(int x, int y)
  {
    X = x;
    Y = y;
  }

  public void AddActor(IMapActor actor)
  {
    if (!Actors.Contains(actor))
    {
      Actors.Add(actor);
    }
  }

  public void RemoveActor(IMapActor actor)
  {
    Actors.Remove(actor);
  }

  public void Clear()
  {
    Actors.Clear();
  }
}
