using System;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Min(1f)] private float maxHealth = 100f;

    [Header("Death")]
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private MonoBehaviour[] behavioursToDisable;
    [SerializeField, Min(0f)] private float destroyDelay = 5f;

    public event Action<float, float> HealthChanged;
    public event Action Died;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;
    public bool IsDead { get; private set; }

    private static readonly int DieParameter =
        Animator.StringToHash("Die");

    private void Awake()
    {
        CurrentHealth = maxHealth;

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        Debug.Log(
            $"{gameObject.name} health initialized: {CurrentHealth}/{maxHealth}",
            gameObject
        );
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(
            $"{gameObject.name} received damage: {damage}",
            gameObject
        );

        if (IsDead)
        {
            Debug.LogWarning(
                $"{gameObject.name} is already dead.",
                gameObject
            );

            return;
        }

        if (damage <= 0f)
        {
            Debug.LogWarning(
                $"Invalid damage value: {damage}",
                gameObject
            );

            return;
        }

        CurrentHealth = Mathf.Max(0f, CurrentHealth - damage);

        Debug.Log(
            $"{gameObject.name} current health: {CurrentHealth}/{maxHealth}",
            gameObject
        );

        HealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        CurrentHealth = 0f;

        Debug.Log($"{gameObject.name} died!", gameObject);

        StopNavMeshAgent();
        DisableBossBehaviours();
        DisableBossColliders();
        PlayDeathAnimation();

        Died?.Invoke();

        if (destroyDelay > 0f)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void StopNavMeshAgent()
    {
        if (agent == null)
        {
            return;
        }

        if (agent.enabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }

        agent.enabled = false;
    }

    private void DisableBossBehaviours()
    {
        if (behavioursToDisable == null)
        {
            return;
        }

        foreach (MonoBehaviour behaviour in behavioursToDisable)
        {
            if (behaviour != null)
            {
                behaviour.enabled = false;
            }
        }
    }

    private void DisableBossColliders()
    {
        Collider[] bossColliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider bossCollider in bossColliders)
        {
            if (bossCollider != null)
            {
                bossCollider.enabled = false;
            }
        }
    }

    private void PlayDeathAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning(
                $"Animator is not assigned on {gameObject.name}.",
                gameObject
            );

            return;
        }

        animator.ResetTrigger("Attack");
        animator.SetFloat("Speed", 0f);
        animator.SetTrigger(DieParameter);
    }
}