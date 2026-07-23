using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Values")]
    [SerializeField] private int energy = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private int totalEnergyCells = 3;

    private TMP_Text energyText;
    private TMP_Text scoreText;

    public int Energy => energy;
    public int Score => score;

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
        FindGameplayUI();
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
            FindGameplayUI();
            UpdateUI();
        }
    }

    private void FindGameplayUI()
    {
        GameObject energyObject = GameObject.Find("EnergyText");
        GameObject scoreObject = GameObject.Find("ScoreText");

        if (energyObject != null)
        {
            energyText = energyObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("EnergyText GameObject was not found.");
        }

        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("ScoreText GameObject was not found.");
        }
    }

    public void AddEnergy(int energyAmount, int scoreAmount)
    {
        energy += energyAmount;
        score += scoreAmount;

        UpdateUI();
        CheckGameFinished();
    }

    private void UpdateUI()
    {
        if (energyText != null)
        {
            energyText.text = $"EnergyCan : {energy}";
        }

        if (scoreText != null)
        {
            scoreText.text = $"Score : {score}";
        }
    }

    private void CheckGameFinished()
    {
        if (totalEnergyCells > 0 && energy >= totalEnergyCells)
        {
            LoadGameOver();
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
        energyText = null;
        scoreText = null;
    }
}