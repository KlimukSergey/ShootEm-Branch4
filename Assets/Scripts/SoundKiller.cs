using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKiller : MonoBehaviour
{
AudioSource sourse;
void Awake(){sourse=GetComponent<AudioSource>();}
void Update()
{
    if(!sourse.isPlaying)Destroy(this.gameObject);
}
}
