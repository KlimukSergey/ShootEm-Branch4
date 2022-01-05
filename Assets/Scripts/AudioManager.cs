using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioClip _newClip;
    private AudioSource sound_SFX;
    private AudioSource sound_SFX_1;
    private AudioSource musicSource;

    [SerializeField]
    public AudioClip[] sfx;

    [SerializeField]
    public AudioClip[] backGrnd_sfx;

    [SerializeField]
    public AudioClip _gameOverMusic;

    [SerializeField]
    GameObject sound_object;

    private float _volumeMusic,
        _volumeSound;

    private bool _mutedMusic,
        _mutedSound;

    private GameObject newSound;

    void Awake()
    {
        instance = this;
        musicSource = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        sound_SFX = GameObject.Find("BackGroundSFX").GetComponent<AudioSource>();
        sound_SFX_1 = GameObject.Find("BackGroundSFX_1").GetComponent<AudioSource>();
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
        newSound = Instantiate(sound_object, place);
        newSound.GetComponent<AudioSource>().clip = _newClip;
        newSound.GetComponent<AudioSource>().Play();
        newSound.GetComponent<AudioSource>().volume = _volumeSound;

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

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("AM_MusicVolume", _volumeMusic);
        PlayerPrefs.SetFloat("AM_SoundVolume", _volumeSound);

        PlayerPrefs.SetInt("AM_MusicMute", _mutedMusic ? 1 : 0);
        PlayerPrefs.SetInt("AM_SoundMute", _mutedSound ? 1 : 0);
    }

    public void LoadSettings()
    {
        _volumeMusic = PlayerPrefs.GetFloat("AM_MusicVolume", 1);
        _volumeSound = PlayerPrefs.GetFloat("AM_SoundVolume", 1);

        _mutedMusic = PlayerPrefs.GetInt("AM_MusicMute", 0) == 1;
        _mutedSound = PlayerPrefs.GetInt("AM_SoundMute", 0) == 1;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        sound_SFX.volume = volume;
        sound_SFX_1.volume = volume;
        _volumeMusic = volume;
        SaveSettings();
    }

    public float GetMusicVolume()
    {
        return _volumeMusic;
    }

    public void SetSoundVolume(float volume)
    {
        _volumeSound = volume;
        SaveSettings();
    }

    public float GetSoundVolume()
    {
        return _volumeSound;
    }

    public void SetMusicMuted(bool mute)
    {
        _mutedMusic = mute;
        SaveSettings();
    }

    public bool GetMusicMuted()
    {
        return _mutedMusic;
    }

    public void SetSoundMuted(bool mute)
    {
        _mutedSound = mute;
        SaveSettings();
    }

    public bool GetSoundMuted()
    {
        return _mutedSound;
    }
}
