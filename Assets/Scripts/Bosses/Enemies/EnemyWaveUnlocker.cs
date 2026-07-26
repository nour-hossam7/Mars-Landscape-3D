using UnityEngine;

public class EnemyWaveUnlocker : MonoBehaviour
{
    [Header("Main Boss")]
    [SerializeField] private BossHealth mainBossHealth;

    [Header("Enemies Activated After Boss Death")]
    [SerializeField] private GameObject[] enemiesToActivate;

    private void Awake()
    {
        DisableEnemies();
    }

    private void OnEnable()
    {
        if (mainBossHealth != null)
        {
            mainBossHealth.Died += ActivateEnemies;
        }
    }

    private void OnDisable()
    {
        if (mainBossHealth != null)
        {
            mainBossHealth.Died -= ActivateEnemies;
        }
    }

    private void DisableEnemies()
    {
        if (enemiesToActivate == null)
        {
            return;
        }

        foreach (GameObject enemy in enemiesToActivate)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }

    private void ActivateEnemies()
    {
        if (enemiesToActivate == null)
        {
            return;
        }

        foreach (GameObject enemy in enemiesToActivate)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

        Debug.Log("The main boss has been defeated. Regular enemies are now active!");
    }
}