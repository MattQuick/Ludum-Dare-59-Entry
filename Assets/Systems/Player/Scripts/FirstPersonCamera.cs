using System.Collections;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private float switchTime = .2f;

    private GameStateHolder.GameState activeState;
    [SerializeField] private TargetPose[] poses;

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
        if (_state == activeState)
            return;

        StartCoroutine(SwitchCamTargetPose(GetPose(_state)));
        activeState = _state;
    }

    private TargetPose GetPose(GameStateHolder.GameState _state)
    {
        foreach (var p in poses)
        {
            if (p._state == _state)
                return p;
        }

        return poses[0];
    }

    private IEnumerator SwitchCamTargetPose(TargetPose _pose)
    {
        float elapsed = 0f;

        Quaternion initRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(_pose.xRot, _pose.yRot, 0f);

        float initFOV = cam.fieldOfView;

        while (elapsed <= switchTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / switchTime);

            cam.fieldOfView = Mathf.Lerp(initFOV, _pose.FOV, t);

            transform.localRotation = Quaternion.Slerp(initRot, targetRot, t);
    
            yield return null;
        }

        cam.fieldOfView = _pose.FOV;
        transform.localRotation = targetRot;

        yield break;
    }

    [System.Serializable]
    private struct TargetPose
    {
        public GameStateHolder.GameState _state;
        public float xRot;
        public float yRot;
        public float FOV;
    }
}
