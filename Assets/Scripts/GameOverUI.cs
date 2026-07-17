using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        // فتح وإظهار الماوس علشان أزرار Game Over تشتغل
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ShowFinalScore();
    }

    private void ShowFinalScore()
    {
        if (finalScoreText == null)
        {
            Debug.LogWarning("Final Score Text is not assigned.");
            return;
        }

        if (GameManager.Instance != null)
        {
            finalScoreText.text =
                $"Final Score: {GameManager.Instance.Score}";
        }
        else
        {
            finalScoreText.text = "Final Score: 0";
        }
    }

    public void PlayAgain()
    {
        Debug.Log("Play Again clicked.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Main Menu clicked.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exit clicked.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ExitGame();
        }
        else
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}