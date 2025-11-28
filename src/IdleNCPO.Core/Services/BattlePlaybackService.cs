using IdleNCPO.Abstractions.Interfaces;
using IdleNCPO.Core.DTOs;
using IdleNCPO.Core.Profiles;

namespace IdleNCPO.Core.Services;

/// <summary>
/// Service for playing back battles in real-time
/// Plays at 30 ticks per second (1 tick = 1/30 second)
/// </summary>
public class BattlePlaybackService : IBattlePlaybackService
{
  private readonly IBattleServiceFactory<BattleSeedDTO, BattleResultDTO> _battleFactory;
  private BattleService? _battle;
  private bool _isPlaying;
  private CancellationTokenSource? _cancellationTokenSource;

  /// <summary>
  /// Event fired when a tick is processed during playback
  /// </summary>
  public event Action<BattleService>? OnTickProcessed;

  /// <summary>
  /// Event fired when playback completes
  /// </summary>
  public event Action<BattleService>? OnPlaybackComplete;

  // Interface events
  event Action<IBattle>? IBattlePlaybackService.OnTickProcessed
  {
    add { _onTickProcessedInterface += value; }
    remove { _onTickProcessedInterface -= value; }
  }

  event Action<IBattle>? IBattlePlaybackService.OnPlaybackComplete
  {
    add { _onPlaybackCompleteInterface += value; }
    remove { _onPlaybackCompleteInterface -= value; }
  }

  private event Action<IBattle>? _onTickProcessedInterface;
  private event Action<IBattle>? _onPlaybackCompleteInterface;

  /// <summary>
  /// Current battle being played back
  /// </summary>
  public BattleService? Battle => _battle;

  /// <summary>
  /// Interface implementation for current battle
  /// </summary>
  IBattle? IBattlePlaybackService.Battle => _battle;

  /// <summary>
  /// Whether playback is currently running
  /// </summary>
  public bool IsPlaying => _isPlaying;

  /// <summary>
  /// Ticks per second for playback (default 30)
  /// </summary>
  public int TicksPerSecond { get; set; } = MapIdleProfile.TicksPerSecond;

  /// <summary>
  /// Delay between ticks in milliseconds
  /// </summary>
  public int TickDelayMs => 1000 / TicksPerSecond;

  public BattlePlaybackService(IBattleServiceFactory<BattleSeedDTO, BattleResultDTO> battleFactory)
  {
    _battleFactory = battleFactory;
  }

  /// <summary>
  /// Start playback of a battle result
  /// </summary>
  public async Task StartPlaybackAsync(BattleResultDTO result)
  {
    // Stop any existing playback
    await StopPlaybackAsync();

    // Create a new battle from the seed
    var seed = result.ToSeedDTO();
    _battle = (BattleService)_battleFactory.CreateBattle(seed);

    _isPlaying = true;
    _cancellationTokenSource = new CancellationTokenSource();

    try
    {
      var token = _cancellationTokenSource.Token;
      var targetTicks = result.TotalTicks;

      while (!_battle.IsFinished && _battle.CurrentTick < targetTicks && !token.IsCancellationRequested)
      {
        _battle.ProcessTick();
        OnTickProcessed?.Invoke(_battle);
        _onTickProcessedInterface?.Invoke(_battle);

        // Wait 1/30 second between ticks
        await Task.Delay(TickDelayMs, token);
      }

      _isPlaying = false;
      OnPlaybackComplete?.Invoke(_battle);
      _onPlaybackCompleteInterface?.Invoke(_battle);
    }
    catch (OperationCanceledException)
    {
      // Playback was cancelled
      _isPlaying = false;
    }
  }

  /// <summary>
  /// Stop the current playback
  /// </summary>
  public Task StopPlaybackAsync()
  {
    if (_cancellationTokenSource != null)
    {
      _cancellationTokenSource.Cancel();
      _cancellationTokenSource.Dispose();
      _cancellationTokenSource = null;
    }

    _isPlaying = false;
    return Task.CompletedTask;
  }

  /// <summary>
  /// Pause/resume playback
  /// </summary>
  public void TogglePause()
  {
    // This would require more complex state management
    // For now, stopping and restarting is the simplest approach
  }
}
