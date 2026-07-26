using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text finalScoreValue;

    private void Start()
    {
        ShowFinalScore();
    }

    private void ShowFinalScore()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        if (finalScoreValue != null)
        {
            finalScoreValue.text = finalScore.ToString();
        }
        else
        {
            Debug.LogWarning(
                "FinalScoreValue is not assigned in VictoryUI."
            );
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}