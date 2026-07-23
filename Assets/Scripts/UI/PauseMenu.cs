using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TMP_Text soundButtonText;

    private bool isPaused = false;
    private bool isMuted = false;

    private const string MuteKey = "IsMuted";

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        

        LoadSoundSettings();
        UpdateSoundButtonText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;

        AudioListener.volume = isMuted ? 0f : 1f;

        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundButtonText();
    }

    private void LoadSoundSettings()
    {
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    private void UpdateSoundButtonText()
    {
        if (soundButtonText == null)
        {
            return;
        }

        soundButtonText.text = isMuted ? "SOUND OFF" : "SOUND ON";
    }
}