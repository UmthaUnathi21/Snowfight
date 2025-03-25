using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public string nextLevel;
    public string gameOverScene = "GameOver"; // Set Game Over scene name

    private bool gameEnded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameEnded && collision.CompareTag("Player"))
        {
            gameEnded = true;
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                Debug.Log(player.playerColor + " Wins!");
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene(gameOverScene); // Load Game Over scene after Level 2
        }
        else if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("Next Level not set in the Inspector.");
        }
    }
}
