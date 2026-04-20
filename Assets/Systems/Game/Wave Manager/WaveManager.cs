using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave = 0;

    [SerializeField] private TMP_Text[] texts;

    private void OnEnable()
    {
        GameEvents.ON_GAME_STATE_CHANGED += Handle_OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameEvents.ON_GAME_STATE_CHANGED -= Handle_OnGameStateChanged;
    }

    private void Handle_OnGameStateChanged(GameStateHolder.GameState _state)
    {
        if (_state == GameStateHolder.GameState.MAIN_MENU)
            currentWave = 0;

        if (_state == GameStateHolder.GameState.SEARCHING_FOR_SIGNAL)
            currentWave++;

        SetTexts();
    }

    private void SetTexts()
    {
        foreach (var text in texts)
        {
            text.text = $"Wave: {currentWave}";
        }
    }
}
