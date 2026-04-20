using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private bool _isPlayerActive = false;
    private bool IsPlayerActive
    {
        get { return _isPlayerActive; }
        set
        {
            if (value == _isPlayerActive) return;

            _isPlayerActive = value;

            if (_isPlayerActive)
                OnPlayerEnabled();
            else
                OnPlayerDisabled();

            Debug.Log("Set player active: " + _isPlayerActive);
        }
    }

    [SerializeField] private PlayerMovement movement = new PlayerMovement();
    [SerializeField] private PlayerFloater floater = new PlayerFloater();
    [SerializeField] private PlayerLooking looker = new PlayerLooking();

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameEvents.ON_GAME_STATE_CHANGED += Handle_OnGameStateChanged;

        looker.OnEnable(this);
        floater.OnEnable(this);
        movement.OnEnable(this);
    }

    private void OnDisable()
    {
        GameEvents.ON_GAME_STATE_CHANGED -= Handle_OnGameStateChanged;

        looker.OnDisable();
        floater.OnDisable();
    }

    private void Handle_OnGameStateChanged(GameStateHolder.GameState _state)
    {
        IsPlayerActive = _state == GameStateHolder.GameState.SEARCHING_FOR_SIGNAL;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!IsPlayerActive)
            return;

        movement.OnUpdate();
    }

    private void FixedUpdate()
    {
        floater.OnFixedUpdate();

        if (!IsPlayerActive)
            return;

        movement.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if (!IsPlayerActive)
            return;

        looker.OnLateUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        floater.DrawGizmos();
    }

    private void OnPlayerEnabled()
    {

    }

    private void OnPlayerDisabled()
    {
        movement.FreezeMovement();
    }

    public PlayerFloater GetFloater()
    {
        return floater;
    }
}
