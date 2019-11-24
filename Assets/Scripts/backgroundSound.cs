using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundSound : MonoBehaviour
{
    //public AudioClip bgSound;
    //private AudioSource source;
    public SoundVolume sliderValue;
    public AudioSource source;

    //private void Awake()
    //{
    //    source = this.gameObject.AddComponent<AudioSource>();
    //    source.PlayOneShot(bgSound, 0.5F);
    //    source.loop = true;

    //}

    //Update is called once per frame
    void Update()
    {
        source.volume = sliderValue.value/2;
        
        
    }
}
