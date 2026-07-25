using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    [Header("UI References")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject storyPanel;

    private void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
        }

        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }
    }

    public void OpenStory()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }

        if (storyPanel != null)
        {
            storyPanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void BackToMenu()
    {
        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }

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
}