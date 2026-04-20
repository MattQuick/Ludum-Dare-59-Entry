using System;
using UnityEngine;

public static class GameEvents
{
    public static Action ON_SIGNAL_FOUND;

    public static Action<GameStateHolder.GameState> ON_GAME_STATE_CHANGED;

    public static Action ON_TIMER_HIT_0;

    public static Action<GameStats.STAT_NAME, float> CALL_UPGRADE_STAT;

    public static Action ON_PLAYER_SPRINT_START;

    public static Action ON_PLAYER_SPRINT_STOP;

    public static Action ON_PLAYER_JUMP;

    public static Action ON_HIT_BOMB;
}
