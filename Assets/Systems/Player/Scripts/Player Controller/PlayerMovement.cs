using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    private PlayerManager manager;

    [SerializeField] private Rigidbody body;

    [SerializeField] private Transform camera;

    private float moveSpeed { get { return GameStats.MOVE_SPEED; } }

    private Vector2 inputVector;

    private bool _isSprinting = false;
    private bool IsSprinting
    {
        get { return _isSprinting; }
        set
        {
            if (_isSprinting == value)
                return;

            _isSprinting = value;

            if (_isSprinting)
                GameEvents.ON_PLAYER_SPRINT_START?.Invoke();
            else
                GameEvents.ON_PLAYER_SPRINT_STOP?.Invoke();
        }
    }

    public void OnEnable(PlayerManager _manager)
    {
        manager = _manager;
    }

    public void OnUpdate()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            IsSprinting = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            IsSprinting = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void OnFixedUpdate()
    {
        Vector3 forwardDir = camera.forward.FlatY().normalized;
        Vector3 rightDir = camera.right.FlatY().normalized;

        Vector3 movement = Vector3.zero;
        movement += (forwardDir * inputVector.y) + (rightDir * inputVector.x);
        movement = Vector3.ClampMagnitude(movement, 1f);
        movement *= moveSpeed * (IsSprinting ? 1.25f : 1f);

        Debug.Log("Move speed: " + moveSpeed);

        Vector3 force = movement - body.linearVelocity;
        force.y = 0f;
        body.AddForce(force, ForceMode.VelocityChange);
    }

    public void FreezeMovement()
    {
        body.AddForce(-body.linearVelocity, ForceMode.VelocityChange);
        IsSprinting = false;
    }

    private void Jump()
    {
        if (!manager.GetFloater().touchingGround)
            return;

        GameEvents.ON_PLAYER_JUMP?.Invoke();

        body.AddForce(Vector3.up * GameStats.JUMP_FORCE, ForceMode.VelocityChange);
    }
}
