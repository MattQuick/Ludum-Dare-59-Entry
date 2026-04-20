using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private Rigidbody body;

    [SerializeField] private AudioSource footStepSource;

    private float footStepPitch = 1f;
    [SerializeField] private float footStepPitchRange = .1f;
    private Vector3 lastFootStepPos;
    [SerializeField] private float minFootStepRange = .1f;

    private void OnEnable()
    {
        GameEvents.ON_PLAYER_SPRINT_START += RaiseFootstepPitch;
        GameEvents.ON_PLAYER_SPRINT_STOP += LowerFootstepPitch;
    }

    private void OnDisable()
    {
        GameEvents.ON_PLAYER_SPRINT_START -= RaiseFootstepPitch;
        GameEvents.ON_PLAYER_SPRINT_STOP -= LowerFootstepPitch;
    }

    private void Update()
    {
        if (!PlayerManager.instance.GetFloater().touchingGround)
            return;

        if (Vector3.Distance(body.position.FlatY(), lastFootStepPos) >= minFootStepRange)
        {
            //footStepSource.volume = Mathf.Clamp01(body.linearVelocity.magnitude);
            footStepSource.pitch = footStepPitch + Random.Range(-footStepPitchRange, footStepPitchRange);
            footStepSource.Play();

            lastFootStepPos = body.position.FlatY();
        }
    }

    private void RaiseFootstepPitch()
    {
       // footStepPitch += .5f;
    }

    private void LowerFootstepPitch()
    {
        //footStepPitch -= .5f;
    }
}
