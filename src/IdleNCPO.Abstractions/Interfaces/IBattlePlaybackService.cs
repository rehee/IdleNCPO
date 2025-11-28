namespace IdleNCPO.Abstractions.Interfaces;

/// <summary>
/// Interface for battle playback service
/// </summary>
public interface IBattlePlaybackService
{
  /// <summary>
  /// Current battle being played back
  /// </summary>
  IBattle? Battle { get; }

  /// <summary>
  /// Whether playback is currently running
  /// </summary>
  bool IsPlaying { get; }

  /// <summary>
  /// Ticks per second for playback
  /// </summary>
  int TicksPerSecond { get; set; }

  /// <summary>
  /// Delay between ticks in milliseconds
  /// </summary>
  int TickDelayMs { get; }

  /// <summary>
  /// Event fired when a tick is processed during playback
  /// </summary>
  event Action<IBattle>? OnTickProcessed;

  /// <summary>
  /// Event fired when playback completes
  /// </summary>
  event Action<IBattle>? OnPlaybackComplete;

  /// <summary>
  /// Stop the current playback
  /// </summary>
  Task StopPlaybackAsync();
}
