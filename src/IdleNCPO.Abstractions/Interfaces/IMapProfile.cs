using IdleNCPO.Abstractions.Enums;

namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for map profile
/// </summary>
public interface IMapProfile : IIdleProfile<EnumMap>
{
  int Width { get; }
  int Height { get; }
  int MinLevel { get; }
  int MaxLevel { get; }
  int MaxBattleDuration { get; }
  int MaxTicks { get; }
}
