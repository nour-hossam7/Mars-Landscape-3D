using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField, Min(1)] private int energyDamagePerHit = 1;

    [Header("Game Over")]
    [SerializeField, Min(0f)] private float restartDelay = 1.5f;

    private bool isDead;

    public bool IsDead => isDead;

    private void Awake()
    {
        isDead = false;
    }

    public void TakeHit(int hitAmount = 1)
    {
        if (isDead || hitAmount <= 0)
        {
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogWarning(
                "GameManager Instance was not found.",
                gameObject
            );

            return;
        }

        int totalDamage = hitAmount * energyDamagePerHit;

        GameManager.Instance.RemoveEnergy(totalDamage);

        Debug.Log(
            $"Player hit! Energy remaining: {GameManager.Instance.Energy}",
            gameObject
        );

        if (GameManager.Instance.Energy <= 0)
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

        Debug.Log("Player energy reached zero!", gameObject);

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