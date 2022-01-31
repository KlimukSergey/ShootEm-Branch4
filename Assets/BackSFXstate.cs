using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSFXstate : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = AudioManager.instance.Play_BackGrnd_sfx();
            source.Play();
        }
        else return;
    }
}
