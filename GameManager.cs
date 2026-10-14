using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;   // drag GameOverPanel here

    void Start()
    {
        gameOverPanel.SetActive(false);   // hide it at the start
    }

    // Call this when the player dies
    public void GameOver()
    {
        gameOverPanel.SetActive(true);    // show the panel
        Time.timeScale = 0f;              // freeze the game (optional)
    }

    // The button will call this
    public void RestartGame()
    {
        Time.timeScale = 1f;              // un-freeze, important!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}