using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private string bossDisplayName = "Spider Guardian";

    [Header("UI References")]
    [SerializeField] private TMP_Text bossNameText;
    [SerializeField] private Image healthBarFill;

    private void Awake()
    {
        if (bossNameText != null)
        {
            bossNameText.text = bossDisplayName;
        }

        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
        }
    }

    private void OnEnable()
    {
        if (bossHealth == null)
        {
            Debug.LogWarning(
                "BossHealth is not assigned in BossHealthUI.",
                gameObject
            );

            return;
        }

        bossHealth.HealthChanged += UpdateHealthBar;
        bossHealth.Died += HandleBossDeath;

        UpdateHealthBar(
            bossHealth.CurrentHealth,
            bossHealth.MaxHealth
        );
    }

    private void OnDisable()
    {
        if (bossHealth == null)
        {
            return;
        }

        bossHealth.HealthChanged -= UpdateHealthBar;
        bossHealth.Died -= HandleBossDeath;
    }

    private void UpdateHealthBar(
        float currentHealth,
        float maxHealth
    )
    {
        if (healthBarFill == null || maxHealth <= 0f)
        {
            return;
        }

        float healthPercentage = currentHealth / maxHealth;

        healthBarFill.fillAmount =
            Mathf.Clamp01(healthPercentage);

        Debug.Log(
            $"Boss UI Health: {currentHealth}/{maxHealth}"
        );
    }

    private void HandleBossDeath()
    {
        gameObject.SetActive(false);
    }
}