using UnityEngine;

public class SpiderWebProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Min(0.1f)] private float speed = 15f;
    [SerializeField, Min(0.1f)] private float lifeTime = 5f;

    [Header("Web Effect")]
    [SerializeField, Min(0.1f)] private float freezeDuration = 2f;
    [SerializeField, Min(1)] private int hitDamage = 1;

    private Transform target;
    private bool hasHit;

    public void Initialize(Transform newTarget)
    {
        target = newTarget;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (target == null || hasHit)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            return;
        }

        Vector3 targetPoint = target.position + Vector3.up;

        Vector3 direction =
            (targetPoint - transform.position).normalized;

        transform.forward = direction;

        transform.position +=
            direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
        {
            return;
        }

        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        hasHit = true;

        PlayerWebStatus webStatus =
            other.GetComponentInParent<PlayerWebStatus>();

        playerHealth.TakeHit(hitDamage);

        if (webStatus != null && !playerHealth.IsDead)
        {
            webStatus.ApplyWeb(freezeDuration);
        }

        Destroy(gameObject);
    }
}