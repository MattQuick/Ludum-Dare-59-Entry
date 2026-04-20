using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerLooking 
{
    private PlayerManager manager;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Camera camera;

    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private float walkFOV = 70f, sprintFOV = 85f;

    private float rotX, rotY;

    public void OnEnable(PlayerManager _manager)
    {
        manager = _manager;

        GameEvents.ON_PLAYER_SPRINT_START += Handle_OnPlayerSprintStart;
        GameEvents.ON_PLAYER_SPRINT_STOP += Handle_OnPlayerSprintStop;
    }

    public void OnDisable()
    {
        GameEvents.ON_PLAYER_SPRINT_START -= Handle_OnPlayerSprintStart;
        GameEvents.ON_PLAYER_SPRINT_STOP -= Handle_OnPlayerSprintStop;
    }

    public void OnLateUpdate()
    {
        float inputX = Input.GetAxisRaw("Mouse X");
        float inputY = Input.GetAxisRaw("Mouse Y");

        rotX += inputX * lookSpeed * Time.deltaTime;
        rotY -= inputY * lookSpeed * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -75f, 75f);

        cameraTransform.localRotation = Quaternion.Euler(rotY, rotX, 0f);
    }

    private void Handle_OnPlayerSprintStart()
    {
        SetFOV(sprintFOV);
    }

    private void Handle_OnPlayerSprintStop()
    {
        SetFOV(walkFOV);
    }

    private void SetFOV(float _fov)
    {
        manager.StartCoroutine(SetFOVCoroutine(_fov));
    }

    private IEnumerator SetFOVCoroutine(float _fieldOfView)
    {
        float elapsed = 0f;
        float max = .1f;

        float init = camera.fieldOfView;

        while (elapsed <= max)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / max).EasingCurve_InOutSine();

            camera.fieldOfView = Mathf.Lerp(init, _fieldOfView, t);
        yield return null;
        }

        camera.fieldOfView = _fieldOfView;
        yield break;
    }
}
