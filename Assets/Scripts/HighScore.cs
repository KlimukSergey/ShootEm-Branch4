using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
private Text uiText;

    void Awake()
    {
        uiText = GetComponent<Text>();
        if(ES3.KeyExists("HiScore"))
        uiText.text=ES3.Load<int>("HiScore").ToString();
        else print("Нет сохранений");
    }

}
