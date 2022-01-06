using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    [SerializeField]
    private Slider _musicSlider,
        _soundSlider;
        [SerializeField]
    private float _musicVolume,
        _soundVolume;

    void Start()
    {
        
        _musicVolume = AudioManager.instance.GetMusicVolume();

        _soundVolume = AudioManager.instance.GetSoundVolume();
    }

    void Update()
     {
         if(_musicSlider.value!=_musicVolume)
         AudioManager.instance.SetMusicVolume(_musicSlider.value);

         if(_soundSlider.value!=_soundVolume)
         AudioManager.instance.SetSoundVolume(_soundSlider.value);
      }
}
