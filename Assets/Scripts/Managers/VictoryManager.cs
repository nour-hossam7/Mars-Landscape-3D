using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("Enemies Required For Victory")]
    [SerializeField] private BossHealth mainBoss;
    [SerializeField] private BossHealth[] regularEnemies;

    private readonly HashSet<BossHealth> defeatedEnemies = new();

    private int totalRequiredEnemies;
    private bool victoryTriggered;

    private void Start()
    {
        SubscribeToEnemies();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEnemies();
    }

    private void SubscribeToEnemies()
    {
        defeatedEnemies.Clear();
        totalRequiredEnemies = 0;
        victoryTriggered = false;

        if (mainBoss != null)
        {
            mainBoss.Died += HandleMainBossDeath;
            totalRequiredEnemies++;
        }
        else
        {
            Debug.LogWarning(
                "Main Boss is not assigned in VictoryManager.",
                gameObject
            );
        }

        if (regularEnemies != null)
        {
            foreach (BossHealth enemy in regularEnemies)
            {
                if (enemy == null)
                {
                    continue;
                }

                enemy.Died += HandleRegularEnemyDeath;
                totalRequiredEnemies++;
            }
        }

        Debug.Log(
            $"Enemies required for victory: {totalRequiredEnemies}",
            gameObject
        );
    }

    private void UnsubscribeFromEnemies()
    {
        if (mainBoss != null)
        {
            mainBoss.Died -= HandleMainBossDeath;
        }

        if (regularEnemies == null)
        {
            return;
        }

        foreach (BossHealth enemy in regularEnemies)
        {
            if (enemy != null)
            {
                enemy.Died -= HandleRegularEnemyDeath;
            }
        }
    }

    private void HandleMainBossDeath()
    {
        RegisterEnemyDeath(mainBoss);
    }

    private void HandleRegularEnemyDeath()
    {
        BossHealth deadEnemy = FindNewlyDeadRegularEnemy();

        if (deadEnemy != null)
        {
            RegisterEnemyDeath(deadEnemy);
        }
    }

    private BossHealth FindNewlyDeadRegularEnemy()
    {
        if (regularEnemies == null)
        {
            return null;
        }

        foreach (BossHealth enemy in regularEnemies)
        {
            if (enemy == null)
            {
                continue;
            }

            if (enemy.IsDead && !defeatedEnemies.Contains(enemy))
            {
                return enemy;
            }
        }

        return null;
    }

    private void RegisterEnemyDeath(BossHealth defeatedEnemy)
    {
        if (victoryTriggered || defeatedEnemy == null)
        {
            return;
        }

        if (!defeatedEnemies.Add(defeatedEnemy))
        {
            return;
        }

        Debug.Log(
            $"Enemy defeated: {defeatedEnemy.gameObject.name}. " +
            $"{defeatedEnemies.Count}/{totalRequiredEnemies}",
            gameObject
        );

        if (
            totalRequiredEnemies > 0 &&
            defeatedEnemies.Count >= totalRequiredEnemies
        )
        {
            TriggerVictory();
        }
    }

    private void TriggerVictory()
    {
        if (victoryTriggered)
        {
            return;
        }

        victoryTriggered = true;

        int finalScore = 0;

        if (GameManager.Instance != null)
        {
            finalScore = GameManager.Instance.Score;
        }

        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        Debug.Log(
            $"All required enemies defeated. Victory! Final Score: {finalScore}",
            gameObject
        );

        SceneManager.LoadScene("Victory");
    }
}