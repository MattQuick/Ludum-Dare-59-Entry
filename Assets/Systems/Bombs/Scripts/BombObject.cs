using UnityEngine;

public class BombObject : MonoBehaviour
{
    

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private AudioSource sizzleSound;
    [SerializeField] private AudioSource explodeSound;
    [SerializeField] private GameObject sizzleParticles;
    [SerializeField] private ParticleSystem explosionParticles;

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void OnEnable()
    {
        DeActivate();
    }

    private void Explode()
    {
        GameEvents.ON_HIT_BOMB?.Invoke();

        explosionParticles.Play();
        explodeSound.Play();

        DeActivate();
    }

    public void EnableBomb()
    {
        explodeSound.Stop();
        sizzleSound.Play();

        meshRenderer.enabled = true;

        sizzleParticles.SetActive(true);
    }

    public void DeActivate()
    {

        sizzleSound.Stop();

        meshRenderer.enabled = false;
        sizzleParticles.SetActive(false);
    }
}
