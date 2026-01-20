using System.Collections;
using UnityEngine;

public class Camera2D: MonoBehaviour
{
    float shakeMagnitude = 0.1f; 
    float shakeDuration = 0.5f;
    Transform camTransform;
    public Transform target;
    public float smoothSpeed = 0.125f;    
    void Start()
    {
        camTransform = Camera.main.transform;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void TriggerShake(float duration, float mag)
    {
        shakeMagnitude = mag;
        shakeDuration = duration;
        StartCoroutine(Shake());
    }
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
    IEnumerator Shake()
    {
        Vector3 originalPos = camTransform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            camTransform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        camTransform.localPosition = originalPos;
    }
}
