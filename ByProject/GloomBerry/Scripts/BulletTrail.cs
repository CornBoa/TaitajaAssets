using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    TrailRenderer trail;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float stopDistance = 0.1f;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        if (trail == null)
        {
            Debug.LogWarning("BulletTrail: no TrailRenderer found on the GameObject. Destroying in 2s.");
            Destroy(gameObject, 2f);
        }
    }

    public void Setup(Transform Target)
    {
        if (trail == null)
        {
            trail = GetComponent<TrailRenderer>();
            if (trail == null)
            {
                Destroy(gameObject, 2f);
                return;
            }
        }

        StartCoroutine(MoveTowardsTarget(Target));
    }

    IEnumerator MoveTowardsTarget(Transform target)
    {
        if (target == null)
        {
            HandleImpact();
            yield break;
        }

        while (true)
        {
            if (target == null)
            {
                HandleImpact();
                yield break;
            }

            Vector3 currentPos = transform.position;
            Vector3 targetPos = target.position;
            Vector3 dir = (targetPos - currentPos);
            float distanceToTarget = dir.magnitude;

            if (distanceToTarget <= stopDistance)
            {
                transform.position = targetPos;
                HandleImpact();
                yield break;
            }

            dir = dir.normalized;
            float step = speed * Time.deltaTime;
            if (Physics.Raycast(currentPos, dir, out RaycastHit hit, step))
            {
                transform.position = hit.point;
                HandleImpact();
                yield break;
            }
            transform.position = Vector3.MoveTowards(currentPos, targetPos, step);

            yield return null;
        }
    }

    private void HandleImpact()
    {
        if (trail != null)
        {
            trail.emitting = false;
            Destroy(gameObject, Mathf.Max(0f, trail.time));
        }
        else
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
