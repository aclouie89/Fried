using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    //public static Audio Instance;

    public AudioClip choppingSound;
    public AudioClip sizzleSound;
    public AudioClip shoutSound;
    public AudioClip hitSound;
    public AudioClip startSound;
    public AudioClip winSound;
    public float volume=1F;
    
    public static AudioSource source1;
    public static AudioSource source2;
    public static AudioSource source3;
    public static AudioSource source4;
    public static AudioSource source5;


    // Start is called before the first frame update
    void Start()
    {
        source1 = this.gameObject.AddComponent<AudioSource>();
        source2 = this.gameObject.AddComponent<AudioSource>();
        source3 = this.gameObject.AddComponent<AudioSource>();
        source4 = this.gameObject.AddComponent<AudioSource>();
        source5 = this.gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playChoopingSound()
    {
       // AudioSource source= this.gameObject.AddComponent<AudioSource>(); 
       source1.PlayOneShot(choppingSound, volume);
    }
    public void stopSound()
    {
        //AudioSource source = this.gameObject.AddComponent<AudioSource>();
  
        source1.Stop();
    }
    public void playSizzleSound()
    {
        //AudioSource source = this.gameObject.AddComponent<AudioSource>(); 
        source2.PlayOneShot(sizzleSound, volume);
    }
    public void stopSizzleSound()
    {
        //AudioSource source = this.gameObject.AddComponent<AudioSource>();

        source2.Stop();
    }
    public void playshoutSound()
    {
       // AudioSource source = this.gameObject.AddComponent<AudioSource>(); 
        source3.PlayOneShot(shoutSound, volume);
    }
    public void playHitSound()
    {
       // AudioSource source = this.gameObject.AddComponent<AudioSource>(); 
        source4.PlayOneShot(hitSound, volume);
    }
    public void playStartSound()
    {
       // AudioSource source = this.gameObject.AddComponent<AudioSource>(); 
        source5.PlayOneShot(startSound, volume);
    }

}
