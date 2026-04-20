using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameStats
{
    private static float MOVE_SPEED_BASE = 5f;
    private static float _default_MOVE_SPEED_MULTIPLIER = 100f;
    public static float MOVE_SPEED_MULTIPLIER = 100f;

    public static float MOVE_SPEED
    {
        get
        {
            return MOVE_SPEED_BASE * (MOVE_SPEED_MULTIPLIER / 100f);
        }
    }


    private static float REQUIRED_SIGNAL_ACCURACY_BASE = 95f;
    private static float _default_SIGNAL_ACCURACY_MULTIPLIER = 100f;
    public static float REQUIRED_SIGNAL_ACCURACY_MULTIPLIER = 100f;

    public static float REQUIRED_SIGNAL_ACCURACY
    {
        get
        {
            return REQUIRED_SIGNAL_ACCURACY_BASE * (REQUIRED_SIGNAL_ACCURACY_MULTIPLIER / 100f);
        }
    }



    private static float ROUND_TIME_BASE = 60f;
    private static float _default_ROUND_TIME_MULTIPLIER = 100f;
    public static float ROUND_TIME_MULTIPLIER;
    public static float ROUND_TIME
    {
        get
        {
            return ROUND_TIME_BASE * (ROUND_TIME_MULTIPLIER / 100f);
        }
    }

    private static float JUMP_FORCE_BASE = 5f;
    private static float _default_JUMP_FORCE_MULTIPLIER = 100f;
    public static float JUMP_FORCE_MULTIPLIER = 100f;
    public static float JUMP_FORCE
    {
        get
        {
            return JUMP_FORCE_BASE * (JUMP_FORCE_MULTIPLIER / 100f);
        }
    }

    public static void ResetStats()
    {
        MOVE_SPEED_MULTIPLIER = _default_MOVE_SPEED_MULTIPLIER;
        ROUND_TIME_MULTIPLIER = _default_ROUND_TIME_MULTIPLIER;
        REQUIRED_SIGNAL_ACCURACY_MULTIPLIER = _default_SIGNAL_ACCURACY_MULTIPLIER;
        JUMP_FORCE_MULTIPLIER = _default_JUMP_FORCE_MULTIPLIER;
    }

    public enum STAT_NAME
    {
        MOVE_SPEED,
        ROUND_TIME,
        REQUIRED_SIGNAL_ACCURACY,
        JUMP_FORCE
    }

    public static void UpgradeStat(STAT_NAME _stat, float _add)
    {
        switch (_stat)
        {
            case STAT_NAME.MOVE_SPEED:
                MOVE_SPEED_MULTIPLIER += _add;
                break;
            case STAT_NAME.ROUND_TIME:
                ROUND_TIME_MULTIPLIER += _add;
                break;
            case STAT_NAME.REQUIRED_SIGNAL_ACCURACY:
                REQUIRED_SIGNAL_ACCURACY_MULTIPLIER += _add;
                break;
            case STAT_NAME.JUMP_FORCE:
                JUMP_FORCE_MULTIPLIER += _add;
                break;
        }
    }

    public static float GetStat(STAT_NAME _stat)
    {
        switch (_stat)
        {
            case STAT_NAME.MOVE_SPEED:
                return MOVE_SPEED_MULTIPLIER;
            case STAT_NAME.ROUND_TIME:
                return ROUND_TIME_MULTIPLIER;
            case STAT_NAME.REQUIRED_SIGNAL_ACCURACY:
                return REQUIRED_SIGNAL_ACCURACY_MULTIPLIER;
            case STAT_NAME.JUMP_FORCE:
                return JUMP_FORCE_MULTIPLIER;
        }

        return 0f;
    }
}
