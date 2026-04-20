using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [SerializeField] private GameStateHolder stateHolder = new GameStateHolder();

    private GameStateHolder.GameState activeStateType = GameStateHolder.GameState.NULL;
    private GameState_Base activeState;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        SetActiveState(GameStateHolder.GameState.MAIN_MENU);
    }

    private void Update()
    {
        if (activeState == null) return;

        activeState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        if (activeState == null) return;

        activeState.OnStateFixedUpdate();
    }

    private void LateUpdate()
    {
        if (activeState == null) return;

        activeState.OnStateLateUpdate();
    }

    public void SetActiveState(GameStateHolder.GameState _state)
    {
        if (_state == activeStateType)
            return;

        if (activeState != null)
            activeState.OnStateExit();

        activeState = stateHolder.stateDictionary[_state];
        activeStateType = _state;

        activeState.OnStateEnter();

        GameEvents.ON_GAME_STATE_CHANGED?.Invoke(_state);
        Debug.Log("Game state set to " + _state.ToString());
    }

    public void SetActiveState(int _stateIndex)
    {
        SetActiveState((GameStateHolder.GameState)_stateIndex);
    }
}
