using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IncreasingSnow : MonoBehaviour
{
    ParticleSystem snow;
    private float _snowSize;
    // Start is called before the first frame update
    void Start()
    {
        snow = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Health.isAlive)
        {
            var main = snow.main;

            if (main.maxParticles <= 5000 && main.startSize.constant <= 5)
            {
                int value = Mathf.RoundToInt(100 * Time.deltaTime);
                main.maxParticles += value;
                float minsize = main.startSize.constant;
                minsize += 0.1f * Time.deltaTime;
                main.startSize = minsize;
            }
        }
    }
}
