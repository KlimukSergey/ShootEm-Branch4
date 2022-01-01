using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioClip _newClip;
    private AudioSource sound_SFX;
    private AudioSource musicSource;

    [SerializeField]
    public AudioClip[] sfx;

    [SerializeField]
    public AudioClip[] backGrnd_sfx;

    [SerializeField]
    public AudioClip _gameOverMusic;

    [SerializeField]
    GameObject sound_object;

    void Awake()
    {
        instance = this;

        musicSource = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        sound_SFX = GameObject.Find("BackGroundSFX").GetComponent<AudioSource>();
        //   sound_SFX = GameObject.Find("Sound").GetComponent<AudioSource>();
    }

    public void Play_SFX(string clipName, Transform place)
    {
        foreach (var e in sfx)
        {
            if (e.name == clipName)
            {
                _newClip = e;
            }
        }
        GameObject newSound = Instantiate(sound_object, place);
        newSound.GetComponent<AudioSource>().clip = _newClip;
        newSound.GetComponent<AudioSource>().Play();
        //  sound_SFX.clip = _newClip;
        //  sound_SFX.Play();dw
    }
    public AudioClip Play_BackGrnd_sfx()
    {
        AudioClip _BackGrndSFX = backGrnd_sfx[Random.Range(0, backGrnd_sfx.Length)];
        return _BackGrndSFX;
    }
    public void GameOverMusic()
    {
        sound_SFX.Stop();

        musicSource.clip = _gameOverMusic;
    }
    public void PlayFinalMusic()
    {
        musicSource.Play();
    }
}
