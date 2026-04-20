using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerFloater
{
    public bool isActive = false;
    public bool touchingGround = false;

    private PlayerManager manager;

    [SerializeField] private Rigidbody body;

    [SerializeField] private float rayOriginY = .5f;
    [SerializeField] private float rayLengthOffset = .1f;

    [SerializeField] private float strength;
    private Vector3 force;

    private RaycastHit hit;

    public void OnEnable(PlayerManager _manager)
    {
        manager = _manager;

        isActive = true;

        GameEvents.ON_PLAYER_JUMP += Handle_OnPlayerJump;
    }

    public void OnDisable()
    {
        GameEvents.ON_PLAYER_JUMP -= Handle_OnPlayerJump;
    }

    public void OnFixedUpdate()
    {
        if (!isActive)
            return;

        Vector3 rayPos = body.transform.TransformPoint(Vector3.up * rayOriginY);

        if (!Physics.Raycast(rayPos, Vector3.down, out hit, rayOriginY + rayLengthOffset))
        {
            touchingGround = false;
            return;
        }

        touchingGround = true;

        Vector3 force = Vector3.up * strength * (rayOriginY - hit.distance);
        force.y -= body.linearVelocity.y;

        body.AddForce(force, ForceMode.VelocityChange);
    }

    private void Handle_OnPlayerJump()
    {
        manager.StartCoroutine(DisableFloater((GameStats.JUMP_FORCE / Mathf.Abs(Physics.gravity.y)) * .5f));
    }

    private IEnumerator DisableFloater(float _time)
    {
        isActive = false;

        yield return new WaitForSeconds(_time);

        isActive = true;
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(body.transform.TransformPoint(Vector3.up * rayOriginY), body.transform.position);

        Gizmos.color = Color.darkRed;
        Gizmos.DrawRay(body.transform.position, Vector3.down * rayLengthOffset);
    }
}
