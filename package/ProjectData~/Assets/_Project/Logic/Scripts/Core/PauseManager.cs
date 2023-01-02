
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PauseManager
{

    private static List<string> playerPauseReasons;
    private static List<string> gamePauseReasons;

    public static bool IsGamePaused { get; private set; }

    public static bool IsPlayerPaused{ get; private set; }

    private static UnityEvent<bool> onGamePauseChanged = new UnityEvent<bool>();
    private static UnityEvent<bool> onPlayerPauseChanged = new UnityEvent<bool>();

    public static UnityEvent<bool> OnGamePauseChanged => onGamePauseChanged;
    public static UnityEvent<bool> OnPlayerPauseChanged => onPlayerPauseChanged;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void StaticReload()
    {
        onGamePauseChanged = new UnityEvent<bool>();
        onPlayerPauseChanged = new UnityEvent<bool>();
        playerPauseReasons = new List<string>();
        gamePauseReasons = new List<string>();
    }

    public static void AddGamePauseReason(string reason)
    {
        if (!gamePauseReasons.Contains(reason))
        {
            gamePauseReasons.Add(reason);
            if (gamePauseReasons.Count == 1) // One pause reason means the game has gone from unpaused to paused
            {
                IsGamePaused = true;
                onGamePauseChanged.Invoke(IsGamePaused);
            }
        }
    }

    public static void AddPlayerPauseReason(string reason)
    {
        if (!playerPauseReasons.Contains(reason))
        {
            playerPauseReasons.Add(reason);
            if (playerPauseReasons.Count == 1) // One pause reason means the game has gone from unpaused to paused
            {
                IsPlayerPaused = true;
                onPlayerPauseChanged.Invoke(IsPlayerPaused);
            }
        }
    }

    public static void RemoveGamePauseReason(string reason)
    {
        if (!gamePauseReasons.Contains(reason))
        {
            gamePauseReasons.Remove(reason);
            if (gamePauseReasons.Count == 0)
            {
                IsGamePaused = false;
                onGamePauseChanged.Invoke(IsGamePaused);
            }
        }
    }

    public static void RemovePlayerPauseReason(string reason)
    {
        if (playerPauseReasons.Contains(reason))
        {
            playerPauseReasons.Remove(reason);
            if (playerPauseReasons.Count == 0)
            {
                IsPlayerPaused = false;
                onPlayerPauseChanged.Invoke(IsPlayerPaused);
            }
        }
    }

}