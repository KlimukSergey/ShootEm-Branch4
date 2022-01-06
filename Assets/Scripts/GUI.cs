using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    private Text _health,
        _bullet,
        _score,
        _sweet;

        private GameObject dialoguePanel;

    void Start()
    {
        _health = GameObject.Find("HealthText").GetComponent<Text>();
        _bullet = GameObject.Find("BulletCount").GetComponent<Text>();
        _score = GameObject.Find("ScoreText").GetComponent<Text>();
        _sweet = GameObject.Find("SweetCountText").GetComponent<Text>();
        dialoguePanel = GameObject.Find("Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        _health.text = $"Health: {Health.currentHealth}";
        _bullet.text = $"Bullet: {Shooting.instance.bulletCount}";
        _score.text = $"Score: {Score.instance.score}   / {Score.high_Score}";
        _sweet.text = $"Sweet: {Score.sweetCount}";

        if (Score.sweetCount >= 10)
        {
            if (!dialoguePanel.activeInHierarchy)
                dialoguePanel.SetActive(true);
        }
        else
        {
            if (dialoguePanel.activeInHierarchy)
                dialoguePanel.SetActive(false);
        }
    }
}
