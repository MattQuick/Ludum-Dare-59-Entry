using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private bool isTimerActive = false;

    private float timer;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private float decreaseEachRound = 2.5f;

    private void OnEnable()
    {
        GameEvents.ON_GAME_STATE_CHANGED += Handle_OnGameStateChanged;
        GameEvents.CALL_UPGRADE_STAT += Handle_CallUpgradeStat;
    }

    private void OnDisable()
    {
        GameEvents.ON_GAME_STATE_CHANGED -= Handle_OnGameStateChanged;
        GameEvents.CALL_UPGRADE_STAT -= Handle_CallUpgradeStat;
    }

    private void Handle_OnGameStateChanged(GameStateHolder.GameState _state)
    {
        if (_state == GameStateHolder.GameState.SEARCHING_FOR_SIGNAL)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    private void Handle_CallUpgradeStat(GameStats.STAT_NAME _stat, float _add)
    {
        if (_stat == GameStats.STAT_NAME.ROUND_TIME)
            return;

        if (GameStats.ROUND_TIME <= 10f)
            return;

        GameEvents.CALL_UPGRADE_STAT?.Invoke(GameStats.STAT_NAME.ROUND_TIME, -decreaseEachRound);
    }

    private void StartTimer()
    {
        isTimerActive = true;
        timer = GameStats.ROUND_TIME;
    }

    private void StopTimer()
    {
        isTimerActive = false;
    }

    private void GameOver()
    {
        GameEvents.ON_TIMER_HIT_0?.Invoke();
    }

    private void Update()
    {
        if (!isTimerActive)
            return;

        timer -= Time.deltaTime;
        if (timer < 0f)
            timer = 0f;


        timerText.SetText(timer.ToString("n1"));
    
        if (timer <= 0f)
        {
            GameOver();
        }
    }
}
