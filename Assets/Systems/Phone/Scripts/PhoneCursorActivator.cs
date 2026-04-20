using UnityEngine;

public class PhoneCursorActivator : MonoBehaviour
{
    [SerializeField] private GameStateHolder.GameState[] validStates;

    [SerializeField] private GameObject cursorObject;

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
        cursorObject.SetActive(IsStateValid(_state));
    }

    private bool IsStateValid(GameStateHolder.GameState _state)
    {
        for (int i = 0; i < validStates.Length; i++)
        {
            if (validStates[i] == _state)
                return true;
        }

        return false;
    }
}
