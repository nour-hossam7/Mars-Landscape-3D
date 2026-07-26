using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Values")]
    [SerializeField] private int energy = 0;
    [SerializeField] private int score = 0;

    [Header("Energy Settings")]
    [SerializeField, Min(1)] private int requiredEnergyToStartBoss = 5;
    [SerializeField, Min(1)] private int maxAlienEnergy = 10;

    [Header("Alien Energy UI")]
    [SerializeField] private Image alienEnergyFill;
    [SerializeField] private TMP_Text alienEnergyValueText;

    [Header("Spider Mission")]
    [SerializeField] private SpiderGuardianAI spiderGuardianAI;
    [SerializeField] private GameObject bossHealthUI;
    [SerializeField] private GameObject spiderGate;

    private TMP_Text scoreText;
    private bool spiderMissionUnlocked;

    public int Energy => energy;
    public int Score => score;
    public int MaxAlienEnergy => maxAlienEnergy;
    public bool SpiderMissionUnlocked => spiderMissionUnlocked;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        FindGameplayReferences();
        PrepareSpiderMission();
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            FindGameplayReferences();
            PrepareSpiderMission();
            UpdateUI();
        }
    }

    private void FindGameplayReferences()
    {
        GameObject fillObject = GameObject.Find("AlienEnergyFill");
        GameObject valueTextObject = GameObject.Find("AlienEnergyValueText");
        GameObject scoreObject = GameObject.Find("ScoreText");

        if (fillObject != null)
        {
            alienEnergyFill = fillObject.GetComponent<Image>();
        }
        else
        {
            Debug.LogWarning("AlienEnergyFill was not found.");
        }

        if (valueTextObject != null)
        {
            alienEnergyValueText =
                valueTextObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("AlienEnergyValueText was not found.");
        }

        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TMP_Text>();
        }

        if (spiderGuardianAI == null)
        {
            spiderGuardianAI =
                FindFirstObjectByType<SpiderGuardianAI>();
        }

        if (bossHealthUI == null)
        {
            bossHealthUI =
                GameObject.Find("BossHealthUI");
        }

        if (spiderGate == null)
        {
            spiderGate =
                GameObject.Find("SpiderGate");
        }
    }

    private void PrepareSpiderMission()
    {
        if (spiderMissionUnlocked)
        {
            ActivateSpiderMission();
            return;
        }

        if (spiderGuardianAI != null)
        {
            spiderGuardianAI.enabled = false;
        }

        if (bossHealthUI != null)
        {
            bossHealthUI.SetActive(false);
        }

        if (spiderGate != null)
        {
            spiderGate.SetActive(true);
        }
    }

    public void AddEnergy(int energyAmount, int scoreAmount)
    {
        if (energyAmount <= 0)
        {
            return;
        }

        energy += energyAmount;
        energy = Mathf.Clamp(energy, 0, maxAlienEnergy);

        score += Mathf.Max(0, scoreAmount);

        UpdateUI();
        CheckEnergyMission();
    }

    public void RemoveEnergy(int energyAmount)
    {
        if (energyAmount <= 0)
        {
            return;
        }

        energy -= energyAmount;
        energy = Mathf.Clamp(energy, 0, maxAlienEnergy);

        UpdateUI();
    }

    private void CheckEnergyMission()
    {
        if (spiderMissionUnlocked)
        {
            return;
        }

        if (energy >= requiredEnergyToStartBoss)
        {
            spiderMissionUnlocked = true;
            ActivateSpiderMission();
        }
    }

    private void ActivateSpiderMission()
    {
        Debug.Log("Spider Guardian mission unlocked!");

        if (spiderGate != null)
        {
            spiderGate.SetActive(false);
        }

        if (spiderGuardianAI != null)
        {
            spiderGuardianAI.enabled = true;
        }

        if (bossHealthUI != null)
        {
            bossHealthUI.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (alienEnergyFill != null)
        {
            alienEnergyFill.fillAmount =
                (float)energy / maxAlienEnergy;
        }

        if (alienEnergyValueText != null)
        {
            alienEnergyValueText.text =
                energy.ToString();
        }

        if (scoreText != null)
        {
            scoreText.text =
                $"Score: {score}";
        }
    }

    public void LoadGameplay()
    {
        ResetGame();
        SceneManager.LoadScene("Gameplay");
    }

    public void RestartGame()
    {
        ResetGame();
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ResetGame()
    {
        energy = 0;
        score = 0;
        spiderMissionUnlocked = false;

        alienEnergyFill = null;
        alienEnergyValueText = null;
        scoreText = null;

        spiderGuardianAI = null;
        bossHealthUI = null;
        spiderGate = null;
    }
}