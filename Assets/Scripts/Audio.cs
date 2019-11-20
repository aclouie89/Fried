using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioClip choppingSound;
    public AudioClip sizzleSound;
    public AudioClip HitPlayerSound;
    

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playChoopingSound()
    {
        source.PlayOneShot(choppingSound, 1F);
    }
}
