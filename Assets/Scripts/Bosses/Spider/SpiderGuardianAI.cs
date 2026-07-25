using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpiderGuardianAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private SpiderWebAttack webAttack;

    [Header("Ranges")]
    [SerializeField, Min(0f)] private float detectionRange = 20f;
    [SerializeField, Min(0f)] private float attackRange = 8f;

    [Header("Attack")]
    [SerializeField, Min(0.1f)] private float attackCooldown = 2.5f;

    [Tooltip("الوقت بين بداية الأنيميشن وخروج الشبكة")]
    [SerializeField, Min(0f)] private float projectileDelay = 0.45f;

    [Tooltip("مدة توقف البوس أثناء تنفيذ الهجوم")]
    [SerializeField, Min(0.1f)] private float attackDuration = 1.2f;

    [Header("Rotation")]
    [SerializeField, Min(0f)] private float rotationSpeed = 8f;

    private float nextAttackTime;
    private bool isAttacking;
    private Coroutine attackCoroutine;

    private static readonly int SpeedParameter =
        Animator.StringToHash("Speed");

    private static readonly int AttackParameter =
        Animator.StringToHash("Attack");

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (webAttack == null)
        {
            webAttack = GetComponentInChildren<SpiderWebAttack>();
        }

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

    private void Update()
    {
        if (player == null || agent == null || !agent.enabled)
        {
            return;
        }

        float distanceToPlayer =
            Vector3.Distance(transform.position, player.position);

        if (isAttacking)
        {
            FacePlayer();
            SetAnimationSpeed(0f);
            return;
        }

        if (distanceToPlayer > detectionRange)
        {
            StopMoving();
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            StopMoving();
            FacePlayer();

            if (Time.time >= nextAttackTime)
            {
                StartWebAttack();
            }

            return;
        }

        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        SetAnimationSpeed(agent.velocity.magnitude);
    }

    private void StopMoving()
    {
        if (agent.enabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }

        SetAnimationSpeed(0f);
    }

    private void StartWebAttack()
    {
        if (isAttacking)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(WebAttackRoutine());
    }

    private IEnumerator WebAttackRoutine()
    {
        isAttacking = true;

        StopMoving();
        FacePlayer();

        if (animator != null)
        {
            animator.ResetTrigger(AttackParameter);
            animator.SetTrigger(AttackParameter);
        }

        // ننتظر حتى تصل حركة الأنيميشن للحظة إطلاق الشبكة.
        yield return new WaitForSeconds(projectileDelay);

        FacePlayer();

        if (webAttack != null)
        {
            webAttack.ShootWeb();
        }
        else
        {
            Debug.LogWarning(
                "SpiderWebAttack is not assigned.",
                gameObject
            );
        }

        float remainingAttackTime =
            Mathf.Max(0f, attackDuration - projectileDelay);

        if (remainingAttackTime > 0f)
        {
            yield return new WaitForSeconds(remainingAttackTime);
        }

        isAttacking = false;
        attackCoroutine = null;
    }

    private void FacePlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void SetAnimationSpeed(float movementSpeed)
    {
        if (animator != null)
        {
            animator.SetFloat(SpeedParameter, movementSpeed);
        }
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        isAttacking = false;

        if (agent != null &&
            agent.enabled &&
            agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }
}