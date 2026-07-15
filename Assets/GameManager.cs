using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Starting Values")]
    [SerializeField] private int startingEnergy = 0;
    [SerializeField] private int startingScore = 0;

    private int currentEnergy;
    private int currentScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentEnergy = startingEnergy;
        currentScore = startingScore;

        UpdateUI();
    }

    public void AddEnergy(int energyAmount, int scoreAmount)
    {
        currentEnergy += energyAmount;
        currentScore += scoreAmount;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (energyText != null)
        {
            energyText.text = $"Energy : {currentEnergy}";
        }

        if (scoreText != null)
        {
            scoreText.text = $"Score : {currentScore}";
        }
    }
}