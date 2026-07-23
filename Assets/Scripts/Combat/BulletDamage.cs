using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField, Min(1)] private int damage = 20;

    [Header("Impact Effect")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField, Min(0f)] private float impactEffectLifetime = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        CreateImpactEffect(collision);
        ApplyDamage(collision);

        Destroy(gameObject);
    }

    private void ApplyDamage(Collision collision)
    {
        IDamageable damageableTarget =
            collision.collider.GetComponentInParent<IDamageable>();

        damageableTarget?.TakeDamage(damage);
    }

    private void CreateImpactEffect(Collision collision)
    {
        if (impactEffect == null || collision.contactCount == 0)
        {
            return;
        }

        ContactPoint contact = collision.GetContact(0);

        GameObject effect = Instantiate(
            impactEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        Destroy(effect, impactEffectLifetime);
    }
}