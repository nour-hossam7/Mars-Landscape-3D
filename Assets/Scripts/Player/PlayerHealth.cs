using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Min(1)] private int maxHits = 3;

    [Header("Game Over")]
    [SerializeField, Min(0f)] private float restartDelay = 1.5f;

    private int currentHits;
    private bool isDead;

    public int CurrentHits => currentHits;
    public int RemainingHits => Mathf.Max(0, maxHits - currentHits);
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHits = 0;
        isDead = false;
    }

    public void TakeHit(int hitAmount = 1)
    {
        if (isDead || hitAmount <= 0)
        {
            return;
        }

        currentHits += hitAmount;

        Debug.Log(
            $"Player hit! Remaining hits: {RemainingHits}/{maxHits}",
            gameObject
        );

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        Debug.Log("Player lost! Restarting scene...", gameObject);

        StartCoroutine(RestartSceneRoutine());
    }

    private IEnumerator RestartSceneRoutine()
    {
        yield return new WaitForSeconds(restartDelay);

        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}