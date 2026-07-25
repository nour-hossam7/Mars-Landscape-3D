using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float lifeTime = 5f;

    private bool hasHit;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageTarget(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageTarget(other);
    }

    private void DamageTarget(Collider targetCollider)
    {
        if (hasHit)
            return;

        hasHit = true;

        Debug.Log("Bullet hit: " + targetCollider.name);

        BossHealth bossHealth =
            targetCollider.GetComponentInParent<BossHealth>();

        if (bossHealth != null)
        {
            Debug.Log("BossHealth found!");
            bossHealth.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("BossHealth was not found on hit object.");
        }

        Destroy(gameObject);
    }
}