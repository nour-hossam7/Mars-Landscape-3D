using UnityEngine;

public class EnergyCollectable : MonoBehaviour
{
    [Header("Reward")]
    [SerializeField] private int energyValue = 1;
    [SerializeField] private int scoreValue = 10;

    [Header("Audio")]
    [SerializeField] private AudioClip collectSound;
    [SerializeField, Range(0f, 1f)] private float collectVolume = 1f;

    [Header("Visual Movement")]
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float floatingHeight = 0.15f;
    [SerializeField] private float floatingSpeed = 2f;

    private Vector3 startPosition;
    private bool collected;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        RotateCollectable();
        FloatCollectable();
    }

    private void RotateCollectable()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime,
            Space.World
        );
    }

    private void FloatCollectable()
    {
        float yOffset =
            Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;

        transform.position =
            startPosition + Vector3.up * yOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEnergy(
                energyValue,
                scoreValue
            );
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(
                collectSound,
                transform.position,
                collectVolume
            );
        }

        Destroy(gameObject);
    }
}