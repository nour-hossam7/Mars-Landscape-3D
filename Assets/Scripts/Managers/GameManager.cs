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
    private bool gameEnded;

    public int Energy => energy;
    public int Score => score;
    public int MaxAlienEnergy => maxAlienEnergy;
    public bool SpiderMissionUnlocked => spiderMissionUnlocked;
    public bool GameEnded => gameEnded;

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
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            StartNewGameplayRun();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            StartNewGameplayRun();
        }
        else
        {
            ClearSceneReferences();
        }
    }

    private void StartNewGameplayRun()
    {
        ResetRuntimeValues();
        FindGameplayReferences();
        PrepareSpiderMission();
        UpdateUI();

        Debug.Log("A new gameplay run has started.");
    }

    private void ResetRuntimeValues()
    {
        energy = 0;
        score = 0;

        spiderMissionUnlocked = false;
        gameEnded = false;
    }

    private void ClearSceneReferences()
    {
        alienEnergyFill = null;
        alienEnergyValueText = null;
        scoreText = null;

        spiderGuardianAI = null;
        bossHealthUI = null;
        spiderGate = null;
    }

    private void FindGameplayReferences()
    {
        ClearSceneReferences();

        GameObject fillObject =
            GameObject.Find("AlienEnergyFill");

        GameObject valueTextObject =
            GameObject.Find("AlienEnergyValueText");

        GameObject scoreObject =
            GameObject.Find("ScoreText");

        if (fillObject != null)
        {
            alienEnergyFill =
                fillObject.GetComponent<Image>();
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
            scoreText =
                scoreObject.GetComponent<TMP_Text>();
        }

        SpiderGuardianAI[] allSpiderAIs =
            FindObjectsByType<SpiderGuardianAI>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (SpiderGuardianAI spiderAI in allSpiderAIs)
        {
            if (spiderAI.gameObject.name == "SpiderGuardian")
            {
                spiderGuardianAI = spiderAI;
                break;
            }
        }

        if (spiderGuardianAI == null)
        {
            Debug.LogWarning(
                "The main SpiderGuardian AI was not found."
            );
        }

        bossHealthUI =
            FindSceneObjectByName("BossHealthUI");

        spiderGate =
            FindSceneObjectByName("SpiderGate");
    }

    private GameObject FindSceneObjectByName(string objectName)
    {
        Transform[] allTransforms =
            FindObjectsByType<Transform>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (Transform currentTransform in allTransforms)
        {
            if (currentTransform.gameObject.name == objectName)
            {
                return currentTransform.gameObject;
            }
        }

        return null;
    }

    private void PrepareSpiderMission()
    {
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
        if (gameEnded || energyAmount <= 0)
        {
            return;
        }

        energy += energyAmount;
        energy = Mathf.Clamp(
            energy,
            0,
            maxAlienEnergy
        );

        score += Mathf.Max(0, scoreAmount);

        UpdateUI();
        CheckEnergyMission();
    }

    public void RemoveEnergy(int energyAmount)
    {
        if (gameEnded || energyAmount <= 0)
        {
            return;
        }

        /*
         * قبل فتح مهمة الـ Boss اللاعب يبدأ بطاقة 0،
         * لذلك لا يتم تشغيل Game Over في هذه المرحلة.
         */
        if (!spiderMissionUnlocked)
        {
            return;
        }

        energy -= energyAmount;
        energy = Mathf.Clamp(
            energy,
            0,
            maxAlienEnergy
        );

        UpdateUI();

        if (energy <= 0)
        {
            TriggerGameOver();
        }
    }

    private void CheckEnergyMission()
    {
        if (spiderMissionUnlocked || gameEnded)
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

    private void TriggerGameOver()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        energy = 0;

        UpdateUI();

        /*
         * عند الخسارة الطاقة أصبحت صفر،
         * لذلك Final Score المعروض سيكون صفر.
         */
        PlayerPrefs.SetInt("FinalScore", 0);
        PlayerPrefs.Save();

        Debug.Log("Alien energy reached zero. Game Over!");

        SceneManager.LoadScene("GameOver");
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
        SceneManager.LoadScene("Gameplay");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        TriggerGameOver();
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}