using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static Score instance;

    //  private Text scoreText;
    //private Text sweetCountText;

    [SerializeField]
    public int score;

    public static int high_Score;
    [SerializeField]
    private int sweet;
    public static int sweetCount;
    EneysSpawner enemySpawner;
    private GameObject dialoguePanel;
    private bool key = true;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        sweetCount = sweet;
        //  scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        //  sweetCountText = GameObject.Find("SweetCountText").GetComponent<Text>();
        enemySpawner = GetComponent<EneysSpawner>();
        dialoguePanel = GameObject.Find("Dialogue");
        high_Score = LoadHiScore();
    }

    // Update is called once per frame
    void Update()
    {
        //   scoreText.text = $"Score: {score}";
        //  sweetCountText.text = $"Sweet  {sweetCount} /10";


        if (score == 50 && key)
        {
            enemySpawner.LevelUp();
            key = false;
        }
    }
    public void CountScore(int sc)
    {
        score += sc;
    }
    public int LoadHiScore()
    {
        high_Score = ES3.Load("HiScore", high_Score);
        return high_Score;
    }
    public void SaveHiScore()
    {
        ES3.Save("HiScore", high_Score);
    }
    public int Record()
    {
        if (score > high_Score)
        {
            high_Score = score;
            SaveHiScore();
        }
        return high_Score;
    }
}
