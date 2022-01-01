using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject menuPanel;
    private GameObject deadPanel;
    private bool escKey;
    void Start()
    {
        menuPanel = GameObject.Find("MenuPanel");
        deadPanel = GameObject.Find("DeadPanel");
        if (menuPanel != null)
            menuPanel.SetActive(false);
        if (deadPanel != null)
            deadPanel.SetActive(false);
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }

    public void EscapeMenu()
    {
        if (escKey == false)
        {
            Time.timeScale = 0;
            menuPanel.SetActive(true);
            escKey = true;
        }
        else
        {
            Time.timeScale = 1;
            menuPanel.SetActive(false);
            escKey = false;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        // Time.timeScale = 0;
        deadPanel.SetActive(true);
    }
    public void Restart()
    {
        //  Time.timeScale=1;
        Score.sweetCount = 0;
        Health.isAlive = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
