using UnityEngine;
using UnityEngine.UI;
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
        Score.sweetCount = 0;
        Health.isAlive = true;
Score.instance.Record();
        SceneManager.LoadScene(0);
    }
    public void GameQuit()
    {
        if(Score.instance.score!=0)
        Score.instance.Record();
        Application.Quit();
    }

    public void GameOver()
    {
        deadPanel.SetActive(true);
        GameObject.Find("DP_ScoreText").GetComponent<Text>().text = Score.instance.score.ToString();
        Score.instance.Record();

       // Destroy(GameObject.FindObjectOfType<EneysSpawner>());
    }
    public void Restart()
    {
        Score.sweetCount = 0;
        Health.isAlive = true;
        Score.instance.Record();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetScore()
    {
        Score.high_Score = 0;
        Score.instance.SaveHiScore();
        GameObject.Find("HighScore").GetComponent<Text>().text = "0";
    }
}
