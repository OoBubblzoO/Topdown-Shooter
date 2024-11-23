using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;  }

    public GameObject gameOverUI;

    private void Awake()
    {

        // Make sure no duplicates of game manager exist
        // This allows the game to keep the same instance and references when originally played
        // The debug console was just grouping the messages together
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
            
        }
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
       // }
       
        
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        // Show game over UI

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("GAMEOVERUI reference is missing!");
        }
        // Pause game 
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        // Unpause game
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING GAME...");
        Application.Quit();
    }

}
