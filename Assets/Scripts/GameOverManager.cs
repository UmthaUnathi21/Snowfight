using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button gameOverButton; // Manual change in Inspector
    public string firstLevel = "Level 1"; // First level name

    private void Start()
    {
        if (gameOverButton != null)
        {
            gameOverButton.onClick.AddListener(RetryGame);
        }
        else
        {
            Debug.LogError("GameOverButton is not assigned in the Inspector.");
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(firstLevel); // Load the first level
    }
}
