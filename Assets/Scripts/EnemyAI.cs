using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chase
    }

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;

    [Header("Detection Settings")]
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float stoppingDistance = 1.5f;

    private EnemyState currentState = EnemyState.Idle;

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (agent != null)
        {
            agent.stoppingDistance = stoppingDistance;
        }
    }

    private void Update()
    {
        if (player == null || agent == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(
            transform.position,
            player.position
        );

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState(distanceToPlayer);
                break;

            case EnemyState.Chase:
                HandleChaseState(distanceToPlayer);
                break;
        }
    }

    private void HandleIdleState(float distanceToPlayer)
    {
        agent.isStopped = true;

        if (distanceToPlayer <= chaseRange)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    private void HandleChaseState(float distanceToPlayer)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (distanceToPlayer > chaseRange)
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        Debug.Log(
            gameObject.name + " changed state to: " + currentState
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}