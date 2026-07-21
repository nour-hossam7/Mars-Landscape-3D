using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int damage = 20;

    [Header("Effects")]
    [SerializeField] private GameObject impactEffect;

    private void OnCollisionEnter(Collision collision)
    {
      
        if (impactEffect != null)
        {
            ContactPoint contact = collision.contacts[0];

            GameObject effect = Instantiate(
                impactEffect,
                contact.point,
                Quaternion.LookRotation(contact.normal)
            );

            Destroy(effect, 2f);
        }

      
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

      
        Destroy(gameObject);
    }
}