using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text scoreText;
    public int score;
    EneysSpawner enemySpawner;

    // Start is called before the first frame update
    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        enemySpawner=GetComponent<EneysSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
        if(score == 50)
        {
            enemySpawner.LevelUp();
        }
    }
    public void CountScore(int sc)
    {
        score += sc;
    }
}
