using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    private Slider _musicSlider,
        _soundSlider;
    private float _musicVolume,
        _soundVolume;

    void Start()
    {
        
         _musicSlider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
        _musicVolume = AudioManager.instance.GetMusicVolume();

         _soundSlider = GameObject.Find("SoundVolumeSlider").GetComponent<Slider>();
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
