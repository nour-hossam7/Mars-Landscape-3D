using UnityEngine;

public abstract class BossBase : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField, Min(0f)] protected float detectionRange = 20f;
    [SerializeField, Min(0f)] protected float attackRange = 4f;

    protected BossState currentState = BossState.Idle;
    protected Transform player;

    public BossState CurrentState => currentState;

    protected virtual void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError(
                $"{name}: No GameObject with the Player tag was found.",
                this
            );
        }
    }

    protected virtual void Update()
    {
        UpdateState();
    }

    protected abstract void UpdateState();
}