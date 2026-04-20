using UnityEngine;

public class PhoneRotator : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lerpSpeed = 7.5f;

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, cameraTransform.localRotation, lerpSpeed * Time.deltaTime);
    }
}
