using UnityEngine;
using UnityEngine.UI;

public class SignalManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform signalTarget;

    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private Bounds bounds;

    [Header("UI")]
    [SerializeField] private Image signalImage;

    private float tickRate = 0.1f;
    private float lastTickTime = 0f;

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
        if (_state != GameStateHolder.GameState.SEARCHING_FOR_SIGNAL)
            return;

        RandomlyPlaceSignal();
    }

    private void Update()
    {
        if (Time.time - lastTickTime < tickRate)
            return;

        CheckTargetDistance();

        lastTickTime = Time.time;
    }

    private void CheckTargetDistance()
    {
        float distance = (signalTarget.position.FlatY() - player.position.FlatY()).magnitude;
        distance = Mathf.Clamp(distance, 0f, maxDistance);

        signalImage.fillAmount = Mathf.Clamp01(1f - (distance / maxDistance));

        if (signalImage.fillAmount * 100f < GameStats.REQUIRED_SIGNAL_ACCURACY)
            return;

        GameEvents.ON_SIGNAL_FOUND?.Invoke();
    }

    private void RandomlyPlaceSignal()
    {
        float x = Random.Range(-bounds.extents.x, bounds.extents.x);
        float z = Random.Range(-bounds.extents.z, bounds.extents.z);

        Vector3 desiredPos = new Vector3(x, 0f, z);

        Vector3 diff = desiredPos - player.position.FlatY();
        float maxWalkableDistance = GameStats.ROUND_TIME * GameStats.MOVE_SPEED;

        if (diff.magnitude >= maxDistance)
        {
            desiredPos = player.position.FlatY() + (diff.normalized * (maxDistance * .975f));
            desiredPos = bounds.ClosestPoint(desiredPos);
        }

        signalTarget.position = desiredPos.FlatY();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawCube(bounds.center, bounds.size);
    }
}
