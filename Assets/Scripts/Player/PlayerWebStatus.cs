using System.Collections;
using UnityEngine;

public class PlayerWebStatus : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private MonoBehaviour[] movementBehaviours;

    [Header("Web UI")]
    [SerializeField] private GameObject webOverlay;

    [Header("Settings")]
    [SerializeField, Min(0.1f)] private float defaultFreezeDuration = 2f;

    private Coroutine freezeCoroutine;
    private bool isFrozen;

    public bool IsFrozen => isFrozen;

    private void Start()
    {
        if (webOverlay != null)
        {
            webOverlay.SetActive(false);
        }
    }

    public void ApplyWeb()
    {
        ApplyWeb(defaultFreezeDuration);
    }

    public void ApplyWeb(float freezeDuration)
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(
            FreezeRoutine(Mathf.Max(0.1f, freezeDuration))
        );
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        isFrozen = true;

        SetMovementEnabled(false);

        if (webOverlay != null)
        {
            webOverlay.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        if (webOverlay != null)
        {
            webOverlay.SetActive(false);
        }

        SetMovementEnabled(true);

        isFrozen = false;
        freezeCoroutine = null;
    }

    private void SetMovementEnabled(bool enabledState)
    {
        if (movementBehaviours == null)
        {
            return;
        }

        foreach (MonoBehaviour behaviour in movementBehaviours)
        {
            if (behaviour != null)
            {
                behaviour.enabled = enabledState;
            }
        }
    }
}