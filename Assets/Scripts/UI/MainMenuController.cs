using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuUI;

    [Header("Panels")]
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CloseAllPanels();

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
        }
    }

    public void OpenStory()
    {
        ShowPanel(storyPanel);
    }

    public void OpenSettings()
    {
        ShowPanel(settingsPanel);
    }

    public void OpenCredits()
    {
        ShowPanel(creditsPanel);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void BackToMenu()
    {
        CloseAllPanels();

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ShowPanel(GameObject panelToShow)
    {
        CloseAllPanels();

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }

        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }
    }

    private void CloseAllPanels()
    {
        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }
    }
}