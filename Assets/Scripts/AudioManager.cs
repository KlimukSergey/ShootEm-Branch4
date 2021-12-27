using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioClip _newClip;
    private AudioSource sound_SFX;

    [SerializeField]
    public AudioClip[] sfx;

    [SerializeField]
    GameObject sound_object;

    void Awake()
    {
        instance = this;
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
        newSound.GetComponent<AudioSource>().clip=_newClip;
        newSound.GetComponent<AudioSource>().Play();
      //  sound_SFX.clip = _newClip;
      //  sound_SFX.Play();
    }
}
