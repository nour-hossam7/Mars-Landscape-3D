using UnityEngine;

public class SpiderWebAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform webShootPoint;
    [SerializeField] private SpiderWebProjectile webProjectilePrefab;

    [Header("Audio - Optional")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip webShootSound;

    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    public void ShootWeb()
    {
        if (player == null ||
            webShootPoint == null ||
            webProjectilePrefab == null)
        {
            Debug.LogWarning(
                "SpiderWebAttack references are incomplete.",
                gameObject
            );

            return;
        }

        Vector3 targetPoint =
            player.position + Vector3.up;

        Vector3 direction =
            (targetPoint - webShootPoint.position).normalized;

        SpiderWebProjectile projectile = Instantiate(
            webProjectilePrefab,
            webShootPoint.position,
            Quaternion.LookRotation(direction)
        );

        projectile.Initialize(player);

        if (audioSource != null && webShootSound != null)
        {
            audioSource.PlayOneShot(webShootSound);
        }
    }
}