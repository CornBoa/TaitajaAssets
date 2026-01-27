using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.7f;
    private float shakeTimeRemaining;

    private Vector3 shakeOffset;
    private Transform cameraTransform;
    private Vector3 originalPositionOffset;

    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
        originalPositionOffset = cameraTransform.localPosition;
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeOffset = Random.insideUnitSphere * shakeIntensity;
            shakeOffset.z = 0;
            cameraTransform.localPosition += shakeOffset;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            cameraTransform.localPosition = originalPositionOffset;
        }
    }
    [ContextMenu("Shake")]
    void shaky()
    {
        StartShake(5, 1);
    }
    public void StartShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeTimeRemaining = shakeDuration;
    }
}
