using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    
    private Text scoreText;
    private Text sweetCountText;
    [SerializeField]
    public  int score;
    [SerializeField] private int sweet;
    //public static int score;
    public static int sweetCount;
    EneysSpawner enemySpawner;
    private GameObject dialoguePanel;
    private bool key=true;

    // Start is called before the first frame update
    void Awake()
    {
        sweetCount=sweet;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        sweetCountText = GameObject.Find("SweetCountText").GetComponent<Text>();
        enemySpawner = GetComponent<EneysSpawner>();
        dialoguePanel = GameObject.Find("Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
        sweetCountText.text =$"Sweet  {sweetCount} /10";

        if (sweetCount >= 10)
        {
            if (!dialoguePanel.activeInHierarchy)
                dialoguePanel.SetActive(true);

        }
        else
        {
            if (dialoguePanel.activeInHierarchy)
                dialoguePanel.SetActive(false);

        }


        if (score == 50&&key)
        {
            enemySpawner.LevelUp();
            key=false;
        }
    }
    public void CountScore(int sc)
    {
        score += sc;
    }
}
